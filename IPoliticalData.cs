using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLParserV1
{
    public interface IPoliticalData
    {
        string Party { get; set; }
        string Constituency { get; set; }
    }
}
