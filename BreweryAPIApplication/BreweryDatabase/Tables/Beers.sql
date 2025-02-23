CREATE TABLE [dbo].[Beers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Price] DECIMAL(10, 2) NOT NULL, 
    [BrewerId] INT NOT NULL, 
    FOREIGN KEY ([BrewerId]) REFERENCES [Brewers]([Id]) 
)
