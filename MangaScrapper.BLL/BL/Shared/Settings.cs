using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangaScrapper.Core.Interface.Shared;

namespace MangaScrapper.BLL.BL.Shared
{
    class Settings : ISettings
    {
        public void WriteSettings(IDictionary<string, string> settings)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, string> ReadSettings()
        {
            throw new NotImplementedException();
        }
    }
}
