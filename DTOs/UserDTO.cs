using AlifTestTask.Models;

namespace AlifTestTask.DTOs;

public class UserDTO
{
    public int UserVerification { get; set; } = 0;
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int? PhoneNumber { get; set; }
    public int PassportNumber { get; set; }
    
    
}