using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using GameWPF.Model;
using GameWPF.Model.Enum;

namespace GameWPF.ViewModel
{
    public class TradingViewModel : BindableBase, INotifyLoad
    {
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

        public ObservableCollection<User> UserSet { get; private set; }

        private User selectedUser;
        public User SelectedUser
        {
            get => selectedUser;
            set => SetProperty(ref selectedUser, value);
        }

        public ObservableCollection<ContainerItem> UserCharacterInventory { get; private set; }

        private ContainerItem selectedUserItem;
        public ContainerItem SelectedUserItem
        {
            get => selectedUserItem;
            set => SetProperty(ref selectedUserItem, value);
        }

        public ObservableCollection<ContainerItem> SelectedUserCharacterInventory { get; private set; }

        private ContainerItem selectedRecipientItem;
        public ContainerItem SelectedRecipientItem
        {
            get => selectedRecipientItem;
            set => SetProperty(ref selectedRecipientItem, value);
        }

        private ContainerItem userTradeOfferItem;
        public ContainerItem UserTradeOfferItem
        {
            get => userTradeOfferItem;
            set => SetProperty(ref userTradeOfferItem, value);
        }

        private int maxUserItemQuantity;
        public int MaxUserItemQuantity
        {
            get => maxUserItemQuantity;
            set => SetProperty(ref maxUserItemQuantity, value);
        }

        private ContainerItem recipientTradeOfferItem;
        public ContainerItem RecipientTradeOfferItem
        {
            get => recipientTradeOfferItem;
            set => SetProperty(ref recipientTradeOfferItem, value);
        }

        private int maxRecipientItemQuantity;
        public int MaxRecipientItemQuantity
        {
            get => maxRecipientItemQuantity;
            set => SetProperty(ref maxRecipientItemQuantity, value);
        }

        public ObservableCollection<ContainerItem> UserTOContainer { get; private set; }
        public ObservableCollection<ContainerItem> RecipientTOContainer { get; private set; }

        private string tradeOfferComment;
        public string TradeOfferComment
        {
            get => tradeOfferComment;
            set => SetProperty(ref tradeOfferComment, value);
        }

        private Task Timer { get; set; }

        public ObservableCollection<TradeOffer> tradeOfferSet;
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

        private bool isTradeOfferSetEmpty;
        public bool IsTradeOfferSetEmpty
        {
            get => isTradeOfferSetEmpty;
            set => SetProperty(ref isTradeOfferSetEmpty, value);
        }

        private bool isTradeOfferInvalid;
        public bool IsTradeOfferInvalid
        {
            get => isTradeOfferInvalid;
            set => SetProperty(ref isTradeOfferInvalid, value);
        }

        public ICommand SetVMCommand
        {
            get => MainViewModel.SetVMCommand;
        }

        private CommandDelegate createTradeOfferCommand;
        public ICommand CreateTradeOfferCommand
        {
            get => createTradeOfferCommand ?? (createTradeOfferCommand = new CommandDelegate(CreateTradeOffer));
        }

        private CommandDelegate userSelectionChangedCommand;
        public ICommand UserSelectionChangedCommand
        {
            get => userSelectionChangedCommand ?? (userSelectionChangedCommand = new CommandDelegate(UserSelectionChangedHandler));
        }

        private CommandDelegate<ContainerItem> userItemSelectionChangedCommand;
        public ICommand UserItemSelectionChangedCommand
        {
            get => userItemSelectionChangedCommand ?? (userItemSelectionChangedCommand
                = new CommandDelegate<ContainerItem>(UserItemSelectionChangedHandler));
        }

        private CommandDelegate<ContainerItem> recipientItemSelectionChangedCommand;
        public ICommand RecipientItemSelectionChangedCommand
        {
            get => recipientItemSelectionChangedCommand ?? (recipientItemSelectionChangedCommand
                = new CommandDelegate<ContainerItem>(RecipientItemSelectionChangedHandler));
        }

        private CommandDelegate<ContainerItem> userItemQuantityChangedCommand;
        public ICommand UserItemQuantityChangedCommand
        {
            get => userItemQuantityChangedCommand ?? (userItemQuantityChangedCommand = new CommandDelegate<ContainerItem>(parameter =>
            {
                if (UserTradeOfferItem.Quantity < 1)
                    UserTOContainer.Remove(UserTradeOfferItem);
            }));
        }

        private CommandDelegate<ContainerItem> recipientItemQuantityChangedCommand;
        public ICommand RecipientItemQuantityChangedCommand
        {
            get => recipientItemQuantityChangedCommand ?? (recipientItemQuantityChangedCommand = new CommandDelegate<ContainerItem>(parameter =>
            {
                if (RecipientTradeOfferItem.Quantity < 1)
                    RecipientTOContainer.Remove(RecipientTradeOfferItem);
            }));
        }

