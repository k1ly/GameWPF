using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Prism.Mvvm;
using GameWPF.Model;
using GameWPF.Model.Enum;
using GameWPF.View;

namespace GameWPF.ViewModel
{
    public class MainViewModel : BindableBase
    {
        private GameDBContainer dbContext;
        public GameDBContainer DbContext
        {
            get => dbContext ?? (dbContext = new GameDBContainer());
        }

        public User User { get; set; }
        public bool IsUserAuthorizing { get; set; }

        private WindowState windowState;
        public WindowState WindowState
        {
            get => windowState;
            set => SetProperty(ref windowState, value);
        }

        private WindowStyle windowStyle;
        public WindowStyle WindowStyle
        {
            get => windowStyle;
            set => SetProperty(ref windowStyle, value);
        }

        private BindableBase currentViewModel;
        public BindableBase CurrentViewModel
        {
            get => currentViewModel;
            set => SetProperty(ref currentViewModel, value);
        }

        private RegisterViewModel RegisterVM { get; set; }
        private LoginViewModel LoginVM { get; set; }
        private MenuViewModel MenuVM { get; set; }
        private SettingsViewModel SettingsVM { get; set; }
        private AdminViewModel AdminVM { get; set; }
        private MessagingViewModel MessagingVM { get; set; }
        private TradingViewModel TradingVM { get; set; }
        private StoreViewModel StoreVM { get; set; }
        private CharacterViewModel CharacterVM { get; set; }
        private PlayViewModel PlayVM { get; set; }

        private CommandDelegate<ViewModelEnum> setVMCommand;
        public ICommand SetVMCommand
        {
            get => setVMCommand ?? (setVMCommand = new CommandDelegate<ViewModelEnum>(SetViewModel));
        }

        public MainViewModel()
        {
            InitializeDatabase();
            SetWindowMode(WindowMode.GetWindowMode(Properties.Settings.Default.DefaultWindowMode));
            User = DbContext.Users.Find(Properties.Settings.Default.UserId);
            IsUserAuthorizing = true;
            SetViewModel(User != null ? ViewModelEnum.Menu : ViewModelEnum.Login);
        }

        public void SetWindowMode(WindowMode windowMode)
        {
            WindowState = windowMode.WindowState;
            WindowStyle = windowMode.WindowStyle;
        }

        public void SetViewModel(ViewModelEnum vm)
        {
            if (User != null && IsUserAuthorizing)
            {
                IsUserAuthorizing = false;
                MenuVM = new MenuViewModel(this);
                SettingsVM = new SettingsViewModel(this);
                AdminVM = new AdminViewModel(this);
                MessagingVM = new MessagingViewModel(this);
                TradingVM = new TradingViewModel(this);
                StoreVM = new StoreViewModel(this);
                CharacterVM = new CharacterViewModel(this);
                PlayVM = new PlayViewModel(this);
            }
            switch (vm)
            {
                case ViewModelEnum.Register:
                    RegisterVM = new RegisterViewModel(this);
                    CurrentViewModel = RegisterVM;
                    break;
                case ViewModelEnum.Login:
                    LoginVM = new LoginViewModel(this);
                    CurrentViewModel = LoginVM;
                    break;
                case ViewModelEnum.Menu: CurrentViewModel = MenuVM; break;
                case ViewModelEnum.Settings: CurrentViewModel = SettingsVM; break;
                case ViewModelEnum.Admin:
                    CurrentViewModel = AdminVM;
                    AdminVM.Load();
                    break;
                case ViewModelEnum.Messaging:
                    CurrentViewModel = MessagingVM;
                    MessagingVM.Load();
                    break;
                case ViewModelEnum.Trading:
                    CurrentViewModel = TradingVM;
                    TradingVM.Load();
                    break;
                case ViewModelEnum.Store: CurrentViewModel = StoreVM; break;
                case ViewModelEnum.Character:
                    CurrentViewModel = CharacterVM;
                    CharacterVM.Load();
                    break;
                case ViewModelEnum.Play:
                    CurrentViewModel = PlayVM;
                    PlayVM.Load();
                    break;
                case ViewModelEnum.Exit: Application.Current.Shutdown(); break;
            }
        }

