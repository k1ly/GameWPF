   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  B >GameWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:GameWPF.ViewGameWPF.View    GameWPF.View.RegisterView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  .s&clr-namespace:System;assembly=mscorlib  &localclr-namespace:GameWPF.View  .���    RegisterBackground#���     � RowDefinitions  �� �� $
��Auto ��� .+��� $��1��  	AuthPanel#��[ $	��300��._��   �� $
��Auto ��� .+��� $��Center=�.+��� $��0 10 0 0q�  m_Register_Name#���  ��   PlaceHolder#��[   m_Register_EnterName
 � Tag# �  ���� *
UserName+�� $��0 10 0 0q�  m_Register_Login#���  �� #��[   m_Register_EnterLogin# �  ���� *	UserLogin+.��*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ TextChanged��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command �� *LoginChangedCommand+.��� $��0 10 0 0q� � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� .+���   m_Register_Password#���  �� $��1��  PswValidation#��[  � ToolTip _� .����  � 	Resources 	 �� 
TargetType
 �� Property �� Value��%2   '��       �� "	 ��.��� 
 � ��$ 0 5 0 0q�.+���  	 m_PasswordTip#��� 	 ��  
 m_PasswordTip1#��� 
 ��   m_PasswordTip2#���  ��   m_PasswordTip3#���  ��   m_PasswordTip4#���  .���� $��Bottom=�$%��/Resources/Icon/WarningIcon.png���  �� �� $��40 �.+�G� $��2��  PswBoxStyle#��[    $ PasswordChanged��.    �� *PasswordChangedCommand+   CommandParameter ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType$ PasswordBoxL��� $��2��  PswTextStyle#��[ ���� *UserPassword+�� $��1��  TogglePswVisibility#��[ $
��👁��   $ PreviewMouseDown��.    �� *TogglePasswordCommand+ ��True��  $ PreviewMouseUp��.    �� *TogglePasswordCommand+ ��	False���� $��0 10 0 0q�  m_Register_Repeat#���  � $��0 0 0 10q� �� �� $��30 �.+�G� $��2��#��[    $ PasswordChanged��.    �� *PswrptChangedCommand+ ��  �� $ FindAncestor=�$ PasswordBoxL��� $��2��#��[ ���� *PasswordRepeat+�� $��1������ "	 �� �� Triggers ��  �� Binding �� *PasswordRepeatStatus+ �� Value 1.a��� 
 � ��$	 ✔���� 
 � ��
 ������ 
 � y�$ Visible=���  �� *PasswordRepeatStatus+ -1.a��� 
 � ��$	 ❌���� 
 � ��
 �  ���� 
 � y�$ Visible=���  �� *PasswordRepeatStatus+ 0.a��� 
 � y�$ Hidden=��� $��Center=�
���  ��  m_Register_Warning#���  y��� *IsUserIncorrect+  boolConverter �� 	Converter# [ ��   DefaultButton#��[ $	��200��  m_Register_Register#���  ���� *RegisterCommand+� $��0 10 0 0q� �� �� $
��Auto �.+���   	RefButton#��[   m_Register_LoginRef#���  ���� *SetLoginVMCommand+