using System;
using System.Diagnostics;
using System.Windows.Input;
using Prism.Mvvm;
using GameWPF.Model;
using GameWPF.View;
using System.Threading.Tasks;

namespace GameWPF.ViewModel
{
    public class PlayViewModel : BindableBase, INotifyLoad
    {
        private readonly string GameExecutablePath = @"..\Game\GameUnity.exe";
        private readonly string UserIdArgument = "UserId";

        private MainViewModel MainViewModel { get; set; }

        public User User
        {
            get => MainViewModel.User;
        }

        private bool isGameActive;
        public bool IsGameActive
        {
            get => isGameActive;
            set => SetProperty(ref isGameActive, value);
        }
        private Process Process { get; set; }
        private Task Timer { get; set; }

        public CommandDelegate setMenuVMCommand;
        public ICommand SetMenuVMCommand
        {
            get => setMenuVMCommand ?? (setMenuVMCommand = new CommandDelegate(parameter => MainViewModel.SetViewModel(ViewModelEnum.Menu),
                parameter => !IsGameActive));
        }

        public PlayViewModel(MainViewModel _mainViewModel)
        {
            MainViewModel = _mainViewModel;
        }

        private async Task AwaitGameExitAsync()
        {
            while (!Process.HasExited)
            {
                await Task.Delay(2);
            }
            IsGameActive = false;
            MainViewModel.SetWindowMode(WindowMode.GetWindowMode(Properties.Settings.Default.DefaultWindowMode));
        }

        public void Load()
        {
            if (User.ActiveCharacter != null)
            {
                try
                {
                    Process = new Process()
                    {
                        StartInfo =
                {
                    FileName = GameExecutablePath,
                    Arguments = $"{UserIdArgument}={MainViewModel.User.Id}"
                }
                    };
                    MainViewModel.SetWindowMode(WindowMode.GetWindowMode("WindowBordered"));
                    IsGameActive = true;
                    Process.Start();
                    Timer = Task.Factory.StartNew(AwaitGameExitAsync);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    Process?.Close();
                    IsGameActive = false;
                }
            }
        }
    }
}
