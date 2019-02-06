using System;

namespace WpfApp1.Model
{
    public class Rent
    {
        public Guid Id { get; set; }
        public DateTime HireDate { get; set; }
        public Nullable<DateTime> ReturnDate { get; set; }
        public Car Car { get; set; }
    }
}
