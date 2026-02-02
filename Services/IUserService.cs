using BankKapital.Models;
using BankKapital.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankKapital.Services
{
    public interface IUserService
    {
        List<User> UploadingUserData();

        bool RegisterUser(RegistrationViewModel registrationData);

        User UserSearchByLogin(string login);

        User Authenticate(string login, string password);

        bool CheckingTheUserForNewData(RegistrationViewModel registrationData);

        void DeletingAUser(User theUserToDelete);
    }
}