        private CommandDelegate addUserTOItemCommand;
        public ICommand AddUserTOItemCommand
        {
            get => addUserTOItemCommand ?? (addUserTOItemCommand = new CommandDelegate(
                parameter => UserTOContainer.Add(UserTradeOfferItem),
                parameter => !UserTOContainer.Contains(UserTradeOfferItem) && MaxUserItemQuantity != 0));
        }

        private CommandDelegate addRecipientTOItemCommand;
        public ICommand AddRecipientTOItemCommand
        {
            get => addRecipientTOItemCommand ?? (addRecipientTOItemCommand = new CommandDelegate(
                parameter => RecipientTOContainer.Add(RecipientTradeOfferItem),
                parameter => !RecipientTOContainer.Contains(RecipientTradeOfferItem) && maxRecipientItemQuantity != 0));
        }

        private CommandDelegate sendTradeOfferCommand;
        public ICommand SendTradeOfferCommand
        {
            get => sendTradeOfferCommand ?? (sendTradeOfferCommand = new CommandDelegate(SendTradeOffer,
                parameter => UserTOContainer.Count + RecipientTOContainer.Count > 0));
        }

        private CommandDelegate closeModalCommand;
        public ICommand CloseModalCommand
        {
            get => closeModalCommand ?? (closeModalCommand = new CommandDelegate(parameter => IsModalVisible = false));
        }

        private CommandDelegate acceptTradeOfferCommand;
        public ICommand AcceptTradeOfferCommand
        {
            get => acceptTradeOfferCommand ?? (acceptTradeOfferCommand = new CommandDelegate(AcceptTradeOffer));
        }

        private CommandDelegate closeWarningCommand;
        public ICommand CloseWarningCommand
        {
            get => closeWarningCommand ?? (closeWarningCommand = new CommandDelegate(parameter => IsTradeOfferInvalid = false));
        }

        public TradingViewModel(MainViewModel _mainViewModel)
        {
            MainViewModel = _mainViewModel;
            UserCharacterInventory = new ObservableCollection<ContainerItem>();
            SelectedUserCharacterInventory = new ObservableCollection<ContainerItem>();
            UserTOContainer = new ObservableCollection<ContainerItem>();
            RecipientTOContainer = new ObservableCollection<ContainerItem>();
            TradeOfferSet = new ObservableCollection<TradeOffer>();
            TradeOfferComment = string.Empty;
        }

        private void UserSelectionChangedHandler(object parameter)
        {
            RecipientTOContainer.Clear();
            if (SelectedUser.ActiveCharacter != null)
                foreach (ContainerItem containerItem in SelectedUser.ActiveCharacter.Inventory.ContainerItems)
                {
                    UserCharacterInventory.Add(containerItem);
                }
        }

        private void CreateTradeOffer(object parameter)
        {
            UserCharacterInventory.Clear();
            if (User.ActiveCharacter != null)
                foreach (ContainerItem containerItem in User.ActiveCharacter.Inventory.ContainerItems)
                {
                    UserCharacterInventory.Add(containerItem);
                }
            IsModalVisible = true;
        }

        private void UserItemSelectionChangedHandler(object parameter)
        {
            ContainerItem tradeOfferItem = UserTOContainer.Where(c => c == SelectedUserItem).FirstOrDefault();
            if (tradeOfferItem == null)
            {
                tradeOfferItem = new ContainerItem();
                tradeOfferItem.Quantity = 1;
                tradeOfferItem.Item = SelectedUserItem.Item;
            }
            MaxUserItemQuantity = User.ActiveCharacter.Inventory.ContainerItems.Where(c => c.Item == tradeOfferItem.Item).First().Quantity;
            if (IsItemEquipped(tradeOfferItem.Item, User.ActiveCharacter))
                MaxUserItemQuantity -= 1;
            if (MaxUserItemQuantity == 0)
                tradeOfferItem.Quantity = 0;
            UserTradeOfferItem = tradeOfferItem;
        }

