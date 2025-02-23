CREATE PROCEDURE [dbo].[spWholesaler_AddSale]
    @WholesalerId INT,
    @BeerId INT,
    @Quantity INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM WholesalerBeers WHERE WholesalerId = @WholesalerId AND BeerId = @BeerId)
    BEGIN
        RAISERROR('Beer is not sold by this wholesaler.', 16, 1);
        RETURN;
    END

    DECLARE @Stock INT;
    SELECT @Stock = Stock FROM WholesalerBeers WHERE WholesalerId = @WholesalerId AND BeerId = @BeerId;

    IF @Stock < @Quantity
    BEGIN
        RAISERROR('Not enough stock available.', 16, 1);
        RETURN;
    END

    UPDATE WholesalerBeers
    SET Stock = Stock - @Quantity
    WHERE WholesalerId = @WholesalerId AND BeerId = @BeerId;
END


