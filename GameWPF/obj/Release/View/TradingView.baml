   M S B A M L   `   `   `  ���� B  >GameWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:GameWPF.ViewGameWPF.View      GameWPF.View.TradingView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  &localclr-namespace:GameWPF.View  .���    MenuBackground#��[    � RowDefinitions  �� $��75 ��� $
��Auto ��� .+�   GameWPF.View.Header   � DataContext �� *DataContext+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType$ UserControlL��� $��3�� K� ZIndex$ 10��y��� *IsTradeOfferInvalid+  boolConverter �� 	Converter# [ ����  �� Color$
 Gray�� �� Opacity$	 0.4Q�._��   �� �� $
��Auto ��� .+��� $��1������ 	 �� 
TargetType"	 ��.��� 
 �� Property
 � �� �� Value$	 300���� 
 � ��$ 5q��� 
 � �� �	#FF17000B�� 
 � �� �	#FFF0E970 �� Triggers ��  �� Binding �� *IsModalVisible+ �� Value True._��   �� �� $
��Auto �.+��� $��20q�.���� $
��Wrap=� �� TextAlignment$ Center=�  m_Trading_Warning#���  �� $��1��  DefaultButton#��[ $��0 0 0 15q�$	��100������ *CloseWarningCommand+.��OK�� $��3��$ 10��y��� *IsModalVisible+# [ ���� $
 Gray��$	 0.4Q�._��   �� �� $
��Auto ��� .+��� $��1������ "	 ��.��� 
 � ��$	 750���� 
 � ��$ 5q��� 
 � ��
 �   ��� 
 � ��
 ����� ��  �� *IsModalVisible+ True._��   �� $
��Auto ���  � ColumnDefinitions �� �� .+��� $��2��$��Center=�$��Center=�$	��300��$ Center=�$
��Wrap=�  m_Trading_Character#���  �� $��2������ "	 �� ��  True `�   equalityConverter `� 	Converter# [ .1���  �� Path$ SelectedUser���  �� Source Z� .a��� 
 � y�$ Hidden=���  True `� # [ .1��� $ User.ActiveCharacter���  Z� .a��� 
 � y�$ Hidden=���  True `� # [ .1��� $" SelectedUser.ActiveCharacter���  Z� .a��� 
 � y�$ Hidden=�.@���   m_Trading_YourInventory#���  .���   �� �� $
��Auto �.+��� $��Disabled=����� */-User.ActiveCharacter.Inventory.ContainerItems+ �� SelectedItem �� *SelectedUserItem+���� .R�A�  K� IsItemsHost .  A� Columns$ 4�� A� Rows$ 4��$��Stretch=����� "	 ��.��� 
 � ��$ Stretch=����� .R�� .+��� ���� *
Item.Image+�� $��Right=�$��Bottom=�
��� ������� *
Quantity+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ SelectionChanged��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command �� *!UserItemSelectionChangedCommand+�� $��1������ "	 �� ��  True `� # [ .1��� $ UserTradeOfferItem���  Z� .a��� 
 � y�$ 	Collapsed=�.+��� $��0 10 0 0q�$��Center=����� *UserTradeOfferItem.Item.Name+�� $��0 10 0 0q� � Minimum$ 0Q� � SmallChange$ 1Q�$	��100�� � Value �� *UserTradeOfferItem.Quantity+ � Maximum �� *MaxUserItemQuantity+����   �� 
