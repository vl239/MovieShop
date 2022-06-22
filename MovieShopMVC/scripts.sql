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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220616214852_CreatingFewTables')
BEGIN
    CREATE TABLE [Genre] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(64) NOT NULL,
        CONSTRAINT [PK_Genre] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220616214852_CreatingFewTables')
BEGIN
    CREATE TABLE [Movie] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(256) NULL,
        [Overview] nvarchar(max) NULL,
        [Tagline] nvarchar(512) NULL,
        [Budget] decimal(18,4) NULL DEFAULT 9.9,
        [Revenue] decimal(18,4) NULL DEFAULT 9.9,
        [ImdbUrl] nvarchar(2084) NULL,
        [TmdbUrl] nvarchar(2084) NULL,
        [PosterUrl] nvarchar(2084) NULL,
        [BackdropUrl] nvarchar(2084) NULL,
        [OriginalLanguage] nvarchar(64) NULL,
        [ReleaseDate] datetime2 NULL,
        [RunTime] int NULL,
        [Price] decimal(5,2) NULL DEFAULT 9.9,
        [CreatedDate] datetime2 NULL DEFAULT (getdate()),
        [UpdatedDate] datetime2 NULL,
        [UpdatedBy] nvarchar(max) NULL,
        [CreatedBy] nvarchar(max) NULL,
        CONSTRAINT [PK_Movie] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220616214852_CreatingFewTables')
