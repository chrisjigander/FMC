using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopProject.Utils
{
    public static class SessionUtils
    {
        public static string GetSessionCount(Controller controller)
        {
            string count = "-1";

            for (int i = 0; i < 10; i++)
            {
                if (controller.HttpContext.Session.GetString(i.ToString()) == null)
                {
                    count= i.ToString();
                    break;
                }

            }
            return count;
        }
    }
}
