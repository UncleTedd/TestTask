namespace AlifTestTask.Models;

public class CreateUserModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PassportNumber { get; set; }
}