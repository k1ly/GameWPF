using System;
using System.Windows;
using System.Windows.Input;
using System.Globalization;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using GameWPF.Model;
using GameWPF.Model.Enum;
using GameWPF.View;

namespace GameWPF.ViewModel
{
    public class SettingsViewModel : BindableBase
    {
        private MainViewModel MainViewModel { get; set; }

        public User User
        {
            get => MainViewModel.User;
        }

        public bool IsUserRoleAdmin
        {
            get => MainViewModel.User.Role == UserRoleEnum.Admin;
        }

        public ObservableCollection<CultureInfo> Languages { get; private set; }

        private CultureInfo selectedLanguage;
        public CultureInfo SelectedLanguage
        {
            get => selectedLanguage;
            set => SetProperty(ref selectedLanguage, value);
        }

        public ObservableCollection<WindowMode> WindowModes { get; private set; }

        private WindowMode selectedWindowMode;
        public WindowMode SelectedWindowMode
        {
            get => selectedWindowMode;
            set => SetProperty(ref selectedWindowMode, value);
        }

        private bool doNotLogOut;
        public bool DoNotLogOut
        {
            get => doNotLogOut;
            set => SetProperty(ref doNotLogOut, value);
        }

        public ICommand SetVMCommand
        {
            get => MainViewModel.SetVMCommand;
        }

        private CommandDelegate logOutCommand;
        public ICommand LogOutCommand
        {
            get => logOutCommand ?? (logOutCommand = new CommandDelegate(LogOut));
        }

        private CommandDelegate saveChangesCommand;
        public ICommand SaveChangesCommand
        {
            get => saveChangesCommand ?? (saveChangesCommand = new CommandDelegate(SaveChanges));
        }

        public SettingsViewModel(MainViewModel _mainViewModel)
        {
            MainViewModel = _mainViewModel;
            Languages = new ObservableCollection<CultureInfo>(((App)Application.Current).Languages);
            SelectedLanguage = Properties.Settings.Default.DefaultLanguage;
            WindowModes = new ObservableCollection<WindowMode>();
            WindowModes.Add(WindowMode.GetWindowMode("FullScreen"));
            WindowModes.Add(WindowMode.GetWindowMode("Windowed"));
            WindowModes.Add(WindowMode.GetWindowMode("WindowBordered"));
            SelectedWindowMode = WindowMode.GetWindowMode(Properties.Settings.Default.DefaultWindowMode);
            DoNotLogOut = Properties.Settings.Default.UserId != null;
        }

        private void LogOut(object parameter)
        {
            Properties.Settings.Default.UserId = Guid.Empty;
            Properties.Settings.Default.Save();
            MainViewModel.User = null;
            MainViewModel.IsUserAuthorizing = true;
            MainViewModel.SetViewModel(ViewModelEnum.Login);
        }

        private void SaveChanges(object parameter)
        {
            ((App)Application.Current).Language = SelectedLanguage;
            Properties.Settings.Default.DefaultLanguage = SelectedLanguage;
            MainViewModel.SetWindowMode(SelectedWindowMode);
            Properties.Settings.Default.DefaultWindowMode = SelectedWindowMode.Name;
            Properties.Settings.Default.UserId = DoNotLogOut ? MainViewModel.User.Id : Guid.Empty;
            Properties.Settings.Default.Save();
        }
    }
}
