using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APMS.Domain.Entities;

namespace APMS.DataAccess.Context
{
    internal class Seeder
    {
        public void Seed()
        {
            using (var context = new ApmsDbContext())
            {
                using (var transaccion = context.Database.BeginTransaction())
                {
                    if (!context.ApiKeys.Any())
                    {
                        context.ApiKeys.Add(new ApiKey
                        {
                            KeyId = "20bd419b-45a9-49c9-ac49-cd437b0ac2bf",
                            Secret = "NBJ&sPwYfNNU49ChPxEp&e*Yj3rMArdMY(+4zV+%$pDey4y%ELsp+&k&Pa#wmEG^"
                        });
                        context.SaveChanges();
                    }

                    context.SaveChanges();
                    transaccion.Commit();
                }
            }
        }
    }
}
