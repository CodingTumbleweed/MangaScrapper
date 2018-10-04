using System.Collections.Generic;

namespace MangaScrapper.Core.Common
{
    interface ISettings
    {
        void WriteSettings(IDictionary<string, string> settings);
        IDictionary<string, string> ReadSettings();
    }
}
