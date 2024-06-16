using AlifTestTask.DbContext;
using AlifTestTask.DTOs;
using AlifTestTask.Helper;
using AlifTestTask.Models;
using AlifTestTask.RepositoryService;

namespace AlifTestTask.Services;

public class UserService
{
    
    private readonly UserRepositoryService _repositoryService;
    private readonly Functions _functions;

    public UserService(UserRepositoryService userRepositoryService, Functions functions)
    {
      
        _repositoryService = userRepositoryService;
        _functions = functions;
    }

    public async Task<ResponseModel> CreateUser(CreateUserModel userModel)
    {
        var user = _functions.MapCreateUserToUser(userModel);
        
        var createUserResponse = await _repositoryService.CreateUser(user);
        return createUserResponse;
    }

    public async Task<UserDTO?> GetUser(int id)
    {
        var serviceResponse = await _repositoryService.GetUser(id);
        if (serviceResponse is null)
        {
            return null;
        }
        return serviceResponse;
    }

    public async Task<ResponseModel> CheckUserVerification(int id)
    {
        var resultOfVerification = await _repositoryService.UserVerified(id);

        if (resultOfVerification == 0)
        {
            return new ResponseModel()
            {
                Result = resultOfVerification,
                Comment = "user is not verified"
            };
        }

        return new ResponseModel()
        {
            Result = -1,
            Comment = "Error occured while verifying, incorrect data"
        };
    }

    public async Task<ResponseModel> VerifyUser( ToVerifyUserModel user)
    {
        var resultOfVerifyingUser = await _repositoryService.VerifyUser(user);
        return resultOfVerifyingUser;
    }

    public async Task<ResponseModel> Replenish(int id, decimal amount)
    {
        var resultOfReplenish = await _repositoryService.Replenish(id, amount);
        return resultOfReplenish;
    }
    
    
    
}