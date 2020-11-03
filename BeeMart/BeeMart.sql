CREATE DATABASE BeeMart
USE BeeMart

CREATE TABLE Customer (
	CustomerID INT PRIMARY KEY IDENTITY(1,1),
	CusName NVARCHAR(30),
	DOB DATETIME,
	Gender BIT,
	PhomeNum INT
);

CREATE TABLE Staff (
	StaffID INT PRIMARY KEY IDENTITY(1,1),
	StaffName NVARCHAR(30),
	DOB DATETIME,
	Gender BIT,
	Position BIT,
	Username NVARCHAR(100),
	Passwords NVARCHAR(100)
);

CREATE TABLE Categories(
	CategoryID INT PRIMARY KEY IDENTITY(1,1),
	CategoryName NVARCHAR(30)
);

CREATE TABLE Product(
	ProductID INT PRIMARY KEY IDENTITY(1,1),
	ProductName NVARCHAR(30),
	Price INT,
	SaleQuality INT,
	Pro_CategoryID INT FOREIGN KEY REFERENCES Categories(CategoryID)
);

CREATE TABLE Bill (
	BillID INT PRIMARY KEY IDENTITY(1,1),
	DateOfPrint DATETIME,
	Bill_CustomerID INT FOREIGN KEY REFERENCES Customer(CustomerID),
	Bill_StaffID INT FOREIGN KEY REFERENCES Staff(StaffID),
	Total INT
);

CREATE TABLE Bill_Details (
	BillID INT PRIMARY KEY FOREIGN KEY REFERENCES Bill(BillID),
	Details_ProductID INT FOREIGN KEY REFERENCES Product(ProductID),
	Promotion INT,
	Quality INT,
	Price INT
);

SELECT * FROM Staff WHERE Gender = 1;

SELECT * FROM Staff

CREATE PROC Check_Staff
@username NVARCHAR(30)
AS
BEGIN
	SELECT* FROM Staff WHERE Username = @username
END

EXEC Check_Staff



CREATE PROC Logins
@username NVARCHAR(30),
@password NVARCHAR(30)
AS
BEGIN
	SELECT * FROM Staff WHERE Username = @username AND Passwords = @password
END

EXEC Logins 'admin', '12345'

CREATE PROC Sign_up
@username NVARCHAR(30),
@password NVARCHAR(30),
@name NVARCHAR(30),
@DOB DATETIME,
@position NVARCHAR(30),
@gender NVARCHAR(30)
AS
BEGIN
	IF @position = 'Admin' AND @gender = 'Male'
	BEGIN
		INSERT INTO Staff VALUES(@name, @DOB, 1, 1, @username, @password);
	END
	IF @position = 'Admin' AND @gender = 'Female'
	BEGIN
		INSERT INTO Staff VALUES(@name, @DOB, 0, 1, @username, @password);
	END
	IF @position = 'Staff' AND @gender = 'Male'
	BEGIN 
		INSERT INTO Staff VALUES(@name, @DOB, 1, 0, @username, @password);
	END
	IF @position = 'Staff' AND @gender = 'Female'
	BEGIN
		INSERT INTO Staff VALUES(@name, @DOB, 0, 0, @username, @password);
	END
END

EXEC Sign_up 'tungcs', '12345', 'Thị Tùng', '10/6/2020', 'Nhân viên','Gái'

INSERT INTO Staff VALUES('Cao Sơn Tùng', '1998-05-08', 'Nam', 'Quản lý', 'tungcs', '12345');

DELETE FROM Staff WHERE StaffID = 12

DROP PROC Sign_up

SELECT * FROM Staff

CREATE PROC Update_Staff
@username NVARCHAR(30),
@name NVARCHAR(30),
@DOB DATETIME,
@position NVARCHAR(30),
@gender NVARCHAR(30)
AS
BEGIN
	IF @position = 'Admin' AND @gender = 'Male'
	BEGIN
		UPDATE Staff
		SET
			StaffName = @name,
			DOB = @DOB,
			Gender = 1,
			Position = 1
		WHERE 
			Username = @username
	END

	IF @position = 'Admin' AND @gender = 'Female'
	BEGIN
		UPDATE Staff
		SET
			StaffName = @name,
			DOB = @DOB,
			Gender = 0,
			Position = 1
		WHERE 
			Username = @username
	END
	ELSE IF @position = 'Staff' AND @gender = 'Male'
	BEGIN
		UPDATE Staff
		SET
			StaffName = @name,
			DOB = @DOB,
			Position = 0,
			Gender = 1
		WHERE 
			Username = @username
	END
	ELSE IF @position = 'Staff' AND @gender = 'Female'
	BEGIN
		UPDATE Staff
		SET
			StaffName = @name,
			DOB = @DOB,
			Gender = 0,
			Position = 0
		WHERE 
			Username = @username
	END
END

DROP PROC Update_Staff

