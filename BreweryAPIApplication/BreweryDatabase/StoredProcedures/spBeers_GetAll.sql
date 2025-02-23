CREATE PROCEDURE [dbo].[spBeers_GetAll]
AS
BEGIN
    SELECT b.Id, b.Name, b.Price, b.BrewerId, br.Name AS BrewerName
    FROM Beers b
    JOIN Brewers br ON b.BrewerId = br.Id
END