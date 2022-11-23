using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Windows.Input;
using AutomeasR3.Core;
using ZimLabs.WpfBase;

namespace AutomeasR3
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

        public ICommand LoadPluginCommandTest => new RelayCommand<string>(LoadPluginTest);

        private void LoadPlugin()
        {
            if (SelectedPlugin == null)
                return;

            if (Plugin != null)
                Plugin = null;

            Plugin = SelectedPlugin.Plugin.Value;
        }

        private void LoadPluginTest(string header)
        {
            var plugin = _pluginList.FirstOrDefault(f => f.Metadata["PluginName"].Equals(header));
            if (plugin != null)
            {
                Plugin = plugin.Value;
            }
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
                    var name = entry.Metadata["PluginName"].ToString();
                    tmpList.Add(new MenuItemObject
                    {
                        Name = name,
                        Plugin = entry,
                        Command = new DelegateCommand(() => LoadPluginTest(name))
                    });
                }
            }

            PluginNameList = new ObservableCollection<MenuItemObject>(tmpList);
        }


    }
}
