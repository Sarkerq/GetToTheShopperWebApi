using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetToTheShopper.WebApi.Exceptions
{
    public class NonExistingRecordException : Exception
    {
        private string entityName, givenAttribute;

        public NonExistingRecordException(string entityName, string givenAttribute)
        {
            this.entityName = entityName;
            this.givenAttribute = givenAttribute;
        }

        public override string Message => String.Format("{0} of given {1} does not exist.", entityName, givenAttribute);
    }
}
