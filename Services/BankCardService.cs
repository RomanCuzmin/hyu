using BankKapital.Models;

namespace BankCapital.Services
{
    public static class BankCardService
    {
       // класс - сервис для  банковских карт
        public static DataBaseContext context = new DataBaseContext();
        public static List<BankCard> UploadingBankCardData()
        {
            List <BankCard> registredBankCard = context.BankCards.ToList();
            return registredBankCard;
        }

        public static BankCard CreatingABankkCardForRegestration(decimal bankAccount, decimal bankCardNumber, int cviCode,
            int pinCode, DateTime validUntil)
        {
            BankCard theBankCardForRegesstration = new BankCard
            {
                AccountNumber = bankAccount,
                BankCardNumber = bankCardNumber,
                CVICode = cviCode,
                PinCode = pinCode,
                ValidUntil = validUntil,
            };

            return theBankCardForRegesstration;
        }

        public static bool CheckingTheBankCardForNewData(BankCard bankCardForVerification)
        {
            List<BankCard> registredBankCard = UploadingBankCardData();

            foreach (var bankCard in registredBankCard)
            {
                if (bankCardForVerification.BankCardNumber == bankCardForVerification.BankCardNumber)
                    return false;
            }
            return true;
        } 

        public static void BankCardRegestration(BankCard theBankCardForRegestration)
        {
            context.BankCards.Add(theBankCardForRegestration);
            context.SaveChanges();
        }

        public static void DeletingABankCard(BankCard theBankCardToDelete)
        {
            context.BankCards.Remove(theBankCardToDelete);
            context.SaveChanges();
        }

    }
}
