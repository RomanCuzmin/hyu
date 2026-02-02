using BankKapital.Models;

namespace BankCapital.Services
{
   // класс - сервиис для банковских счетов
   public static class BankAccountService
    {
       public static DataBaseContext context = new DataBaseContext();
 
        public static List<BankAccount> UpdatingBankAccountData()
        {
            var bankAccounts = context.BankAccounts.ToList();
            return bankAccounts;
        }

        public static BankAccount CreatingABankAccountForRegestration(float phoneNumberOwner, decimal accountNumber, decimal correspondentAccountNumber)
        {
            var theBankAccountForRegistration = new BankAccount
            {
                OwnerPhoneNumber = phoneNumberOwner,
                AccountNumber = accountNumber,
                CorrespondentAccountNumber = correspondentAccountNumber,
                BIC = 031102503,
                Balance = 0,
                BankName = "Капитал банк",
                BankAdress = "Нижний Новгород улица 40 лет октября дом 5а"
            };
            return theBankAccountForRegistration;

        }

        public static bool CheckingTheBankAccountForNewData(BankAccount bankAccountForRegistration)
        {
            var registredBankAccounts = UpdatingBankAccountData();

            foreach (var bankAccount in registredBankAccounts)
            {
                if (bankAccount.OwnerPhoneNumber == bankAccountForRegistration.OwnerPhoneNumber ||
                    bankAccount.AccountNumber == bankAccountForRegistration.AccountNumber ||
                    bankAccount.CorrespondentAccountNumber == bankAccountForRegistration.CorrespondentAccountNumber)
                    return false;
            }
            return true;   
        }

        public static void BankAccountRegesstration(BankAccount theBankAccountForRegesstration)
        {
            context.BankAccounts.Add(theBankAccountForRegesstration);
            context.SaveChanges();
        }
        
        public static void DeletingABankAccount(BankAccount theBankAccountToDelete)
        {
            context.BankAccounts.Remove(theBankAccountToDelete);
            context.SaveChanges();
        }

        public static void UpdatingBankAccountData(BankAccount bankAccountForRenewal)
        {
            context.BankAccounts.Update(bankAccountForRenewal);
            context.SaveChanges();
        }
    }
}
