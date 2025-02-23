CREATE PROCEDURE [dbo].[spBeers_GetByBrewer]
    @BrewerId INT
AS
BEGIN
    SELECT Id, Name, Price, BrewerId
    FROM Beers
    WHERE BrewerId = @BrewerId;
END

