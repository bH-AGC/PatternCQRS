-- Création de la base de données pour les utilisateurs
CREATE DATABASE UsersDB;
GO
USE UsersDB;
GO
CREATE TABLE [dbo].[Users] (
    [Id] INT NOT NULL IDENTITY,
    [Name] NVARCHAR(255) UNIQUE NOT NULL
);
GO

USE UsersDB;
GO
CREATE OR ALTER PROCEDURE AddUser
    @UserName NVARCHAR(255),
    @UserId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Vérifier si l'utilisateur existe déjà
    IF EXISTS (SELECT 1 FROM [dbo].[Users] WHERE [Name] = @UserName)
        BEGIN
            -- Récupérer l'ID de l'utilisateur existant
            SELECT @UserId = [Id] FROM [dbo].[Users] WHERE [Name] = @UserName;
            RETURN;
        END

    -- Insérer le nouvel utilisateur
    INSERT INTO [dbo].[Users] ([Name]) VALUES (@UserName);

    -- Récupérer l'ID du nouvel utilisateur
    SET @UserId = SCOPE_IDENTITY();
END;
GO

USE UsersDB;
GO
CREATE OR ALTER PROCEDURE GetUserByUsername
@UserName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Name
    FROM Users
    WHERE Name = @UserName;
END;
GO

-- Création de la base de données pour les messages
CREATE DATABASE MessagesDB;
GO
USE MessagesDB;
GO
CREATE TABLE [dbo].[Messages] (
    [Id] INT NOT NULL IDENTITY,
    [UserId] INT NOT NULL,
    [Content] NVARCHAR(MAX) NOT NULL
);
GO

USE MessagesDB;
GO
CREATE OR ALTER PROCEDURE AddMessage
    @UserId INT,
    @Content NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    -- Vérifier si l'utilisateur existe dans UsersDB avant d'insérer le message
    IF NOT EXISTS (SELECT 1 FROM UsersDB.[dbo].[Users] WHERE [Id] = @UserId)
        BEGIN
            PRINT 'Erreur : UserId non trouvé dans UsersDB';
            RETURN;
        END

    -- Insérer le message
    INSERT INTO [dbo].[Messages] ([UserId], [Content])
    VALUES (@UserId, @Content);
END;
GO

USE MessagesDB;
GO
CREATE OR ALTER PROCEDURE GetAllMessages
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, UserId, Content
    FROM [dbo].[Messages];
END;
GO
