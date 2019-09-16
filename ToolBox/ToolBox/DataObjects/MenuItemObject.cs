using System.Windows.Input;

namespace ToolBox.DataObjects
{
    /// <summary>
    /// Represents a menu item object
    /// </summary>
    public class MenuItemObject
    {
        /// <summary>
        /// Gets or sets the header of the menu item
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the command which should be executed when the user hits the menu item
        /// </summary>
        public ICommand Command { get; set; }

        /// <summary>
        /// Gets the header as command parameter
        /// </summary>
        public string CommandParameter => Header;

        /// <summary>
        /// Returns the header name of the object
        /// </summary>
        /// <returns>The header name</returns>
        public override string ToString()
        {
            return Header;
        }
    }
}
