DROP DATABASE [GameDB];

CREATE DATABASE [GameDB];

USE [GameDB];

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [Users] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [Login] nvarchar(100)  NOT NULL,
    [PswHash] varbinary(max)  NOT NULL,
    [PswSalt] varbinary(max)  NOT NULL,
    [Role_Id] int  NOT NULL,
    [Status_Id] int  NOT NULL,
    [ActiveCharacter_Id] uniqueidentifier,
	CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

-- Creating table 'UserRoles'
CREATE TABLE [UserRoles] (
    [Id] int  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
	CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);

-- Creating table 'UserStatuses'
CREATE TABLE [UserStatuses] (
    [Id] int  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
	CONSTRAINT [PK_UserStatuses] PRIMARY KEY CLUSTERED ([Id] ASC)
);

-- Creating table 'Characters'
CREATE TABLE [Characters] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [Level] int  NOT NULL,
    [Experience] int  NOT NULL,
    [Gold] int  NOT NULL,
    [Health] int  NOT NULL,
    [Endurance] int  NOT NULL,
    [Damage] int  NOT NULL,
    [Class_Id] int  NOT NULL,
    [User_Id] uniqueidentifier  NOT NULL,
	[Inventory_Id] uniqueidentifier  NOT NULL,
	[Weapon_Id] uniqueidentifier,
	[Util_Id] uniqueidentifier,
	[Head_Id] uniqueidentifier,
	[Armor_Id] uniqueidentifier,
	CONSTRAINT [PK_Characters] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

-- Creating table 'CharacterClasses'
CREATE TABLE [CharacterClasses] (
    [Id] int  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Description] nvarchar(1000)  NOT NULL,
    [BaseHealth] int  NOT NULL,
    [BaseEndurance] int  NOT NULL,
    [BaseDamage] int  NOT NULL,
	[Image] nvarchar(max) NOT NULL,
	CONSTRAINT [PK_CharacterClasses] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

-- Creating table 'Items'
CREATE TABLE [Items] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [Description] nvarchar(1000)  NOT NULL,
    [Level] int  NOT NULL,
    [Price] int  NOT NULL,
	[Stat] int  NOT NULL,
	[Image] nvarchar(max) NOT NULL,
    [ItemType_Id] int  NOT NULL,
	[ItemClass_Id] int  NOT NULL,
	CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

