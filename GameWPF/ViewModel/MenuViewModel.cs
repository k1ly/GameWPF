using System.Windows.Input;
using Prism.Mvvm;

namespace GameWPF.ViewModel
{
    public class MenuViewModel : BindableBase
    {
        private MainViewModel MainViewModel { get; set; }

        public ICommand SetVMCommand
        {
            get => MainViewModel.SetVMCommand;
        }

        public MenuViewModel(MainViewModel _mainViewModel)
        {
            MainViewModel = _mainViewModel;
        }
    }
}
