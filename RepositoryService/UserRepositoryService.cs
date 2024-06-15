using AlifTestTask.DbContext;
using AlifTestTask.Models;

namespace AlifTestTask.RepositoryService;

public class UserRepositoryService
{
    private readonly AlifDbContext _dbContext;

    public UserRepositoryService(AlifDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseModel> CreateUser(User user)
    {
        var result = await _dbContext.Users.AddAsync(new User() { Name = user.Name, Surname = user.Surname });
        var dbResponse = await _dbContext.SaveChangesAsync();
        if (dbResponse == 1)
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