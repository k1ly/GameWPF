using System;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using GameWPF.Model;

namespace GameWPF.ViewModel
{
    public class StoreViewModel : BindableBase
    {
        public static readonly int MaxQuantity = 100;

        private MainViewModel MainViewModel { get; set; }

        public User User
        {
            get => MainViewModel.User;
        }

        private bool isModalVisible;
        public bool IsModalVisible
        {
            get => isModalVisible;
            set => SetProperty(ref isModalVisible, value);
        }

        private int itemQuantity;
        public int ItemQuantity
        {
            get => itemQuantity;
            set => SetProperty(ref itemQuantity, value);
        }

        private int maxItemQuantity;
        public int MaxItemQuantity
        {
            get => maxItemQuantity;
            set => SetProperty(ref maxItemQuantity, value);
        }

        public ObservableCollection<Item> ItemSet { get; private set; }

        private Item selectedItem;
        public Item SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        public ICommand SetVMCommand
        {
            get => MainViewModel.SetVMCommand;
        }

        private CommandDelegate purchaseItemCommand;
        public ICommand PurchaseItemCommand
        {
            get => purchaseItemCommand ?? (purchaseItemCommand = new CommandDelegate(PurchaseItem));
        }

        private CommandDelegate confirmPurchaseCommand;
        public ICommand ConfirmPurchaseCommand
        {
            get => confirmPurchaseCommand ?? (confirmPurchaseCommand = new CommandDelegate(ConfirmPurchase,
                parameter => User.ActiveCharacter != null && SelectedItem != null
                && MainViewModel.User.ActiveCharacter.Gold >= SelectedItem.Price * ItemQuantity));
        }

        private CommandDelegate closeModalCommand;
        public ICommand CloseModalCommand
        {
            get => closeModalCommand ?? (closeModalCommand = new CommandDelegate(parameter => IsModalVisible = false));
        }

        public StoreViewModel(MainViewModel _mainViewModel)
        {
            MainViewModel = _mainViewModel;
            ItemSet = new ObservableCollection<Item>(MainViewModel.DbContext.Items);
        }

        private void PurchaseItem(object parameter)
        {
            if (User.ActiveCharacter != null)
            {
                ItemQuantity = 1;
                ContainerItem containerItem = MainViewModel.User.ActiveCharacter.Inventory.ContainerItems.Where(i => i.Item == SelectedItem).FirstOrDefault();
                MaxItemQuantity = MaxQuantity - (containerItem != null ? containerItem.Quantity : 0);
            }
            IsModalVisible = true;
        }

        private void ConfirmPurchase(object parameter)
        {
            var transaction = MainViewModel.DbContext.Database.BeginTransaction();
            try
            {
                ContainerItem containerItem = MainViewModel.User.ActiveCharacter.Inventory.ContainerItems.Where(i => i.Item == SelectedItem).FirstOrDefault();
                if (containerItem == null)
                {
                    containerItem = new ContainerItem();
                    containerItem.Id = Guid.NewGuid();
                    containerItem.Quantity = ItemQuantity;
                    containerItem.Item = SelectedItem;
                    containerItem.Container = MainViewModel.User.ActiveCharacter.Inventory;
                    MainViewModel.DbContext.ContainerItems.Add(containerItem);
                }
                else
                {
                    containerItem.Quantity += ItemQuantity;
                    MainViewModel.DbContext.Entry(containerItem).State = System.Data.Entity.EntityState.Modified;
                }
                MainViewModel.User.ActiveCharacter.Gold -= SelectedItem.Price * ItemQuantity;
                MainViewModel.DbContext.SaveChanges();
                transaction.Commit();

                RaisePropertyChanged("User");
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                throw new Exception("User character inventory item was not created or updated", exception);
            }
            IsModalVisible = false;
            MainViewModel.DbContext.Entry(MainViewModel.User.ActiveCharacter.Inventory).Collection(i => i.ContainerItems).Load();
        }
    }
}
