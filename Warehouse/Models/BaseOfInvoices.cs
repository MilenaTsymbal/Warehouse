using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    public class BaseOfInvoices : List<Invoice>
    {
        List<Invoice> allInvoices;
        public BaseOfInvoices() 
        {
            allInvoices = new List<Invoice>();
        }

        public static int CountNumberOfInvoices(BaseOfInvoices invoices)
        {
            int count = 0;

            foreach (Invoice invoice in invoices)
            {
                    count++;
            }

            return count + 1;
        }

    }
}
