using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DemoScaffolinding02.Model;

namespace DemoScaffolinding02.Data
{
    public class DemoScaffolinding02Context : DbContext
    {
        public DemoScaffolinding02Context (DbContextOptions<DemoScaffolinding02Context> options)
            : base(options)
        {
        }

        public DbSet<DemoScaffolinding02.Model.Reservation> Reservation { get; set; } = default!;
    }
}
