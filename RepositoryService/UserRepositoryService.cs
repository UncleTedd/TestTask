using System.Globalization;
using AlifTestTask.DbContext;
using AlifTestTask.DTOs;
using AlifTestTask.Helper;
using AlifTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace AlifTestTask.RepositoryService;

public class UserRepositoryService
{
    private readonly AlifDbContext _dbContext;
    private readonly Functions _functions;

    public UserRepositoryService(AlifDbContext dbContext, Functions functions)
    {
        _dbContext = dbContext;
        _functions = functions;
    }

    public async Task<ResponseModel> CreateUser(User user)
    {
        var userInstance = new User
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            Wallet = new Wallet(),
            PhoneNumber = user.PhoneNumber,
            PassportNumber = user.PassportNumber
        };

        if (!string.IsNullOrWhiteSpace(userInstance.PhoneNumber) &&
            !string.IsNullOrWhiteSpace(userInstance.PassportNumber))
            if (_functions.VerifyPhonePassportNumber(userInstance.PhoneNumber, userInstance.PassportNumber) == 1)
                userInstance.UserVerification = 1;
        var result = await _dbContext.Users.AddAsync(userInstance);
        var dbResponse = await _dbContext.SaveChangesAsync();
        if (dbResponse > 0)
            return new ResponseModel
            {
                Result = dbResponse,
                Comment = "successfully added"
            };
        return new ResponseModel
        {
            Result = -1,
            Comment = "unsuccessful creation"
        };
    }

    public async Task<UserDTO?> GetUser(int id)
    {
        var dbResponse = await _dbContext.Users.Include(x => x.Wallet).Where(y => y.Wallet.UserId == id)
            .FirstOrDefaultAsync();

        var user = _functions.MapToUserDto(dbResponse);
        return user;
    }

    public async Task<ResponseModel> VerifyUser(ToVerifyUserModel user)
    {
        var toVerifyUser = await _dbContext.Users.FindAsync(user.Id);
        var res = _functions.VerifyPhonePassportNumber(user.PhoneNumber, user.PassportSeries);
        if (res == 0 || toVerifyUser is null)
            return new ResponseModel
            {
                Comment = "wrong data!"
            };
        toVerifyUser.PassportNumber = user.PassportSeries;
        toVerifyUser.PhoneNumber = user.PhoneNumber;
        toVerifyUser.UserVerification = 1;
        var responseFromSavingChanges = await _dbContext.SaveChangesAsync();
        return new ResponseModel
        {
            Result = responseFromSavingChanges,
            Comment = "Successfully Verified User"
        };
    }

    public async Task<ResponseModel> UserVerified(int id)
    {
        var userToVerify = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (userToVerify == null)
            return new ResponseModel
            {
                Result = -1,
                Comment = "invalid data"
            };
        // ??
        if (userToVerify is { PassportNumber: not null, PhoneNumber: not null } &&
            _functions.VerifyPhonePassportNumber(userToVerify.PhoneNumber, userToVerify.PassportNumber) == 1)
        {
            userToVerify.UserVerification = 1;
            await _dbContext.SaveChangesAsync();
        }

        var result = userToVerify.UserVerification;

        switch (result)
        {
            case 0:
                return new ResponseModel
                {
                    Result = result,
                    Comment = "user is not verified!"
                };
            case 1:
                return new ResponseModel
                {
                    Result = result,
                    Comment = "user is verified!"
                };
        }
        return new ResponseModel
        {
            Result = -1,
            Comment = "invalid data"
        };
    }

    public async Task<ResponseModel> Replenish(int id, decimal amount)
    {
        var transaction = new Transaction
        {
            Amount = amount,
            transactionTime = DateTime.Now
        };
        var user = await _dbContext.Users.Include(user => user.Wallet).FirstOrDefaultAsync(x => x.Id == id);

        if (user is null) return _functions.UserIsNull();

        if (user.UserVerification == 0)
        {
            if (_functions.IsUnverifiedUserWalletLimitReached(user.Wallet.Balance))
                return _functions.UnverifiedUserLimitReached();
            user.Wallet.Balance += amount;

            if (_functions.IsUnverifiedUserWalletLimitReached(user.Wallet.Balance))
                return _functions.UnverifiedUserLimitReached();

            user.Wallet.Transactions.Add(transaction);
        }

        if (user.UserVerification == 1)
        {
            if (_functions.IsVerifiedUserWalletLimitReached(user.Wallet.Balance))
                return _functions.VerifiedUserLimitReached();

            user.Wallet.Balance += amount;

            if (_functions.IsVerifiedUserWalletLimitReached(user.Wallet.Balance))
                return _functions.VerifiedUserLimitReached();

            user.Wallet.Transactions.Add(transaction);
        }

        var response = await _dbContext.SaveChangesAsync();

        if (response > 0)
            return new ResponseModel
            {
                Result = response,
                Comment = "replenish was successful"
            };
        return new ResponseModel
        {
            Result = 0,
            Comment = "could not replenish balance!"
        };
    }

    public async Task<GetTransactionsModel> GetBalanceAndTransactions(int id)
    {
        var dbResponse = await _dbContext.Users.Include(user => user.Wallet)
            .FirstOrDefaultAsync(x => x.Wallet.UserId == id);

        var response = await _dbContext.Users.Include(x => x.Wallet)
            .Where(x => x.Wallet.UserId == id).Include(y => y.Wallet.Transactions).ToListAsync();
        
        if (dbResponse != null)
        {
            var balance = dbResponse.Wallet.Balance;
        }

        var result = _functions.GetBalanceAndTransaction(response[0].Wallet);
        return result;
    }

    public async Task<ResponseModel> GetBalance(int id)
    {
        var dbResponse = await _dbContext.Users.Include(user => user.Wallet)
            .FirstOrDefaultAsync(x => x.Wallet.UserId == id);
        if (dbResponse != null)
            return new ResponseModel
            {
                Result = 1,
                Comment = $"Balance:{dbResponse.Wallet.Balance.ToString(CultureInfo.InvariantCulture)}"
            };
        return new ResponseModel
        {
            Result = -1,
            Comment = "user does not exist! incorrect data"
        };
    }
}