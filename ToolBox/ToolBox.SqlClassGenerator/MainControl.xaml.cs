using System.ComponentModel.Composition;
using System.Windows.Controls;
using ToolBox.Core.Interfaces;

namespace ToolBox.SqlClassGenerator
{
    /// <summary>
    /// Interaction logic for MainControl.xaml
    /// </summary>
    [Export(typeof(IPlugin))]
    [ExportMetadata(nameof(IMetadata.Name), "Sql class generator")]
    [ExportMetadata(nameof(IMetadata.Version), 1)]
    [ExportMetadata(nameof(IMetadata.Description), "Tool to create classes out of sql tables")]
    public partial class MainControl : UserControl, IPlugin
    {
        /// <summary>
        /// Creates a new instance of the <see cref="MainControl"/>
        /// </summary>
        public MainControl()
        {
            InitializeComponent();
        }
    }
}
