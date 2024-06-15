namespace AlifTestTask.Models;

public class Transaction
{
    public int  Id { get; set; }
    public DateTime transactionTime { get; set; }
    public User User { get; set; }
}