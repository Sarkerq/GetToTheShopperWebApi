using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GetToTheShopper.Clients.Core.DTO
{
    public class ShopDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ShopDTO() { }

        public ShopDTO(ShopDTO pattern)
        {
            Id = pattern.Id;
            Name = pattern.Name;
            Address = pattern.Address;
            Latitude = pattern.Latitude;
            Longitude = pattern.Longitude;
        }
    }
}
