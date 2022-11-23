using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AutomeasR3.Core;

namespace AutomeasR3
{
    public class MenuItemObject
    {
        public string Name { get; set; }
        public Lazy<IPlugin> Plugin { get; set; }

        public ICommand Command { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
