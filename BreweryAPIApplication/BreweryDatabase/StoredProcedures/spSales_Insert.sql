CREATE PROCEDURE [dbo].[spSales_Insert]
    @WholesalerId INT,
    @BeerId INT,
    @Quantity INT,
    @TotalPrice DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Sales (WholesalerId, BeerId, Quantity, TotalPrice, SaleDate)
    VALUES (@WholesalerId, @BeerId, @Quantity, @TotalPrice, GETDATE());
END
