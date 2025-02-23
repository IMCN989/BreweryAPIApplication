CREATE PROCEDURE [dbo].[spBeers_Delete]
    @Id INT
AS
BEGIN
    DELETE FROM Beers WHERE Id = @Id;
END
