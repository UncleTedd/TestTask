namespace AlifTestTask.Models;

public class GetTransactionsModel
{
    public decimal totalAmountOfMoneyTransfered { get; set; }
    public List<Transaction> Transactions { get; set; }
}