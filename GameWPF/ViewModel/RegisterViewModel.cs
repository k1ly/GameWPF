using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Prism.Mvvm;
using GameWPF.Model;
using GameWPF.Model.Enum;
using GameWPF.Util.Validation;
using GameWPF.Util.Hash;

namespace GameWPF.ViewModel
{
    public class RegisterViewModel : BindableBase
    {
        private MainViewModel MainViewModel { get; set; }

        private string userName;
        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        private string userLogin;
        public string UserLogin
        {
            get => userLogin;
            set => SetProperty(ref userLogin, value);
        }

        private string userPassword;
        public string UserPassword
        {
            get => userPassword;
            set => SetProperty(ref userPassword, value);
        }

        private string passwordRepeat;
        public string PasswordRepeat
        {
            get => passwordRepeat;
            set => SetProperty(ref passwordRepeat, value);
        }

        private int passwordRepeatStatus;
        public int PasswordRepeatStatus
        {
            get => passwordRepeatStatus;
            set => SetProperty(ref passwordRepeatStatus, value);
        }

        private bool isPasswordVisible;
        public bool IsPasswordVisible
        {
            get => isPasswordVisible;
            set => SetProperty(ref isPasswordVisible, value);
        }

        private bool isPasswordValid;
        public bool IsPasswordValid
        {
            get => isPasswordValid;
            set => SetProperty(ref isPasswordValid, value);
        }

        private bool isUserIncorrect;
        public bool IsUserIncorrect
        {
            get => isUserIncorrect;
            set => SetProperty(ref isUserIncorrect, value);
        }

        private CommandDelegate loginChangedCommand;
        public ICommand LoginChangedCommand
        {
            get => loginChangedCommand ?? (loginChangedCommand = new CommandDelegate(parameter => IsUserIncorrect = false));
        }

        private CommandDelegate<PasswordBox> passwordChangedCommand;
        public ICommand PasswordChangedCommand
        {
            get => passwordChangedCommand ?? (passwordChangedCommand = new CommandDelegate<PasswordBox>(PasswordChangedHandler));
        }

        private CommandDelegate<PasswordBox> pswrptChangedCommand;
        public ICommand PswrptChangedCommand
        {
            get => pswrptChangedCommand ?? (pswrptChangedCommand = new CommandDelegate<PasswordBox>(PswrptChangedHandler));
        }

        private CommandDelegate<bool> togglePasswordCommand;
        public ICommand TogglePasswordCommand
        {
            get => togglePasswordCommand ?? (togglePasswordCommand = new CommandDelegate<bool>(value => IsPasswordVisible = value));
        }

        private CommandDelegate registerCommand;
        public ICommand RegisterCommand
        {
            get => registerCommand ?? (registerCommand = new CommandDelegate(Register, parameter =>
                IsPasswordValid && PasswordRepeatStatus > 0 && UserValidator.IsLoginValid(UserLogin) && UserValidator.IsNameValid(UserName)));
        }

        private CommandDelegate setLoginVMCommand;
        public ICommand SetLoginVMCommand
        {
            get => setLoginVMCommand ?? (setLoginVMCommand = new CommandDelegate(parameter => MainViewModel.SetViewModel(ViewModelEnum.Login)));
        }

        public RegisterViewModel(MainViewModel _mainViewModel)
        {
            MainViewModel = _mainViewModel;
        }

        private void PasswordChangedHandler(PasswordBox box)
        {
            UserPassword = box.Password;
            PasswordRepeatStatus = userPassword.Length == 0 ? 0 : (userPassword == passwordRepeat) ? 1 : -1;
            IsPasswordValid = UserValidator.IsPasswordValid(userPassword);
        }

        private void PswrptChangedHandler(PasswordBox box)
        {
            PasswordRepeat = box.Password;
            PasswordRepeatStatus = userPassword.Length == 0 ? 0 : (userPassword == passwordRepeat) ? 1 : -1;
            IsPasswordValid = UserValidator.IsPasswordValid(userPassword);
        }

        private void Register(object parameter)
        {
            if (MainViewModel.DbContext.Users.Where(u => u.Login == UserLogin).Count() == 0)
            {
                User user = new User();
                user.Id = Guid.NewGuid();
                user.Role = UserRoleEnum.Client.GetModel(MainViewModel.DbContext.UserRoles);
                user.Status = UserStatusEnum.Active.GetModel(MainViewModel.DbContext.UserStatuses);
                user.Name = UserName;
                user.Login = UserLogin;
                SaltedHash saltedHash = new SaltedHash(UserPassword);
                user.PswHash = saltedHash.Hash;
                user.PswSalt = saltedHash.Salt;
                MainViewModel.DbContext.Users.Add(user);
                MainViewModel.DbContext.SaveChanges();
                MainViewModel.User = user;
                MainViewModel.SetViewModel(ViewModelEnum.Menu);
            }
            else
                IsUserIncorrect = true;
        }
    }
}
