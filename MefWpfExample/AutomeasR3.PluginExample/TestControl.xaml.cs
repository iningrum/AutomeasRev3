using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutomeasR3.Core;

namespace AutomeasR3.PluginExample
{
    /// <summary>
    /// Interaction logic for TestControl.xaml
    /// </summary>
    [Export(typeof(IPlugin))]
    [ExportMetadata("PluginName", "Fancy Plugin")]
    public partial class TestControl : UserControl, IPlugin
    {
        public TestControl()
        {
            InitializeComponent();
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }
    }
}
