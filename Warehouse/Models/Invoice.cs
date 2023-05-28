namespace Warehouse
{
    public class Invoice : Warehouse
    {
        Warehouse invoice;
        public int NumberOfInvoice { get; set; }
        public DateTime DateOfMakingInvoice { get; set; }

        public Invoice()
        {
            invoice = new Warehouse();
        }

        public Invoice(Invoice other)
        {
            invoice = other.invoice;
        }
    }
}