-- Creating table 'ItemTypes'
CREATE TABLE [ItemTypes] (
    [Id] int  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
	[Image] nvarchar(max) NOT NULL,
	CONSTRAINT [PK_ItemTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

-- Creating table 'Containers'
CREATE TABLE [Containers] (
    [Id] uniqueidentifier  NOT NULL,
	CONSTRAINT [PK_Containers] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

-- Creating table 'ContainerItems'
CREATE TABLE [ContainerItems] (
    [Id] uniqueidentifier  NOT NULL,
    [Quantity] int  NOT NULL,
    [Item_Id] uniqueidentifier  NOT NULL,
    [Container_Id] uniqueidentifier  NOT NULL,
	CONSTRAINT [PK_InventoryItems] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

-- Creating table 'Messages'
CREATE TABLE [Messages] (
    [Id] uniqueidentifier  NOT NULL,
    [Text] nvarchar(1000)  NOT NULL,
    [IsNew] bit  NOT NULL,
    [Date] datetime  NOT NULL,
    [Sender_Id] uniqueidentifier  NOT NULL,
    [Receiver_Id] uniqueidentifier  NOT NULL,
	CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

-- Creating table 'TradeOffers'
CREATE TABLE [TradeOffers] (
    [Id] uniqueidentifier  NOT NULL,
    [Comment] nvarchar(max)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [Date] datetime  NOT NULL,
    [Sender_Id] uniqueidentifier  NOT NULL,
    [Receiver_Id] uniqueidentifier  NOT NULL,
	[SenderContainer_Id] uniqueidentifier  NOT NULL,
	[ReceiverContainer_Id] uniqueidentifier  NOT NULL,
	CONSTRAINT [PK_TradeOffers] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Role_Id] in table 'Users'
ALTER TABLE [Users]
ADD CONSTRAINT [FK_UserUserRole]
    FOREIGN KEY ([Role_Id])
    REFERENCES [UserRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Status_Id] in table 'Users'
ALTER TABLE [Users]
ADD CONSTRAINT [FK_UserUserStatus]
    FOREIGN KEY ([Status_Id])
    REFERENCES [UserStatuses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ActiveCharacter_Id] in table 'Users'
ALTER TABLE [Users]
ADD CONSTRAINT [FK_UserActiveCharacter]
    FOREIGN KEY ([ActiveCharacter_Id])
    REFERENCES [Characters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Class_Id] in table 'Characters'
ALTER TABLE [Characters]
ADD CONSTRAINT [FK_CharacterCharacterClass]
    FOREIGN KEY ([Class_Id])
    REFERENCES [CharacterClasses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [User_Id] in table 'Characters'
ALTER TABLE [Characters]
ADD CONSTRAINT [FK_UserCharacters]
    FOREIGN KEY ([User_Id])
    REFERENCES [Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Inventory_Id] in table 'Characters'
ALTER TABLE [Characters]
ADD CONSTRAINT [FK_CharacterInventory]
    FOREIGN KEY ([Inventory_Id])
    REFERENCES [Containers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Weapon_Id] in table 'Characters'
ALTER TABLE [Characters]
ADD CONSTRAINT [FK_CharacterWeapon]
    FOREIGN KEY ([Weapon_Id])
    REFERENCES [ContainerItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Util_Id] in table 'Characters'
ALTER TABLE [Characters]
ADD CONSTRAINT [FK_CharacterUtil]
    FOREIGN KEY ([Util_Id])
    REFERENCES [ContainerItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Head_Id] in table 'Characters'
ALTER TABLE [Characters]
ADD CONSTRAINT [FK_CharacterHead]
    FOREIGN KEY ([Head_Id])
    REFERENCES [ContainerItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Armor_Id] in table 'Characters'
ALTER TABLE [Characters]
ADD CONSTRAINT [FK_CharacterArmor]
    FOREIGN KEY ([Armor_Id])
    REFERENCES [ContainerItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ItemType_Id] in table 'Items'
ALTER TABLE [Items]
ADD CONSTRAINT [FK_ItemItemType]
    FOREIGN KEY ([ItemType_Id])
    REFERENCES [ItemTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ItemClass_Id] in table 'Items'
ALTER TABLE [Items]
ADD CONSTRAINT [FK_ItemItemClass]
    FOREIGN KEY ([ItemClass_Id])
    REFERENCES [CharacterClasses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Item_Id] in table 'ContainerItems'
ALTER TABLE [ContainerItems]
ADD CONSTRAINT [FK_ContainerItemItem]
    FOREIGN KEY ([Item_Id])
    REFERENCES [Items]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Container_Id] in table 'ContainerItems'
ALTER TABLE [ContainerItems]
ADD CONSTRAINT [FK_ContainerItemContainer]
    FOREIGN KEY ([Container_Id])
    REFERENCES [Containers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Sender_Id] in table 'Messages'
ALTER TABLE [Messages]
ADD CONSTRAINT [FK_MessageSender]
    FOREIGN KEY ([Sender_Id])
    REFERENCES [Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Receiver_Id] in table 'Messages'
ALTER TABLE [Messages]
ADD CONSTRAINT [FK_MessageReceiver]
    FOREIGN KEY ([Receiver_Id])
    REFERENCES [Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Sender_Id] in table 'TradeOffers'
ALTER TABLE [TradeOffers]
ADD CONSTRAINT [FK_TradeOfferSender]
    FOREIGN KEY ([Sender_Id])
    REFERENCES [Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Receiver_Id] in table 'TradeOffers'
ALTER TABLE [TradeOffers]
ADD CONSTRAINT [FK_TradeOfferReceiver]
    FOREIGN KEY ([Receiver_Id])
    REFERENCES [Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SenderContainer_Id] in table 'TradeOffers'
ALTER TABLE [TradeOffers]
ADD CONSTRAINT [FK_TradeOfferSenderContainer]
    FOREIGN KEY ([SenderContainer_Id])
    REFERENCES [Containers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ReceiverContainer_Id] in table 'TradeOffers'
ALTER TABLE [TradeOffers]
ADD CONSTRAINT [FK_TradeOfferReceiverContainer]
    FOREIGN KEY ([ReceiverContainer_Id])
    REFERENCES [Containers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------