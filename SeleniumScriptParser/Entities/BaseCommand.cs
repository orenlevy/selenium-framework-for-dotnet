using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumScriptParser.Entities
{
    /// <summary>
    /// A single step command of a script.
    /// </summary>
    public class BaseCommand 
    {
        public int Order { get; set; }
        public string Command{ get; set; }
        public string Value { get; set; }
        public string Target { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// overridden to allow testing.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            BaseCommand objCompareTo = obj as BaseCommand;
            if (objCompareTo == null)
            {
                return false;
            }
            return Order.Equals(objCompareTo.Order)
                   && Command.Equals(objCompareTo.Command)
                   && Value.Equals(objCompareTo.Value)
                   && Target.Equals(objCompareTo.Target)
                   && Name.Equals(objCompareTo.Name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }


}
