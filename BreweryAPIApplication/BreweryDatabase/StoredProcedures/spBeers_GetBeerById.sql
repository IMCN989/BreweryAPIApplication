CREATE PROCEDURE [dbo].[spBeers_GetBeerById]
    @BeerId INT
AS
BEGIN
    SELECT Id, Name, Price
    FROM Beers
    WHERE Id = @BeerId;
END
