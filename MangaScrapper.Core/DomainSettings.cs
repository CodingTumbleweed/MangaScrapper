using MangaScrapper.Core.Configuration;
using MangaScrapper.Core.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace MangaScrapper.Core
{
    public sealed class DomainSettings
    {
        #region Properties/Constructor
        private static DomainSettings _instance = null;
        private static readonly object padlock = new object();
        private static Configurations _configurations = null;
        private ConfigurationRepository _configRepo;

        DomainSettings() { }

        public static DomainSettings Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new DomainSettings();
                    }
                    return _instance;
                }
            }
        }

        public static Configurations Configurations
        {
            get
            {
                if (_configurations == null)
                {
                    Instance.LoadConfiguration();
                }
                return _configurations;
            }
        }

        public static List<ConfigurationModel> ConfigurationList
        {
            get
            {
                if (_configurations == null)
                {
                    Instance.LoadConfiguration();
                }
                return _configurations.ConfigurationList;
            }
        }

        public static ConfigurationModel SelectedConfiguration
        {
            get
            {
                if (_configurations == null)
                {
                    Instance.LoadConfiguration();
                }

                if (_configurations != null)
                    return _configurations.Selected;

                return null;
            }
        }


        public static string AppName
        {
            get
            {
                return ConfigurationManager.AppSettings["AppName"] == null ? string.Empty
                    : ConfigurationManager.AppSettings["AppName"];
            }
        }

        public static string AppFolder
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), AppName);
            }
        }

        public static string ConfigFolder
        {
            get
            {
                return Path.Combine(AppFolder, "Config");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads Configuration List from file and Checks their
        /// validity. If valid, assign List to Configurations
        /// Property in Domain Settings
        /// </summary>
        private void LoadConfiguration()
        {
            _configRepo = new ConfigurationRepository();
            var objConfig = _configRepo.ReadSourceConfig();

            if (_configRepo.IsConfigurationValid(objConfig))
            {
                if (objConfig.ConfigurationList.HasItems<ConfigurationModel>())
                {
                    _configurations = new Configurations();
                    _configurations.AddConfigurationList(objConfig.ConfigurationList);
                }
            }
        }

        #endregion
    }
}
