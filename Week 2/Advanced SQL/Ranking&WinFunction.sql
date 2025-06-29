CREATE DATABASE ProductDB;
GO

USE ProductDB;
GO

CREATE TABLE Products (
    ProductID INT PRIMARY KEY,
    ProductName VARCHAR(100),
    Category VARCHAR(50),
    Price DECIMAL(10,2)
);
GO

INSERT INTO Products (ProductID, ProductName, Category, Price) VALUES
(1, 'iPhone 14', 'Electronics', 999.99),
(2, 'Samsung TV', 'Electronics', 750.00),
(3, 'Sony Headphones', 'Electronics', 199.99),
(4, 'Laptop Dell', 'Electronics', 800.00),
(5, 'T-Shirt', 'Clothing', 19.99),
(6, 'Jacket', 'Clothing', 89.99),
(7, 'Jeans', 'Clothing', 49.99),
(8, 'Shoes', 'Clothing', 89.99),
(9, 'Oven', 'Appliances', 149.99),
(10, 'Washing Machine', 'Appliances', 499.99),
(11, 'Fridge', 'Appliances', 599.99),
(12, 'Toaster', 'Appliances', 49.99);


SELECT 
    Category,
    ProductName,
    Price,
    ROW_NUMBER() OVER(PARTITION BY Category ORDER BY Price DESC) AS RowNum,
    RANK() OVER(PARTITION BY Category ORDER BY Price DESC) AS PriceRank,
    DENSE_RANK() OVER(PARTITION BY Category ORDER BY Price DESC) AS DensePriceRank
FROM Products;

SELECT *
FROM (
    SELECT 
        Category,
        ProductName,
        Price,
        ROW_NUMBER() OVER(PARTITION BY Category ORDER BY Price DESC) AS RowNum
    FROM Products
) AS RankedProducts
WHERE RowNum <= 3;
