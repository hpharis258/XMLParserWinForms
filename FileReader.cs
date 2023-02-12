using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLParserV1
{
    public interface IFileReader<T>
    {
        List<T> GetAllData(); 
    }
}