TargetType"  ��.R�� #��z�� �� �� $��20 ��� $��20 ��� .+��� $��Center=�$��20(�$��20��.��`�   mathConverter# [ ! `� ConverterParameter$! -��.1��� $ Maximum� �� *TemplatedParent+�� $ Value� �� *TemplatedParent+�� $��1��" �� LineUpCommand#��Z" |�.  .��F� #��z��$��1��
�������"���� 	=     � 	=  z �     	= 	�� $��2��# �� LineDownCommand#��Z# |�.  .��F� #��z��$��1��
�������"����        � 	=  	= �     z 	�� $��3��$��Center=�$��20(�$��20������ *Value+ �� *TemplatedParent+   $ ValueChanged��.    �� * UserItemQuantityChangedCommand+�� #��[ $��10q�$	��150��  m_Trading_AddItem#���  ���� *AddUserTOItemCommand+��  	 m_Trading_RecInventory#��� 	 .���   �� �� $
��Auto �.+��� $��Disabled=����� *75SelectedUser.ActiveCharacter.Inventory.ContainerItems+ �� *SelectedRecipientItem+���� .R�A�  . $ 4��$ 4��$��Stretch=����� "	 ��.��� 
 � ��$ Stretch=����� .R�� .+��� ���� *
Item.Image+�� $��Right=�$��Bottom=�
��� ������� *
Quantity+   $ SelectionChanged��.    �� *&$RecipientItemSelectionChangedCommand+�� $��1������ "	 �� ��  True `� # [ .1��� $ RecipientTradeOfferItem���  Z� .a��� 
 � y�$ 	Collapsed=�.+��� $��0 10 0 0q�$��Center=����� *#!RecipientTradeOfferItem.Item.Name+�� $��0 10 0 0q�$ 0Q�$ 1Q�$	��100�� �� *" RecipientTradeOfferItem.Quantity+ �� *MaxRecipientItemQuantity+���� "  ��.R�� #��z�� �� �� $��20 ��� $��20 ��� .+��� $��Center=�$��20(�$��20��.��`� # [ $! -��.1��� $ Maximum� �� *TemplatedParent+�� $ Value� �� *TemplatedParent+�� $��1��#��Z" |�.  .��F� #��z��$��1��
�������"���� 	=     � 	=  z �     	= 	�� $��2��#��Z# |�.  .��F� #��z��$��1��
�������"����        � 	=  	= �     z 	�� $��3��$��Center=�$��20(�$��20������ *Value+ �� *TemplatedParent+   $ ValueChanged��.    �� *%#RecipientItemQuantityChangedCommand+�� #��[ $��10q�$	��150��#���  ���� *AddRecipientTOItemCommand+� $��2��$��1��  �� $
��Auto ��� �� $
��Auto �.+���  
 DefaultTitle#��[
 $��Center=�  m_Trading_TradeOffer#���  �� $��1��  DefaultComboBox#��[ $��25��$
��30 0q����� *	UserSet+ �� *SelectedUser+���� .R��� ���� *Name+   $ SelectionChanged��.    �� *UserSelectionChangedCommand+� $��2��$��0 10 0 0q�$	��300��
�������  �� �� $��75 �.+��   �� $
��Auto ��� �� $
��Auto ��� .+��� 
$ � Tag#$ �  .��`� % �� StringFormat$% 	{0} ({1})��.1��� $	 Tag� �� $
 Self=��� $ User.ActiveCharacter.Name��� $��1��$��5q����	#FF393939$��Disabled=����� *UserTOContainer+ �� *SelectedUserItem+���� .R�A�  . $ 4��$ 2��$��Stretch=����� "	 ��.��� 
 � ��$ Stretch=����� .R�� .+��� ���� *
Item.Image+�� $��Right=�$��Bottom=�
��� ������� *
Quantity+   $ SelectionChanged��.    �� *!UserItemSelectionChangedCommand+��   m_Trading_Inventory#$ �  $��2��.��`� $% {0} {1} ({2})��.1��� $	 Tag� �� $
 Self=��� $ SelectedUser.Name��� $' !SelectedUser.ActiveCharacter.Name��� $��3��$��5q����	#FF393939$��Disabled=����� *RecipientTOContainer+ �� *SelectedRecipientItem+���� .R�A�  . $ 4��$ 2��$��Stretch=����� "	 ��.��� 
 � ��$ Stretch=����� .R�� .+��� ���� *
