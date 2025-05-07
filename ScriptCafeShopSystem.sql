use master 
go
CREATE DATABASE CafeShopSystem;
GO
USE CafeShopSystem;
GO

-- Table: Tables
CREATE TABLE Tables (
    tableId VARCHAR(10) PRIMARY KEY,  -- Đổi tên cột 'id' thành 'tableId'
    name NVARCHAR(100) NOT NULL,
    status NVARCHAR(50) NOT NULL,
    capacity INT NULL
);



-- Table: Users
CREATE TABLE Users (
    userId VARCHAR(10) PRIMARY KEY,  -- Đổi tên cột 'id' thành 'userId'
    userName NVARCHAR(50) NOT NULL,
    password NVARCHAR(255) NOT NULL,
    type NVARCHAR(50) NOT NULL
);


-- Table: ProductCategory
CREATE TABLE ProductCategory (
    categoryId VARCHAR(10) PRIMARY KEY,  -- Đổi tên cột 'id' thành 'categoryId'
    name NVARCHAR(100) NOT NULL
);


-- Table: Supplier
CREATE TABLE Supplier (
    supplierId VARCHAR(10) PRIMARY KEY,  -- Đổi tên cột 'id' thành 'supplierId'
    name NVARCHAR(100) NOT NULL,
    address NVARCHAR(255)
);


-- Table: Product
CREATE TABLE Product (
    productId VARCHAR(10) PRIMARY KEY,  -- Đổi tên cột 'id' thành 'productId'
    name NVARCHAR(100) NOT NULL,
    purchasePrice DECIMAL(18,2) NOT NULL,
    sellingPrice DECIMAL(18,2) NOT NULL,
    categoryId VARCHAR(10) NOT NULL,  -- Khoá ngoại liên kết với ProductCategory
    supplierId VARCHAR(10) NOT NULL,  -- Khoá ngoại liên kết với Supplier
    FOREIGN KEY (categoryId) REFERENCES ProductCategory(categoryId),
    FOREIGN KEY (supplierId) REFERENCES Supplier(supplierId)
);


-- Table: Orders
CREATE TABLE Orders (
    orderId VARCHAR(10) PRIMARY KEY,  -- Đổi tên cột 'id' thành 'orderId'
    orderDate DATETIME NOT NULL DEFAULT GETDATE(),
    tableId VARCHAR(10) NOT NULL,  -- Khoá ngoại liên kết với Tables
    userId VARCHAR(10) NOT NULL,   -- Khoá ngoại liên kết với Users
    totalAmount DECIMAL(18,2) DEFAULT 0,
    status NVARCHAR(50) NOT NULL,
    paymentStatus NVARCHAR(50) DEFAULT 'Unpaid',
    FOREIGN KEY (tableId) REFERENCES Tables(tableId),
    FOREIGN KEY (userId) REFERENCES Users(userId)
);


-- Table: OrderItem
CREATE TABLE OrderItem (
    orderItemId VARCHAR(10) PRIMARY KEY,  -- Đổi tên cột 'id' thành 'orderItemId'
    orderId VARCHAR(10) NOT NULL,  -- Khoá ngoại liên kết với Orders
    productId VARCHAR(10) NOT NULL,  -- Khoá ngoại liên kết với Product
    quantity INT NOT NULL,
    unitPrice DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (orderId) REFERENCES Orders(orderId),
    FOREIGN KEY (productId) REFERENCES Product(productId)
);



INSERT INTO ProductCategory (categoryId, name) VALUES
('C0001', N'Đồ có cồn'),
('C0002', N'Giải khát'),
('C0003', N'Pha chế');



INSERT INTO Supplier (supplierId, name, address) VALUES
-- Đồ có cồn
('S0001', N'Sabeco', N'123 Đường 1, Quận 1, TP.HCM, Việt Nam'),
('S0002', N'Vinacafé', N'456 Đường 2, Quận 2, TP.HCM, Việt Nam'),
-- Giải khát
('S0003', N'Coca-Cola Việt Nam', '789 Đường 3, Quận 3, TP.HCM, Việt Nam'),
('S0004', N'PepsiCo Việt Nam', N'321 Đường 4, Quận 4, TP.HCM, Việt Nam'),
('S0005', N'Tân Hiệp Phát', N'654 Đường 5, Quận 5, TP.HCM, Việt Nam'),

