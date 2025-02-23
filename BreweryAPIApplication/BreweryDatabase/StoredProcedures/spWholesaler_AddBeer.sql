CREATE PROCEDURE [dbo].[spWholesaler_AddBeer]
    @WholesalerId INT,
    @BeerId INT,
    @Stock INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM WholesalerBeers WHERE WholesalerId = @WholesalerId AND BeerId = @BeerId)
    BEGIN
        UPDATE WholesalerBeers
        SET Stock = Stock + @Stock
        WHERE WholesalerId = @WholesalerId AND BeerId = @BeerId;
    END
    ELSE
    BEGIN
        INSERT INTO WholesalerBeers (WholesalerId, BeerId, Stock)
        VALUES (@WholesalerId, @BeerId, @Stock);
    END
END
