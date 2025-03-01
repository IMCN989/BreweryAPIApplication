CREATE PROCEDURE [dbo].[spWholesalers_GetById]
    @WholesalerId INT
AS
BEGIN
    SELECT 
        Id, 
        Name
    FROM Wholesalers
    WHERE Id = @WholesalerId;
END
