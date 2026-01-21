using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DemoWebAPIScaffolding.Model;

namespace DemoWebAPIScaffolding.Data
{
    public class DemoWebAPIScaffoldingContext : DbContext
    {
        public DemoWebAPIScaffoldingContext (DbContextOptions<DemoWebAPIScaffoldingContext> options)
            : base(options)
        {
        }

        public DbSet<DemoWebAPIScaffolding.Model.Reservation> Reservation { get; set; } = default!;
    }
}
