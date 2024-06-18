using AlifTestTask.DTOs;
using AlifTestTask.Helper;
using AlifTestTask.Models;
using AlifTestTask.RepositoryService;

namespace AlifTestTask.Services;

public class UserService
{
    private readonly Functions _functions;

    private readonly UserRepositoryService _repositoryService;

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
        if (serviceResponse is null) return null;
        return serviceResponse;
    }

    public async Task<ResponseModel> CheckUserVerification(int id)
    {
        var resultOfVerification = await _repositoryService.UserVerified(id);
        return resultOfVerification;
    }

    public async Task<ResponseModel> VerifyUser(ToVerifyUserModel user)
    {
        var resultOfVerifyingUser = await _repositoryService.VerifyUser(user);
        return resultOfVerifyingUser;
    }

    public async Task<ResponseModel> Replenish(int id, decimal amount)
    {
        var resultOfReplenish = await _repositoryService.Replenish(id, amount);
        return resultOfReplenish;
    }

    public async Task<GetTransactionsModel> GetBalanceAndTransactions(int id)
    {
        var resultGetBalanceAndTransactions = await _repositoryService.GetBalanceAndTransactions(id);
        return resultGetBalanceAndTransactions;
    }

    public async Task<ResponseModel> GetBalance(int id)
    {
        var resultOfGetBalance = await _repositoryService.GetBalance(id);
        return resultOfGetBalance;
    }
}