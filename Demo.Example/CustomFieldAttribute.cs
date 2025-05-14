
namespace Demo.Example.Version2
{
    public class CustomFieldAttribute : Attribute
    {
        public string Name { get; set; } = string.Empty;
        public bool Visible { get; set; } = true;
        public bool Persist { get; set; } = true;   
        public CustomFieldAttribute(string name, bool visible = true,bool persist=true)
        {
            Name = name;
            Visible = visible;
            Persist = persist;
        }
    }
}