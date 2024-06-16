using AlifTestTask.Models;

namespace AlifTestTask.Services;

public class TransactionService
{
    private readonly UserService _userService;

    public TransactionService(UserService userService)
    {
        _userService = userService;
    }

    // public Task<ResponseModel> Replenish(int id)
    // {
    //     
    // }
}