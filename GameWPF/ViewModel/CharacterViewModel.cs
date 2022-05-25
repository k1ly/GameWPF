using System;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using GameWPF.Model;
using GameWPF.Model.Enum;

namespace GameWPF.ViewModel
{
    public class CharacterViewModel : BindableBase, INotifyLoad
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

        public ObservableCollection<CharacterClass> CharacterClasses { get; private set; }

        private CharacterClass selectedCharacterClass;
        public CharacterClass SelectedCharacterClass
        {
            get => selectedCharacterClass;
            set => SetProperty(ref selectedCharacterClass, value);
        }

        private string characterName;
        public string CharacterName
        {
            get => characterName;
            set => SetProperty(ref characterName, value);
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

        public ObservableCollection<ContainerItem> inventory;
        public ObservableCollection<ContainerItem> Inventory
        {
            get => inventory;
            set => SetProperty(ref inventory, value);
        }

        private int maxExp;
        public int MaxExp
        {
            get => maxExp;
            set => SetProperty(ref maxExp, value);
        }

        public ICommand SetVMCommand
        {
            get => MainViewModel.SetVMCommand;
        }

        private CommandDelegate createCharacterCommand;
        public ICommand CreateCharacterCommand
        {
            get => createCharacterCommand ?? (createCharacterCommand = new CommandDelegate(parameter => IsModalVisible = true));
        }

        private CommandDelegate confirmCreateCommand;
        public ICommand ConfirmCreateCommand
        {
            get => confirmCreateCommand ?? (confirmCreateCommand = new CommandDelegate(ConfirmCreate));
        }

        private CommandDelegate characterSelectionChangedCommand;
        public ICommand CharacterSelectionChangedCommand
        {
            get => characterSelectionChangedCommand ?? (characterSelectionChangedCommand = new CommandDelegate(CharacterSelectionChangedHandler));
        }

        private CommandDelegate chooseCharacterCommand;
        public ICommand ChooseCharacterCommand
        {
            get => chooseCharacterCommand ?? (chooseCharacterCommand = new CommandDelegate(ChooseCharacter));
        }

        private CommandDelegate<ContainerItem> toggleEquipItemCommand;
        public ICommand ToggleEquipItemCommand
        {
            get => toggleEquipItemCommand ?? (toggleEquipItemCommand = new CommandDelegate<ContainerItem>(ToggleEquipItem, containerItem =>
                SelectedCharacter != null & containerItem != null &&
                SelectedCharacter.Level >= containerItem.Item.Level && SelectedCharacter.Class == containerItem.Item.ItemClass));
        }

        private CommandDelegate<ContainerItem> sellItemCommand;
        public ICommand SellItemCommand
        {
            get => sellItemCommand ?? (sellItemCommand = new CommandDelegate<ContainerItem>(SellItem, CanRemoveItem));
        }

        private CommandDelegate<ContainerItem> removeItemCommand;
        public ICommand RemoveItemCommand
        {
            get => removeItemCommand ?? (removeItemCommand = new CommandDelegate<ContainerItem>(RemoveItem, CanRemoveItem));
        }

        private CommandDelegate closeModalCommand;
        public ICommand CloseModalCommand
        {
            get => closeModalCommand ?? (closeModalCommand = new CommandDelegate(parameter => IsModalVisible = false));
        }

        public CharacterViewModel(MainViewModel _mainViewModel)
        {
            MainViewModel = _mainViewModel;
            CharacterClasses = new ObservableCollection<CharacterClass>(Enum.GetValues(typeof(CharacterClassEnum))
                .Cast<CharacterClassEnum>().Select(c => c.GetModel(MainViewModel.DbContext.CharacterClasses)));
            SelectedCharacterClass = CharacterClassEnum.Knight.GetModel(MainViewModel.DbContext.CharacterClasses);
            CharacterName = string.Empty;
            MaxExp = 100;
        }

        private void CharacterSelectionChangedHandler(object parameter)
        {
            Inventory = new ObservableCollection<ContainerItem>(SelectedCharacter.Inventory.ContainerItems);
        }

        private void ConfirmCreate(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(CharacterName))
            {
                var transaction = MainViewModel.DbContext.Database.BeginTransaction();
                try
                {
                    Character character = new Character();
                    character.Id = Guid.NewGuid();
                    character.Name = CharacterName;
                    character.Level = 1;
                    character.Gold = 100;
                    character.Health = SelectedCharacterClass.BaseHealth;
                    character.Endurance = SelectedCharacterClass.BaseEndurance;
                    character.Damage = SelectedCharacterClass.BaseEndurance;
                    character.Class = SelectedCharacterClass;
                    character.User = MainViewModel.User;

                    Container inventory = new Container();
                    inventory.Id = Guid.NewGuid();
                    MainViewModel.DbContext.Containers.Add(inventory);
                    MainViewModel.DbContext.SaveChanges();

                    character.Inventory = inventory;
                    MainViewModel.DbContext.Characters.Add(character);
                    MainViewModel.DbContext.SaveChanges();
                    CharacterName = "";
                    SelectedCharacterClass = CharacterClassEnum.Knight.GetModel(MainViewModel.DbContext.CharacterClasses);
                    transaction.Commit();

                    CharacterSet.Add(character);
                    SelectedCharacter = character;
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    throw new Exception("User character was not created", exception);
                }
                IsModalVisible = false;
            }
        }