        private void RecipientItemSelectionChangedHandler(object parameter)
        {
            ContainerItem tradeOfferItem = RecipientTOContainer.Where(c => c == SelectedRecipientItem).FirstOrDefault();
            if (tradeOfferItem == null)
            {
                tradeOfferItem = new ContainerItem();
                tradeOfferItem.Quantity = 1;
                tradeOfferItem.Item = SelectedRecipientItem.Item;
            }
            MaxRecipientItemQuantity = SelectedUser.ActiveCharacter.Inventory.ContainerItems.Where(c => c.Item == tradeOfferItem.Item).First().Quantity;
            if (IsItemEquipped(tradeOfferItem.Item, SelectedUser.ActiveCharacter))
                MaxRecipientItemQuantity -= 1;
            if (MaxRecipientItemQuantity == 0)
                tradeOfferItem.Quantity = 0;
            RecipientTradeOfferItem = tradeOfferItem;
        }

        private bool IsItemEquipped(Item item, Character character)
        {
            bool IsItemEquipped = false;
            switch ((ItemTypeEnum)item.ItemType)
            {
                case ItemTypeEnum.Weapon: IsItemEquipped = character.Weapon != null && character.Weapon.Item == item; break;
                case ItemTypeEnum.Util: IsItemEquipped = character.Util != null && character.Util.Item == item; break;
                case ItemTypeEnum.Head: IsItemEquipped = character.Head != null && character.Head.Item == item; break;
                case ItemTypeEnum.Armor: IsItemEquipped = character.Armor != null && character.Armor.Item == item; break;
            }
            return IsItemEquipped;
        }

        private void SendTradeOffer(object parameter)
        {
            for (int i = 0; i < UserTOContainer.Count; i++)
            {
                if (RecipientTOContainer[i].Quantity == 0)
                    RecipientTOContainer.Remove(RecipientTOContainer[i]);
            }
            for (int i = 0; i < RecipientTOContainer.Count; i++)
            {
                if (RecipientTOContainer[i].Quantity == 0)
                    RecipientTOContainer.Remove(RecipientTOContainer[i]);
            }
            if (UserTOContainer.Count + RecipientTOContainer.Count > 0)
            {
                var transaction = MainViewModel.DbContext.Database.BeginTransaction();
                try
                {
                    TradeOffer tradeOffer = new TradeOffer();
                    tradeOffer.Id = Guid.NewGuid();
                    tradeOffer.Comment = TradeOfferComment;
                    tradeOffer.IsActive = true;
                    tradeOffer.Date = DateTime.Now;
                    tradeOffer.Sender = MainViewModel.User;
                    tradeOffer.Receiver = SelectedUser;

                    Container senderContainer = new Container();
                    senderContainer.Id = Guid.NewGuid();
                    MainViewModel.DbContext.Containers.Add(senderContainer);
                    MainViewModel.DbContext.SaveChanges();
                    foreach (ContainerItem containerItem in UserTOContainer)
                    {
                        containerItem.Id = Guid.NewGuid();
                        containerItem.Container = senderContainer;
                        MainViewModel.DbContext.ContainerItems.Add(containerItem);
                    }
                    MainViewModel.DbContext.SaveChanges();

                    Container receiverContainer = new Container();
                    receiverContainer.Id = Guid.NewGuid();
                    MainViewModel.DbContext.Containers.Add(receiverContainer);
                    MainViewModel.DbContext.SaveChanges();
                    foreach (ContainerItem containerItem in RecipientTOContainer)
                    {
                        containerItem.Id = Guid.NewGuid();
                        containerItem.Container = receiverContainer;
                        MainViewModel.DbContext.ContainerItems.Add(containerItem);
                    }
                    MainViewModel.DbContext.SaveChanges();

                    tradeOffer.SenderContainer = senderContainer;
                    tradeOffer.ReceiverContainer = receiverContainer;
                    MainViewModel.DbContext.TradeOffers.Add(tradeOffer);
                    MainViewModel.DbContext.SaveChanges();
                    UserTOContainer.Clear();
                    RecipientTOContainer.Clear();
                    TradeOfferComment = string.Empty;

                    transaction.Commit();
                    TradeOfferSet.Prepend(tradeOffer);
                    IsTradeOfferSetEmpty = TradeOfferSet.Count == 0;
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    throw new Exception("Trade offer was not sent", exception);
                }
                IsModalVisible = false;
            }
        }

