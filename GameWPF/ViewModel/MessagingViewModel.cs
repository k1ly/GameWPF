using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using GameWPF.Model;

namespace GameWPF.ViewModel
{
    public class MessagingViewModel : BindableBase, INotifyLoad
    {
        private MainViewModel MainViewModel { get; set; }

        public User User
        {
            get => MainViewModel.User;
        }

        public ObservableCollection<User> userSet;
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

        private Task Timer { get; set; }

        public ObservableCollection<Message> messageSet;
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

        private bool isMessageSetEmpty;
        public bool IsMessageSetEmpty
        {
            get => isMessageSetEmpty;
            set => SetProperty(ref isMessageSetEmpty, value);
        }

        private string messageText;
        public string MessageText
        {
            get => messageText;
            set => SetProperty(ref messageText, value);
        }

        public bool scrollToEnd;
        public bool ScrollToEnd
        {
            get => scrollToEnd;
            set => SetProperty(ref scrollToEnd, value);
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

        private CommandDelegate sendMessageCommand;
        public ICommand SendMessageCommand
        {
            get => sendMessageCommand ?? (sendMessageCommand = new CommandDelegate(SendMessage));
        }

        public MessagingViewModel(MainViewModel _mainViewModel)
        {
            MainViewModel = _mainViewModel;
            MessageText = string.Empty;
        }

        private void UserSelectionChangedHandler(object parameter)
        {
            MainViewModel.DbContext.SaveChanges();
            Timer = Task.Factory.StartNew(LoadMessagesAsync);
        }

        private async Task LoadMessagesAsync()
        {
            while (Timer != null)
            {
                MainViewModel.DbContext.Entry(MainViewModel.User).Collection(u => u.SentMessages).Load();
                MainViewModel.DbContext.Entry(MainViewModel.User).Collection(u => u.ReceivedMessages).Load();
                IEnumerable<Message> userMessages = MainViewModel.User.SentMessages.Where(m => m.Receiver == SelectedUser)
                    .Union(MainViewModel.User.ReceivedMessages.Where(m => m.Sender == SelectedUser)).OrderBy(m => m.Date);
                if (MessageSet.Count != userMessages.Count() || (MessageSet.Count > 0 && !MessageSet.Any(m => m.Sender == SelectedUser || m.Receiver == SelectedUser)))
                {
                    MessageSet = new ObservableCollection<Message>(userMessages);
                    if (ScrollToEnd)
                        RaisePropertyChanged("ScrollToEnd");
                    else ScrollToEnd = true;
                    foreach (Message message in MessageSet.Where(m => m.Receiver == MainViewModel.User && m.IsNew))
                    {
                        message.IsNew = false;
                        MainViewModel.DbContext.Entry(message).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                IsMessageSetEmpty = MessageSet.Count == 0;
                IEnumerable<User> users = MainViewModel.DbContext.Users.OrderByDescending(u => u.SentMessages.Count() > 0 || u.ReceivedMessages.Count() > 0)
                    .ThenByDescending(u => u.SentMessages.Count > 0 ? u.SentMessages.FirstOrDefault().Date : u.ReceivedMessages.FirstOrDefault().Date)
                    .ThenByDescending(u => u.SentMessages.FirstOrDefault().IsNew || u.ReceivedMessages.FirstOrDefault().IsNew);
                UserSet = new ObservableCollection<User>(users);
                await Task.Delay(TimeSpan.FromSeconds(4));
            }
        }

        private void SendMessage(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(MessageText))
            {
                Message message = new Message();
                message.Id = Guid.NewGuid();
                message.Text = MessageText;
                message.IsNew = true;
                message.Date = DateTime.Now;
                message.Sender = MainViewModel.User;
                message.Receiver = SelectedUser;
                MainViewModel.DbContext.Messages.Add(message);
                MainViewModel.DbContext.SaveChanges();
                MessageText = string.Empty;
                MessageSet.Add(message);
                IsMessageSetEmpty = MessageSet.Count == 0;
                if (ScrollToEnd)
                    RaisePropertyChanged("ScrollToEnd");
                else ScrollToEnd = true;
                IEnumerable<User> users = MainViewModel.DbContext.Users.OrderByDescending(u => u.SentMessages.Count() > 0 || u.ReceivedMessages.Count() > 0)
                    .ThenByDescending(u => u.SentMessages.Count > 0 ? u.SentMessages.FirstOrDefault().Date : u.ReceivedMessages.FirstOrDefault().Date)
                    .ThenByDescending(u => u.SentMessages.FirstOrDefault().IsNew || u.ReceivedMessages.FirstOrDefault().IsNew);
                UserSet = new ObservableCollection<User>(users);
            }
        }

        public void Load()
        {
            IEnumerable<User> users = MainViewModel.DbContext.Users.OrderByDescending(u => u.SentMessages.Count() > 0 || u.ReceivedMessages.Count() > 0)
                .ThenByDescending(u => u.SentMessages.Count > 0 ? u.SentMessages.FirstOrDefault().Date : u.ReceivedMessages.FirstOrDefault().Date)
                .ThenByDescending(u => u.SentMessages.FirstOrDefault().IsNew || u.ReceivedMessages.FirstOrDefault().IsNew);
            UserSet = new ObservableCollection<User>(users);
            MessageSet = new ObservableCollection<Message>();
        }
    }
}