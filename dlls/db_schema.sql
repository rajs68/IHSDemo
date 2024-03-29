
/****** Object:  Table [dbo].[MonthlyProduction]    Script Date: 2015/5/18 18:29:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MonthlyProduction](
	[Entity] [nvarchar](255) NULL,
	[Source] [nvarchar](255) NULL,
	[EntityType] [nvarchar](255) NULL,
	[PrimaryProduct] [nvarchar](255) NULL,
	[LeaseName] [nvarchar](255) NULL,
	[WellNumber] [nvarchar](255) NULL,
	[API] [nvarchar](255) NULL,
	[RegulatoryAPI] [nvarchar](255) NULL,
	[Year] [nvarchar](255) NULL,
	[Month] [nvarchar](255) NULL,
	[Liquid] [nvarchar](255) NULL,
	[Gas] [nvarchar](255) NULL,
	[Water] [nvarchar](255) NULL,
	[RatioGasOil] [nvarchar](255) NULL,
	[PercentWater] [nvarchar](255) NULL,
	[Wells] [nvarchar](255) NULL,
	[DaysOn] [nvarchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductionHeader]    Script Date: 2015/5/18 18:29:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductionHeader](
	[Entity] [nvarchar](255) NULL,
	[Source] [nvarchar](255) NULL,
	[EntityType] [nvarchar](255) NULL,
	[PrimaryProduct] [nvarchar](255) NULL,
	[ProvinceStateName] [nvarchar](255) NULL,
	[DistrictName] [nvarchar](255) NULL,
	[CountyName] [nvarchar](255) NULL,
	[OSIndicator] [nvarchar](255) NULL,
	[BasinName] [nvarchar](255) NULL,
	[OperatorName] [nvarchar](255) NULL,
	[FieldName] [nvarchar](255) NULL,
	[ProdZoneName] [nvarchar](255) NULL,
	[LeaseName] [nvarchar](255) NULL,
	[LeaseNumber] [nvarchar](255) NULL,
	[WellNumber] [nvarchar](255) NULL,
	[Location] [nvarchar](255) NULL,
	[GathererGas] [nvarchar](255) NULL,
	[GathererLiquid] [nvarchar](255) NULL,
	[StatusDate] [datetime] NULL,
	[StatusCurrentName] [nvarchar](255) NULL,
	[DateProductionStart] [datetime] NULL,
	[DateProductionStop] [datetime] NULL,
	[DateInjectionStart] [nvarchar](255) NULL,
	[DateInjectionStop] [nvarchar](255) NULL,
	[PoolName] [nvarchar](255) NULL,
	[TemperatureGradient] [nvarchar](255) NULL,
	[NFactor] [nvarchar](255) NULL
) ON [PRIMARY]

GO
