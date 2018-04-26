using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetToTheShopper.WebApi.DTO
{
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
