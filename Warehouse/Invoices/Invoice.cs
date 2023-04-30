namespace Warehouse
{
    public class Invoice : List<Warehouse>
    {
        List<Warehouse> invoice;
        public int NumberOfInvoice { get; set; }
        public DateTime DateOfMakingInvoice { get; set; }

        public Invoice()
        {
            invoice = new List<Warehouse>();
        }
        
    }
}
