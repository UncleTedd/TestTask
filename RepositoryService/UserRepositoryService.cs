using AlifTestTask.DbContext;
using AlifTestTask.Models;
using Transaction = System.Transactions.Transaction;

namespace AlifTestTask.RepositoryService;

public class UserRepositoryService
{
    private readonly AlifDbContext _dbContext;

    public UserRepositoryService(AlifDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseModel> CreateUser(User user, Wallet wallet)
    {
        // var transactionCreation = await _dbContext.Transactions.AddAsync(new Models.Transaction()
        //     { Id = transaction.Id, User = transaction.User });
      
         // var walletCreation =_dbContext.Wallets.Add(new Wallet() { Balance = wallet.Balance});
         // var dbResponse = await _dbContext.SaveChangesAsync();
         var result = _dbContext.Users.Add(new User() { Name = user.Name, Surname = user.Surname, Wallet = new Wallet(){Balance = wallet.Balance}});
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
}