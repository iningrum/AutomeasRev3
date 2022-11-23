using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomeasR3.Core
{
    public interface IPlugin
    {
        void Start();
        void Stop();
    }
}
