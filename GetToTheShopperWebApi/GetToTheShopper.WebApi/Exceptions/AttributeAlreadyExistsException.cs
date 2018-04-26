using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetToTheShopper.WebApi.Exceptions
{
    public class AttributeAlreadyExistsException : Exception
    {
        private string entityName, givenAttribute;

        public AttributeAlreadyExistsException(string entityName, string givenAttribute)
        {
            this.entityName = entityName;
            this.givenAttribute = givenAttribute;
        }

        public override string Message => String.Format("{0} with given {1} already exists.", entityName, givenAttribute);
    }
}
