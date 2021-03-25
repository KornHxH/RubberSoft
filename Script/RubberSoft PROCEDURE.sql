
if exists (select * from sysobjects where id = object_id(N'[spt_GetRoles]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spt_GetRoles
GO
Create PROCEDURE spt_GetRoles
	--@IDCard nvarchar(50)
AS
BEGIN

SELECT 0 AS RoleId, '' AS RoleName, '--- เลือกสิทธิ์การใช้งาน ---' AS RoleFullName, 1 AS Active
UNION
SELECT RoleId, RoleName, RoleFullName, Active FROM UserRole

END
GO

if exists (select * from sysobjects where id = object_id(N'[spt_GetUsers]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spt_GetUsers
GO
Create PROCEDURE spt_GetUsers
	--@itemCode nvarchar(250)
AS
BEGIN

SELECT UserId, UserTypeId, RoleName, UserName, Password, FirstName, LastName, t1.Active 
FROM Users t1
INNER JOIN UserRole t2 ON t1.UserTypeId = t2.RoleId
ORDER BY UserId

END
GO

if exists (select * from sysobjects where id = object_id(N'[spt_GetOptions]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spt_GetOptions
GO
Create PROCEDURE spt_GetOptions
	--@OptionID int
AS
BEGIN

SELECT OptionID, TerminalId, TerminalName, OptionName, OptionValue, DateValue, TimeValue FROM Options

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_AddOption]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_AddOption
GO
Create PROCEDURE spt_AddOption
@TerminalId int,
@TerminalName nvarchar(250),
@OptionName nvarchar(250),
@OptionValue nvarchar(250),
@DateValue date,
@TimeValue time(7)

AS
BEGIN

    SET NOCOUNT ON;

   INSERT INTO Options (TerminalId, TerminalName, OptionName, OptionValue, DateValue, TimeValue)
                   VALUES (@TerminalId, @TerminalName, @OptionName, @OptionValue, @DateValue, @TimeValue)

    SELECT SCOPE_IDENTITY() AS OptionID

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_UpdateOptions]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_UpdateOptions
GO
Create PROCEDURE spt_UpdateOptions
@OptionID int,
@TerminalId int,
@TerminalName nvarchar(250),
@OptionName nvarchar(250),
@OptionValue nvarchar(250),
@DateValue date,
@TimeValue time(7)

AS
BEGIN

    SET NOCOUNT ON;

     UPDATE Options SET TerminalId=@TerminalId, TerminalName=@TerminalName, OptionName=@OptionName, OptionValue=@OptionValue, 
	 DateValue=@DateValue, TimeValue=@TimeValue WHERE OptionID=@OptionID

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_AddLog]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_AddLog
GO
Create PROCEDURE spt_AddLog
  @LogTypeId int,
  @LogName nvarchar(250),
  @LogDetail nvarchar(500),
  @UserId int,
  @IPMachine nvarchar(150),
  @MachineName nvarchar(150)

AS
BEGIN

    SET NOCOUNT ON;

   INSERT INTO LogDetails (LogTypeId, LogName, LogDetail, UserId, IPMachine, MachineName, LogDate)
     VALUES (@LogTypeId, @LogName, @LogDetail, @UserId, @IPMachine, @MachineName, GETDATE())

    SELECT SCOPE_IDENTITY() AS LogId

END
GO

if exists (select * from sysobjects where id = object_id(N'[spt_GetLogDetails]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spt_GetLogDetails
GO
Create PROCEDURE spt_GetLogDetails
	--@LogDate Datetime
AS
BEGIN

SELECT LogId, LogTypeId, LogName, LogDetail, UserId, IPMachine, MachineName, LogDate 
FROM LogDetails 
--WHERE LogDate = @LogDate

END
GO

if exists (select * from sysobjects where id = object_id(N'[spt_GetTerminal]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spt_GetTerminal
GO
Create PROCEDURE spt_GetTerminal
	--@LogDate Datetime
AS
BEGIN

SELECT TerminalId, TerminalName, IPMachine, MachineName, Active FROM Terminal

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_AddTerminal]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_AddTerminal
GO
Create PROCEDURE spt_AddTerminal
@TerminalName nvarchar(250),
@IPMachine nvarchar(250),
@MachineName nvarchar(250),
@Active bit

AS
BEGIN

    SET NOCOUNT ON;
	--TRUNCATE TABLE Terminal
   INSERT INTO Terminal (TerminalName, IPMachine, MachineName, CreateDate, Active)
                   VALUES (@TerminalName, @IPMachine, @MachineName, GETDATE(), @Active)

    SELECT SCOPE_IDENTITY() AS TerminalId

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_UpdateTerminal]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_UpdateTerminal
GO
Create PROCEDURE spt_UpdateTerminal

@TerminalId int,
@IPMachine nvarchar(250),
@Active bit

AS
BEGIN

    SET NOCOUNT ON;

     UPDATE Terminal SET IPMachine=@IPMachine, Active=@Active WHERE TerminalId=@TerminalId

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_AddUsers]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_AddUsers
GO
Create PROCEDURE spt_AddUsers
@UserTypeId int,
@UserName nvarchar(250),
@Password nvarchar(250),
@FirstName nvarchar(250),
@LastName nvarchar(250),
@Active bit

AS
BEGIN

    SET NOCOUNT ON;

   INSERT INTO Users (UserTypeId, UserName, Password, FirstName, LastName, Active)
                   VALUES (@UserTypeId, @UserName, @Password, @FirstName, @LastName, @Active)

    SELECT SCOPE_IDENTITY() AS UserId

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_UpdateUsers]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_UpdateUsers
GO
Create PROCEDURE spt_UpdateUsers

@UserId int,
@UserTypeId int,
@UserName nvarchar(250),
@Password nvarchar(250),
@FirstName nvarchar(250),
@LastName nvarchar(250),
@Active bit

AS
BEGIN

    SET NOCOUNT ON;

     UPDATE Users SET UserTypeId=@UserTypeId, UserName=@UserName, Password=@Password, FirstName=@FirstName, LastName=@LastName, Active=@Active 
	 WHERE UserId=@UserId

END
GO

if exists (select * from sysobjects where id = object_id(N'[spt_GetCustomerTypes]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spt_GetCustomerTypes
GO
Create PROCEDURE spt_GetCustomerTypes
	--@LogDate Datetime
AS
BEGIN

SELECT CustomerTypeId, CustomerTypeName, Active, InsertDate, InsertBy, 
UpdateDate, UpdateBy, DeleteDate, DeleteBy
FROM CustomerTypes
WHERE Active = 1 ORDER By CustomerTypeId

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_AddCustomerTypes]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_AddCustomerTypes
GO
Create PROCEDURE spt_AddCustomerTypes

--@CustomerTypeId int, 
@CustomerTypeName nvarchar(250),
@InsertBy int

AS
BEGIN

    SET NOCOUNT ON;

   INSERT INTO CustomerTypes (CustomerTypeName, Active, InsertDate, InsertBy)
                 VALUES (@CustomerTypeName, 1, GETDATE(), @InsertBy)

    SELECT SCOPE_IDENTITY() AS CustomerTypeId

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_UpdateCustomerTypes]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_UpdateCustomerTypes
GO
Create PROCEDURE spt_UpdateCustomerTypes

@CustomerTypeId int, 
@CustomerTypeName nvarchar(250),
@UpdateBy int

AS
BEGIN

    SET NOCOUNT ON;

     UPDATE CustomerTypes SET CustomerTypeName=@CustomerTypeName, UpdateDate=GETDATE(), UpdateBy=@UpdateBy 
	 WHERE CustomerTypeId=@CustomerTypeId

END
GO

if exists (select * from sysobjects where id = object_id(N'[spt_GetCustomer]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spt_GetCustomer
GO
Create PROCEDURE spt_GetCustomer
	--@LogDate Datetime
AS
BEGIN

SELECT CustomerId, A.CustomerTypeId, CustomerTypeName, CustomerCode, CustomerName, CustomerAddress, LicensePlate, Phone
FROM Customer A
LEFT OUTER JOIN CustomerTypes B ON A.CustomerTypeId = B.CustomerTypeId
WHERE A.Active = 1 
ORDER BY A.CustomerTypeId, CustomerId

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_AddCustomer]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_AddCustomer
GO
Create PROCEDURE spt_AddCustomer

--@CustomerId int, 
@CustomerTypeId int,
@CustomerCode nvarchar(250),
@CustomerName nvarchar(250),
@CustomerAddress nvarchar(500),
@LicensePlate nvarchar(250),
@Phone nvarchar(250),
@InsertBy int

AS
BEGIN

    SET NOCOUNT ON;

   INSERT INTO Customer (CustomerTypeId, CustomerCode, CustomerName, CustomerAddress, LicensePlate, Phone, Active, InsertDate, InsertBy)
                 VALUES (@CustomerTypeId, @CustomerCode, @CustomerName, @CustomerAddress, @LicensePlate, @Phone, 1, GETDATE(), @InsertBy)

    SELECT SCOPE_IDENTITY() AS CustomerId

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_UpdateCustomer]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_UpdateCustomer
GO
Create PROCEDURE spt_UpdateCustomer

@CustomerId int, 
@CustomerTypeId int,
@CustomerCode nvarchar(250),
@CustomerName nvarchar(250),
@CustomerAddress nvarchar(500),
@LicensePlate nvarchar(250),
@Phone nvarchar(250),
@UpdateBy int

AS
BEGIN

    SET NOCOUNT ON;

     UPDATE Customer SET CustomerTypeId=@CustomerTypeId, CustomerCode=@CustomerCode, CustomerName=@CustomerName, CustomerAddress=@CustomerAddress, 
	 LicensePlate=@LicensePlate, Phone=@Phone, UpdateDate=GETDATE(), UpdateBy=@UpdateBy 
	 WHERE CustomerId=@CustomerId

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_DeleteCustomer]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_DeleteCustomer
GO
Create PROCEDURE spt_DeleteCustomer

@CustomerId int,
@DeleteBy int

AS
BEGIN

    SET NOCOUNT ON;

     UPDATE Customer SET Active = 0, DeleteDate = GETDATE(), DeleteBy = @DeleteBy WHERE CustomerId=@CustomerId

END
GO

if exists (select * from sysobjects where id = object_id(N'[spt_GetCustomerPrice]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spt_GetCustomerPrice
GO
Create PROCEDURE spt_GetCustomerPrice
	--@LogDate Datetime
AS
BEGIN

SELECT PriceId, A.CustomerId, CustomerName, SaleDate, SalePriceAdvance, WeightAmount, DeliveryPrice, 
A.Active, A.InsertDate, A.UpdateDate
FROM CustomerPrice A
INNER JOIN Customer B ON A.CustomerId = B.CustomerId
WHERE A.Active = 1 
ORDER BY PriceId

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_AddCustomerPrice]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_AddCustomerPrice
GO
Create PROCEDURE spt_AddCustomerPrice

--@PriceId int, 
@CustomerId int,
@SaleDate datetime,
@SalePriceAdvance money,
@WeightAmount float,
@DeliveryPrice money

AS
BEGIN

    SET NOCOUNT ON;

   INSERT INTO CustomerPrice (CustomerId, SaleDate, SalePriceAdvance, WeightAmount, DeliveryPrice, Active, InsertDate)
                 VALUES (@CustomerId, @SaleDate, @SalePriceAdvance, @WeightAmount, @DeliveryPrice, 1, GETDATE())

    SELECT SCOPE_IDENTITY() AS PriceId

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_UpdateCustomerPrice]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_UpdateCustomerPrice
GO
Create PROCEDURE spt_UpdateCustomerPrice

@PriceId int, 
@CustomerId int,
@SaleDate datetime,
@SalePriceAdvance money,
@WeightAmount float,
@DeliveryPrice money

AS
BEGIN

    SET NOCOUNT ON;

     UPDATE CustomerPrice SET CustomerId=@CustomerId, SaleDate=@SaleDate, SalePriceAdvance=@SalePriceAdvance, WeightAmount=@WeightAmount, 
	 DeliveryPrice=@DeliveryPrice, UpdateDate=GETDATE()
	 WHERE PriceId=@PriceId

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_DeleteCustomerPrice]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_DeleteCustomerPrice
GO
Create PROCEDURE spt_DeleteCustomerPrice

@PriceId int

AS
BEGIN

    SET NOCOUNT ON;

     DELETE CustomerPrice WHERE PriceId=@PriceId

END
GO

if exists (select * from sysobjects where id = object_id(N'[spt_GetBuyNumber]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spt_GetBuyNumber
GO
Create PROCEDURE spt_GetBuyNumber
	@BuyDate Datetime
AS
BEGIN

SELECT ReceiptNo FROM (
SELECT OptionValue + PreFix + SUBSTRING(CONVERT(nvarchar, BuyNumber + 10000000), 2, 8) ReceiptNo 
FROM (
SELECT COUNT(BuyNumber) + 1 AS BuyNumber, 
CONVERT(VARCHAR, FORMAT(@BuyDate, 'yyyy', 'Th-th')) + CONVERT(VARCHAR, FORMAT(@BuyDate, 'MM')) AS PreFix
FROM Buy A
WHERE YEAR(BuyDate) = YEAR(@BuyDate) AND MONTH(BuyDate) = MONTH(@BuyDate)
) BUYRC
CROSS JOIN Options B WHERE OptionID = 1
) Buy

END
GO

if exists (select * from sysobjects where id = object_id(N'[spt_GetBuy]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spt_GetBuy
GO
Create PROCEDURE spt_GetBuy
	@Active bit
AS
BEGIN

SELECT BuyId, BuyNumber, CustomerId, CustomerName, BuyDate, SubTotal, DownValue, NetTotal, SetOffValue, ValueBalance, ProductTypeId, IsFinalze, Active
FROM Buy
WHERE Active = @Active

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_AddBuy]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_AddBuy
GO
Create PROCEDURE spt_AddBuy

--@BuyId int, 
@BuyNumber nvarchar(150),
@CustomerId int, 
@CustomerName nvarchar(250),
@BuyDate datetime,
@SubTotal money,
@DownValue money,
@NetTotal money,
@SetOffValue money,
@ValueBalance money,
@ProductTypeId int, 
@IsFinalze bit, 
@InsertBy int

AS
BEGIN

    SET NOCOUNT ON;

   INSERT INTO Buy (BuyNumber, CustomerId, CustomerName, BuyDate, SubTotal, DownValue, NetTotal, SetOffValue, ValueBalance, ProductTypeId, IsFinalze, Active, InsertDate, InsertBy)
                 VALUES (@BuyNumber, @CustomerId, @CustomerName, @BuyDate, @SubTotal, @DownValue, @NetTotal, @SetOffValue, @ValueBalance, @ProductTypeId, @IsFinalze, 1, GETDATE(), @InsertBy)

    SELECT SCOPE_IDENTITY() AS BuyId

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_UpdateBuy]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_UpdateBuy
GO
Create PROCEDURE spt_UpdateBuy

@BuyId int, 
@BuyNumber nvarchar(150),
@CustomerId int, 
@CustomerName nvarchar(250),
@BuyDate datetime,
@SubTotal money,
@DownValue money,
@NetTotal money,
@SetOffValue money,
@ValueBalance money,
@ProductTypeId int, 
@IsFinalze bit, 
@UpdateBy int

AS
BEGIN

    SET NOCOUNT ON;

     UPDATE Buy SET BuyNumber=@BuyNumber, CustomerId=@CustomerId, CustomerName=@CustomerName, BuyDate=@BuyDate, SubTotal=@SubTotal, DownValue=@DownValue, NetTotal=@NetTotal, 
	 SetOffValue=@SetOffValue, ValueBalance=@ValueBalance, ProductTypeId=@ProductTypeId, IsFinalze=@IsFinalze, UpdateDate=GETDATE(), UpdateBy=@UpdateBy
	 WHERE BuyId=@BuyId

END
GO

if exists (select * from sysobjects where id = object_id(N'[spt_GetBuyProduct]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spt_GetBuyProduct
GO
Create PROCEDURE spt_GetBuyProduct
	@Active bit
AS
BEGIN

SELECT BuyProductId, BuyId, PriceId, [Percentage], WeightAmount, WeightAmount_Plate, Drc, TotalPrice_Smoke, WeightAmount_Raw,  
TotalPrice_Raw, CalRubber, TotalPrice, Remark, Active, InsertDate, InsertBy, UpdateDate, UpdateBy, DeleteDate, DeleteBy
FROM BuyProduct
WHERE Active = @Active

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_AddBuyProduct]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_AddBuyProduct
GO
Create PROCEDURE spt_AddBuyProduct

@BuyId int, 
@PriceId int, 
@Percentage money,
@WeightAmount money, 
@WeightAmount_Plate money,
@Drc money,
@TotalPrice_Smoke money,
@WeightAmount_Raw money,
@TotalPrice_Raw money,
@CalRubber money,
@TotalPrice money,
@Remark nvarchar(500),
@InsertBy int

AS
BEGIN

    SET NOCOUNT ON;

   INSERT INTO BuyProduct (BuyId, PriceId, [Percentage], WeightAmount, WeightAmount_Plate, Drc, TotalPrice_Smoke, WeightAmount_Raw,  
                           TotalPrice_Raw, CalRubber, TotalPrice, Remark, Active, InsertDate, InsertBy)
                 VALUES (@BuyId, @PriceId, @Percentage, @WeightAmount, @WeightAmount_Plate, @Drc, @TotalPrice_Smoke, @WeightAmount_Raw,  
                           @TotalPrice_Raw, @CalRubber, @TotalPrice, @Remark, 1, GETDATE(), @InsertBy)

    SELECT SCOPE_IDENTITY() AS BuyProductId

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_UpdateBuyProduct]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_UpdateBuyProduct
GO
Create PROCEDURE spt_UpdateBuyProduct

@BuyProductId int, 
@BuyId int, 
@PriceId int, 
@Percentage money,
@WeightAmount money, 
@WeightAmount_Plate money,
@Drc money,
@TotalPrice_Smoke money,
@WeightAmount_Raw money,
@TotalPrice_Raw money,
@CalRubber money,
@TotalPrice money,
@Remark nvarchar(500),
@UpdateBy int

AS
BEGIN

    SET NOCOUNT ON;

     UPDATE BuyProduct SET BuyId=@BuyId, PriceId=@PriceId, [Percentage]=@Percentage, WeightAmount=@WeightAmount, WeightAmount_Plate=@WeightAmount_Plate, Drc=@Drc, TotalPrice_Smoke=@TotalPrice_Smoke, 
	 WeightAmount_Raw=@WeightAmount_Raw, TotalPrice_Raw=@TotalPrice_Raw, CalRubber=@CalRubber, TotalPrice=@TotalPrice, Remark=@Remark, UpdateDate=GETDATE(), UpdateBy=@UpdateBy
	 WHERE BuyProductId=@BuyProductId

END
GO

if exists (select * from sysobjects where id = object_id(N'[spt_GetTransactionLog]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spt_GetTransactionLog
GO
Create PROCEDURE spt_GetTransactionLog
	--@LogDate Datetime
AS
BEGIN

SELECT LogId, CustomerId, PriceId, WeightAmount_Raw, WeightBalanceAmt, RefId, LogTypeId 
FROM TransactionLog

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_AddTransactionLog]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_AddTransactionLog
GO
Create PROCEDURE spt_AddTransactionLog

--@LogId int, 
@CustomerId int,
@PriceId int,
@WeightAmount_Raw money,
@WeightBalanceAmt money,
@RefId int,
@LogTypeId int

AS
BEGIN

    SET NOCOUNT ON;

   INSERT INTO TransactionLog (CustomerId, PriceId, WeightAmount_Raw, WeightBalanceAmt, RefId, LogTypeId, InsertDate)
                 VALUES (@CustomerId, @PriceId, @WeightAmount_Raw, @WeightBalanceAmt, @RefId, @LogTypeId, GETDATE())

    SELECT SCOPE_IDENTITY() AS LogId

END
GO

if exists (select * from sysobjects where id = object_id(N'[spt_GetWeightBalance]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spt_GetWeightBalance
GO
Create PROCEDURE spt_GetWeightBalance
	--@LogDate Datetime
AS
BEGIN

SELECT WeightBalanceId, CustomerId, PriceId, BeginBalanceAmt, WeightBalanceAmt, LastLogId 
FROM WeightBalance

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_AddWeightBalance]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_AddWeightBalance
GO
Create PROCEDURE spt_AddWeightBalance

--@WeightBalanceId int, 
@CustomerId int,
@PriceId int,
@BeginBalanceAmt money,
@WeightBalanceAmt money,
@LastLogId int

AS
BEGIN

    SET NOCOUNT ON;

   INSERT INTO WeightBalance (CustomerId, PriceId, BeginBalanceAmt, WeightBalanceAmt, LastLogId, UpdateDate)
                 VALUES (@CustomerId, @PriceId, @BeginBalanceAmt, @WeightBalanceAmt, @LastLogId, GETDATE())

    SELECT SCOPE_IDENTITY() AS WeightBalanceId

END
GO

if exists (select * from sysobjects where id = object_id(N'[spt_GetBuyWeightBalance]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spt_GetBuyWeightBalance
GO
Create PROCEDURE spt_GetBuyWeightBalance
	@CustomerId int
AS
BEGIN

SELECT PriceId, CustomerId, sName, SaleDate, SalePriceAdvance, WeightAmount, BeginAmt, 
                            DeliveryPrice, BeginBalanceAmt, WeightBalanceAmt, LastLogId, Active
                            FROM (
SELECT A.PriceId, A.CustomerId, A.PriceId AS sName, SaleDate, SalePriceAdvance, WeightAmount, DeliveryPrice, 
                            ISNULL(BeginBalanceAmt, 0) BeginBalanceAmt, ISNULL(WeightBalanceAmt, 0) WeightBalanceAmt, 
							ISNULL(WeightBalanceAmt, 0) AS BeginAmt, LastLogId, A.Active
                            FROM CustomerPrice A
                            INNER JOIN Customer B ON A.CustomerId = B.CustomerId
							LEFT OUTER JOIN (

							SELECT DD.WeightBalanceId, DD.PriceId, BeginBalanceAmt, WeightBalanceAmt, LastLogId FROM (
							SELECT MAX(WeightBalanceId) AS WeightBalanceId, PriceId FROM WeightBalance 
							WHERE CustomerId = @CustomerId 
							GROUP BY WeightBalanceId, PriceId) DT
							INNER JOIN WeightBalance DD ON Dt.WeightBalanceId = DD.WeightBalanceId

							) C ON A.PriceId = C.PriceId
                            WHERE A.Active = 1 AND A.CustomerId = @CustomerId

) WeightBalance

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_UpdateWeightBalance]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_UpdateWeightBalance
GO
Create PROCEDURE spt_UpdateWeightBalance

@CustomerId int,  
@PriceId int, 
@BeginBalanceAmt money, 
@WeightBalanceAmt money,  
@LastLogId int

AS
BEGIN

    SET NOCOUNT ON;

   UPDATE A SET A.BeginBalanceAmt = @BeginBalanceAmt, 
                A.WeightBalanceAmt = @WeightBalanceAmt, 
				A.LastLogId = @LastLogId, A.UpdateDate=GETDATE()
                FROM WeightBalance A
                WHERE A.CustomerId = @CustomerId AND A.PriceId = @PriceId

END
GO

if exists (select * from sysobjects where id = object_id(N'[spt_GetOutStandingLog]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spt_GetOutStandingLog
GO
Create PROCEDURE spt_GetOutStandingLog
	--@LogDate Datetime
AS
BEGIN

SELECT OutStandingLogId, CustomerId, OutstandingDebt, RefId, OutStandingStatus, LogTypeId 
FROM OutStandingLog

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_AddOutStandingLog]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_AddOutStandingLog
GO
Create PROCEDURE spt_AddOutStandingLog

--@LogId int, 
@CustomerId int,
@OutstandingDebt money,
@RefId int,
@OutStandingStatus int, 
@LogTypeId int

AS
BEGIN

    SET NOCOUNT ON;

   INSERT INTO OutStandingLog (CustomerId, OutstandingDebt, RefId, OutStandingStatus, LogTypeId , InsertDate)
                 VALUES (@CustomerId, @OutstandingDebt, @RefId, @OutStandingStatus, @LogTypeId, GETDATE())

    SELECT SCOPE_IDENTITY() AS OutStandingLogId

END
GO

if exists (select * from sysobjects where id = object_id(N'[spt_GetOutStandingBalance]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spt_GetOutStandingBalance
GO
Create PROCEDURE spt_GetOutStandingBalance
	--@LogDate Datetime
AS
BEGIN

SELECT OutStandingBalanceId, CustomerId, BeginDebt, OutstandingDebt, LastLogId 
FROM OutStandingBalance

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_AddOutStandingBalance]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_AddOutStandingBalance
GO
Create PROCEDURE spt_AddOutStandingBalance

--@WeightBalanceId int, 
@CustomerId int,
@BeginDebt money,
@OutstandingDebt money,
@LastLogId int

AS
BEGIN

    SET NOCOUNT ON;

   INSERT INTO OutStandingBalance (CustomerId, BeginDebt, OutstandingDebt, LastLogId, UpdateDate)
                 VALUES (@CustomerId, @BeginDebt, @OutstandingDebt, @LastLogId, GETDATE())

    SELECT SCOPE_IDENTITY() AS OutStandingBalanceId

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_UpdateOutStandingBalance]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_UpdateOutStandingBalance
GO
Create PROCEDURE spt_UpdateOutStandingBalance

@CustomerId int,  
@BeginDebt money, 
@OutstandingDebt money,  
@LastLogId int

AS
BEGIN

    SET NOCOUNT ON;

   UPDATE A SET A.BeginDebt = @BeginDebt,  
                A.OutstandingDebt = @OutstandingDebt, 
				A.LastLogId = @LastLogId, A.UpdateDate=GETDATE()
                FROM OutStandingBalance A
                WHERE A.CustomerId = @CustomerId

END
GO

if exists (select * from sysobjects where id = object_id(N'[spt_GetBuyBill]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spt_GetBuyBill
GO
Create PROCEDURE spt_GetBuyBill
	@BuyId int
AS
BEGIN

SELECT BuyId, BuyNumber, BuyDate, CustomerId, CustomerName, CustomerAddress, LicensePlate, Phone, 
ValueBalance, SubTotal, DownValue, SetOffValue, NetTotal, ProductTypeId, IsFinalze, Active,
BuyProductId, PriceId, [Percentage], WeightAmount, WeightAmount_Plate, Drc, TotalPrice_Smoke, WeightAmount_Raw,  
TotalPrice_Raw, CalRubber, TotalPrice, Remark
FROM (

SELECT A.BuyId, BuyNumber, BuyDate, A.CustomerId, A.CustomerName, CustomerAddress, LicensePlate, Phone, 
ValueBalance, SubTotal, DownValue, SetOffValue, NetTotal, ProductTypeId, IsFinalze, A.Active,
BuyProductId, PriceId, [Percentage], WeightAmount, WeightAmount_Plate, Drc, TotalPrice_Smoke, WeightAmount_Raw,  
TotalPrice_Raw, CalRubber, TotalPrice, Remark
FROM Buy A
INNER JOIN Customer B ON A.CustomerId = B.CustomerId
INNER JOIN BuyProduct C ON A.BuyId = C.BuyId

) Buy
WHERE BuyId = @BuyId

END
GO
-- รายงาน By nart

if exists (select * from sysobjects where id = object_id(N'[rpt_BuyByDate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure rpt_BuyByDate
GO
Create PROCEDURE rpt_BuyByDate
	@StartDate datetime,
	@EndDate datetime,
	@ProductTypeId int
AS
BEGIN

SELECT BuyId, BuyNumber, BuyDate, CustomerId, CustomerName, CustomerAddress, LicensePlate, Phone, 
ValueBalance, SubTotal, DownValue, SetOffValue, NetTotal, ProductTypeId, IsFinalze, Active,
BuyProductId, PriceId, [Percentage], WeightAmount, WeightAmount_Plate, Drc, TotalPrice_Smoke, WeightAmount_Raw,  
TotalPrice_Raw, CalRubber, TotalPrice, Remark
FROM (

SELECT A.BuyId, BuyNumber, BuyDate, A.CustomerId, A.CustomerName, CustomerAddress, LicensePlate, Phone, 
ValueBalance, SubTotal, DownValue, SetOffValue, NetTotal, ProductTypeId, IsFinalze, A.Active,
BuyProductId, PriceId, [Percentage], WeightAmount, WeightAmount_Plate, Drc, TotalPrice_Smoke, WeightAmount_Raw,  
TotalPrice_Raw, CalRubber, TotalPrice, Remark
FROM Buy A
INNER JOIN Customer B ON A.CustomerId = B.CustomerId
INNER JOIN BuyProduct C ON A.BuyId = C.BuyId

) Buy
WHERE BuyDate BETWEEN @StartDate AND @EndDate AND ProductTypeId =  @ProductTypeId

END
GO

if exists (select * from sysobjects where id = object_id(N'[rpt_BuyByMonth]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure rpt_BuyByMonth
GO
Create PROCEDURE rpt_BuyByMonth
	@StartDate datetime,
	@ProductTypeId int
AS
BEGIN

SELECT BuyId, BuyNumber, BuyDate, CustomerId, CustomerName, CustomerAddress, LicensePlate, Phone, 
ValueBalance, SubTotal, DownValue, SetOffValue, NetTotal, ProductTypeId, IsFinalze, Active,
BuyProductId, PriceId, [Percentage], WeightAmount, WeightAmount_Plate, Drc, TotalPrice_Smoke, WeightAmount_Raw,  
TotalPrice_Raw, CalRubber, TotalPrice, Remark
FROM (

SELECT A.BuyId, BuyNumber, BuyDate, A.CustomerId, A.CustomerName, CustomerAddress, LicensePlate, Phone, 
ValueBalance, SubTotal, DownValue, SetOffValue, NetTotal, ProductTypeId, IsFinalze, A.Active,
BuyProductId, PriceId, [Percentage], WeightAmount, WeightAmount_Plate, Drc, TotalPrice_Smoke, WeightAmount_Raw,  
TotalPrice_Raw, CalRubber, TotalPrice, Remark
FROM Buy A
INNER JOIN Customer B ON A.CustomerId = B.CustomerId
INNER JOIN BuyProduct C ON A.BuyId = C.BuyId

) Buy
WHERE YEAR(BuyDate) = YEAR(@StartDate) AND MONTH(BuyDate) = MONTH(@StartDate) 
AND ProductTypeId =  @ProductTypeId

END
GO

if exists (select * from sysobjects where id = object_id(N'[rpt_BuyByYear]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure rpt_BuyByYear
GO
Create PROCEDURE rpt_BuyByYear
	@StartDate datetime,
	@ProductTypeId int
AS
BEGIN

SELECT BuyId, BuyNumber, BuyDate, CustomerId, CustomerName, CustomerAddress, LicensePlate, Phone, 
ValueBalance, SubTotal, DownValue, SetOffValue, NetTotal, ProductTypeId, IsFinalze, Active,
BuyProductId, PriceId, [Percentage], WeightAmount, WeightAmount_Plate, Drc, TotalPrice_Smoke, WeightAmount_Raw,  
TotalPrice_Raw, CalRubber, TotalPrice, Remark
FROM (

SELECT A.BuyId, BuyNumber, BuyDate, A.CustomerId, A.CustomerName, CustomerAddress, LicensePlate, Phone, 
ValueBalance, SubTotal, DownValue, SetOffValue, NetTotal, ProductTypeId, IsFinalze, A.Active,
BuyProductId, PriceId, [Percentage], WeightAmount, WeightAmount_Plate, Drc, TotalPrice_Smoke, WeightAmount_Raw,  
TotalPrice_Raw, CalRubber, TotalPrice, Remark
FROM Buy A
INNER JOIN Customer B ON A.CustomerId = B.CustomerId
INNER JOIN BuyProduct C ON A.BuyId = C.BuyId

) Buy
WHERE YEAR(BuyDate) = YEAR(@StartDate) AND ProductTypeId =  @ProductTypeId

END
GO

-- รายงานการรับซื้อยางแผ่นดิบ
if exists (select * from sysobjects where id = object_id(N'[rpt_BuyByDate_2]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure rpt_BuyByDate_2
GO
Create PROCEDURE rpt_BuyByDate_2
	@StartDate datetime,
	@EndDate datetime,
	@ProductTypeId int
AS
BEGIN

select Customer.CustomerId,Customer.CustomerCode,Customer.CustomerName 
,Buy.BuyId,Buy.BuyNumber,buy.BuyDate,Buy.SubTotal,Buy.DownValue,buy.NetTotal,Buy.SetOffValue,Buy.ValueBalance
,BuyProduct.WeightAmount,BuyProduct.WeightAmount_Plate,BuyProduct.Drc,BuyProduct.TotalPrice_Smoke,BuyProduct.WeightAmount_Raw,BuyProduct.TotalPrice_Raw,BuyProduct.TotalPrice,BuyProduct.Remark
from Buy
inner join Customer on Customer.CustomerId = Buy.CustomerId
inner join BuyProduct on Buy.BuyId = BuyProduct.BuyId
where Buy.IsFinalze = 1 and Buy.Active = 1
and BuyDate BETWEEN @StartDate AND @EndDate AND ProductTypeId =  @ProductTypeId

END
GO


if exists (select * from sysobjects where id = object_id(N'[rpt_BuyByMonth_2]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure rpt_BuyByMonth_2
GO
Create PROCEDURE rpt_BuyByMonth_2
	@StartDate datetime,
	@ProductTypeId int
AS
BEGIN

select Customer.CustomerId,Customer.CustomerCode,Customer.CustomerName 
,Buy.BuyId,Buy.BuyNumber,buy.BuyDate,Buy.SubTotal,Buy.DownValue,buy.NetTotal,Buy.SetOffValue,Buy.ValueBalance
,BuyProduct.WeightAmount,BuyProduct.WeightAmount_Plate,BuyProduct.Drc,BuyProduct.TotalPrice_Smoke,BuyProduct.WeightAmount_Raw,BuyProduct.TotalPrice_Raw,BuyProduct.TotalPrice,BuyProduct.Remark
from Buy
inner join Customer on Customer.CustomerId = Buy.CustomerId
inner join BuyProduct on Buy.BuyId = BuyProduct.BuyId
where Buy.IsFinalze = 1 and Buy.Active = 1
AND YEAR(BuyDate) = YEAR(@StartDate) AND MONTH(BuyDate) = MONTH(@StartDate) 
AND ProductTypeId =  @ProductTypeId

END
GO

if exists (select * from sysobjects where id = object_id(N'[rpt_BuyByYear_2]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure rpt_BuyByYear_2
GO
Create PROCEDURE rpt_BuyByYear_2
	@StartDate datetime,
	@ProductTypeId int
AS
BEGIN

select Customer.CustomerId,Customer.CustomerCode,Customer.CustomerName 
,Buy.BuyId,Buy.BuyNumber,buy.BuyDate,Buy.SubTotal,Buy.DownValue,buy.NetTotal,Buy.SetOffValue,Buy.ValueBalance
,BuyProduct.WeightAmount,BuyProduct.WeightAmount_Plate,BuyProduct.Drc,BuyProduct.TotalPrice_Smoke,BuyProduct.WeightAmount_Raw,BuyProduct.TotalPrice_Raw,BuyProduct.TotalPrice,BuyProduct.Remark
from Buy
inner join Customer on Customer.CustomerId = Buy.CustomerId
inner join BuyProduct on Buy.BuyId = BuyProduct.BuyId
where Buy.IsFinalze = 1 and Buy.Active = 1
AND YEAR(BuyDate) = YEAR(@StartDate) AND ProductTypeId =  @ProductTypeId

END
GO

-- รายงานยอดคงเหลือรับซื้อยางพารา
if exists (select * from sysobjects where id = object_id(N'[rpt_BuyWeightBalance_ByDate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure rpt_BuyWeightBalance_ByDate
GO
Create PROCEDURE rpt_BuyWeightBalance_ByDate
	@StartDate datetime,
	@EndDate datetime
AS
BEGIN

select Customer.CustomerId,Customer.CustomerCode,Customer.CustomerName
,DATEADD(DD,DATEDIFF(DD,0,CustomerPrice.SaleDate),0) as SaleDate
,CustomerPrice.SalePriceAdvance, CustomerPrice.WeightAmount	as CP_WeightAmount,WeightBalance.WeightBalanceAmt AS BL_WeightBalanceAmt
,TransactionLog.WeightAmount_Raw
,TransactionLog.WeightBalanceAmt
,LogType.LogName
,Buy.BuyNumber,Buy.BuyDate 
from CustomerPrice
inner join TransactionLog on TransactionLog.PriceId = CustomerPrice.PriceId
inner join LogType on LogType.LogTypeId = TransactionLog.LogTypeId
inner join Customer on Customer.CustomerId = TransactionLog.CustomerId
left join WeightBalance on WeightBalance.PriceId = CustomerPrice.PriceId
left join Buy on Buy.BuyId = TransactionLog.RefId
WHERE BuyDate BETWEEN @StartDate AND @EndDate 
END
GO


if exists (select * from sysobjects where id = object_id(N'[rpt_BuyWeightBalance_ByMonth]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure rpt_BuyWeightBalance_ByMonth
GO
Create PROCEDURE rpt_BuyWeightBalance_ByMonth
	@StartDate datetime

AS
BEGIN

select Customer.CustomerId,Customer.CustomerCode,Customer.CustomerName
,DATEADD(DD,DATEDIFF(DD,0,CustomerPrice.SaleDate),0) as SaleDate
,CustomerPrice.SalePriceAdvance, CustomerPrice.WeightAmount	as CP_WeightAmount,WeightBalance.WeightBalanceAmt AS BL_WeightBalanceAmt
,TransactionLog.WeightAmount_Raw
,TransactionLog.WeightBalanceAmt
,LogType.LogName
,Buy.BuyNumber,Buy.BuyDate 
from CustomerPrice
inner join TransactionLog on TransactionLog.PriceId = CustomerPrice.PriceId
inner join LogType on LogType.LogTypeId = TransactionLog.LogTypeId
inner join Customer on Customer.CustomerId = TransactionLog.CustomerId
left join WeightBalance on WeightBalance.PriceId = CustomerPrice.PriceId
left join Buy on Buy.BuyId = TransactionLog.RefId
WHERE YEAR(BuyDate) = YEAR(@StartDate) AND MONTH(BuyDate) = MONTH(@StartDate) 

END
GO


if exists (select * from sysobjects where id = object_id(N'[rpt_BuyWeightBalance_ByYear]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure rpt_BuyWeightBalance_ByYear
GO
Create PROCEDURE rpt_BuyWeightBalance_ByYear
	@StartDate datetime
AS
BEGIN

select Customer.CustomerId,Customer.CustomerCode,Customer.CustomerName
,DATEADD(DD,DATEDIFF(DD,0,CustomerPrice.SaleDate),0) as SaleDate
,CustomerPrice.SalePriceAdvance, CustomerPrice.WeightAmount	as CP_WeightAmount,WeightBalance.WeightBalanceAmt AS BL_WeightBalanceAmt
,TransactionLog.WeightAmount_Raw
,TransactionLog.WeightBalanceAmt
,LogType.LogName
,Buy.BuyNumber,Buy.BuyDate 
from CustomerPrice
inner join TransactionLog on TransactionLog.PriceId = CustomerPrice.PriceId
inner join LogType on LogType.LogTypeId = TransactionLog.LogTypeId
inner join Customer on Customer.CustomerId = TransactionLog.CustomerId
left join WeightBalance on WeightBalance.PriceId = CustomerPrice.PriceId
left join Buy on Buy.BuyId = TransactionLog.RefId
WHERE YEAR(BuyDate) = YEAR(@StartDate)

END
GO

if exists (select * from sysobjects where id = object_id(N'[rpt_SaleByDate_1]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure rpt_SaleByDate_1
GO
Create PROCEDURE rpt_SaleByDate_1
	@StartDate datetime,
	@EndDate datetime,
	@ProductTypeId int
AS
BEGIN
select Sale.SaleNumber,sale.SaleDate,SaleProduct.WeightAmount,SaleProduct.Drc,SaleProduct.WeightAmount_Raw,SaleProduct.TotalPrice_Raw,SaleProduct.TotalPrice 
, Customer.CustomerId, Customer.CustomerCode, Customer.CustomerName from Sale
inner join SaleProduct on Sale.SaleId = SaleProduct.SaleId
inner join Customer on Sale.CustomerId = Customer.CustomerId
where Sale.Active = 1 and Sale.SaleDate BETWEEN @StartDate AND @EndDate AND ProductTypeId = @ProductTypeId

END
GO

if exists (select * from sysobjects where id = object_id(N'[rpt_SaleByMonth_1]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure rpt_SaleByMonth_1
GO
Create PROCEDURE rpt_SaleByMonth_1
	@StartDate datetime,
	@ProductTypeId int
AS
BEGIN

select Sale.SaleNumber,sale.SaleDate,SaleProduct.WeightAmount,SaleProduct.Drc,SaleProduct.WeightAmount_Raw,SaleProduct.TotalPrice_Raw,SaleProduct.TotalPrice 
, Customer.CustomerId, Customer.CustomerCode, Customer.CustomerName from Sale
inner join SaleProduct on Sale.SaleId = SaleProduct.SaleId
inner join Customer on Sale.CustomerId = Customer.CustomerId
where Sale.Active = 1 and  YEAR(Sale.SaleDate) = YEAR(@StartDate) AND MONTH(Sale.SaleDate) = MONTH(@StartDate) 
AND ProductTypeId =  @ProductTypeId

END
GO


if exists (select * from sysobjects where id = object_id(N'[rpt_SaleByYear_1]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure rpt_SaleByYear_1
GO
Create PROCEDURE rpt_SaleByYear_1
	@StartDate datetime,
	@ProductTypeId int
AS
BEGIN

select Sale.SaleNumber,sale.SaleDate,SaleProduct.WeightAmount,SaleProduct.Drc,SaleProduct.WeightAmount_Raw,SaleProduct.TotalPrice_Raw,SaleProduct.TotalPrice 
, Customer.CustomerId, Customer.CustomerCode, Customer.CustomerName from Sale
inner join SaleProduct on Sale.SaleId = SaleProduct.SaleId
inner join Customer on Sale.CustomerId = Customer.CustomerId
where Sale.Active = 1 and YEAR(Sale.SaleDate) = YEAR(@StartDate) AND ProductTypeId =  @ProductTypeId

END
GO
-- รายงานสรุปยอดขายยางแผ่นดิบตามบิล
if exists (select * from sysobjects where id = object_id(N'[rpt_SaleByDate_2]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure rpt_SaleByDate_2
GO
Create PROCEDURE rpt_SaleByDate_2
	@StartDate datetime,
	@EndDate datetime,
	@ProductTypeId int
AS
BEGIN

select Sale.SaleId,Sale.SaleNumber,Sale.SaleDate,Customer.CustomerName,Sale.SubTotal,Sale.DownValue,Sale.NetTotal,Sale.SetOffValue,Sale.ValueBalance
,SaleProduct.Percentage,SaleProduct.WeightAmount,SaleProduct.WeightAmount_Plate,SaleProduct.Drc,SaleProduct.TotalPrice_Smoke,SaleProduct.TotalPrice_Raw,SaleProduct.CalRubber,SaleProduct.TotalPrice
,SaleProduct.Remark
 from Sale
inner join SaleProduct on Sale.SaleId = SaleProduct.SaleId
inner join Customer on Sale.CustomerId = Customer.CustomerId 
where Sale.Active = 1 and Sale.IsFinalze = 1 
and Sale.SaleDate BETWEEN @StartDate AND @EndDate AND ProductTypeId =  @ProductTypeId

END
GO


if exists (select * from sysobjects where id = object_id(N'[rpt_SaleByMonth_2]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure rpt_SaleByMonth_2
GO
Create PROCEDURE rpt_SaleByMonth_2
	@StartDate datetime,
	@ProductTypeId int
AS
BEGIN

select Sale.SaleNumber,Sale.SaleDate,Customer.CustomerName,Sale.SubTotal,Sale.DownValue,Sale.NetTotal,Sale.SetOffValue,Sale.ValueBalance
,SaleProduct.Percentage,SaleProduct.WeightAmount,SaleProduct.WeightAmount_Plate,SaleProduct.Drc,SaleProduct.TotalPrice_Smoke,SaleProduct.TotalPrice_Raw,SaleProduct.CalRubber,SaleProduct.TotalPrice
,SaleProduct.Remark
 from Sale
inner join SaleProduct on Sale.SaleId = SaleProduct.SaleId
inner join Customer on Sale.CustomerId = Customer.CustomerId 
where Sale.Active = 1 and Sale.IsFinalze = 1 
AND YEAR(Sale.SaleDate) = YEAR(@StartDate) AND MONTH(Sale.SaleDate) = MONTH(@StartDate) 
AND ProductTypeId =  @ProductTypeId

END
GO

if exists (select * from sysobjects where id = object_id(N'[rpt_SaleByYear_2]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure rpt_SaleByYear_2
GO
Create PROCEDURE rpt_SaleByYear_2
	@StartDate datetime,
	@ProductTypeId int
AS
BEGIN

select Sale.SaleNumber,Sale.SaleDate,Customer.CustomerName,Sale.SubTotal,Sale.DownValue,Sale.NetTotal,Sale.SetOffValue,Sale.ValueBalance
,SaleProduct.Percentage,SaleProduct.WeightAmount,SaleProduct.WeightAmount_Plate,SaleProduct.Drc,SaleProduct.TotalPrice_Smoke,SaleProduct.TotalPrice_Raw,SaleProduct.CalRubber,SaleProduct.TotalPrice
,SaleProduct.Remark
 from Sale
inner join SaleProduct on Sale.SaleId = SaleProduct.SaleId
inner join Customer on Sale.CustomerId = Customer.CustomerId 
where Sale.Active = 1 and Sale.IsFinalze = 1 
AND YEAR(Sale.SaleDate) = YEAR(@StartDate) AND ProductTypeId =  @ProductTypeId

END
GO

if exists (select * from sysobjects where id = object_id(N'[spt_GetSaleNumber]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spt_GetSaleNumber
GO
Create PROCEDURE spt_GetSaleNumber
	@SaleDate Datetime
AS
BEGIN

SELECT ReceiptNo FROM (
SELECT OptionValue + PreFix + SUBSTRING(CONVERT(nvarchar, SaleNumber + 10000000), 2, 8) ReceiptNo 
FROM (
SELECT COUNT(SaleNumber) + 1 AS SaleNumber, 
CONVERT(VARCHAR, FORMAT(@SaleDate, 'yyyy', 'Th-th')) + CONVERT(VARCHAR, FORMAT(@SaleDate, 'MM')) AS PreFix
FROM Sale A
WHERE YEAR(SaleDate) = YEAR(@SaleDate) AND MONTH(SaleDate) = MONTH(@SaleDate)
) SaleRC
CROSS JOIN Options B WHERE OptionID = 1
) Buy

END
GO

if exists (select * from sysobjects where id = object_id(N'[spt_GetSale]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spt_GetSale
GO
Create PROCEDURE spt_GetSale
	@Active bit
AS
BEGIN

SELECT SaleId, SaleNumber, CustomerId, CustomerName, SaleDate, SubTotal, DownValue, NetTotal, SetOffValue, ValueBalance, ProductTypeId, IsFinalze, Active
FROM Sale
WHERE Active = @Active

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_AddSale]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_AddSale
GO
Create PROCEDURE spt_AddSale

--@SaleId int, 
@SaleNumber nvarchar(150),
@CustomerId int, 
@CustomerName nvarchar(250),
@SaleDate datetime,
@SubTotal money,
@DownValue money,
@NetTotal money,
@SetOffValue money,
@ValueBalance money,
@ProductTypeId int, 
@IsFinalze bit, 
@InsertBy int

AS
BEGIN

    SET NOCOUNT ON;

   INSERT INTO Sale (SaleNumber, CustomerId, CustomerName, SaleDate, SubTotal, DownValue, NetTotal, SetOffValue, ValueBalance, ProductTypeId, IsFinalze, Active, InsertDate, InsertBy)
                 VALUES (@SaleNumber, @CustomerId, @CustomerName, @SaleDate, @SubTotal, @DownValue, @NetTotal, @SetOffValue, @ValueBalance, @ProductTypeId, @IsFinalze, 1, GETDATE(), @InsertBy)

    SELECT SCOPE_IDENTITY() AS SaleId

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_UpdateSale]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_UpdateSale
GO
Create PROCEDURE spt_UpdateSale

@SaleId int, 
@SaleNumber nvarchar(150),
@CustomerId int, 
@CustomerName nvarchar(250),
@SaleDate datetime,
@SubTotal money,
@DownValue money,
@NetTotal money,
@SetOffValue money,
@ValueBalance money,
@ProductTypeId int, 
@IsFinalze bit, 
@UpdateBy int

AS
BEGIN

    SET NOCOUNT ON;

     UPDATE Sale SET SaleNumber=@SaleNumber, CustomerId=@CustomerId, CustomerName=@CustomerName, SaleDate=@SaleDate, SubTotal=@SubTotal, DownValue=@DownValue, NetTotal=@NetTotal, 
	 SetOffValue=@SetOffValue, ValueBalance=@ValueBalance, ProductTypeId=@ProductTypeId, IsFinalze=@IsFinalze, UpdateDate=GETDATE(), UpdateBy=@UpdateBy
	 WHERE SaleId=@SaleId

END
GO

if exists (select * from sysobjects where id = object_id(N'[spt_GetSaleProduct]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spt_GetSaleProduct
GO
Create PROCEDURE spt_GetSaleProduct
	@Active bit
AS
BEGIN

SELECT SaleProductId, SaleId, PriceId, [Percentage], WeightAmount, WeightAmount_Plate, Drc, TotalPrice_Smoke, WeightAmount_Raw,  
TotalPrice_Raw, CalRubber, TotalPrice, Remark, Active, InsertDate, InsertBy, UpdateDate, UpdateBy, DeleteDate, DeleteBy
FROM SaleProduct
WHERE Active = @Active

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_AddSaleProduct]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_AddSaleProduct
GO
Create PROCEDURE spt_AddSaleProduct

@SaleId int, 
@PriceId int, 
@Percentage money,
@WeightAmount money, 
@WeightAmount_Plate money,
@Drc money,
@TotalPrice_Smoke money,
@WeightAmount_Raw money,
@TotalPrice_Raw money,
@CalRubber money,
@TotalPrice money,
@Remark nvarchar(500),
@InsertBy int

AS
BEGIN

    SET NOCOUNT ON;

   INSERT INTO SaleProduct (SaleId, PriceId, [Percentage], WeightAmount, WeightAmount_Plate, Drc, TotalPrice_Smoke, WeightAmount_Raw,  
                           TotalPrice_Raw, CalRubber, TotalPrice, Remark, Active, InsertDate, InsertBy)
                 VALUES (@SaleId, @PriceId, @Percentage, @WeightAmount, @WeightAmount_Plate, @Drc, @TotalPrice_Smoke, @WeightAmount_Raw,  
                           @TotalPrice_Raw, @CalRubber, @TotalPrice, @Remark, 1, GETDATE(), @InsertBy)

    SELECT SCOPE_IDENTITY() AS SaleProductId

END
GO

If exists (select * from sysobjects where id = object_id(N'[spt_UpdateSaleProduct]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE spt_UpdateSaleProduct
GO
Create PROCEDURE spt_UpdateSaleProduct

@SaleProductId int, 
@SaleId int, 
@PriceId int, 
@Percentage money,
@WeightAmount money, 
@WeightAmount_Plate money,
@Drc money,
@TotalPrice_Smoke money,
@WeightAmount_Raw money,
@TotalPrice_Raw money,
@CalRubber money,
@TotalPrice money,
@Remark nvarchar(500),
@UpdateBy int

AS
BEGIN

    SET NOCOUNT ON;

     UPDATE SaleProduct SET SaleId=@SaleId, PriceId=@PriceId, [Percentage]=@Percentage, WeightAmount=@WeightAmount, WeightAmount_Plate=@WeightAmount_Plate, Drc=@Drc, TotalPrice_Smoke=@TotalPrice_Smoke, 
	 WeightAmount_Raw=@WeightAmount_Raw, TotalPrice_Raw=@TotalPrice_Raw, CalRubber=@CalRubber, TotalPrice=@TotalPrice, Remark=@Remark, UpdateDate=GETDATE(), UpdateBy=@UpdateBy
	 WHERE SaleProductId=@SaleProductId

END
GO

if exists (select * from sysobjects where id = object_id(N'[spt_GetSaleWeightBalance]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure spt_GetSaleWeightBalance
GO
Create PROCEDURE spt_GetSaleWeightBalance
	@CustomerId int
AS
BEGIN

SELECT PriceId, CustomerId, sName, SaleDate, SalePriceAdvance, WeightAmount, BeginAmt, 
                            DeliveryPrice, BeginBalanceAmt, WeightBalanceAmt, LastLogId, Active
                            FROM (
SELECT A.PriceId, A.CustomerId, A.PriceId AS sName, SaleDate, SalePriceAdvance, WeightAmount, DeliveryPrice, 
                            ISNULL(BeginBalanceAmt, 0) BeginBalanceAmt, ISNULL(WeightBalanceAmt, 0) WeightBalanceAmt, 
							ISNULL(WeightBalanceAmt, 0) AS BeginAmt, LastLogId, A.Active
                            FROM CustomerPrice A
                            INNER JOIN Customer B ON A.CustomerId = B.CustomerId
							LEFT OUTER JOIN (

							SELECT DD.WeightBalanceId, DD.PriceId, BeginBalanceAmt, WeightBalanceAmt, LastLogId FROM (
							SELECT MAX(WeightBalanceId) AS WeightBalanceId, PriceId FROM WeightBalance 
							WHERE CustomerId = @CustomerId 
							GROUP BY WeightBalanceId, PriceId) DT
							INNER JOIN WeightBalance DD ON Dt.WeightBalanceId = DD.WeightBalanceId

							) C ON A.PriceId = C.PriceId
                            WHERE A.Active = 1 AND A.CustomerId = @CustomerId

) WeightBalance

END
GO
