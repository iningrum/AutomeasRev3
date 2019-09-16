using System;

namespace ToolBox.Core.Interfaces
{
    public interface IMetadata
    {
        /// <summary>
        /// Gets or set the name of the plugin
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets the version of the plugin
        /// </summary>
        int Version { get; }

        /// <summary>
        /// Gets or sets the description of the plugin
        /// </summary>
        string Description { get; }
    }
}
