   M S B A M L   `   `   `  ���� B  >GameWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:GameWPF.ViewGameWPF.View      GameWPF.View.AdminView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  &localclr-namespace:GameWPF.View  .���    MenuBackground#��[    � RowDefinitions  �� $��75 ��� $
��Auto ��� .+�   GameWPF.View.Header   � DataContext �� *DataContext+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType$ UserControlL��� $��3�� K� ZIndex$ 10��y��� *IsModalVisible+  boolConverter �� 	Converter# [ ����  �� Color$
 Gray�� �� Opacity$	 0.4Q�._��   �� �� $
��Auto ��� .+��� $��1������ 	 �� 
TargetType"	 ��.��� 
 �� Property
 � �� �� Value$	 250���� 
 � ��$ 5q��� 
 � ��
 �   ��� 
 � ��
 �����._��   �� $
��Auto ��� .+��� $��2��$��15q�.+���   DefaultTitle#��[ $��Center=�  m_Admin_Editing#���  �� $��0 15 0 0q����� *SelectedUser.Name+�� $��0 15 0 0q�  m_Admin_UserRole#���  ��   DefaultComboBox#��[ ���� *	UserRoles+ �� SelectedItem �� *SelectedUserRole+���� .R��� ���� *Name+�� $��0 15 0 0q�  m_Admin_UserStatus#���  �� #��[ ���� *UserStatuses+ �� *SelectedUserStatus+���� .R��� ���� *Name+��   DefaultButton#��[ $��0 15 0 0q�$	��100��  
m_Admin_Ok#���  ���� *SaveEditCommand+��  	 CloseModalButton#��[	 ���� *CloseModalCommand+.��X�� $��1��#��[ $��10q�$��Center=�$	��100�� 
 m_Admin_Edit#��� 
 ���� *EditCommand+����  �� $ UserControlL�$ FindAncestor=�� $��2��$��
10 0 10 10q�  �� �� $
��Auto ��� ���� "	 �� �� Triggers ��  �� Value True �� Binding `�   equalityConverter `� 	Converter# [ .1���  �� Path$ SelectedUser���  �� Source Z� .a��� 
 � ��$
 Auto �.+��� -    ��Name$ UsersListView������ *	UserSet+ �� *SelectedUser+ �� View �� P���   m_Admin_Name#���   �� DisplayMemberBinding �� *Name+��   m_Admin_Login#���   �� *Login+��   m_Admin_Role#���   �� *	Role.Name+��   m_Admin_Status#���   �� *Status.Name+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ SelectionChanged��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command �� *UserSelectionChangedCommand+�� $��1��$��4��$��Stretch=�$��Center=����� "	 �� ��  True `� # [ .1��� $ SelectedUser���  Z� .a��� 
 � y�$ 	Collapsed=��� $��2������ "	 �� ��  True `� # [ .1��� $ SelectedUser���  Z� .a��� 
 � y�$ 	Collapsed=�.@���   m_Admin_Messages#���  .���� ���� *
MessageSet+ �� *SelectedMessage+���� "	 ��.��� 
 � ��$ 5q��� 
 � ��
 ��·� �� P���   m_Admin_Sender#���   �� *Sender.Name+��   m_Admin_Receiver#���   �� *Receiver.Name+��   m_Admin_Date#���   �� *Date+ �� StringFormat$ {0:dd/MM/yyyy HH:mm}����   m_Admin_Text#���   �� CellTemplate �� .R��� ���� "	 ��.��� 
 � y�$ 	Collapsed=� ��  True `� # [ .1��� �� $! DataContext.SelectedMessage� �� $ FindAncestor=�$ UserControlL�.a��� 
 � y�$ Visible=�.���� ���� *Text+��   m_Admin_TradeOffers#���  .���� ���� *TradeOfferSet+ �� *SelectedTradeOffer+���� "	 ��.��� 
 � ��$ 5q��� 
 � ��
 ��·� �� P��� #���   �� *Sender.Name+�� #���   �� *Receiver.Name+�� #���   �� *Date+$ {0:dd/MM/yyyy HH:mm}����   m_Admin_Items#���   �� .R�� ���� "	 �.��� 
 � y�$ 	Collapsed=� ��  True `� # [ .1��� �� $$ DataContext.SelectedTradeOffer� �� $ FindAncestor=�$ UserControlL�.a��� 
 � y�$ Visible=�  �� $
��Auto ���  � ColumnDefinitions �� �� $��15 ��� .+��� $
��Left=�  m_Trading_UserGives
 � Tag# �  ��`� $ {0} {1}��.1��� $ Sender.Name��� $	 Tag� �� $
 Self=��� $��1��$��Disabled=����� * SenderContainer.ContainerItems+���� .R�A�  K� IsItemsHost .  A� Columns$ 4��  A� Rows$  2��$��Stretch=����� "	 ��.��� 
 � ��$ Stretch=����� .R�� .+��� ���� *
Item.Image+�� $��Right=�$��Bottom=�
��� ������� *
Quantity+�� $��2��$
��Left=�# �  ��`� $ {0} {1}��.1��� $ Receiver.Name��� $	 Tag� �� $
 Self=��� $��2��$��1��$��Disabled=����� *" ReceiverContainer.ContainerItems+���� .R�A�  . $ 4��$  2��$��Stretch=����� "	 ��.��� 
 � ��$ Stretch=����� .R�� .+��� ���� *
Item.Image+�� $��Right=�$��Bottom=�
��� ������� *
Quantity+��   m_Admin_Comment#���   �� .R��� $
��Wrap=�! �� TextAlignment$
! Left=����� *	Comment+��   m_Admin_Characters#���  .���� ���� *CharacterSet+ �� *SelectedCharacter+���� "	 ��.��� 
 � ��$ 5q��� 
 � ��
 ��·� �� P��� #���   �� *Name+��   m_Admin_Class#���   �� *
Class.Name+��   m_Admin_Level#���   �� *Level+��   m_Admin_Exp#���   �� *
Experience+��   m_Admin_Gold#���   �� *Gold+��   m_Admin_Health#���   �� *Health+��   m_Admin_Endurance#���   �� *	Endurance+��    m_Admin_Damage#���    �� *Damage+��  ! m_Admin_Weapon#��� !  �� *Weapon.Item.Name+��  " m_Admin_Util#��� "  �� *Util.Item.Name+��  # m_Admin_Head#��� #  �� *Head.Item.Name+��  $ m_Admin_Armor#��� $  �� *Armor.Item.Name+�� #���   �� .R��� ���� "	 ��.��� 
 � y�$ 	Collapsed=� ��  True `� # [ .1��� �� $! DataContext.SelectedMessage� �� $ FindAncestor=�$ UserControlL�.a��� 
 � y�$ Visible=�.���� ���	#FF393939$��Disabled=����� *Inventory.ContainerItems+���� .R�A�  . $ 4��$  2��$��Stretch=����� "	 ��.��� 
 � ��$ Stretch=����� .R�� .+��� ���� *
Item.Image+�� $��Right=�$��Bottom=�
��� ������� *
Quantity+