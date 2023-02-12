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
        public List<string> ReadData(string path)
        {
            try
            {
                using(StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null )
                    
                        files.Add(line);
                        System.Diagnostics.Debug.WriteLine(line);
                   
                    
                }
            }
            catch(Exception ex)
            {
                 
                     return files;
            }
            return files;
         
         }

        public List<string> GetAllData()
        {
            throw new NotImplementedException();
        }
    }
}
