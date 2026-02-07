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
('Electronics', 'Devices and gadgets such as mobiles, laptops, and accessories'),
('Mobile Phones', 'Smartphones, feature phones, and mobile accessories'),
('Laptops & Computers', 'Laptops, desktops, monitors, and computer peripherals'),
('Computer Accessories', 'Keyboards, mouse, printers, scanners, and cables'),
('Groceries', 'Daily consumable food items and household essentials'),
('Fruits & Vegetables', 'Fresh fruits and vegetables'),
('Dairy Products', 'Milk, cheese, butter, curd, and dairy items'),
('Beverages', 'Soft drinks, juices, tea, coffee, and energy drinks'),
('Snacks & Packaged Foods', 'Biscuits, chips, instant foods, and snacks'),
('Clothing & Apparel', 'Men, women, and children clothing'),
('Men Clothing', 'Shirts, trousers, jeans, and ethnic wear for men'),
('Women Clothing', 'Sarees, dresses, tops, and ethnic wear for women'),
('Kids Clothing', 'Clothing and accessories for children'),
('Footwear', 'Shoes, sandals, slippers, and boots'),
('Men Footwear', 'Formal shoes, casual shoes, and sandals for men'),
('Women Footwear', 'Heels, flats, sandals, and shoes for women'),
('Kids Footwear', 'Footwear designed for children'),
('Home & Kitchen', 'Household items and kitchen essentials'),
('Kitchen Appliances', 'Mixers, grinders, ovens, and kitchen machines'),
('Cookware', 'Pots, pans, pressure cookers, and utensils'),
('Home Decor', 'Decorative items, wall art, and lighting'),
('Furniture', 'Home and office furniture'),
('Living Room Furniture', 'Sofas, tables, and TV units'),
('Bedroom Furniture', 'Beds, wardrobes, and side tables'),
('Office Furniture', 'Office desks, chairs, and storage units'),
('Health & Personal Care', 'Personal hygiene and wellness products'),
('Beauty & Cosmetics', 'Makeup, skincare, and beauty products'),
('Hair Care', 'Shampoos, conditioners, and hair oils'),
('Oral Care', 'Toothpaste, toothbrushes, and mouthwash'),
('Books & Stationery', 'Books and office or school stationery'),
('Educational Books', 'Academic, reference, and learning books'),
('Notebooks & Diaries', 'Writing notebooks and planners'),
('Office Supplies', 'Files, folders, pens, and staplers'),
('Sports & Fitness', 'Sports equipment and fitness accessories'),
('Gym Equipment', 'Weights, treadmills, and workout machines'),
('Outdoor Sports', 'Cricket, football, badminton, and outdoor gear'),
('Yoga & Wellness', 'Yoga mats, accessories, and wellness items'),
('Toys & Baby Products', 'Toys and baby care essentials'),
('Baby Care', 'Diapers, baby food, and baby hygiene products'),
('Educational Toys', 'Learning and activity-based toys'),
('Games & Puzzles', 'Board games, puzzles, and indoor games'),
('Automotive Accessories', 'Vehicle accessories and car care products'),
('Car Electronics', 'Car audio systems, chargers, and GPS'),
('Cleaning Supplies', 'Cleaning liquids, tools, and disinfectants'),
('Pet Supplies', 'Food, toys, and accessories for pets'),
('Gardening Supplies', 'Plants, seeds, tools, and gardening equipment'),
('Hardware & Tools', 'Tools, electricals, and hardware supplies');


IF NOT EXISTS (
    SELECT 1 
    FROM sys.objects 
    WHERE name = 'Customers' AND type = 'U'
)
BEGIN
    CREATE TABLE Customers
    (
        CustomerId          INT IDENTITY(1,1)
                            CONSTRAINT PK_Customers 
                            PRIMARY KEY,

        CustomerCode        NVARCHAR(50) NOT NULL
                            CONSTRAINT UQ_Customers_CustomerCode 
                            UNIQUE,

        CustomerName        NVARCHAR(200) NOT NULL,

        CustomerType        NVARCHAR(50) NOT NULL,  
        -- B2B / B2C / Export / SEZ

        -------------------------------------------------
        -- GST & TAX DETAILS
        -------------------------------------------------
        GSTIN               NVARCHAR(15) NULL,     
        -- 15 chars: 22AAAAA0000A1Z5

        PAN                 NVARCHAR(10) NULL,     
        -- AAAAA9999A

        GSTRegistrationType NVARCHAR(50) NULL,     
        -- Regular / Composition / Unregistered / SEZ / Export

        IsGSTRegistered     BIT NOT NULL DEFAULT 0,

        PlaceOfSupplyState  NVARCHAR(50) NULL,     
        -- Used to calculate IGST / CGST / SGST

        -------------------------------------------------
        -- CONTACT DETAILS
        -------------------------------------------------
        Email               NVARCHAR(200) NULL,
        PhoneNumber         NVARCHAR(20)  NULL,

        -------------------------------------------------
        -- BILLING ADDRESS
        -------------------------------------------------
        BillingAddress1     NVARCHAR(300) NULL,
        BillingAddress2     NVARCHAR(300) NULL,
        BillingCity         NVARCHAR(100) NULL,
        BillingState        NVARCHAR(100) NULL,
        BillingStateCode    NVARCHAR(2)   NULL,    
        -- GST State Code (TN = 33, KA = 29, etc.)

        BillingPincode      NVARCHAR(10)  NULL,
        BillingCountry      NVARCHAR(100) NOT NULL DEFAULT 'India',

        ---------------------------------------------------
        ---- SHIPPING ADDRESS
        ---------------------------------------------------
        ShippingAddress1    NVARCHAR(300) NULL,
        ShippingAddress2    NVARCHAR(300) NULL,
        ShippingCity        NVARCHAR(100) NULL,
        ShippingState       NVARCHAR(100) NULL,
        ShippingStateCode   NVARCHAR(2)   NULL,
        ShippingPincode     NVARCHAR(10)  NULL,

        ---------------------------------------------------
        -- FINANCIAL CONTROLS
        -------------------------------------------------
        CreditLimit         DECIMAL(18,2) NOT NULL DEFAULT 0,
        PaymentTermsDays    INT NOT NULL DEFAULT 0,

        -------------------------------------------------
        -- STATUS & AUDIT
        -------------------------------------------------
        IsActive            BIT NOT NULL DEFAULT 1,

        CreatedBy           NVARCHAR(100) NOT NULL,
        CreatedDate         DATETIME2 NOT NULL DEFAULT SYSDATETIME(),

        ModifiedBy          NVARCHAR(100) NULL,
        ModifiedDate        DATETIME2 NULL
    );

    -------------------------------------------------
    -- INDEXES
    -------------------------------------------------
    CREATE INDEX IX_Customers_Name
        ON Customers (CustomerName);

    CREATE INDEX IX_Customers_GSTIN
        ON Customers (GSTIN);

