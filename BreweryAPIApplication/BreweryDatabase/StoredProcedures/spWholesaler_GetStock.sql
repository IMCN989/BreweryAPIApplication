CREATE PROCEDURE [dbo].[spWholesaler_GetStock]
    @WholesalerId INT,
    @BeerId INT
AS
BEGIN
    SELECT wb.WholesalerId, wb.BeerId, wb.Stock, 
           b.Name AS BeerName, b.Price AS BeerPrice
    FROM WholesalerBeers wb
    INNER JOIN Beers b ON wb.BeerId = b.Id
    WHERE wb.WholesalerId = @WholesalerId 
      AND wb.BeerId = @BeerId;
END
