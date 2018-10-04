
namespace MangaScrapper.Core.IO
{
    /// <summary>
    /// Interface to fetch/persist data
    /// </summary>
    interface IDataRepository<T> where T : class, new()
    {
        /// <summary>
        /// Saves passed object to file as serialized data
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <param name="objData">object to be saved</param>
        /// <param name="append">Either to append data or to overwrite file</param>
        void WriteToFile(string filePath, T objectToWrite, bool append = false);


        /// <summary>
        /// Reads serialized data from file
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns>Configuration Object OR null in case of Exception</returns>
        T ReadFromFile(string filePath);
    }
}
