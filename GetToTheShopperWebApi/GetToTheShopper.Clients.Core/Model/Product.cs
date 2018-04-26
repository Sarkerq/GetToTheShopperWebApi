using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Core.Model
{

    public class Product : ModelBase
    {
        private string name;
        private Unit unit;
        private double price;

        public int Id { get; set; }
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        public Unit Unit
        {
            get { return unit; }
            set { SetProperty(ref unit, value); }
        }

        public virtual ICollection<ShopProduct> ShopProducts { get; set; }
        public virtual ICollection<ReceiptProduct> ReceiptProduct { get; set; }
        public double Price
        {
            get { return price; }
            set { SetProperty(ref price, value); }
        }
        public Product()
        {
            Unit = new Unit();
        }
        
        public Product(Product pattern)
        {
            Id = pattern.Id;
            Name = pattern.Name;
            Unit = pattern.Unit;
            Price = pattern.Price;
        }
    }
    public class Unit
    {
        public enum UnitName
        {
            sztuka,
            kilogram,
            litr
        }

        public UnitName UnitType { get; set; }

        public Unit()
        {
            UnitType = UnitName.sztuka;
        }
        public Unit(UnitName unit)
        {
            UnitType = unit;
        }

        static public IEnumerable<Unit> UnitValues
        {
            get
            {
                var enumValues = Enum.GetValues(typeof(UnitName)).Cast<UnitName>();
                List<Unit> unitsList = new List<Unit>();
                foreach (var enumValue in enumValues)
                {
                    unitsList.Add(new Unit(enumValue));
                }
                return unitsList;
            }
        }

        public string Name
        {
            get
            {
                switch (UnitType)
                {
                    case UnitName.sztuka:
                        return "sztuka";
                    case UnitName.litr:
                        return "litr";
                    case UnitName.kilogram:
                        return "kilogram";
                    default:
                        return "";
                }
            }
        }
        public string Shortcut
        {
            get
            {
                switch (UnitType)
                {
                    case UnitName.sztuka:
                        return "szt.";
                    case UnitName.litr:
                        return "l";
                    case UnitName.kilogram:
                        return "kg";
                    default:
                        return "";
                }
            }
        }
    }

}