        private void AcceptTradeOffer(object parameter)
        {
            SelectedTradeOffer.IsActive = false;
            MainViewModel.DbContext.SaveChanges();
            RaisePropertyChanged("SelectedTradeOffer");
            var transaction = MainViewModel.DbContext.Database.BeginTransaction();
            try
            {
                if (SelectedTradeOffer.Sender.ActiveCharacter == null || SelectedTradeOffer.Receiver.ActiveCharacter == null)
                    throw new Exception("Invalid tradeoffers characters");
                foreach (ContainerItem containerItem in SelectedTradeOffer.SenderContainer.ContainerItems)
                {
                    ContainerItem senderItem = SelectedTradeOffer.Sender.ActiveCharacter.Inventory.ContainerItems.Where(i => i.Item == containerItem.Item).FirstOrDefault();
                    if (senderItem != null && ((!IsItemEquipped(senderItem.Item, SelectedTradeOffer.Sender.ActiveCharacter) && containerItem.Quantity >= senderItem.Quantity)
                        || containerItem.Quantity >= senderItem.Quantity - 1))
                    {
                        senderItem.Quantity = senderItem.Quantity - containerItem.Quantity;
                        if (senderItem.Quantity < 1)
                            MainViewModel.DbContext.ContainerItems.Remove(senderItem);
                        ContainerItem receiverItem = SelectedTradeOffer.Receiver.ActiveCharacter.Inventory.ContainerItems.Where(i => i.Item == containerItem.Item).FirstOrDefault();
                        if (receiverItem != null)
                            receiverItem.Quantity = receiverItem.Quantity + containerItem.Quantity;
                        else
                        {
                            receiverItem = new ContainerItem();
                            receiverItem.Id = Guid.NewGuid();
                            receiverItem.Quantity = containerItem.Quantity;
                            receiverItem.Item = containerItem.Item;
                            receiverItem.Container = SelectedTradeOffer.Receiver.ActiveCharacter.Inventory;
                            MainViewModel.DbContext.ContainerItems.Add(receiverItem);
                        }
                        MainViewModel.DbContext.SaveChanges();
                    }
                    else
                        throw new Exception("Invalid sender items");
                }
                foreach (ContainerItem containerItem in SelectedTradeOffer.ReceiverContainer.ContainerItems)
                {
                    ContainerItem receiverItem = SelectedTradeOffer.Receiver.ActiveCharacter.Inventory.ContainerItems.Where(i => i.Item == containerItem.Item).FirstOrDefault();
                    if (receiverItem != null && ((!IsItemEquipped(receiverItem.Item, SelectedTradeOffer.Receiver.ActiveCharacter) && containerItem.Quantity >= receiverItem.Quantity)
                        || containerItem.Quantity >= receiverItem.Quantity - 1))
                    {
                        receiverItem.Quantity = receiverItem.Quantity - containerItem.Quantity;
                        if (receiverItem.Quantity < 1)
                            MainViewModel.DbContext.ContainerItems.Remove(receiverItem);
                        ContainerItem senderItem = SelectedTradeOffer.Sender.ActiveCharacter.Inventory.ContainerItems.Where(i => i.Item == containerItem.Item).FirstOrDefault();
                        if (senderItem != null)
                            senderItem.Quantity = senderItem.Quantity + containerItem.Quantity;
                        else
                        {
                            senderItem = new ContainerItem();
                            senderItem.Id = Guid.NewGuid();
                            senderItem.Quantity = containerItem.Quantity;
                            senderItem.Item = containerItem.Item;
                            senderItem.Container = SelectedTradeOffer.Sender.ActiveCharacter.Inventory;
                            MainViewModel.DbContext.ContainerItems.Add(senderItem);
                        }
                        MainViewModel.DbContext.SaveChanges();
                    }
                    else
                        throw new Exception("Invalid receiver items");
                }
                transaction.Commit();

                SelectedTradeOffer = null;
                Task.Factory.StartNew(async () => await MainViewModel.DbContext.Entry(User.ActiveCharacter.Inventory).Collection(i => i.ContainerItems).LoadAsync());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                IsTradeOfferInvalid = exception != null;
                transaction.Rollback();
            }
        }

        private async Task LoadTradeOffersAsync()
        {
            while (Timer != null)
            {
                MainViewModel.DbContext.Entry(User).Collection(u => u.SentTradeOffers).Load();
                MainViewModel.DbContext.Entry(User).Collection(u => u.ReceivedTradeOffers).Load();
                IEnumerable<TradeOffer> userTradeOffers = User.SentTradeOffers.Union(MainViewModel.User.ReceivedTradeOffers).OrderByDescending(t => t.Date);
                if (TradeOfferSet.Count != userTradeOffers.Count())
                {
                    TradeOfferSet = new ObservableCollection<TradeOffer>(userTradeOffers);
                }
                IsTradeOfferSetEmpty = TradeOfferSet.Count == 0;
                await Task.Delay(TimeSpan.FromSeconds(4));
            }
        }

        public void Load()
        {
            UserSet = new ObservableCollection<User>(MainViewModel.DbContext.Users.ToList().Where(u => u != User));
            Timer = Timer ?? Task.Factory.StartNew(LoadTradeOffersAsync);
        }
    }
}