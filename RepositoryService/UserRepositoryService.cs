using AlifTestTask.DbContext;
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
             Wallet = new Wallet()
         };
         // var toSend = _dbContext.Users.Add(userInstance);
         var result = _dbContext.Users.Add(new User()
         {
             Name = user.Name, Surname = user.Surname, Wallet = new Wallet(){Balance = 0}
         });
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

    public async Task<ResponseModel> VerifyUser(ToVerifyUserModel user)
    {
        User updatedUser;
        var toVerifyUser = await _dbContext.Users.FindAsync(user.Id);
        var res = _functions.VerifyUser(user);
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
        toVerifyUser.PassportNumber =int.Parse(user.PassportSeries); 
        toVerifyUser.PhoneNumber =int.Parse(user.PhoneNumber); 
        toVerifyUser.UserVerification = 1;
        var responseFromSavingChanges = await _dbContext.SaveChangesAsync();
        return new ResponseModel()
        {
            Result = responseFromSavingChanges,
            Comment = "Successfully Verified User"
        };


    }
    
    
    
    public async Task<int> UserVerified(int id)
    {
        var userToVerify = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (userToVerify != null) return userToVerify.UserVerification;
        return -1;
    }
    
    
    
    
    
    
    
}