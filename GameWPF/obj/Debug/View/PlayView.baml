   M S B A M L   `   `   `  ����B  >GameWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:GameWPF.ViewGameWPF.View  5   	       GameWPF.View.PlayView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:GameWPF.View     5   	   .��5	      �    SemiTransparentBackground#���   6     � RowDefinitions  5
   
   �� 5      �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��1��6   $��Center=�6   $��10q�6:     m_Play_Warning#���  6F   ��5      �� 5       �� 
TargetType" ��6     DefaultLabel �� BasedOn# [ 6+   .�5      ��  �� Property � y� �� Value$ 	Collapsed=�63    �� Triggers 5      �� 5       �� Value True6&    �� Binding 5      `� 5   "     equalityConverter `� 	Converter# [ 6/   .1�5   &   �� 	 �� Path$	 User.ActiveCharacter�6.   �� 5   &   
 �� Source
 6.   Z� 5   #   .a�5      ��  � y�$ Visible=�6;   5      5      5!      �� 5"   
   $��2��6     
MenuButton#��[ 6     m_Play_Menu#���  6g   ��6B   �� *SetMenuVMCommand+6g   5#      5$      