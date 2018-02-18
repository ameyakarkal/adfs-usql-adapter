namespace Adapter.Usql
{
    public class UsqlContext
    {
        public string DatabaseName { get; set; }

        public string Schema { get; set; }
    }
    class NullUsqlContext : UsqlContext
    {

    }
}