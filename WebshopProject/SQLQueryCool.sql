use WebshopDB 
go

create table fmc.Brand (
	Brand_Id int identity primary key not null,
	Brand_Name nvarchar(50) not null,
)
go

create table fmc.Size (
	Size_Id int identity primary key not null,
	Size_Name nvarchar(2) not null,
)
go

create table fmc.Color (
	Color_Id int identity primary key not null,
	Color_Name nvarchar(20) not null,
)
go

create table fmc.Model (
	Model_Id int identity primary key not null,
	Model_Name nvarchar(25) not null,
)
go

create table fmc.Product (
	Prod_Id int identity primary key not null,
	Prod_ArtNr nvarchar(30) not null,
	Prod_Qty int not null,
	Prod_Price money not null,
	Prod_Description nvarchar(400),
	Prod_BrandId int references fmc.Brand(Brand_Id) not null,
	Prod_SizeId int references fmc.Size(Size_Id) not null,
	Prod_ColorId int references fmc.Color(Color_Id) not null,
	Prod_ModelId int references fmc.Model(Model_Id) not null
)
go

drop table fmc.Product
go

drop table Brand
go

drop table Model
go

drop table Size
go

drop table Color
go


insert into fmc.Brand (Brand_Name) values ('Adidas')
go

select * from fmc.Brand

insert into fmc.Size(Size_Name) values ('42')
go

