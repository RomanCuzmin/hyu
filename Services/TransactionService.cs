using BankKapital.Models;

namespace BankCapital.Services
{
    // класс - сервис для транзакций
    public static class TransactionService
    {   
        public static  DataBaseContext context = new DataBaseContext();

        public static List<TransactionBank> UploadingTransactionData()
        {
            List<TransactionBank> registredTransaction = context.Transactions.ToList();
            return registredTransaction;
        }

        public static TransactionBank CreatingATransactionForRegestration (BankAccount bankAccount, string type,
            decimal theAmount)
        {
            TransactionBank transaction = new TransactionBank
            {
                BankAccount = bankAccount,
                Type = type,
                TheAmount = theAmount,
                Date = DateTime.Now               
            };
            return transaction;
        }

        public static void TransactionRegestration(TransactionBank theBankAccountForRegesstration)
        {
            context.Transactions.Add(theBankAccountForRegesstration);
            context.SaveChanges();
        }
    }
}