-- Pha chế
('S0006', N'Highlands Coffee', N'789 Đường 6, Quận 6, TP.HCM, Việt Nam'),
('S0007', N'Trung Nguyên', N'321 Đường 7, Quận 7, TP.HCM, Việt Nam');

INSERT INTO Product (productId, name, purchasePrice, sellingPrice, categoryId, supplierId) VALUES
('P0001', N'Bia Tiger', 20.00, 50.00, 'C0001', 'S0001'),  -- Đồ có cồn
('P0002', N'Rượu vang đỏ', 100.00, 200.00, 'C0001', 'S0002'),  -- Đồ có cồn
('P0003', N'Nước ngọt Coca Cola', 5.00, 15.00, 'C0002', 'S0003'),  -- Giải khát
('P0004', N'Nước ép cam', 12.00, 20.00, 'C0002', 'S0004'),  -- Giải khát
('P0005', N'Trà sữa truyền thống', 18.00, 28.00, 'C0003', 'S0005'),  -- Pha chế
('P0006', N'Cà phê sữa', 15.00, 25.00, 'C0003', 'S0006'),  -- Pha chế
('P0007', N'Trà xanh', 10.00, 18.00, 'C0003', 'S0007')  -- Pha chế



INSERT INTO Users (userId, userName, password, type) VALUES
('U0001', N'admin1', 'ad123', N'admin'),
('U0002', N'nhanvien1', '123', N'nhanvien'),
('U0003', N'admin2', 'ad123', N'admin'),
('U0004', N'nhanvien2', '123', N'nhanvien'),
('U0005', N'admin3', 'ad123', N'admin')

INSERT INTO Tables (tableId, name, status, capacity) VALUES
('T0001', N'Bàn 1', N'Có sẵn', 4),
('T0002', N'Bàn 2', N'Đang sử dụng', 4),
('T0003', N'Bàn 3', N'Có sẵn', 2),
('T0004', N'Bàn 4', N'Đang sử dụng', 4),
('T0005', N'Bàn 5', N'Có sẵn', 5)


INSERT INTO Orders (orderId, orderDate, tableId, userId, totalAmount, status, paymentStatus) VALUES
('O0001', N'2025-04-01 12:00', 'T0001', 'U0001', 150.00, N'Hoàn thành', N'Đã thanh toán'),
('O0002', N'2025-04-01 12:30', 'T0002', 'U0002', 120.00, N'Hoàn thành', 'Đã thanh toán'),
('O0003', N'2025-04-01 13:00', 'T0003', 'U0003', 180.00, N'Chờ', N'Chưa thanh toán'),
('O0004', N'2025-04-01 13:30', 'T0004', 'U0004', 130.00, N'Chờ', N'Chưa thanh toán'),
('O0005', N'2025-04-01 14:00', 'T0005', 'U0005', 210.00, N'Hoàn thành', N'Đã thanh toán');


INSERT INTO OrderItem (orderItemId, orderId, productId, quantity, unitPrice) VALUES
('I0001', 'O0001', 'P0001', 2, 50.00),  -- Bia Tiger
('I0002', 'O0001', 'P0005', 1, 28.00),  -- Trà sữa truyền thống
('I0003', 'O0002', 'P0006', 2, 25.00),  -- Cà phê sữa
('I0004', 'O0002', 'P0003', 1, 15.00),  -- Nước ngọt Coca Cola
('I0005', 'O0003', 'P0007', 2, 18.00) -- Trà xanh



CREATE TRIGGER trg_AutoGenerate_CategoryID
ON ProductCategory
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @nextId VARCHAR(10)

    -- Lấy mã ID tiếp theo trong dạng C003, C004...
    SELECT @nextId = ISNULL(
        'C' + RIGHT('0000' + CAST(CAST(SUBSTRING(MAX(categoryId), 2, 4) AS INT) + 1 AS VARCHAR), 4),
        'C0001'  -- Nếu bảng rỗng, bắt đầu từ C0001
    )
    FROM ProductCategory

    -- Chèn dữ liệu mới với ID tự sinh
    INSERT INTO ProductCategory (categoryId, name)
    SELECT @nextId, name FROM inserted
