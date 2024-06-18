using AlifTestTask.DTOs;
using AlifTestTask.Models;
using AlifTestTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace AlifTestTask.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly UserService _service;

    public UserController(UserService service)
    {
        _service = service;
    }

    [HttpPost("createUser")]
    public async Task<ResponseModel> CreateUser([FromBody] CreateUserModel user)
    {
        var serviceResponse = await _service.CreateUser(user);
        return serviceResponse;
    }

    [HttpPost("checkVerification")]
    public async Task<ResponseModel> CheckUserVerification(int id)
    {
        var serviceResponse = await _service.CheckUserVerification(id);
        return serviceResponse;
    }

    [HttpPost("verifyUser")]
    public async Task<ResponseModel> VerifyUser([FromBody] ToVerifyUserModel user)
    {
        var serviceresponse = await _service.VerifyUser(user);
        return serviceresponse;
    }

    [HttpPost("GetUser")]
    public async Task<UserDTO?> GetUser(int id)
    {
        var serviceResponse = await _service.GetUser(id);
        return serviceResponse;
    }

    [HttpPost("Replenish")]
    public async Task<ResponseModel> Replenish(int id, decimal amount)
    {
        var serviceResponse = await _service.Replenish(id, amount);
        return serviceResponse;
    }

    [HttpPost("GetBalance")]
    public async Task<ResponseModel> GetBalance(int id)
    {
        var serviceResponse = await _service.GetBalance(id);
        return serviceResponse;
    }
}