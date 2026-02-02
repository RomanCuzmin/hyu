using BankKapital.ValidationRules;
using BankKapital.Validators.BaseValidators;
using BankKapital.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankKapital.Validators
{
    public class ValidationBeforeRegistration : ValidatorBase<RegistrationViewModel>
    {
        protected override void InitializeRules()
        {
            //Валидации для имени
            AddRule(new RequiredRule<RegistrationViewModel>(
                nameof(RegistrationViewModel.Name),
                vm =>vm.Name,
                "Имя обязательно для заполнения"));
            AddRule(new LengthRule<RegistrationViewModel>(
                nameof(RegistrationViewModel.Name),
                vm => vm.Name,
                2, 20,
                "Имя должно содержать от 2 до 20 символов"));
            //Валидация для фамилии
            AddRule(new RequiredRule<RegistrationViewModel>(
                nameof(RegistrationViewModel.SurName),
                vm => vm.SurName,
                "Фамилия обязательна для заполнения"));
            AddRule(new LengthRule<RegistrationViewModel>(
                nameof(RegistrationViewModel.SurName),
                vm => vm.SurName,
                2, 20,
                "Фамилия должна содержать от 2 до 20 символов"));
            //Валидация для логина
            AddRule(new RequiredRule<RegistrationViewModel>(
                nameof(RegistrationViewModel.LoginName),
                vm => vm.LoginName,
                "Логин обязателен для заполнения"));
            AddRule(new LengthRule<RegistrationViewModel>(
                nameof(RegistrationViewModel.LoginName),
                vm => vm.LoginName,
                6, 30,
                "Логин должен содержать от 6 до 30 символов"));

            //Валидация пароля
            AddRule(new RequiredRule<RegistrationViewModel>(
                nameof(RegistrationViewModel.Password),
                vm => vm.Password,
                "Пароль обязателен для заполнения"));

            AddRule(new LengthRule<RegistrationViewModel>(
                nameof(RegistrationViewModel.Password),
                vm => vm.Password,
                8, 16,
                "Пароль должен содержать от 8 до 16 символов"));

            AddRule(new CustomRule<RegistrationViewModel>(
                nameof(RegistrationViewModel.ConfirmPassword),
                vm => vm.Password == vm.ConfirmPassword,
                "Пароли не совподают"));

            //валидация номера телефона
            AddRule(new RequiredRule<RegistrationViewModel>(
                nameof(RegistrationViewModel.PhoneNumber),
                vm => vm.PhoneNumber,
                "Номер телефона обязателен для заполнения"));
            AddRule(new CustomRule<RegistrationViewModel>(
                nameof(RegistrationViewModel.PhoneNumber),
                vm => double.TryParse(vm.PhoneNumber, out _),
                "Номер телефона должен содержать только цифры"));

            AddRule(new LengthRule<RegistrationViewModel>(
                nameof(RegistrationViewModel.PhoneNumber),
                vm => vm.PhoneNumber,
                11, 11,
                "Номер телефона должен содержать 11 цифр"));
            //валидация серии паспорта 
            AddRule(new RequiredRule<RegistrationViewModel>(
                nameof(RegistrationViewModel.PassportSeries),
                vm => vm.PassportSeries,
                "Серия паспорта обязательна для заполнения"));
            AddRule(new NumberRule<RegistrationViewModel>(
                nameof(RegistrationViewModel.PassportSeries),
                vm => vm.PassportSeries,
                "Серия паспорта должна соледержать только цифры"));
            AddRule(new LengthRule<RegistrationViewModel>(
                nameof(RegistrationViewModel.PassportSeries),
                vm => vm.PassportSeries,
                4, 4,
                "Серия паспорта должна содержать 4 символа"));
            

            // валидация номера паспорта
            AddRule(new RequiredRule<RegistrationViewModel>(
                nameof(RegistrationViewModel.PassportNumber),
                vm => vm.PassportNumber,
                "Номер паспорта обязателен для заполнения"));
            AddRule(new CustomRule<RegistrationViewModel>(
                nameof(RegistrationViewModel.PassportNumber),
                vm => int.TryParse(vm.PassportNumber, out _),
                "Номер паспорта должен содержать только цифры"));
            AddRule(new LengthRule<RegistrationViewModel>(
                nameof(RegistrationViewModel.PassportNumber),
                vm => vm.PassportNumber,
                6,6,
                "Номер паспорта должен содержать 6 символов"));
        }
    }
}
