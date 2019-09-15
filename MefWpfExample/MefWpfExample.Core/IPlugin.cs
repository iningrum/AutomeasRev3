using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MefWpfExample.Core
{
    public interface IPlugin
    {
        void Start();
        void Stop();
    }
}
