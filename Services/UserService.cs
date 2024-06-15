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
            Name = userDto.Name,
            Surname = userDto.Surname,
        };
        var response = await _repositoryService.CreateUser(user);
        return response;
    }
}