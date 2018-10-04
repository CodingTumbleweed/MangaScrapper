using System.Collections.Generic;
using MangaScrapper.Core.Model.DataModel;
using MangaScrapper.Core.IO;
using System.IO;
using MangaScrapper.Core.Logging;

namespace MangaScrapper.Core.Configuration
{
    /// <summary>
    /// Saves/Retreives Source Configuration
    /// from XML file on specified path
    /// </summary>
    class ConfigurationRepository
    {
        private IDataRepository<Configurations> _dataRepository;
        List<string> validationErrorMessages;
        public string FileName = "SourceConfig.dat";
        private string _filePath
        {
            get { return Path.Combine(DomainSettings.ConfigFolder, FileName); }
        }
        

        //Calling constructor with a different signature with same class
        public ConfigurationRepository()
            : this(new XmlDataRepository<Configurations>())
        {
        }

        internal ConfigurationRepository(IDataRepository<Configurations> dataRepository)
        {
            if (!Directory.Exists(DomainSettings.ConfigFolder))
                Directory.CreateDirectory(DomainSettings.ConfigFolder);

            _dataRepository = dataRepository;
        }

        /// <summary>
        /// Saves Configuration List to File
        /// </summary>
        public void SaveSourceConfig(Configurations configurationList)
        {
            _dataRepository.WriteToFile(_filePath, configurationList);
        }

        /// <summary>
        /// Gets Saved Configuration List from File
        /// </summary>
        public Configurations ReadSourceConfig()
        {
            return _dataRepository.ReadFromFile(_filePath);
        }

        /// <summary>
        /// Checks if Configuration List Items are valid. If not,
        /// problems found will be logged with Fatal flag
        /// </summary>
        internal bool IsConfigurationValid(Configurations configurations)
        {
            bool isValid = false;
            validationErrorMessages = new List<string>();
            foreach (var configurationItem in configurations.ConfigurationList)
            {
                if (string.IsNullOrEmpty(configurationItem.ImageXpath))
                    validationErrorMessages.Add("ImageXpath missing in Source: " + configurationItem.Name);
                if (string.IsNullOrEmpty(configurationItem.NextLinkXpath))
                    validationErrorMessages.Add("NextLinkXpath missing in Source: " + configurationItem.Name);
                if (string.IsNullOrEmpty(configurationItem.AllChapterXpath))
                    validationErrorMessages.Add("AllChapterXpath missing in Source: " + configurationItem.Name);
                if (string.IsNullOrEmpty(configurationItem.AllSeriesXpath))
                    validationErrorMessages.Add("AllSeriesXpath missing in Source: " + configurationItem.Name);
                if (string.IsNullOrEmpty(configurationItem.AllSeriesUrl))
                    validationErrorMessages.Add("AllSeriesUrl missing in Source: " + configurationItem.Name);
                if (!Helper.IsUrlValid(configurationItem.AllSeriesUrl))
                    validationErrorMessages.Add(string.Format("Invalid AllSeriesUrl value [{0}] in Source: {1}",
                        configurationItem.AllSeriesUrl, configurationItem.AllSeriesUrl));
            }

            if (validationErrorMessages.Count > 0)
            {
                string errorStr = string.Join(",", validationErrorMessages);
                Log.Fatal(errorStr, new MangaScrapperException("Saved Configurations are Faulty"));
            }
            else 
            {
                isValid = true;
            }

            return isValid;
        }

        /// <summary>
        /// Checks If Configuration File Exists
        /// </summary>
        public bool FileExists()
        {
            return File.Exists(_filePath);
        }

    }
}
