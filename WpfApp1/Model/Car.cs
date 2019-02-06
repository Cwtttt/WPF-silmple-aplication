using System;

namespace WpfApp1.Model
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public int Moc { get; set; }
        public int LiczbaMiejsc { get; set; }
        public bool Avaliable { get; set; } = true;
    }
}
