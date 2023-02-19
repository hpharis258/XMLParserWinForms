using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace XMLParserV1
{
    public class CSVLocalFileReader : IFileReader<string>
    {
        string filePath = "./mps.csv";
        List<string> files = new List<string>();
        public List<string> ReadData(string path, int count = 0)
        {
            int _localCounter = 0;
            // If Count was set Read only set amount of records
            if(count > 0) 
            {
                try
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;

                        while ((count + 1) != _localCounter)
                        {
                            line = reader.ReadLine();
                            files.Add(line);
                            _localCounter++;
                            System.Diagnostics.Debug.WriteLine(line);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An Error has occured!!!");
                    Console.WriteLine(ex.Message);
                    return files;
                }
                return files;
            }
            else
            {
                // Read everything
                try
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            files.Add(line);
                            System.Diagnostics.Debug.WriteLine(line);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An Error has occured!!!");
                    Console.WriteLine(ex.Message);
                    return files;
                }
                return files;
            }
            
         
         }
        // Interface
        public List<string> GetAllData()
        {
            return ReadData(filePath);
        }
    }
}
