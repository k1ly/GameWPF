   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  B >GameWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:GameWPF.ViewGameWPF.View    GameWPF.View.StoreView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4s&clr-namespace:System;assembly=mscorlib     ,localclr-namespace:GameWPF.View     .���    MenuBackground#��[    � RowDefinitions  �� $��75 ��� �� $��50 �.+�  GameWPF.View.Header   � DataContext �� *DataContext+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType$ UserControlL��� $��3�� K� ZIndex$ 10��y��� *IsModalVisible+  boolConverter �� 	Converter# [ ����  �� Color$
 Gray�� �� Opacity$	 0.4Q�._��   �� �� $
��Auto ��� .+��� $��1������ 	 �� 
TargetType"	 ��.��� 
 �� Property
 � �� �� Value$	 300���� 
 � ��$ 5q��� 
 � ��
 �   ��� 
 � ��
 �����._��   �� $
��Auto ��� .+�� $��2��.+��� $��Center=�$��15q����� "	 �� �� Triggers ��  �� Value True �� Binding `�   equalityConverter `� 	Converter# [ .1���  �� Path$ User.ActiveCharacter���  �� Source Z� .a��� 
 � y�$ 	Collapsed=�.+���   DefaultTitle#��[ $��Center=�  m_Store_PurchaseItem#���  �  � ColumnDefinitions �� �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��Center=����� *SelectedItem.Name+�� $��1��$
��Left=� � Minimum$ 0Q� � SmallChange$ 1Q�$��50�� � Value �� *ItemQuantity+ � Maximum �� *MaxItemQuantity+����  �� 
TargetType" ��.R�� #��z�� �� �� .+��� $��Center=�$��20(�$��20������ *Value+ �� *TemplatedParent+� $��1��#��� r�#��z��  �� $��18���� $
��Auto ��� $��18��.+���  �� LineDownCommand#��Z |�.  .��F� #��z��$��1��
�������"����     	= � z  	= � 	=     	�� $��2�� �� LineUpCommand#��Z |�.  .��F� #��z��$��1��
�������"����        � 	=  	= � z     	�� $��2��$��10 0 0 0q�.��`�   mathConverter# [  `� ConverterParameter$ *��.1��� $ SelectedItem.Price��� $ ItemQuantity��� $��3��$��20��$"��/Resources/Icon/GoldIcon.png����   DefaultButton#��[ $��0 15 0 0q�$	��100��  m_Store_Purchase#���  ���� *ConfirmPurchaseCommand+�� $��50q�$��Center=����� "	 ��.��� 
 � y�$ 	Collapsed=� ��  True `� # [ .1��� $ User.ActiveCharacter���  Z� .a��� 
 � y�$ Visible=�.����  �� TextAlignment$ Center=�$
��Wrap=�  m_Store_PurchaseWarning#���  ��  	 CloseModalButton#��[	 ���� *CloseModalCommand+.��X� $��1��$	��7 0q� �� $��3* ��� $��2* � J� InputBindings �� 
 �� Key$ E�� �� Command �� *PurchaseItemCommand+.+���  %� 
ScrollUnit$ Pixel=����� *	ItemSet+  �� SelectedItem  �� *SelectedItem+���� "	 ��.��� 
 � ��$ Stretch=����� .R��� $	��100��
���   �$��1q�._��  �� $
��Auto ��� �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $	��100������ *Image+� $��1��$��Center=�  �� �� $��2* �.+��� ���� *Name+�� $��1������ *Description+�� $��2��$
��10 0q�$��Center=�$��50������ *ItemType.Image+�� $��3��$��Center=����� *Price+�� $��4��$��0 0 10 0q�$��20��$"��/Resources/Icon/GoldIcon.png��� $��1��! � 	Resources!  
 DefaultLabel" �� BasedOn��%   '��       7 
 �� "	 ��8"   ���� "	 �.��� 
 � ��  SemiTransparentBackground# [  ��  True `� # [ .1��� $ SelectedItem���  Z� .a��� 
 � y�$ 	Collapsed=�  �� $
��Auto ��� $��50 ��� .+�� $��20q�$	��200��  �� $
��Auto ��� .+��� # �� CornerRadius$# 10��  ImageBgBrush#���  ._��� ���� *SelectedItem.Image+� $��1��$��Center=�  �� ��  �� $
��Auto ��� $
��Auto �.+���   m_Store_ItemClass#���  �� $��1������ *SelectedItem.ItemClass.Name+�� $��1��$��30����
� .��� $ � Color$$ White��% � Offset$	% 0.0Q�� $$ Transparent��$	% 1.0Q�.���� ���� *SelectedItem.ItemType.Image+�� $��1��$��1������ *SelectedItem.Stat+�� $��1��$��Center=����� *SelectedItem.Name+�� $��2��$��15q�.���� $
��Wrap=�$ Center=����� *SelectedItem.Description+� $��2�� �� �� $
��Auto ��� $
��Auto �.+��� #��[
 $
��20 0q�$��Center=�  m_Store_CommandTip#���  �� $��1��#��[
 $��Center=����� *User.ActiveCharacter.Gold+�� $��2��$��0 0 15 0q����� "	 ��#" [
 .��� 
 � ��$ Center=��� 
 � �� �� $��20��$"��/Resources/Icon/GoldIcon.png�� ��  True `� # [ .1��� $ User.ActiveCharacter���  Z� .a��� 
 � ��  m_Store_Warning# �  