using AlifTestTask.DbContext;
using AlifTestTask.DTOs;
using AlifTestTask.Models;
using AlifTestTask.RepositoryService;

namespace AlifTestTask.Services;

public class UserService
{
    private readonly AlifDbContext _dbContext;
    private readonly UserRepositoryService _repositoryService;

    public UserService(AlifDbContext dbContext, UserRepositoryService userRepositoryService)
    {
        _dbContext = dbContext;
        _repositoryService = userRepositoryService;
    }

    public async Task<ResponseModel> CreateUser(UserDTO userDto)
    {
        var user = new User()
        {
            IsVerified = userDto.IsVerified,
            Id = userDto.Id,
            Name = userDto.Name,
            Surname = userDto.Surname,
        };
        var wallet = new Wallet()
        {
            Balance = 0,
            Id = 1,
        };
        
        var createUserResponse = await _repositoryService.CreateUser(user, wallet);
        
        return createUserResponse;
    }
}