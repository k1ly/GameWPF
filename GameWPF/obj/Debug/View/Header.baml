   M S B A M L   `   `   `  ����B  >GameWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:GameWPF.ViewGameWPF.View  5      5clr-namespace:GameWPF.ViewModelGameWPF.ViewModel  5          GameWPF.View.Header   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:GameWPF.View     5      .vmclr-namespace:GameWPF.ViewModel     5        � 	Resources  5
       �� 
TargetType   RoundButton �� BasedOn��%0   '��       7   �� 5   
   " ��6   8   6$   5      .��5      �   HeaderBackground#���  6    � ColumnDefinitions 5   
   �� 5      $��75 �6   �� 5      �� 5      $��75 �6   �� 5      $��75 �6   .+�5   
   �� %   GameWPF.ViewModel.ViewModelEnum   Menu#��Z 62   ��6   �� *SetVMCommand+��5      �� 5       �� ImageSource$" /Resources/Icon/MenuIcon.png��6   5      �� 5   
   $��2��6      	Messaging#��Z 6B   ��6!   �� *SetVMCommand+��5      �� 5      $" /Resources/Icon/MailIcon.png��6   5      �� 5   
   $��3��6      Trading#��Z 6B   ��6!   �� *SetVMCommand+��5      �� 5       $# /Resources/Icon/TradeIcon.png��6   5"      5#      5$      