Item.Image+�� $��Right=�$��Bottom=�
��� ������� *
Quantity+   $ SelectionChanged��.    �� *&$RecipientItemSelectionChangedCommand+� $��2��$��75�� �� �� $��75 �.+��� & �� TextWrapping$
& Wrap=����� *TradeOfferComment+�� $��1��  RoundButton#��[ ���� *SendTradeOfferCommand+���� ' �� ImageSource$"' /Resources/Icon/SendIcon.png���� $��2��  CloseModalButton#��[ ���� *CloseModalCommand+.��X� $��1�� �� �� $
��Auto �.+��� $��1��  PrettyButton#��[ $��15 10q�$	��5 0q�  m_Trading_Create#���  ���� *CreateTradeOfferCommand+� $��2��$��7 0 7 7q� �� $	��300 ��� .+��� ���� *TradeOfferSet+ �� *SelectedTradeOffer+���� .R��  �� �� $
��Auto ���   �� �� $
��Auto �.+��� ���� *Sender.Name+�� $��1��.��F� ���	#FF120743F����    ��j �    @]� р�� @]� р�� @f� -1��� р�� ��- р�� ��j 	�� $��2������ *Receiver.Name+�� $��3��$��1��$
��Left=�.���� $��3��$��1��$��10(�( J� Opacity$	( 0.5Q����� *Date+$% {0:dd/MM/yyyy hh:mm}���� $��1��$��Center=�$��Center=�  m_Trading_NoTradeOffers#���  ���� "	 ��.��� 
 � y�$ 	Collapsed=� ��  True `� # [ .1��� $ SelectedTradeOffer���  Z� .a��� 
 � y�$ Visible=�� $��1������ "	 � ��  True `� # [ .1��� $ SelectedTradeOffer���  Z� .a��� 
 � y�$ 	Collapsed=�  �� $��50 ��� �� $
��Auto �.+��� 
�������.���� $
��Wrap=����� *SelectedTradeOffer.Comment+�� $��1��) �� Padding$) 15q�  SemiTransparentBackground#��[ ._��   �� $
��Auto ��� �� $��15 ��� $
��Auto ��� .+���   DefaultLabel#��[ $
��Left=�.����   m_Trading_UserGives#$ �  ���� "	 ��.��� 
 � ��  m_Trading_YouGive# �   �� 	 False `� # [ .1��� $ SelectedTradeOffer.Sender��� $
 User�.a��� 
 � �� `� $% {0} {1}��.1��� $$ SelectedTradeOffer.Sender.Name��� $	 Tag� �� $
 Self=��� $��1�����	#FF393939$��Disabled=����� *31SelectedTradeOffer.SenderContainer.ContainerItems+���� .R�A�  . $ 4��$ 2��$��Stretch=����� "	 ��.��� 
 � ��$ Stretch=����� .R�� .+��� ���� *
Item.Image+�� $��Right=�$��Bottom=�
��� ������� *
Quantity+�� $��3��#��[ $
��Left=�.���� #$ �  ���� "	 ��.��� 
 � ��# �   �� 	 False `� # [ .1��� $! SelectedTradeOffer.Receiver��� $
 User�.a��� 
 � �� `� $% {0} {1}��.1��� $&  SelectedTradeOffer.Receiver.Name��� $	 Tag� �� $
 Self=��� $��4�����	#FF393939$��Disabled=����� *53SelectedTradeOffer.ReceiverContainer.ContainerItems+���� .R�A�  . $ 4��$ 2��$��Stretch=����� "	 ��.��� 
 � ��$ Stretch=����� .R�� .+��� ���� *
Item.Image+�� $��Right=�$��Bottom=�
��� ������� *
Quantity+�� $��2��$��10q�$	��200��  m_Trading_Accept#���  ���� *AcceptTradeOfferCommand+���� "	 ��* �� BasedOn#* [ .��� 
 � y�$ Hidden=� ^� + ^� 
Conditions+ �� , �� Binding, �� *SelectedTradeOffer.IsActive+- �� Value- True�� - True, `� # [ .1��� $! SelectedTradeOffer.Receiver��� $
 User�.0��� 
 � y�$ Visible=