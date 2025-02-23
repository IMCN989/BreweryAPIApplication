CREATE PROCEDURE [dbo].[spWholesaler_GetQuote]
    @WholesalerId INT,
    @OrderJson NVARCHAR(MAX)
AS
BEGIN
    DECLARE @TotalPrice DECIMAL(10,2) = 0;
    DECLARE @BeerId INT, @Quantity INT, @BeerPrice DECIMAL(10,2), @Stock INT;

    DECLARE @Order TABLE (BeerId INT, Quantity INT);
    INSERT INTO @Order (BeerId, Quantity)
    SELECT BeerId, Quantity FROM OPENJSON(@OrderJson)
    WITH (BeerId INT '$.BeerId', Quantity INT '$.Quantity');

    -- Validate order
    IF NOT EXISTS (SELECT 1 FROM Wholesalers WHERE Id = @WholesalerId)
    BEGIN
        RAISERROR('Wholesaler does not exist.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM @Order)
    BEGIN
        RAISERROR('Order cannot be empty.', 16, 1);
        RETURN;
    END

    -- Check for duplicate beers in order
    IF EXISTS (SELECT BeerId FROM @Order GROUP BY BeerId HAVING COUNT(*) > 1)
    BEGIN
        RAISERROR('Duplicate beers in order.', 16, 1);
        RETURN;
    END

    -- Loop through order and validate stock + calculate total
    DECLARE order_cursor CURSOR FOR 
    SELECT BeerId, Quantity FROM @Order;

    OPEN order_cursor;
    FETCH NEXT FROM order_cursor INTO @BeerId, @Quantity;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Get beer price and stock
        SELECT @BeerPrice = b.Price, @Stock = wb.Stock
        FROM Beers b
        JOIN WholesalerBeers wb ON b.Id = wb.BeerId
        WHERE wb.WholesalerId = @WholesalerId AND b.Id = @BeerId;

        -- Beer must exist and be sold by wholesaler
        IF @BeerPrice IS NULL
        BEGIN
            RAISERROR('Beer must be sold by wholesaler.', 16, 1);
            RETURN;
        END

        -- Check stock availability
        IF @Stock < @Quantity
        BEGIN
            RAISERROR('Ordered quantity exceeds stock.', 16, 1);
            RETURN;
        END

        -- Calculate price
        SET @TotalPrice = @TotalPrice + (@BeerPrice * @Quantity);

        FETCH NEXT FROM order_cursor INTO @BeerId, @Quantity;
    END

    CLOSE order_cursor;
    DEALLOCATE order_cursor;

    -- Apply discounts
    DECLARE @TotalQuantity INT = (SELECT SUM(Quantity) FROM @Order);
    IF @TotalQuantity > 20
        SET @TotalPrice = @TotalPrice * 0.8; -- 20% discount
    ELSE IF @TotalQuantity > 10
        SET @TotalPrice = @TotalPrice * 0.9; -- 10% discount

    -- Return the total price
    SELECT @TotalPrice AS TotalPrice;
END

