using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using MahApps.Metro.Controls.Dialogs;
using ToolBox.Core.Interfaces;
using ToolBox.DataObjects;
using ZimLabs.Utility;
using ZimLabs.WpfBase;

namespace ToolBox.ViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        /// <summary>
        /// Contains the mah app dialog coordinator
        /// </summary>
        private IDialogCoordinator _dialogCoordinator;

        /// <summary>
        /// Contains the list with the plugins which are available.
        /// </summary>
        [ImportMany(typeof(IPlugin))]
        private List<Lazy<IPlugin, IMetadata>> _pluginList;

        /// <summary>
        /// Backing field for <see cref="MenuList"/>
        /// </summary>
        private ObservableCollection<MenuItemObject> _menuList;

        /// <summary>
        /// Gets or sets the menu list
        /// </summary>
        public ObservableCollection<MenuItemObject> MenuList
        {
            get => _menuList;
            set => SetField(ref _menuList, value);
        }

        /// <summary>
        /// Backing field for <see cref="Plugin"/>
        /// </summary>
        private IPlugin _plugin;

        /// <summary>
        /// Gets or sets the current loaded plugin
        /// </summary>
        public IPlugin Plugin
        {
            get => _plugin;
            set => SetField(ref _plugin, value);
        }

        

        /// <summary>
        /// Init the view model
        /// </summary>
        /// <param name="dialogCoordinator">The instance of the mah apps dialog coordinator</param>
        public void InitViewModel(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;

            Compose();

            CreateMenu();
        }

        /// <summary>
        /// Loads the available plugins which are stored in the "Plugins" directory
        /// </summary>
        private async void Compose()
        {
            try
            {
                var pluginDir = Path.Combine(Global.GetBaseFolder(), "Plugins");
                if (!Directory.Exists(pluginDir))
                    Directory.CreateDirectory(pluginDir);

                var catalog = new DirectoryCatalog(pluginDir);
                var container = new CompositionContainer(catalog);
                container.SatisfyImportsOnce(this);
            }
            catch (Exception ex)
            {
                await _dialogCoordinator.ShowMessageAsync(this, "Error",
                    $"An error has occured while loading the plugins.\r\n\r\nMessage: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates the menu 
        /// </summary>
        private void CreateMenu()
        {
            if (_pluginList == null)
                return;

            var menuList = new List<MenuItemObject>();
            foreach (var plugin in _pluginList)
            {
                var name = plugin.Metadata.Name;
                menuList.Add(new MenuItemObject
                {
                    Header = name,
                    Command = new RelayCommand<string>(ExecuteMenuCommand)
                });
            }

            MenuList = new ObservableCollection<MenuItemObject>(menuList);
        }

        /// <summary>
        /// Executes the selected plugin
        /// </summary>
        /// <param name="pluginName">The name of the plugin</param>
        private void ExecuteMenuCommand(string pluginName)
        {
            if (string.IsNullOrEmpty(pluginName))
                return;

            var plugin = _pluginList.FirstOrDefault(f => f.Metadata.Name.Equals(pluginName));

            if (plugin == null)
                return;

            Plugin = plugin.Value;
        }

        
    }
}
