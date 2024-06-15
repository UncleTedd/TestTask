using AlifTestTask.DTOs;
using AlifTestTask.Models;
using AlifTestTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace AlifTestTask.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController: Controller
{
    private UserService _service;

    public UserController(UserService service)
    {
        service = _service;
    }

    [HttpPost]
    public async Task<ResponseModel> CreateUser(UserDTO user)
    {
        var serviceResponse = await _service.CreateUser(user);
        return serviceResponse;
    }
    
}