
using BankKapital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCapital.Services
{
    public static  class Bank
    {
        public static bool CheckingTheBalanceForTransferAmount(BankAccount bankAccountForVerification, uint transferAmount)
        {
            if (bankAccountForVerification.Balance == transferAmount)
                return true;
            return false;
    
        }

        public static void MoneyTransfer(BankAccount sendersBankAccount, BankAccount recipientBankAccount, uint transferAmount)
        {
            sendersBankAccount.Balance -= transferAmount;
            recipientBankAccount.Balance += transferAmount;

        }
    }
}
