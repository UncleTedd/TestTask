using AlifTestTask.Models;

namespace AlifTestTask.DTOs;

public class UserDTO
{
    public bool IsVerified = false;
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    
    
}