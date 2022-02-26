/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [ProductID]
      ,[Name]
      ,[Color]
      ,[Size]
      ,[Price]
      ,[Quantity]
  FROM [database_products].[dbo].[Product]