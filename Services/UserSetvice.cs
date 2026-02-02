using BankKapital.Models;
using BankKapital.Services;
using BankKapital.ViewModels;

namespace BankCapital.Services
{
    //класс - сервис для пользовательей
    public class UserService : IUserService
    {
        static public DataBaseContext context = new DataBaseContext();

        public List<User> UploadingUserData()
        {
            var registredUser = context.Users.ToList();
            return registredUser;
        }



        public bool RegisterUser(RegistrationViewModel registrationData)
        {
            var theUserForRegistration = new User
            {
                Name = registrationData.Name,
                SerName = registrationData.SurName,
                LoginName = registrationData.LoginName,
                Password = registrationData.Password,
                PhoneNumber = double.Parse(registrationData.PhoneNumber),
                PassportSeries = int.Parse(registrationData.PassportSeries),
                PassportNumber = int.Parse(registrationData.PassportNumber),
                RegistrationDate = DateTime.Now,
            };

            context.Users.Add(theUserForRegistration);
            context.SaveChanges();
            return true;
        }

        public User UserSearchByLogin(string login)
        {
            var theFoundUser = context.Users.SingleOrDefault(x => x.LoginName == login);
            return theFoundUser;
        }

        public User Authenticate(string login, string password)
        {
            List<User> users = UploadingUserData();
            var user = users.SingleOrDefault(x => x.LoginName == login && x.Password == password);

            if (user != null)
            {
                return user;
            }

            return null;
        }

        public bool CheckingTheUserForNewData(RegistrationViewModel registrationData)
        {
            List<User> registredUser = UploadingUserData();
            foreach (var user in registredUser)
            {
                if (registrationData.LoginName == user.LoginName ||
                    double.Parse(registrationData.PhoneNumber) == user.PhoneNumber ||
                    int.Parse(registrationData.PassportNumber) == user.PassportNumber)
                    return false;
            }
            return true;
        }


        public void DeletingAUser(User theUserToDelete)
        {
            context.Users.Remove(theUserToDelete);
            context.SaveChanges();
        }
    }
}
