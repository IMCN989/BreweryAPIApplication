CREATE PROCEDURE [dbo].[spWholesalers_UpdateStock]
    @WholesalerId INT,
    @BeerId INT,
    @Quantity INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE WholesalerBeers
    SET Stock = Stock - @Quantity
    WHERE WholesalerId = @WholesalerId AND BeerId = @BeerId;
END

