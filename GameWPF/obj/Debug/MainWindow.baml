   M S B A M L   `   `   `  ����B  >GameWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:GameWPFGameWPF  5   	   +clr-namespace:GameWPF.ViewGameWPF.View  5   	   5clr-namespace:GameWPF.ViewModelGameWPF.ViewModel  5   	       GameWPF.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:GameWPF     5   	   (vclr-namespace:GameWPF.View     5   	   .vmclr-namespace:GameWPF.ViewModel     5   	   -   5          � Title$
  Game��5
   	   $
��1600��6   $	��800��6#    � WindowStartupLocation$ CenterScreen=�60      DefaultFont#��[  5   	    � Icon$ /Resources/Icon/Icon.ico��6.    � WindowState 5   	   �� *WindowState+ � WindowStyle 6-   �� *WindowStyle+ � 	Resources 5       �� DataType)   #GameWPF.ViewModel.RegisterViewModel   GameWPF.View.RegisterView&    GameWPF.ViewModel.LoginViewModel   GameWPF.View.LoginView%   GameWPF.ViewModel.MenuViewModel   GameWPF.View.MenuView)   #GameWPF.ViewModel.SettingsViewModel   GameWPF.View.SettingsView&	    GameWPF.ViewModel.AdminViewModel
   GameWPF.View.AdminView*   $GameWPF.ViewModel.MessagingViewModel    GameWPF.View.MessagingView(   "GameWPF.ViewModel.TradingViewModel   GameWPF.View.TradingView&    GameWPF.ViewModel.StoreViewModel   GameWPF.View.StoreView*   $GameWPF.ViewModel.CharacterViewModel    GameWPF.View.CharacterView%   GameWPF.ViewModel.PlayViewModel   GameWPF.View.PlayView��%�  (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)(�� �     5      *, +)(�� �     5      *,	 +)(�� �     5      *, +)(�� ,    5!      *, +)(�� ^    5$      *, +)(�� �    5'      *, +)(�� �    5*      *, +)�� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   " 	 6   .R�5      
  5      �� 5   
   "  6   .R�5        5       �� 5!   
   "  6   .R�5"        5#      �� 5$   
   "  6   .R�5%        5&      �� 5'   
   "  6   .R�5(        5)      �� 5*   
   "  6   .R�5+        5,      5-       � DataContext 5.      %   GameWPF.ViewModel.MainViewModel  5/   
   .��51      � .+�52   
   �� ��6   �� *CurrentViewModel+53      54      