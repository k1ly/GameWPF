using System;
using System.Windows;

namespace GameWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.Visibility = Visibility.Collapsed;
                this.Topmost = true;
                this.ResizeMode = ResizeMode.NoResize;
                this.Visibility = Visibility.Visible;
            }
            else
            {
                this.Topmost = false;
                this.ResizeMode = ResizeMode.CanResize;
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            this.Topmost = this.WindowState == WindowState.Maximized;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Topmost = false;
        }
    }
}
