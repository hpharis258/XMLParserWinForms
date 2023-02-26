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
        public string filePath { get; set; }
        public int Counter { get; set; }

        List<string> files = new List<string>();
        public List<string> GetAllData()
        {
           
            // If Count was set Read only set amount of records
            if (Counter > 0)
            {
                int _localCounter = 0;
                try
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;

                        while ((_localCounter ) != Counter + 1)
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
    }
}
