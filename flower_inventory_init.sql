IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Categories] (
    [CategoryId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([CategoryId])
);
GO

CREATE TABLE [Flowers] (
    [FlowerId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Type] nvarchar(max) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [CategoryId] int NOT NULL,
    CONSTRAINT [PK_Flowers] PRIMARY KEY ([FlowerId]),
    CONSTRAINT [FK_Flowers_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([CategoryId]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'Description', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] ON;
INSERT INTO [Categories] ([CategoryId], [Description], [Name])
VALUES (1, N'Beautiful and fragrant flowers', N'Roses'),
(2, N'Elegant spring flowers', N'Tulips');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'Description', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'FlowerId', N'CategoryId', N'Name', N'Price', N'Type') AND [object_id] = OBJECT_ID(N'[Flowers]'))
    SET IDENTITY_INSERT [Flowers] ON;
INSERT INTO [Flowers] ([FlowerId], [CategoryId], [Name], [Price], [Type])
VALUES (1, 1, N'Red Rose', 2.5, N'Perennial'),
(2, 1, N'White Rose', 3.0, N'Perennial'),
(3, 2, N'Yellow Tulip', 1.75, N'Annual'),
(4, 2, N'Purple Tulip', 2.0, N'Annual');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'FlowerId', N'CategoryId', N'Name', N'Price', N'Type') AND [object_id] = OBJECT_ID(N'[Flowers]'))
    SET IDENTITY_INSERT [Flowers] OFF;
GO

CREATE INDEX [IX_Flowers_CategoryId] ON [Flowers] ([CategoryId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250614072934_InitialCreate', N'8.0.0');
GO

COMMIT;
GO

