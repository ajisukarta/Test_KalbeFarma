CREATE TABLE Currency(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	[Name] VARCHAR(100) NOT NULL,
	Code VARCHAR(3) NOT NULL
)

CREATE TABLE Recipient(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	[Name] VARCHAR(100) NOT NULL,
	[Address] VARCHAR(200) NOT NULL,
	States VARCHAR(50),
	Country VARCHAR(50) NOT NULL,
	[Image] VARCHAR(MAX) 
)

CREATE TABLE [Language](
	ID INT IDENTITY(1,1) PRIMARY KEY,
	[Name] VARCHAR(30)
)

CREATE TABLE PurchaseOrder(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	[Number] VARCHAR(6) NOT NULL,
	Amount DECIMAL(10,3) NOT NULL,
	PIC VARCHAR(50) NOT NULL
)

CREATE TABLE Orders(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	InvoiceNo VARCHAR(30) NOT NULL,
	CurrencyCode VARCHAR(3) NOT NULL,
	[From] VARCHAR(MAX),
	RecipientID INT FOREIGN KEY REFERENCES Recipient(ID) NOT NULL,
	OrderDate DATE NOT NULL,
	PONo VARCHAR(6) NOT NULL,
	DiscountName VARCHAR(50),
	DiscountPercentage DECIMAL(5,2),
	DiscountValue DECIMAL(30,2),
	SubTotal DECIMAL(30,2) NOT NULL,
	Total DECIMAL(30,2) NOT NULL,
	CreateDate DATETIME
) 


CREATE TABLE OrderDetails(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	OrderID INT FOREIGN KEY REFERENCES Orders(ID) NOT NULL,
	Quantity DECIMAL(30,2) NOT NULL,
	Rate DECIMAL(30,2) NOT NULL,
	Amount DECIMAL(30,2) NOT NULL,
	[Name] VARCHAR(MAX) NOT NULL,
	CreateDate DATETIME
)