END

CREATE TRIGGER trg_AutoGenerate_ProductID
ON Product
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @nextId VARCHAR(10)

    -- Lấy mã ID tiếp theo trong dạng P003, P004...
    SELECT @nextId = ISNULL(
        'P' + RIGHT('0000' + CAST(CAST(SUBSTRING(MAX(productId), 2, 4) AS INT) + 1 AS VARCHAR), 4),
        'P0001'  -- Nếu bảng rỗng, bắt đầu từ P0001
    )
    FROM Product

    -- Chèn dữ liệu mới với ID tự sinh
    INSERT INTO Product (productId, name, purchasePrice, sellingPrice, categoryId, supplierId)
    SELECT @nextId, name, purchasePrice, sellingPrice, categoryId, supplierId FROM inserted
END

CREATE TRIGGER trg_AutoGenerate_SupplierID
ON Supplier
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @nextId VARCHAR(10)

    SELECT @nextId = ISNULL(
        'S' + RIGHT('0000' + CAST(CAST(SUBSTRING(MAX(supplierId), 2, 4) AS INT) + 1 AS VARCHAR), 4),
        'S0001'
    )
    FROM Supplier

    INSERT INTO Supplier (supplierId, name, address)
    SELECT @nextId, name, address FROM inserted
END

CREATE TRIGGER trg_AutoGenerate_TableID
ON Tables
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @nextId VARCHAR(10)

    SELECT @nextId = ISNULL(
        'T' + RIGHT('0000' + CAST(CAST(SUBSTRING(MAX(tableId), 2, 4) AS INT) + 1 AS VARCHAR), 4),
        'T0001'
    )
    FROM Tables

    INSERT INTO Tables (tableId, name, status, capacity)
    SELECT @nextId, name, status, capacity FROM inserted
END

CREATE TRIGGER trg_AutoGenerate_UserID
ON Users
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @nextId VARCHAR(10)

    SELECT @nextId = ISNULL(
        'U' + RIGHT('0000' + CAST(CAST(SUBSTRING(MAX(userId), 2, 4) AS INT) + 1 AS VARCHAR), 4),
        'U0001'
    )
    FROM Users

    INSERT INTO Users (userId, userName, password, type)
    SELECT @nextId, userName, password, type FROM inserted
END


CREATE TRIGGER trg_AutoGenerate_OrderID
ON Orders
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @nextId VARCHAR(10)

    SELECT @nextId = ISNULL(
        'O' + RIGHT('0000' + CAST(CAST(SUBSTRING(MAX(orderId), 2, 4) AS INT) + 1 AS VARCHAR), 4),
        'O0001'
    )
    FROM Orders

    INSERT INTO Orders (orderId, orderDate, tableId, userId, totalAmount, status, paymentStatus)
    SELECT @nextId, orderDate, tableId, userId, totalAmount, status, paymentStatus FROM inserted
END

CREATE TRIGGER trg_AutoGenerate_OrderItemID
ON OrderItem
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @nextId VARCHAR(10)

    SELECT @nextId = ISNULL(
        'I' + RIGHT('0000' + CAST(CAST(SUBSTRING(MAX(orderItemId), 2, 4) AS INT) + 1 AS VARCHAR), 4),
        'I0001'
    )
    FROM OrderItem

    INSERT INTO OrderItem (orderItemId, orderId, productId, quantity, unitPrice)
    SELECT @nextId, orderId, productId, quantity, unitPrice FROM inserted
END

-- Chèn dữ liệu vào bảng ProductCategory
delete from Product
where productId = 'P0009'

-- Chèn dữ liệu vào bảng Product
INSERT INTO Product (name, purchasePrice, sellingPrice, categoryId, supplierId) 
VALUES (N'Sinh tố bơ', 2000, 10000, 'C0002', 'S0003');
INSERT INTO Product (name, purchasePrice, sellingPrice, categoryId, supplierId) 
VALUES (N'Sinh tố dâu', 2000, 10000, 'C0002', 'S0003');

SELECT * FROM ProductCategory;
SELECT * FROM Product;

