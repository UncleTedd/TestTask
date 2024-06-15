using AlifTestTask.DTOs;
using AlifTestTask.Models;
using AlifTestTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace AlifTestTask.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController: Controller
{
    private readonly UserService _service;

    public UserController(UserService service)
    {
        _service = service;
    }

    [HttpPost("createUser")]
    public async Task<ResponseModel> CreateUser([FromBody]UserDTO user)
    {
        
        var serviceResponse = await _service.CreateUser(user);
        return serviceResponse;
    }
    
}