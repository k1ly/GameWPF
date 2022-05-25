using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Prism.Mvvm;
using GameWPF.Model;
using GameWPF.Util.Validation;
using GameWPF.Util.Hash;

namespace GameWPF.ViewModel
{
    public class LoginViewModel : BindableBase
    {
        private MainViewModel MainViewModel { get; set; }

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

        private bool doRememberPassword;
        public bool DoRememberPassword
        {
            get => doRememberPassword;
            set => SetProperty(ref doRememberPassword, value);
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

        private CommandDelegate<bool> togglePasswordCommand;
        public ICommand TogglePasswordCommand
        {
            get => togglePasswordCommand ?? (togglePasswordCommand = new CommandDelegate<bool>(value => IsPasswordVisible = value));
        }

        private CommandDelegate loginCommand;
        public ICommand LoginCommand
        {
            get => loginCommand ?? (loginCommand = new CommandDelegate(Login, parameter =>
                IsPasswordValid && UserValidator.IsLoginValid(UserLogin)));
        }

        private CommandDelegate setRegisterVMCommand;
        public ICommand SetRegisterVMCommand
        {
            get => setRegisterVMCommand ?? (setRegisterVMCommand = new CommandDelegate(parameter => MainViewModel.SetViewModel(ViewModelEnum.Register)));
        }

        public LoginViewModel(MainViewModel _mainViewModel)
        {
            MainViewModel = _mainViewModel;
        }

        private void PasswordChangedHandler(PasswordBox box)
        {
            IsUserIncorrect = false;
            UserPassword = box.Password;
            IsPasswordValid = UserValidator.IsPasswordValid(userPassword);
        }

        private void Login(object parameter)
        {
            User user = MainViewModel.DbContext.Users.Where(u => u.Login == UserLogin).FirstOrDefault();
            if (user != null && SaltedHash.Verify(user.PswHash, user.PswSalt, UserPassword))
            {
                if (DoRememberPassword)
                {
                    Properties.Settings.Default.UserId = user.Id;
                    Properties.Settings.Default.Save();
                }
                MainViewModel.User = user;
                MainViewModel.SetViewModel(ViewModelEnum.Menu);
            }
            else
                IsUserIncorrect = true;
        }
    }
}
