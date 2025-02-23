CREATE PROCEDURE [dbo].[spBeers_Insert]
    @Name NVARCHAR(100),
    @Price DECIMAL(10,2),
    @BrewerId INT
AS
BEGIN
    INSERT INTO Beers (Name, Price, BrewerId)
    VALUES (@Name, @Price, @BrewerId);
END
