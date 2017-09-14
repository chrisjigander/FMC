using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using WebshopProject.Models.Entities;
using WebshopProject.Models.VM;

namespace WebshopProject.Utils
{
    public static class EmailUtils
    {
        public static MailAddress fromAddress = new MailAddress("fmcwebshop@gmail.com", "FMC Webshop");
        public static MailAddress toAddress;

        const string fromPassword = "fmcwebshop1";
        public static string subject = "";
        public static string body = "";

        public static void SendRegistrationConfEmail(User user)
        {
            toAddress = new MailAddress(user.Email, user.Firstname);
            subject = $"Välkommen, {user.Firstname}!";
            body = $"Hej, {user.Firstname}! Du har nu blivit medlem hos oss. \n \nMed vänliga hälsningar, \nFMC Webshop";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

        internal static void SendContactUsConfEmail(CustomerSupportHandleContactFormVM formVM)
        {
            toAddress = new MailAddress(formVM.Email, formVM.FirstName);
            subject = $"Meddelandebekräftelse";
            body = $"Tack för ditt mail, {formVM.FirstName} \nNedan följer en kopia av ditt meddelande. \n \n'{formVM.Message}' \n \nVi svarar så fort som möjligt! \n \nMed vänliga hälsningar, \nFMC Webshop";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
            if (formVM.Email != "fmcwebshop@gmail.com")
            {
                formVM.Email = "fmcwebshop@gmail.com";
                SendContactUsConfEmail(formVM);
            }
        }

        public static void SendOrderConfEmail(int orderNumber, WebShopDBContext context, User user)
        {
            OrderArticles[] myArticles = context.OrderArticles.Where(o => o.Oid == orderNumber).ToArray();
            Product[] myProducts = new Product[myArticles.Length];

            for (int i = 0; i < myArticles.Length; i++)
            {
                myProducts[i] = context.Product.First(p => p.ProdArtNr == myArticles[i].ArticleNumber);
            }
            string[] myarticlesStrings = new string[myArticles.Length];

            for (int i = 0; i < myArticles.Length; i++)
            {
                string brandName = context.Brand.First(b => b.BrandId == myProducts[i].ProdBrandId).BrandName;
                string modelName = context.Model.First(b => b.ModelId == myProducts[i].ProdModelId).ModelName;
                string sizeName = context.Size.First(b => b.SizeId == myProducts[i].ProdSizeId).SizeName;
                string colorName = context.Color.First(b => b.ColorId == myProducts[i].ProdColorId).ColorName;

                myarticlesStrings[i] = $"Artikel nr: {myArticles[i].ArticleNumber}, {brandName} {modelName}, {colorName}, {sizeName}, {myArticles[i].Price} kr";
            }

            toAddress = new MailAddress(user.Email, user.Firstname);
            subject = $"Orderbekräftelse";
            body = $"Tack för din order,  {user.Firstname}. \nNedan följer en sammanfattning av din order (ordernr: {orderNumber}). \n";
            foreach (var item in myarticlesStrings)
            {
                body += $"{item} \n";
            }

            body += "\nSkulle det vara något som inte stämmer, vänligen kontakta vår kundservice. \n \nMed vänliga hälsningar, \nFMC Webshop";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
