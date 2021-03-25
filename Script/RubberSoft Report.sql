
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

if exists (select * from sysobjects where id = object_id(N'[rpt_BuyByDate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure rpt_BuyByDate
GO
Create PROCEDURE rpt_BuyByDate
	@StartDate datetime,
	@EndDate datetime
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
WHERE BuyDate BETWEEN @StartDate AND @EndDate

END
GO

if exists (select * from sysobjects where id = object_id(N'[rpt_BuyByMonth]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure rpt_BuyByMonth
GO
Create PROCEDURE rpt_BuyByMonth
	@StartDate datetime
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

END
GO

if exists (select * from sysobjects where id = object_id(N'[rpt_BuyByYear]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure rpt_BuyByYear
GO
Create PROCEDURE rpt_BuyByYear
	@StartDate datetime
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
WHERE YEAR(BuyDate) = YEAR(@StartDate)

END
GO
