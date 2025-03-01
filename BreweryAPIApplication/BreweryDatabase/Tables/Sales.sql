CREATE TABLE Sales (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    WholesalerId INT NOT NULL,
    BeerId INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    TotalPrice DECIMAL(10,2) NOT NULL CHECK (TotalPrice >= 0),
    SaleDate DATETIME DEFAULT GETDATE(),

    CONSTRAINT FK_Sales_Wholesalers FOREIGN KEY (WholesalerId) 
        REFERENCES Wholesalers(Id) ON DELETE CASCADE,

    CONSTRAINT FK_Sales_Beers FOREIGN KEY (BeerId) 
        REFERENCES Beers(Id) ON DELETE CASCADE
);
