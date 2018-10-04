using MangaScrapper.Core.Logging;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MangaScrapper.Core.IO
{
    class BinaryDataRepository<T> : IDataRepository<T> where T : class, new()
    {
        public T ReadFromFile(string filePath)
        {
            try
            {
                using (Stream stream = File.Open(filePath, FileMode.Open))
                {
                    var binaryFormatter = new BinaryFormatter();
                    return (T)binaryFormatter.Deserialize(stream);
                }
            }
            catch (IOException ex)
            {
                Log.Error("Exception generated for Path: " + filePath, ex);
                return null;
            }
            catch (Exception ex)
            {
                Log.Error("Exception generated while reading File: ", ex);
                return null;
            }
        }

        public void WriteToFile(string filePath, T objectToWrite, bool append = false)
        {
            try
            {
                using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
                {
                    new BinaryFormatter().Serialize(stream, objectToWrite);
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
    }
}
