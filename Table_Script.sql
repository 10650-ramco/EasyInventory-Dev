create table Users
(
	Id			INT IDENTITY(1, 1),
	UserName	NVARCHAR(20) NOT NULL,
	Password	NVARCHAR(20) NOT NULL,
	Name		NVARCHAR(50),
	LastName	NVARCHAR(50),
	Email		NVARCHAR(100)
)


INSERT INTO Users (UserName, Password, Name, LastName, Email)
VALUES
('senthil',    'Pass@123', 'John',   'Doe',     'john.doe@example.com'),
('pavithra',  'Pass@124', 'Anna',   'Smith',   'anna.smith@example.com'),
('dhanya',  'Pass@125', 'Robert', 'Brown',   'robert.brown@example.com'),
('vanitha',  'Pass@126', 'Kiran',  'Patel',   'kiran.patel@example.com'),
('sathish', 'Pass@127', 'Meera',  'Sharma',  'meera.sharma@example.com');


select * from sysobjects where xtype = 'U' 

CREATE TABLE Products
(
    ProductId            INT IDENTITY(1,1) 
                          CONSTRAINT PK_Products PRIMARY KEY,

    ProductName          NVARCHAR(200) NOT NULL,

    CategoryId           INT NOT NULL,

    Unit                 NVARCHAR(50) NOT NULL,

    Price                DECIMAL(18, 2) NOT NULL
                          CONSTRAINT CK_Products_Price CHECK (Price >= 0),

    Stock                INT NOT NULL
                          CONSTRAINT CK_Products_Stock CHECK (Stock >= 0),

    LowStockThreshold    INT NOT NULL
                          CONSTRAINT CK_Products_LowStock CHECK (LowStockThreshold >= 0),

    ProductStatus           NVARCHAR(20) NOT NULL,

    CreatedBy           NVARCHAR(200) NOT NULL,
    CreatedDate          DATETIME NOT NULL
                          CONSTRAINT DF_Products_CreatedDate DEFAULT GETDATE(),

    ModifiedBy           NVARCHAR(200) NULL,
    ModifiedDate         DATETIME  NULL
);
delete from products
select * from products

INSERT INTO Products
(ProductName, CategoryId, Unit, Price, Stock, LowStockThreshold, ProductStatus, CreatedBy, CreatedDate)
VALUES
('Dell Inspiron 15 Laptop', 1, 'PCS', 58999, 20, 5, 'ACTIVE', 'admin', '2024-01-01'),
('HP Pavilion 14 Laptop', 1, 'PCS', 62999, 18, 5, 'ACTIVE', 'admin', '2024-01-02'),
('Lenovo ThinkPad E14', 1, 'PCS', 71999, 15, 4, 'ACTIVE', 'admin', '2024-01-03'),
('Apple MacBook Air M1', 1, 'PCS', 82999, 10, 3, 'ACTIVE', 'admin', '2024-01-04'),
('Samsung 24-inch Monitor', 1, 'PCS', 12499, 22, 5, 'ACTIVE', 'admin', '2024-01-05'),
('LG 27-inch IPS Monitor', 1, 'PCS', 18999, 14, 4, 'ACTIVE', 'admin', '2024-01-06'),
('Logitech USB Mouse', 1, 'PCS', 699, 120, 20, 'ACTIVE', 'admin', '2024-01-07'),
('HP Wireless Mouse', 1, 'PCS', 999, 90, 15, 'ACTIVE', 'admin', '2024-01-08'),
('Logitech Wireless Keyboard', 1, 'PCS', 1499, 70, 10, 'ACTIVE', 'admin', '2024-01-09'),
('Dell USB Keyboard', 1, 'PCS', 899, 60, 10, 'ACTIVE', 'admin', '2024-01-10'),

('SanDisk 64GB Pendrive', 1, 'PCS', 549, 100, 15, 'ACTIVE', 'admin', '2024-01-11'),
('SanDisk 128GB Pendrive', 1, 'PCS', 899, 80, 12, 'ACTIVE', 'admin', '2024-01-12'),
('Seagate 1TB External HDD', 1, 'PCS', 4599, 35, 8, 'ACTIVE', 'admin', '2024-01-13'),
('WD 2TB External HDD', 1, 'PCS', 6799, 28, 6, 'ACTIVE', 'admin', '2024-01-14'),
('Samsung SSD 500GB', 1, 'PCS', 5299, 25, 5, 'ACTIVE', 'admin', '2024-01-15'),

