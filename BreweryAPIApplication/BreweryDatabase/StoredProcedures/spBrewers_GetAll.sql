CREATE PROCEDURE [dbo].[spBrewers_GetAll]
AS
BEGIN
    SELECT Id, Name FROM Brewers
END