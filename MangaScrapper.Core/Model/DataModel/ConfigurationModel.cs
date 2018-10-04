using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace MangaScrapper.Core.Model.DataModel
{
    [XmlRoot("ScrapperConfig")]
    public class Configurations
    {
        private List<ConfigurationModel> _configurationList;
        private ConfigurationModel _selected;

        [XmlArray("ScrapperSources")]
        [XmlArrayItem("Source", typeof(ConfigurationModel))]
        public List<ConfigurationModel> ConfigurationList { get { return _configurationList; } }
        public ConfigurationModel Selected
        {
            get
            {
                if (_selected == null)
                    TryGetSelected(out _selected);
                return _selected;
            }
        }


        public Configurations()
        {
            _configurationList = new List<ConfigurationModel>();
        }

        /// <summary>
        /// Adds Configuration Item.
        /// </summary>
        public void AddConfiguration(ConfigurationModel item)
        {
            //Select first item added by default
            if (!_configurationList.HasItems<ConfigurationModel>())
                item.Selected = true;
            _configurationList.Add(item);
        }

        /// <summary>
        /// Adds Configuration List
        /// </summary>
        public void AddConfigurationList(List<ConfigurationModel> configurations)
        {
            if (configurations.HasItems<ConfigurationModel>())
            {
                _configurationList = configurations;
                _configurationList.First().Selected = true;
            }
        }

        /// <summary>
        /// Changes Selected Configuration to Specified
        /// </summary>
        /// <param name="sourceName">Name of Configuration Object</param>
        public void SelectConfiguration(string sourceName)
        {
            if (_configurationList.HasItems<ConfigurationModel>())
            {
                var selectedItem = _configurationList.Where(c => c.Name.Equals(sourceName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                if (selectedItem != null)
                {
                    _configurationList.ForEach(c => c.Selected = false);
                    selectedItem.Selected = true;
                }
            }
        }

        /// <summary>
        /// Gets Currently Selected Configuration
        /// </summary>
        /// <exception cref="MangaScrapperException">Thrown If Either No Configuration Or Multiple Configurations are Selected</exception> 
        public ConfigurationModel GetSelected()
        {
            bool IsConfigurationCorrect;
            ConfigurationModel selectedConfiguration;

            IsConfigurationCorrect = TryGetSelected(out selectedConfiguration);

            if (!IsConfigurationCorrect)
                throw new MangaScrapperException("Error In Configuration Selection");

            return selectedConfiguration;
        }

        /// <summary>
        /// Gets Currently Selected Configuration If Found
        /// Else returns false
        /// </summary>
        public bool TryGetSelected(out ConfigurationModel selectedConfiguration)
        {
            var SelectedItems = _configurationList.Where(c => c.Selected == true).ToList();

            if (SelectedItems.Count == 1)
            {
                selectedConfiguration = SelectedItems.First();
                return true;
            }
            else
            {
                selectedConfiguration = null;
                return false;
            }
        }

    }

    /// <summary>
    /// Entity representing configuration for
    /// single Manga Scrapping Source
    /// </summary>
    public sealed class ConfigurationModel : IEquatable<ConfigurationModel>
    {
        public string Name { get; set; }
        public string AllSeriesUrl { get; set; }
        public string AllSeriesXpath { get; set; }
        public string AllChapterXpath { get; set; }
        public string ImageXpath { get; set; }
        public string NextLinkXpath { get; set; }
        [XmlIgnore]
        public bool Selected { get; set; }

        //Empty constructor needed for xml serialization
        internal ConfigurationModel() { }

        public bool Equals(ConfigurationModel other)
        {
            if (other == null)
                return false;

            return string.Equals(Name, other.Name)
                   && string.Equals(AllSeriesUrl, other.AllSeriesUrl)
                   && string.Equals(AllSeriesXpath, other.AllSeriesXpath)
                   && string.Equals(AllChapterXpath, other.AllChapterXpath)
                   && string.Equals(ImageXpath, other.ImageXpath)
                   && string.Equals(NextLinkXpath, other.NextLinkXpath);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals(obj as ConfigurationModel);
        }

        /***
         * Overriding it so it can be used
         * in hash-based collections
         */
        public override int GetHashCode()
        {
            //running as unchecked as we don't care about overflow
            unchecked
            {
                // Choosing high co-primes to avoid hashing collisions
                const int HashingBase = (int)2166136261;
                const int HashingMultiplier = 16777619;

                int hash = HashingBase;
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Name) ? Name.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, AllSeriesUrl) ? AllSeriesUrl.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, AllSeriesXpath) ? AllSeriesXpath.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, AllChapterXpath) ? AllChapterXpath.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, ImageXpath) ? ImageXpath.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, NextLinkXpath) ? NextLinkXpath.GetHashCode() : 0);
                return hash;
            }
        }
    }
}
