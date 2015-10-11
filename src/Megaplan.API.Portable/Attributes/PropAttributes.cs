using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Megaplan.API.Attributes
{
    public class BuildWithoutToLowerAttribute : Attribute
    {
        
    }
    public class BuildBoolAsIntAttribute : Attribute
    {
    }

    public class ArrayAttribute : Attribute
    {
        public ArrayAttribute(string mask)
        {
            Mask = mask;
        }

        /// <summary>
        /// Example: Model[Attaches][Add][%] 
        /// where % array index placeholder
        /// </summary>
        public string Mask { get; set; }
    }
}
