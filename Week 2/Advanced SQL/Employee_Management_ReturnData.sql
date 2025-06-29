USE EmployeeDB;
GO

IF OBJECT_ID('sp_GetEmployeesByDepartment', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetEmployeesByDepartment;
GO

CREATE PROCEDURE sp_GetEmployeesByDepartment
    @DeptID INT
AS
BEGIN
    SELECT 
        E.EmployeeID,
        E.FirstName,
        E.LastName,
        E.Salary,
        E.JoinDate,
        D.DepartmentName
    FROM Employees E
    INNER JOIN Departments D ON E.DepartmentID = D.DepartmentID
    WHERE E.DepartmentID = @DeptID;
END;
GO

EXEC sp_GetEmployeesByDepartment @DeptID = 2;
