CREATE PROCEDURE [dbo].[spBeers_Update]
    @Id INT,
    @Name NVARCHAR(100),
    @Price DECIMAL(10,2),
    @BrewerId INT
AS
BEGIN
    UPDATE Beers
    SET Name = @Name, Price = @Price, BrewerId = @BrewerId
    WHERE Id = @Id;
END
