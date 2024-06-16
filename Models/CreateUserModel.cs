namespace AlifTestTask.Models;

public class CreateUserModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int? PhoneNumber { get; set; }
    public int? PassportNumber { get; set; }
}