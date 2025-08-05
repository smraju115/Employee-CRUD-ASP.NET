-- Insert 
CREATE PROCEDURE spInsertEmployee
    @Name NVARCHAR(100),
    @Email NVARCHAR(100),
    @Phone NVARCHAR(20),
	@Address NVARCHAR(200),
    @Designation NVARCHAR(100),
    @Salary DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Employees (Name, Email, Phone, Address, Designation, Salary)
    VALUES (@Name, @Email, @Phone, @Address, @Designation, @Salary)
END
GO
-- Get All Employee

CREATE PROCEDURE spGetAllEmployees
AS
BEGIN
    SELECT * FROM Employees
END
GO

-- Get By Id
CREATE PROCEDURE spGetEmployeeById
    @EmployeeId INT
AS
BEGIN
    SELECT * FROM Employees WHERE EmployeeId = @EmployeeId
END
GO

-- Update
CREATE PROCEDURE spUpdateEmployee
    @EmployeeId INT,
    @Name NVARCHAR(100),
    @Email NVARCHAR(100),
    @Phone NVARCHAR(20),
	@Address NVARCHAR(200),
    @Designation NVARCHAR(100),
    @Salary DECIMAL(10,2)
AS
BEGIN
    UPDATE Employees
    SET Name = @Name,
        Email = @Email,
        Phone = @Phone,
		Address = @Address,
        Designation = @Designation,
        Salary = @Salary
    WHERE EmployeeId = @EmployeeId
END
GO

--Delete
CREATE PROCEDURE spDeleteEmployee
    @EmployeeId INT
AS
BEGIN
    DELETE FROM Employees WHERE EmployeeId = @EmployeeId
END
GO