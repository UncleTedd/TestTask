namespace AlifTestTask.Models;

public class User
{
   public bool IsVerified = false;
   public int Id { get; set; }
   public string Name { get; set; }
   public string Surname { get; set; }
   public Wallet Wallet { get; set; }

}