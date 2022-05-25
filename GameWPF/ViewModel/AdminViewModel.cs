using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using GameWPF.Model;
using GameWPF.Model.Enum;
using System.Threading.Tasks;

namespace GameWPF.ViewModel
{
    public class AdminViewModel : BindableBase, INotifyLoad
    {
        private MainViewModel MainViewModel { get; set; }

        private bool isModalVisible;
        public bool IsModalVisible
        {
            get => isModalVisible;
            set => SetProperty(ref isModalVisible, value);
        }
        public ObservableCollection<UserRole> UserRoles { get; private set; }

        private UserRole selectedUserRole;
        public UserRole SelectedUserRole
        {
            get => selectedUserRole;
            set => SetProperty(ref selectedUserRole, value);
        }

        public ObservableCollection<UserStatus> UserStatuses { get; private set; }

        private UserStatus selectedUserStatus;
        public UserStatus SelectedUserStatus
        {
            get => selectedUserStatus;
            set => SetProperty(ref selectedUserStatus, value);
        }

        private Task Timer { get; set; }

        private ObservableCollection<User> userSet;
        public ObservableCollection<User> UserSet
        {
            get => userSet;
            set => SetProperty(ref userSet, value);
        }

        private User selectedUser;
        public User SelectedUser
        {
            get => selectedUser;
            set => SetProperty(ref selectedUser, value);
        }

        private ObservableCollection<Message> messageSet;
        public ObservableCollection<Message> MessageSet
        {
            get => messageSet;
            set => SetProperty(ref messageSet, value);
        }

        private Message selectedMessage;
        public Message SelectedMessage
        {
            get => selectedMessage;
            set => SetProperty(ref selectedMessage, value);
        }

        private ObservableCollection<TradeOffer> tradeOfferSet;
        public ObservableCollection<TradeOffer> TradeOfferSet
        {
            get => tradeOfferSet;
            set => SetProperty(ref tradeOfferSet, value);
        }

        private TradeOffer selectedTradeOffer;
        public TradeOffer SelectedTradeOffer
        {
            get => selectedTradeOffer;
            set => SetProperty(ref selectedTradeOffer, value);
        }

        private ObservableCollection<Character> characterSet;
        public ObservableCollection<Character> CharacterSet
        {
            get => characterSet;
            set => SetProperty(ref characterSet, value);
        }

        private Character selectedCharacter;
        public Character SelectedCharacter
        {
            get => selectedCharacter;
            set => SetProperty(ref selectedCharacter, value);
        }

        public ICommand SetVMCommand
        {
            get => MainViewModel.SetVMCommand;
        }

        private CommandDelegate userSelectionChangedCommand;
        public ICommand UserSelectionChangedCommand
        {
            get => userSelectionChangedCommand ?? (userSelectionChangedCommand = new CommandDelegate(UserSelectionChangedHandler));
        }

        private CommandDelegate editCommand;
        public ICommand EditCommand
        {
            get => editCommand ?? (editCommand = new CommandDelegate(Edit, parameter => SelectedUser != null));
        }

        private CommandDelegate saveEditCommand;
        public ICommand SaveEditCommand
        {
            get => saveEditCommand ?? (saveEditCommand = new CommandDelegate(SaveEdit));
        }

        private CommandDelegate closeModalCommand;
        public ICommand CloseModalCommand
        {
            get => closeModalCommand ?? (closeModalCommand = new CommandDelegate(parameter => IsModalVisible = false));
        }

        public AdminViewModel(MainViewModel _mainViewModel)
        {
            MainViewModel = _mainViewModel;
            UserRoles = new ObservableCollection<UserRole>(Enum.GetValues(typeof(UserRoleEnum)).Cast<UserRoleEnum>().Select(r => (UserRole)r));
            UserStatuses = new ObservableCollection<UserStatus>(Enum.GetValues(typeof(UserStatusEnum)).Cast<UserStatusEnum>().Select(s => (UserStatus)s));
        }

        private void UserSelectionChangedHandler(object obj)
        {
            IEnumerable<Message> userMessages = SelectedUser.SentMessages.Union(SelectedUser.ReceivedMessages).OrderByDescending(m => m.Date);
            IEnumerable<TradeOffer> userTradeOffers = SelectedUser.SentTradeOffers.Union(SelectedUser.ReceivedTradeOffers).OrderByDescending(t => t.Date);
            IEnumerable<Character> userCharacters = SelectedUser.Characters;
            MessageSet = new ObservableCollection<Message>(userMessages);
            TradeOfferSet = new ObservableCollection<TradeOffer>(userTradeOffers);
            CharacterSet = new ObservableCollection<Character>(userCharacters);
        }

        private void Edit(object parameter)
        {
            SelectedUserRole = SelectedUser.Role;
            SelectedUserStatus = SelectedUser.Status;
            IsModalVisible = true;
        }

        private void SaveEdit(object parameter)
        {
            SelectedUser.Role = SelectedUserRole;
            SelectedUser.Status = SelectedUserStatus;
            MainViewModel.DbContext.Entry(SelectedUser).State = System.Data.Entity.EntityState.Modified;
            IsModalVisible = false;
        }

        private async Task LoadUsersAsync()
        {
            while (Timer != null)
            {
                UserSet = new ObservableCollection<User>(MainViewModel.DbContext.Users);
                await Task.Delay(TimeSpan.FromSeconds(4));
            }
        }

        public void Load()
        {
            Timer = Task.Factory.StartNew(LoadUsersAsync);
        }
    }
}
