CREATE TABLE [dbo].[WholesalerBeers]
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    WholesalerId INT NOT NULL,
    BeerId INT NOT NULL,
    Stock INT NOT NULL CHECK (Stock >= 0),
    FOREIGN KEY (WholesalerId) REFERENCES Wholesalers(Id) ON DELETE CASCADE,
    FOREIGN KEY (BeerId) REFERENCES Beers(Id) ON DELETE CASCADE,
    UNIQUE (WholesalerId, BeerId) -- Ensures no duplicates
)
