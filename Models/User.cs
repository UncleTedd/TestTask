namespace AlifTestTask.Models;

public class User
{
    public int UserVerification { get; set; } = 0;
    public string? PhoneNumber { get; set; }
    public string? PassportNumber { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Wallet Wallet { get; set; }
}