('Boat Rockerz 255 Earphones', 1, 'PCS', 999, 75, 12, 'ACTIVE', 'admin', '2024-01-16'),
('JBL Tune 510 Headphones', 1, 'PCS', 2999, 40, 8, 'ACTIVE', 'admin', '2024-01-17'),
('Sony Bluetooth Speaker', 1, 'PCS', 5499, 30, 6, 'ACTIVE', 'admin', '2024-01-18'),
('Samsung Galaxy Tab A8', 1, 'PCS', 17999, 20, 4, 'ACTIVE', 'admin', '2024-01-19'),
('Apple iPad 9th Gen', 1, 'PCS', 30999, 12, 3, 'ACTIVE', 'admin', '2024-01-20'),

('Canon Inkjet Printer', 1, 'PCS', 4899, 18, 4, 'ACTIVE', 'admin', '2024-01-21'),
('HP LaserJet Printer', 1, 'PCS', 12499, 12, 3, 'ACTIVE', 'admin', '2024-01-22'),
('TP-Link WiFi Router', 1, 'PCS', 1999, 50, 10, 'ACTIVE', 'admin', '2024-01-23'),
('D-Link 8 Port Switch', 1, 'PCS', 2499, 30, 6, 'ACTIVE', 'admin', '2024-01-24'),
('Zebronics Webcam', 1, 'PCS', 1899, 40, 8, 'ACTIVE', 'admin', '2024-01-25'),

('Microsoft USB-C Hub', 1, 'PCS', 3499, 22, 5, 'ACTIVE', 'admin', '2024-01-26'),
('Asus Laptop Charger', 1, 'PCS', 2799, 25, 5, 'ACTIVE', 'admin', '2024-01-27'),
('HP Laptop Backpack', 1, 'PCS', 1999, 35, 7, 'ACTIVE', 'admin', '2024-01-28'),
('Dell Laptop Sleeve', 1, 'PCS', 1299, 45, 8, 'ACTIVE', 'admin', '2024-01-29'),
('Portronics Power Bank 20000mAh', 1, 'PCS', 1699, 55, 10, 'ACTIVE', 'admin', '2024-01-30'),

('Samsung 65W Fast Charger', 1, 'PCS', 2499, 40, 8, 'ACTIVE', 'admin', '2024-01-31'),
('Mi Type-C Cable', 1, 'PCS', 399, 150, 25, 'ACTIVE', 'admin', '2024-02-01'),
('Apple Lightning Cable', 1, 'PCS', 1899, 35, 8, 'ACTIVE', 'admin', '2024-02-02'),
('Logitech HD Webcam', 1, 'PCS', 3499, 20, 5, 'ACTIVE', 'admin', '2024-02-03'),
('BenQ Projector', 1, 'PCS', 45999, 6, 2, 'ACTIVE', 'admin', '2024-02-04'),

('Acer Gaming Monitor', 1, 'PCS', 22999, 10, 3, 'ACTIVE', 'admin', '2024-02-05'),
('Corsair Mechanical Keyboard', 1, 'PCS', 7999, 12, 3, 'ACTIVE', 'admin', '2024-02-06'),
('Razer Gaming Mouse', 1, 'PCS', 4499, 14, 4, 'ACTIVE', 'admin', '2024-02-07'),
('Intel Core i5 Processor', 1, 'PCS', 21999, 8, 2, 'ACTIVE', 'admin', '2024-02-08'),
('AMD Ryzen 5 Processor', 1, 'PCS', 19999, 9, 2, 'ACTIVE', 'admin', '2024-02-09');

select * from Categories

CREATE TABLE Categories
(
    CategoryId     INT IDENTITY(1,1) CONSTRAINT PK_Categories PRIMARY KEY,
    CategoryName   NVARCHAR(100) NOT NULL,
    Description    NVARCHAR(300)
);

INSERT INTO Categories (CategoryName, Description)
VALUES
('Electronics', 'Computers, accessories, and electronic devices'),
('Grocery', 'Daily grocery and food items'),
('Stationery', 'Office and school stationery products'),
('Home Appliances', 'Large and small home appliances'),
('Electrical', 'Electrical fittings and lighting products');