        private void InitializeDatabase()
        {
            var transaction = DbContext.Database.BeginTransaction();
            try
            {
                if (DbContext.UserRoles.Count() == 0)
                    DbContext.UserRoles.AddRange(Enum.GetValues(typeof(UserRoleEnum)).Cast<UserRoleEnum>().Select(@enum => (UserRole)@enum));
                if (DbContext.UserStatuses.Count() == 0)
                    DbContext.UserStatuses.AddRange(Enum.GetValues(typeof(UserStatusEnum)).Cast<UserStatusEnum>().Select(@enum => (UserStatus)@enum));
                if (DbContext.CharacterClasses.Count() == 0)
                    DbContext.CharacterClasses.AddRange(Enum.GetValues(typeof(CharacterClassEnum)).Cast<CharacterClassEnum>().Select(@enum => (CharacterClass)@enum));
                if (DbContext.ItemTypes.Count() == 0)
                    DbContext.ItemTypes.AddRange(Enum.GetValues(typeof(ItemTypeEnum)).Cast<ItemTypeEnum>().Select(@enum => (ItemType)@enum));
                DbContext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                throw new Exception("Primary database tables were not initialized", exception);
            }
            transaction = DbContext.Database.BeginTransaction();
            try
            {
                if (DbContext.Users.Count() == 0)
                {
                    User admin = new User();
                    admin.Id = Guid.NewGuid();
                    admin.Name = "Admin";
                    admin.Login = "admin";
                    Util.Hash.SaltedHash psw = new Util.Hash.SaltedHash("Admin123");
                    admin.PswHash = psw.Hash;
                    admin.PswSalt = psw.Salt;
                    admin.Role = UserRoleEnum.Admin.GetModel(DbContext.UserRoles);
                    admin.Status = UserStatusEnum.Subscriber.GetModel(DbContext.UserStatuses);
                    DbContext.Users.Add(admin);
                }
                if (DbContext.Items.Count() == 0)
                {
                    Item item1 = new Item();
                    item1.Id = Guid.NewGuid();
                    item1.Name = "Sword";
                    item1.Description = "Simple and ordinary medium sword";
                    item1.Level = 1;
                    item1.Price = 20;
                    item1.Stat = 10;
                    item1.Image = "/Resources/Image/Sword.png";
                    item1.ItemType = ItemTypeEnum.Weapon.GetModel(DbContext.ItemTypes);
                    item1.ItemClass = CharacterClassEnum.Knight.GetModel(DbContext.CharacterClasses);
                    DbContext.Items.Add(item1);

                    Item item2 = new Item();
                    item2.Id = Guid.NewGuid();
                    item2.Name = "Claymore";
                    item2.Description = "Big and powerful two-handed sword";
                    item2.Level = 2;
                    item2.Price = 25;
                    item2.Stat = 15;
                    item2.Image = "/Resources/Image/Claymore.png";
                    item2.ItemType = ItemTypeEnum.Weapon.GetModel(DbContext.ItemTypes);
                    item2.ItemClass = CharacterClassEnum.Knight.GetModel(DbContext.CharacterClasses);
                    DbContext.Items.Add(item2);

                    Item item3 = new Item();
                    item3.Id = Guid.NewGuid();
                    item3.Name = "Katana";
                    item3.Description = "Sword with very high quality and sharp edge";
                    item3.Level = 3;
                    item3.Price = 30;
                    item3.Stat = 20;
                    item3.Image = "/Resources/Image/Katana.png";
                    item3.ItemType = ItemTypeEnum.Weapon.GetModel(DbContext.ItemTypes);
                    item3.ItemClass = CharacterClassEnum.Knight.GetModel(DbContext.CharacterClasses);
                    DbContext.Items.Add(item3);

                    Item item4 = new Item();
                    item4.Id = Guid.NewGuid();
                    item4.Name = "Crystal staff";
                    item4.Description = "Mystical and sparkling crystal staff";
                    item4.Level = 2;
                    item4.Price = 40;
                    item4.Stat = 25;
                    item4.Image = "/Resources/Image/Staff.png";
                    item4.ItemType = ItemTypeEnum.Weapon.GetModel(DbContext.ItemTypes);
                    item4.ItemClass = CharacterClassEnum.Witch.GetModel(DbContext.CharacterClasses);
                    DbContext.Items.Add(item4);
                }
                DbContext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                throw new Exception("Secondary database tables were not initialized", exception);
            }
        }
    }
}
