using MangaScrapper.Core.Logging;
using System;
using System.IO;
using System.Xml.Serialization;

namespace MangaScrapper.Core.IO
{
    class XmlDataRepository<T> : IDataRepository<T> where T : class, new()
    {
        public void WriteToFile(string filePath, T objData, bool append = false)
        {
            try
            {                
                using (TextWriter writer = new StreamWriter(filePath, append))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(writer, objData);
                }
            }
            catch (IOException ex)
            {
                Log.Error("Exception generated for Path: " + filePath, ex);
            }
            catch
            {
                throw;
            }
        }

        public T ReadFromFile(string filePath)
        {
            try
            {
                using (TextReader reader = new StreamReader(filePath))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(reader);
                }
            }
            catch (IOException ex)
            {
                Log.Error("Exception generated for Path: " + filePath, ex);
                return null;
            }
            catch(Exception ex)
            {
                Log.Error("Exception generated while reading File: ", ex);
                return null;
            }
        }
    }
}