BEGIN
    CREATE TABLE [MovieGenre] (
        [MovieId] int NOT NULL,
        [GenreId] int NOT NULL,
        CONSTRAINT [PK_MovieGenre] PRIMARY KEY ([MovieId], [GenreId]),
        CONSTRAINT [FK_MovieGenre_Genre_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [Genre] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_MovieGenre_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220616214852_CreatingFewTables')
BEGIN
    CREATE TABLE [Trailer] (
        [Id] int NOT NULL IDENTITY,
        [MovieId] int NOT NULL,
        [TrailerUrl] nvarchar(2084) NOT NULL,
        [Name] nvarchar(2084) NOT NULL,
        CONSTRAINT [PK_Trailer] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Trailer_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220616214852_CreatingFewTables')
BEGIN
    CREATE INDEX [IX_MovieGenre_GenreId] ON [MovieGenre] ([GenreId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220616214852_CreatingFewTables')
BEGIN
    CREATE INDEX [IX_Trailer_MovieId] ON [Trailer] ([MovieId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220616214852_CreatingFewTables')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220616214852_CreatingFewTables', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    ALTER TABLE [MovieGenre] DROP CONSTRAINT [FK_MovieGenre_Genre_GenreId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    ALTER TABLE [MovieGenre] DROP CONSTRAINT [FK_MovieGenre_Movie_MovieId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    ALTER TABLE [Trailer] DROP CONSTRAINT [FK_Trailer_Movie_MovieId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    ALTER TABLE [Trailer] DROP CONSTRAINT [PK_Trailer];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    ALTER TABLE [MovieGenre] DROP CONSTRAINT [PK_MovieGenre];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    ALTER TABLE [Movie] DROP CONSTRAINT [PK_Movie];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    ALTER TABLE [Genre] DROP CONSTRAINT [PK_Genre];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    EXEC sp_rename N'[Trailer]', N'Trailers';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    EXEC sp_rename N'[MovieGenre]', N'MovieGenres';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    EXEC sp_rename N'[Movie]', N'Movies';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    EXEC sp_rename N'[Genre]', N'Genres';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    EXEC sp_rename N'[Trailers].[IX_Trailer_MovieId]', N'IX_Trailers_MovieId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    EXEC sp_rename N'[MovieGenres].[IX_MovieGenre_GenreId]', N'IX_MovieGenres_GenreId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Trailers]') AND [c].[name] = N'TrailerUrl');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Trailers] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Trailers] ALTER COLUMN [TrailerUrl] nvarchar(2084) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Trailers]') AND [c].[name] = N'Name');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Trailers] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Trailers] ALTER COLUMN [Name] nvarchar(2084) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'Title');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Movies] ALTER COLUMN [Title] nvarchar(256) NOT NULL;
    ALTER TABLE [Movies] ADD DEFAULT N'' FOR [Title];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    ALTER TABLE [Trailers] ADD CONSTRAINT [PK_Trailers] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    ALTER TABLE [MovieGenres] ADD CONSTRAINT [PK_MovieGenres] PRIMARY KEY ([MovieId], [GenreId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    ALTER TABLE [Movies] ADD CONSTRAINT [PK_Movies] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    ALTER TABLE [Genres] ADD CONSTRAINT [PK_Genres] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    CREATE TABLE [Casts] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Gender] nvarchar(max) NULL,
        [TmdbUrl] nvarchar(max) NULL,
        [ProfilePath] nvarchar(max) NULL,
        CONSTRAINT [PK_Casts] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    CREATE TABLE [Crews] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Gender] nvarchar(max) NULL,
        [TmdbUrl] nvarchar(max) NULL,
        [ProfilePath] nvarchar(max) NULL,
        CONSTRAINT [PK_Crews] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    CREATE TABLE [Roles] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NULL,
        [LastName] nvarchar(max) NULL,
        [DateOfBirth] datetime2 NULL,
        [Email] nvarchar(max) NULL,
        [HashedPassword] nvarchar(max) NULL,
        [Salt] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [TwoFactorEnabled] bit NULL,
        [LockoutEndDate] datetime2 NULL,
        [LastLoginDateTime] datetime2 NULL,
        [IsLocked] bit NULL,
        [AccessFailedCount] int NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    CREATE TABLE [MovieCasts] (
        [MovieId] int NOT NULL,
        [CastId] int NOT NULL,
        [Character] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_MovieCasts] PRIMARY KEY ([MovieId], [CastId]),
        CONSTRAINT [FK_MovieCasts_Casts_CastId] FOREIGN KEY ([CastId]) REFERENCES [Casts] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_MovieCasts_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    CREATE TABLE [MovieCrews] (
        [MovieId] int NOT NULL,
        [CrewId] int NOT NULL,
        [Department] nvarchar(max) NOT NULL,
        [Job] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_MovieCrews] PRIMARY KEY ([MovieId], [CrewId]),
        CONSTRAINT [FK_MovieCrews_Crews_CrewId] FOREIGN KEY ([CrewId]) REFERENCES [Crews] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_MovieCrews_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    CREATE TABLE [Favorites] (
        [Id] int NOT NULL IDENTITY,
        [MovieId] int NOT NULL,
        [UserId] int NOT NULL,
        CONSTRAINT [PK_Favorites] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Favorites_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Favorites_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    CREATE TABLE [Purchases] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [PurchaseNumber] uniqueidentifier NOT NULL,
        [TotalPrice] decimal(18,2) NOT NULL,
        [PurchaseDateTime] datetime2 NOT NULL,
        [MovieId] int NOT NULL,
        CONSTRAINT [PK_Purchases] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Purchases_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Purchases_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    CREATE TABLE [Reviews] (
        [MovieId] int NOT NULL,
        [UserId] int NOT NULL,
        [Rating] decimal(18,2) NOT NULL,
        [ReviewText] nvarchar(max) NULL,
        CONSTRAINT [PK_Reviews] PRIMARY KEY ([MovieId], [UserId]),
        CONSTRAINT [FK_Reviews_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Reviews_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    CREATE TABLE [UserRoles] (
        [UserId] int NOT NULL,
        [RoleId] int NOT NULL,
        CONSTRAINT [PK_UserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    CREATE INDEX [IX_Favorites_MovieId] ON [Favorites] ([MovieId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    CREATE INDEX [IX_Favorites_UserId] ON [Favorites] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    CREATE INDEX [IX_MovieCasts_CastId] ON [MovieCasts] ([CastId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    CREATE INDEX [IX_MovieCrews_CrewId] ON [MovieCrews] ([CrewId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    CREATE INDEX [IX_Purchases_MovieId] ON [Purchases] ([MovieId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    CREATE INDEX [IX_Purchases_UserId] ON [Purchases] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    CREATE INDEX [IX_Reviews_UserId] ON [Reviews] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    CREATE INDEX [IX_UserRoles_RoleId] ON [UserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    ALTER TABLE [MovieGenres] ADD CONSTRAINT [FK_MovieGenres_Genres_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [Genres] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    ALTER TABLE [MovieGenres] ADD CONSTRAINT [FK_MovieGenres_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    ALTER TABLE [Trailers] ADD CONSTRAINT [FK_Trailers_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220620022934_CreatingRemainingTables')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220620022934_CreatingRemainingTables', N'6.0.6');
END;
GO

COMMIT;
GO

