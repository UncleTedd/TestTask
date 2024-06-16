namespace AlifTestTask.Models;

public class User
{
   public int UserVerification { get; set; } = 0;
   public int? PhoneNumber { get; set; }
   public int? PassportNumber { get; set; }
   public int Id { get; set; }
   public string Name { get; set; }
   public string Surname { get; set; }
   public Wallet Wallet { get; set; }
   

}