namespace AlifTestTask.Models;

public class Wallet
{
    public int Id { get; set; }
    public int Balance { get; set; }
    public List<Transaction> Transactions { get; set; }
}