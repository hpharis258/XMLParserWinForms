using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLParserV1
{
    public interface IFinancialData
    {
        List <string> PaymentDateReceived { get; set; }
        List <int> PaymentsReceived { get; set; }
        List <string> DonnorNames { get; set; }
        int FullAmountReceived { get; set; }
    }
}
