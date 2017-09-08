declare @cnt int = 0;

while @cnt < 8
begin
	insert into fmc.Size(Size_Name) values (46)
	set @cnt = @cnt + 1;
end
go

select * from fmc.Size

insert into fmc.Color (Color_Name) values ('Brown', 'Grey', 'Beige', 'Burgundy', 'Multicolor', 'Blue')
insert into fmc.Color (Color_Name) values ('Blue')

select * from fmc.Model

insert into fmc.Model(Model_Name) values ('Fanfan')

update fmc.Brand
	set Brand_Name = 'Sandberg'
	where Brand_Id = 1
go

insert into fmc.Product values ('100144', 4, 1499.00, 'En sko..', 1, 8, 3, 3)
go

insert into fmc.Product values ('100242', 4, 2499.00, 'En sko till..', 2, 6, 2, 1)
go

insert into fmc.Product values ('100338', 4, 1799.00, 'Ytterligare en sko..', 3, 2, 3, 2)
go

use WebshopDB
