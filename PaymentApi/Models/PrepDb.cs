using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Models
{
    public class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SendData(serviceScope.ServiceProvider.GetService<PaymentDetailContext>());
            }
        }

        private static void SendData(PaymentDetailContext context)
        {
            Console.WriteLine("Appling Migration..."); 
            context.Database.Migrate();

            if (context.PaymentDetails.Any())
            {
                context.PaymentDetails.AddRange(new PaymentDetail() { CardNumber = "1234567891234567", CardOwnerName = "Ram Sharma", ExpirationDate = "08/24", SecurityCode = 004, PaymentDetailId = 1 });
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Already have data- not seeding");
            }
        }
    }
}
