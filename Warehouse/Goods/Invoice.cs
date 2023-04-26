namespace Warehouse
{
    public class Invoice : List<Warehouse>
    {
        List<Warehouse> invoice;
        public DateTime dateOfMakingInvoice;
        public Invoice()
        {
            invoice = new List<Warehouse>();
        }


      /*  public void IndexOf(Warehouse goods)
        {
            IndexOf(goods);
        }
*/
        /*  public Warehouse this[int index]
        {
            get
            {
                return invoice[index];
            }
            *//*set
            {
                invoice[index] = value;
            }*//*
        }*/
    }
}