EXEC Update_Staff 'abababa', 'bbbbbb', '10/6/2020', 'Nhân viên','Nam'

CREATE PROC show_product
AS
BEGIN
	SELECT * FROM Product
END

EXEC show_product

SELECT * FROM Categories

CREATE PROC add_category
@category_name NVARCHAR(30)
AS
BEGIN
	INSERT INTO Categories VALUES(@category_name)
END

EXEC add_category 'nakfmasf'
SELECT * FROM Product WHERE ProductName = 'Trà Cozy'AND Price = '30000' AND SaleQuality = 10 AND Pro_CategoryID = 1

CREATE PROC add_product
@ProductName NVARCHAR(30),
@Price INT,
@CategoryID INT
AS 
BEGIN
	INSERT INTO Product VALUES(@ProductName, @Price, 0, @CategoryID);
	SELECT * FROM Product WHERE ProductName = @ProductName AND Price = @Price AND @CategoryID = @CategoryID
END

DROP PROC add_product

EXEC add_product 'Vani', 150000, 1

--Proc update Sản phẩm
CREATE PROC Update_Product 
@ProID NVARCHAR(30),
@ProName NVARCHAR(30),
@CateID INT,
@Price INT
AS
BEGIN
	UPDATE Product
		SET
			ProductName = @ProName,
			Price = @Price,
			Pro_CategoryID = @CateID
		WHERE 
			ProductID = @ProID

	SELECT * FROM Product WHERE ProductName = @ProName AND Price = @Price AND Pro_CategoryID = @CateID
END

DROP PROC Update_Product

-- Working on table Customer

CREATE PROC add_customer
@CusName NVARCHAR(30),
@DOB DATETIME,
@Gender NVARCHAR(30),
@PhoneNum NVARCHAR(30)
AS
BEGIN
	IF @Gender = 'Male'
		BEGIN
			INSERT INTO Customer VALUES (@CusName, @DOB,1, @PhoneNum);
			SELECT * FROM Customer WHERE CusName = @CusName AND DOB = @DOB AND Gender = 1 AND PhomeNum = @PhoneNum
		END
	IF @Gender = 'Female'
		BEGIN
			INSERT INTO Customer VALUES (@CusName, @DOB,0, @PhoneNum);
			SELECT * FROM Customer WHERE CusName = @CusName AND DOB = @DOB AND Gender = 0 AND PhomeNum = @PhoneNum
		END
END

SELECT * FROM Customer

DROP PROC add_customer

EXEC add_customer 'Trần Thanh Tâm', '1996-05-08', 'Female', 5165161

ALTER TABLE Customer
	ALTER COLUMN PhomeNum NVARCHAR(30)

ALTER TABLE Customer 
	ALTER COLUMN CustomerID NVARCHAR(30)

DROP TABLE Customer

CREATE PROC check_customer
@CusNameID INT
AS
BEGIN
	SELECT * FROM Customer WHERE CustomerID = @CusNameID
END

DROP PROC check_customer

CREATE PROC update_customer
@CusID INT,
@CusName NVARCHAR(30),
@DOB DATETIME,
@Gender NVARCHAR(30),
@PhoneNum NVARCHAR(30)
AS
BEGIN
	IF @Gender = 'Male'
		BEGIN
			UPDATE Customer
			SET
				CusName = @CusName,
				DOB = @DOB,
				Gender = 1,
				PhomeNum = @PhoneNum
			WHERE 
				CustomerID = @CusID
		END
	IF @Gender = 'Female'
		BEGIN
			UPDATE Customer
			SET
				CusName = @CusName,
				DOB = @DOB,
				Gender = 0,
				PhomeNum = @PhoneNum
			WHERE 
				CustomerID = @CusID
		END
END

DROP PROC update_customer

-- Working on Bill

CREATE PROC search_bill
@dateFrom DATETIME,
@dateTo DATETIME,
@staffID INT,
@customerID INT
AS
BEGIN
	IF @staffID = '' AND @customerID = ''
		BEGIN
			SELECT * FROM Bill WHERE DateOfPrint BETWEEN @dateFrom AND @dateTo
		END
	IF @staffID != '' AND @customerID = ''
		BEGIN
			SELECT * FROM Bill WHERE Bill_StaffID = @staffID AND DateOfPrint BETWEEN @dateFrom AND @dateTo
		END
	IF @staffID = '' AND @customerID != ''
		BEGIN
			SELECT * FROM Bill WHERE Bill_CustomerID = @customerID AND DateOfPrint BETWEEN @dateFrom AND @dateTo
		END
	IF @staffID != '' AND @customerID != ''
		BEGIN
			SELECT * FROM Bill WHERE Bill_StaffID = @staffID AND Bill_CustomerID = @customerID AND DateOfPrint BETWEEN @dateFrom AND @dateTo
		END
END

DROP PROC search_bill

SELECT * FROM Bill

EXEC search_bill '2020-10-01', '2020-11-01', '', ''