using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows.Input;
using MefWpfExample.Core;
using ZimLabs.WpfBase;

namespace MefWpfExample
{
    public class MainWindowViewModel : ObservableObject
    {
        [ImportMany(typeof(IPlugin))]
        private List<Lazy<IPlugin, IDictionary<string, object>>> _pluginList;

        private ObservableCollection<MenuItemObject> _pluginNameList;

        public ObservableCollection<MenuItemObject> PluginNameList
        {
            get => _pluginNameList;
            set => SetField(ref _pluginNameList, value);
        }

        private IPlugin _plugin;

        public IPlugin Plugin
        {
            get => _plugin;
            set => SetField(ref _plugin, value);
        }

        private MenuItemObject _selectedPlugin;

        public MenuItemObject SelectedPlugin
        {
            get => _selectedPlugin;
            set => SetField(ref _selectedPlugin, value);
        }

        public ICommand LoadPluginCommand => new DelegateCommand(LoadPlugin);

        private void LoadPlugin()
        {
            if (SelectedPlugin == null)
                return;

            if (Plugin != null)
                Plugin = null;

            Plugin = SelectedPlugin.Plugin.Value;
        }

        public void LoadPlugins()
        {
            var folder = Helper.GetDirectory("Plugins");
            var catalog = new DirectoryCatalog(folder, "*.dll");
            var container = new CompositionContainer(catalog);
            container.SatisfyImportsOnce(this);

            var blub = "";

            var tmpList = new List<MenuItemObject>();
            foreach (var entry in _pluginList)
            {
                if (entry.Metadata.ContainsKey("PluginName"))
                {
                    tmpList.Add(new MenuItemObject
                    {
                        Name = entry.Metadata["PluginName"].ToString(),
                        Plugin = entry
                    });
                }
            }

            PluginNameList = new ObservableCollection<MenuItemObject>(tmpList);
        }


    }
}