END
GO


Customers


CustomerId	int	no	4
CustomerCode	nvarchar	no	100
CustomerName	nvarchar	no	400
CustomerType	nvarchar	no	100
GSTIN	nvarchar	no	30
PAN	nvarchar	no	20
GSTRegistrationType	nvarchar	no	100
IsGSTRegistered	bit	no	1
PlaceOfSupplyState	nvarchar	no	100
Email	nvarchar	no	400
PhoneNumber	nvarchar	no	40



INSERT INTO Customers
(
    CustomerCode, CustomerName, CustomerType,
    GSTIN, PAN, GSTRegistrationType, IsGSTRegistered, PlaceOfSupplyState,
    Email, PhoneNumber,
    BillingAddress1, BillingCity, BillingState, BillingStateCode, BillingPincode,
    ShippingAddress1, ShippingCity, ShippingState, ShippingStateCode, ShippingPincode,
    CreditLimit, PaymentTermsDays,
    IsActive, CreatedBy
)
VALUES
-- 1️⃣ Tamil Nadu – B2B (Same State – CGST + SGST)
(
    'CUST-TN-001',
    'Sri Lakshmi Traders',
    'B2B',
    '33ABCDE1234F1Z5',
    'ABCDE1234F',
    'Regular',
    1,
    'Tamil Nadu',
    'accounts@srilakshmitraders.com',
    '9876543210',
    'No 12, Anna Salai',
    'Chennai',
    'Tamil Nadu',
    '33',
    '600002',
    'Warehouse Road',
    'Chennai',
    'Tamil Nadu',
    '33',
    '600097',
    500000,
    30,
    1,
    'SYSTEM'
),

-- 2️⃣ Karnataka – B2B (Interstate – IGST)
(
    'CUST-KA-002',
    'Bangalore Industrial Supplies',
    'B2B',
    '29AABCU9603R1ZV',
    'AABCU9603R',
    'Regular',
    1,
    'Karnataka',
    'finance@bisupplies.in',
    '9845012345',
    'Peenya Industrial Area',
    'Bengaluru',
    'Karnataka',
    '29',
    '560058',
    'Peenya Industrial Area',
    'Bengaluru',
    'Karnataka',
    '29',
    '560058',
    750000,
    45,
    1,
    'SYSTEM'
),

-- 3️⃣ B2C – Unregistered Customer
(
    'CUST-B2C-003',
    'Walk-in Customer Chennai',
    'B2C',
    NULL,
    NULL,
    'Unregistered',
    0,
    'Tamil Nadu',
    NULL,
    NULL,
    'Retail Counter',
    'Chennai',
    'Tamil Nadu',
    '33',
    '600001',
    'Retail Counter',
    'Chennai',
    'Tamil Nadu',
    '33',
    '600001',
    0,
    0,
    1,
    'SYSTEM'
),

-- 4️⃣ SEZ Customer – Zero Rated Supply
(
    'CUST-SEZ-004',
    'ABC Tech SEZ Pvt Ltd',
    'SEZ',
    '33AAECA9999L1ZP',
    'AAECA9999L',
    'SEZ',
    1,
    'Tamil Nadu',
    'gst@abctechsez.com',
    '9790909090',
    'SEZ Campus',
    'Chennai',
    'Tamil Nadu',
    '33',
    '600119',
    'SEZ Campus',
    'Chennai',
    'Tamil Nadu',
    '33',
    '600119',
    1000000,
    60,
    1,
    'SYSTEM'
),

-- 5️⃣ Export Customer – Outside India (Zero GST)
(
    'CUST-EXP-005',
    'Global Auto Parts LLC',
    'Export',
    NULL,
    NULL,
    'Export',
    0,
    'Outside India',
    'imports@globalautoparts.com',
    '+1-312-555-7890',
    '742 Evergreen Terrace',
    'Chicago',
    'Illinois',
    NULL,
    '60601',
    '742 Evergreen Terrace',
    'Chicago',
    'Illinois',
    NULL,
    '60601',
    2000000,
    90,
    1,
    'SYSTEM'
);
