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