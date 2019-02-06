using System.Data.Entity;
using WpfApp1.Model;

namespace WpfApp1
{
    public class CarContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }

        public DbSet<Rent> Rents { get; set; }

        public CarContext()
            : base("RentCars")
        {
        }
    }
}
