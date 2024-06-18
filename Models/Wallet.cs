namespace AlifTestTask.Models;

public class Wallet
{
    public Wallet()
    {
        Balance = decimal.Zero;
    }
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public decimal Balance { get; set; }
    public List<Transaction> Transactions { get; set; } = new();
}