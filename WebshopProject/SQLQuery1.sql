insert into fmc.Product values ('20098OS', 10, 599.00, '', 6, 12, 8, 11)


select * from fmc.Brand

select * from fmc.Model

select * from fmc.Size

select * from fmc.Product where Prod_ArtNr like '2%'

insert into fmc.Model values ('OG')

update fmc.Product set Prod_Description = 'Med sin karaktäristiska glans och formbarhet har vävda slipsar i siden kommit att bli det klassiska materialvalet. Vårt utbud av vävd slips i siden innefattar såväl slät och enfärgad satäng som mönsterrika vävar med unik struktur. Alla våra slipsar i siden tillverkas i Italien efter traditionellt slipshantverk.' where Prod_BrandId = 6