        private void ChooseCharacter(object parameter)
        {
            User.ActiveCharacter = SelectedCharacter;
            MainViewModel.DbContext.Entry(User).State = System.Data.Entity.EntityState.Modified;
            MainViewModel.DbContext.SaveChanges();
            RaisePropertyChanged("User");
        }

        private void ToggleEquipItem(ContainerItem containerItem)
        {
            switch ((ItemTypeEnum)containerItem.Item.ItemType)
            {
                case ItemTypeEnum.Weapon: SelectedCharacter.Weapon = SelectedCharacter.Weapon != containerItem ? containerItem : null; break;
                case ItemTypeEnum.Util: SelectedCharacter.Util = SelectedCharacter.Util != containerItem ? containerItem : null; break;
                case ItemTypeEnum.Head: SelectedCharacter.Head = SelectedCharacter.Head != containerItem ? containerItem : null; break;
                case ItemTypeEnum.Armor: SelectedCharacter.Armor = SelectedCharacter.Armor != containerItem ? containerItem : null; break;
            }
            MainViewModel.DbContext.Entry(SelectedCharacter).State = System.Data.Entity.EntityState.Modified;
            MainViewModel.DbContext.SaveChanges();
            RaisePropertyChanged("SelectedCharacter");
        }

        private void SellItem(ContainerItem containerItem)
        {
            var transaction = MainViewModel.DbContext.Database.BeginTransaction();
            try
            {
                SelectedCharacter.Gold += containerItem.Quantity * containerItem.Item.Price;
                MainViewModel.DbContext.Entry(SelectedCharacter).State = System.Data.Entity.EntityState.Modified;
                MainViewModel.DbContext.ContainerItems.Remove(containerItem);
                MainViewModel.DbContext.SaveChanges();
                transaction.Commit();

                MainViewModel.DbContext.Entry(SelectedCharacter.Inventory).Collection(i => i.ContainerItems).Load();
                Inventory.Remove(containerItem);
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                throw new Exception("Inventory item was not sold", exception);
            }
        }

        private void RemoveItem(ContainerItem containerItem)
        {
            MainViewModel.DbContext.ContainerItems.Remove(containerItem);
            MainViewModel.DbContext.SaveChanges();
            MainViewModel.DbContext.Entry(SelectedCharacter.Inventory).Collection(i => i.ContainerItems).Load();
            Inventory.Remove(containerItem);
        }

        private bool CanRemoveItem(ContainerItem containerItem)
        {
            bool canRemoveItem = false;
            if (containerItem != null)
            {
                switch ((ItemTypeEnum)containerItem.Item.ItemType)
                {
                    case ItemTypeEnum.Weapon: canRemoveItem = SelectedCharacter.Weapon != containerItem; break;
                    case ItemTypeEnum.Util: canRemoveItem = SelectedCharacter.Util != containerItem; break;
                    case ItemTypeEnum.Head: canRemoveItem = SelectedCharacter.Head != containerItem; break;
                    case ItemTypeEnum.Armor: canRemoveItem = SelectedCharacter.Armor != containerItem; break;
                }
            }
            return canRemoveItem;
        }

        public void Load()
        {
            CharacterSet = new ObservableCollection<Character>(MainViewModel.User.Characters);
            SelectedCharacter = MainViewModel.User.ActiveCharacter;
            Inventory = SelectedCharacter != null ? new ObservableCollection<ContainerItem>(SelectedCharacter.Inventory.ContainerItems) : new ObservableCollection<ContainerItem>();
        }
    }
}