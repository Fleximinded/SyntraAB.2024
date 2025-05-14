
namespace Demo.Example.Version2
{
    public class PersistableClassAttribute : Attribute
    {
        public string TableName { get; set; } = string.Empty;
        public bool Persist { get; set; } = true;
        public PersistableClassAttribute(string tableName, bool persist = true)
        {
            TableName = tableName;
            Persist = persist;
        }
    }
}