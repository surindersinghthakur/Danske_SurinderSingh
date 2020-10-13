IF NOT EXISTS(SELECT name FROM sys.objects WHERE name ='TblMunicipality' AND type ='U')
BEGIN
	CREATE TABLE TblMunicipality
	(
		Id						INT IDENTITY(1,1)	NOT NULL,
		Name					nvarchar(500)		NOT NULL UNIQUE,	

		IsDeleted				bit					NOT NULL default(0),
		CreatedUserId			int					NOT NULL,
		DtCreated				datetime			NOT NULL,
		UpdatedUserId			int					NULL,
		DtUpdated				datetime			NULL,
		DeletedUserId			int					NULL,
		DtDeleted				datetime			NULL,
		Stamp					int					NOT NULL default(0),
		IsActive				bit					NOT NULL default 1
	PRIMARY KEY CLUSTERED 
	(
		Id ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

END

 -- drop table TblTaxes
IF NOT EXISTS(SELECT name FROM sys.objects WHERE name ='TblTaxes' AND type ='U')
BEGIN
	CREATE TABLE TblTaxes
	(
		Id						INT IDENTITY(1,1)	NOT NULL,
		MunicipalityId			int					NOT NULL,	
		TaxType					int					NOT NULL,	
		DtStart					datetime			NOT NULL,
		DtEnd					datetime			NOT NULL,
		Rate					float				NOT NULL,

		IsDeleted				bit					NOT NULL default(0),
		CreatedUserId			int					NOT NULL,
		DtCreated				datetime			NOT NULL,
		UpdatedUserId			int					NULL,
		DtUpdated				datetime			NULL,
		DeletedUserId			int					NULL,
		DtDeleted				datetime			NULL,
		Stamp					int					NOT NULL default(0),
		IsActive				bit					NOT NULL default 1
	PRIMARY KEY CLUSTERED 
	(
		Id ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

END

CREATE PROCEDURE sp_get_tax_applied
	@municipality nvarchar(500),
	@taxDate date
AS
BEGIN

	SELECT Rate FROM
	(
	SELECT Rate, ROW_NUMBER() OVER(ORDER BY TaxType) as RowNum
	FROM TblTaxes tax
	INNER JOIN TblMunicipality m ON m.Id = tax.MunicipalityId
	WHERE m.Name = @municipality 
	AND CASE WHEN TaxType IS NOT NULL AND TaxType = 1 AND DtStart = @taxDate THEN tax.Rate ELSE
		CASE WHEN TaxType IS NOT NULL AND TaxType = 2 AND @taxDate BETWEEN dtStart AND dtEnd THEN tax.Rate ELSE
		CASE WHEN TaxType IS NOT NULL AND TaxType = 3 AND @taxDate BETWEEN dtStart AND dtEnd THEN tax.Rate ELSE
		CASE WHEN TaxType IS NOT NULL AND TaxType = 4 AND @taxDate BETWEEN dtStart AND dtEnd THEN tax.Rate ELSE
		CASE WHEN TaxType IS NOT NULL AND TaxType = 5 AND @taxDate BETWEEN dtStart AND dtEnd THEN tax.Rate 
		END END END END END IS NOT NULL
	) t
	WHERE RowNum = 1
END