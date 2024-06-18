using AlifTestTask.DbContext;
using AlifTestTask.DTOs;
using AlifTestTask.Helper;
using AlifTestTask.Models;
using AlifTestTask.Services;
using Microsoft.EntityFrameworkCore;



namespace AlifTestTask.RepositoryService;

public class UserRepositoryService
{
    private readonly AlifDbContext _dbContext;
    private readonly VerificationService _verificationService;
    private readonly Functions _functions;

    public UserRepositoryService(AlifDbContext dbContext, VerificationService verificationService, Functions functions)
    {
        _dbContext = dbContext;
        _verificationService = verificationService;
        _functions = functions;
    }

    public async Task<ResponseModel> CreateUser(User user)
    {
        // var transactionCreation = await _dbContext.Transactions.AddAsync(new Models.Transaction()
        //     { Id = transaction.Id, User = transaction.User });
      
         // var walletCreation =_dbContext.Wallets.Add(new Wallet() { Balance = wallet.Balance});
         // var dbResponse = await _dbContext.SaveChangesAsync();
         var userInstance = new User()
         {
             Id = user.Id,
             Name = user.Name,
             Surname = user.Surname,
             Wallet = new Wallet(),
             PhoneNumber = user.PhoneNumber,
             PassportNumber = user.PassportNumber
         };

         if (!string.IsNullOrWhiteSpace(userInstance.PhoneNumber)  && !string.IsNullOrWhiteSpace(userInstance.PassportNumber))
         {
             if ( _functions.VerifyPhonePassportNumber(userInstance.PhoneNumber, userInstance.PassportNumber) == 1)
             {
                 userInstance.UserVerification = 1;
             }
         }

         // var toSend = _dbContext.Users.Add(userInstance);
         // var result = _dbContext.Users.Add(new User()
         // {
         //     Name = user.Name, Surname = user.Surname, Wallet = new Wallet(){Balance = 0}
         // });
         var result = await _dbContext.Users.AddAsync(userInstance);
         var dbResponse = await _dbContext.SaveChangesAsync();
        if (dbResponse > 0)
        {
            return new ResponseModel()
            {
                Result = dbResponse,
                Comment = "successfully added"
            };
        }
        return new ResponseModel()
        {
            Result = -1,
            Comment = "unsuccessful creation"
        };
    }

    public async Task<UserDTO?> GetUser(int id)
    {
       var dbResponse = await _dbContext.Users.Include(x=>x.Wallet).Where(y=>y.Wallet.UserId == id).FirstOrDefaultAsync();
        // var response = await _dbContext.Users.Include(x=>x.Wallet).Select(x=>new User()
        // {
        //     Name = x.Name,
        //     Surname = x.Surname,
        //     PhoneNumber = x.PhoneNumber,
        //     PassportNumber = x.PassportNumber,
        //     UserVerification = x.UserVerification,
        //     Wallet = x.Wallet
        //     
        // }).ToList();
        //if (dbResponse is null) return null;
        // var dbWalletResponse = await _dbContext.Wallets.FirstOrDefaultAsync(x=>x.Id == dbResponse.Id);
       // if (dbWalletResponse is null) return null;
        // var user = _functions.MapUserToUserDto(dbResponse);


       // var response = await _dbContext.Users.Include(x => x.Wallet).ToListAsync();

        
        var user = _functions.MapToUserDto(dbResponse);
       
        // var user = dbResponse.Select(x => _functions.MapToUserDto(x)).ToList();
       return user;
    }

    public async Task<ResponseModel> VerifyUser(ToVerifyUserModel user)
    {
       
        var toVerifyUser = await _dbContext.Users.FindAsync(user.Id);
        var res = _functions.VerifyPhonePassportNumber(user.PhoneNumber, user.PassportSeries);
        if (res == 0 || toVerifyUser is null)
        {
            return new ResponseModel()
            {
                Comment = "wrong data!"
            };
        }

        //  updatedUser = new User()
                // {
                //     Id = toVerifyUser.Id,
                //     Name = toVerifyUser.Name,
                //     Surname = toVerifyUser.Surname,
                //     PassportNumber = user.PassportNumber,
                //     PhoneNumber = user.PhoneNumber,
                //     UserVerification = 1
                // };
        toVerifyUser.PassportNumber =user.PassportSeries; 
        toVerifyUser.PhoneNumber =user.PhoneNumber; 
        toVerifyUser.UserVerification = 1;
        var responseFromSavingChanges = await _dbContext.SaveChangesAsync();
        return new ResponseModel()
        {
            Result = responseFromSavingChanges,
            Comment = "Successfully Verified User"
        };


    }
    
    public async Task<ResponseModel> UserVerified(int id)
    {
        var userToVerify = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (userToVerify == null)
            return new ResponseModel()
            {
                Result = -1,
                Comment = "invalid data"
            };
        // not sure this check is needed
        if (userToVerify is { PassportNumber: not null, PhoneNumber: not null } && _functions.VerifyPhonePassportNumber(userToVerify.PhoneNumber, userToVerify.PassportNumber) == 1)
        {
            userToVerify.UserVerification = 1;
            await _dbContext.SaveChangesAsync();
        }
        var result = userToVerify.UserVerification;

        switch (result)
        {
            case 0:
                return new ResponseModel()
                {
                    Result = result,
                    Comment = "user is not verified!"
                };
            case 1:
                return new ResponseModel()
                {
                    Result = result,
                    Comment = "user is verified!"
                };
        }

        return null;
    }

    public async Task<ResponseModel> Replenish(int id, decimal amount)
    {
        var transaction = new Transaction()
        {
            Amount = amount,
            transactionTime = DateTime.Now,
        };
        var user = await _dbContext.Users.Include(user => user.Wallet).FirstOrDefaultAsync(x => x.Id == id);
        
        if (user is null)
            return new ResponseModel()
            {
                Result = -1,
                Comment = "Invalid data in replenish service!"
            };
        if (user.UserVerification != 1)
            return new ResponseModel()
            {
                Result = -1,
                Comment = "Invalid data in replenish service!"
            };
        user.Wallet.Balance += amount;
        user.Wallet.Transactions.Add(transaction);
        
        var response = await _dbContext.SaveChangesAsync();

        if (response>0)
        {
            return new ResponseModel()
            {
                Result = response,
                Comment = "replenish was successful"
            };
        }
        return new ResponseModel()
        {
            Result = 0,
            Comment = "could not replenish balance!"
        };
    }

    public async Task<ResponseModel> GetBalance(int id)
    {
        //var dbResponse = await _dbContext.Users.FirstOrDefaultAsync(x=>x.)

        return await Task.FromResult(new ResponseModel());
    }
    
    
    
    
    
    
}