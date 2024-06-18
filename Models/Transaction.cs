namespace AlifTestTask.Models;

public class Transaction
{
    public int  Id { get; set; }
    public int  WalletId { get; set; }
    public DateTime transactionTime { get; set; }
    public decimal Amount { get; set; }

    public Wallet Wallet { get; set; }
}