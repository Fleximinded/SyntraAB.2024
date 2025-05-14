using Syntra.Cli.Ext;
using System.Reflection;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Loader;

namespace Reflection.Tools
{
    public class ReflectionTools : ICliExecutable
    {
        public string Name { get => "Reflection.Tools"; }
        public string Description { get => "Reflection tools"; }

        AssemblyLoadContext? CurrentContext { get; set; } = null;
        Assembly? CurrentAssembly { get; set; } = null;
        public bool Execute(ICliRuntime owner, ICliCommand parameters)
        {
            bool flag = false;
            ICliCommandParameter? src = null;
            ICliCommandParameter? typeName = null;
            switch(parameters.Command)
            {
                case "plugin.list":
                    foreach(var plugin in owner.Plugins)
                    {
                        Console.WriteLine($"=> {plugin}");
                    }
                    break;
                case "plugin.free":
                    UnloadAssembly();
                    break;
                case "assembly.use":
                    src = parameters.FindOption("src");
                    if(src != null)
                    {
                        LoadAssembly(src.Value);
                    }
                    break;
                case "type.list":
                    src = parameters.FindOption("src");
                    var listOnly = parameters.FindOption("list") != null;
                    var showDetails = parameters.FindOption("details") != null;
                    if(src != null)
                    {
                        flag = parameters.FindOption("free") != null;
                        LoadAssembly(src.Value);
                    }
                    ShowTypes(listOnly);
                    if(flag)
                    {
                        UnloadAssembly();
                    }
                    break;
                case "type.customfields":
                    typeName = parameters.FindOption("name");
                    if(typeName != null)
                    {
                        FindCustomFields(typeName.Value);
                    }
                    break;
                case "type.info":
                    typeName = parameters.FindOption("name");
                    showDetails = parameters.FindOption("details") != null;
                    if(typeName != null)
                    {
                        ShowTypeInfo(typeName.Value, showDetails);
                    }
                    break;
                default:
                    return false;



            }
            return true;
        }

        private void ShowTypes(bool listOnly, bool showDetails = false)
        {
            if(CurrentAssembly != null)
            {
                bool useSep = false;
                Console.WriteLine($"Type information of {CurrentAssembly.FullName}");
                var types = CurrentAssembly.GetTypes();
                foreach(var type in types)
                {
                    if(useSep)
                    {
                        Console.WriteLine(new string('-', 80));
                        Console.WriteLine();
                    }
                    else useSep = (listOnly == false);
                    Console.WriteLine($"Type: {type.FullName}");
                    if(!listOnly)
                    {
                        ShowTypeInfo(type, showDetails);
                    }
                }
                Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}");
            }
            else
            {
                Console.WriteLine("No assembly loaded");
            }

        }
        void ShowHeader(string header)
        {
            Console.WriteLine();
            Console.WriteLine(new string('*', 80));
            Console.WriteLine(header);
            Console.WriteLine(new string('*', 80));
            Console.WriteLine();
        }
        bool FindCustomFields(string typeName)
        {
            var tp = FindType(typeName);
            if(tp != null)
            {
                var customFields = tp.GetMembers().Where(m => m.GetCustomAttributes().Any(a => a.GetType().Name == "CustomFieldAttribute")).ToArray();
                if(customFields.Length > 0)
                {
                    ShowHeader($"Custom fields of {tp.FullName}");
                    foreach(var field in customFields)
                    {
                        Console.WriteLine($"\t\t-\t{field.Name} : {field.MemberType}");
                        ShowCustomAttributeInfo(field);
                    }
                    return true;
                }
            }
            return false;
        }
        bool ShowTypeDetails(Type tp)
        {
            bool useSep = false;
            ConstructorInfo[] constructors = tp.GetConstructors();

            if(constructors.Length > 0)
            {
                ShowHeader($"Constructors of {tp.FullName}");
                foreach(var ctor in constructors)
                {
                    if(useSep)
                    {
                        Console.WriteLine(new string('-', 80));
                        Console.WriteLine();
                    }
                    else useSep = true;
                    Console.WriteLine($"\t\t-\t{ctor.Name}");
                    if(ctor.IsPublic) Console.WriteLine($"This constructor is Public");
                    if(ctor.IsPrivate) Console.WriteLine($"This constructor is Private");
                    if(ctor.IsStatic) Console.WriteLine($"This constructor is Static");
                    var parameters = ctor.GetParameters();
                    if(parameters.Length > 0)
                    {
                        Console.WriteLine($"\t\t\tParameters:");
                        foreach(var param in parameters)
                        {
                            Console.WriteLine($"\t\t\t-\t{param.Name} : {param.ParameterType.FullName}");
                            if(param.IsOptional) Console.WriteLine($"This parameter is optional");
                        }
                    }

                }
            }
            FieldInfo[] fields = tp.GetFields();
            if(fields.Length > 0)
            {
                ShowHeader($"Fields of {tp.FullName}");
                foreach(var field in fields)
                {
                    Console.WriteLine($"\t\t-\t{field.Name} : {field.FieldType.FullName}");
                    if(field.IsPublic) Console.WriteLine($"This field is Public");
                    if(field.IsPrivate) Console.WriteLine($"This field is Private");
                    if(field.IsStatic) Console.WriteLine($"This field is Static");
                    ShowCustomAttributeInfo(field);
                }
            }
            PropertyInfo[] properties = tp.GetProperties();
            if(properties.Length > 0)
            {
                ShowHeader($"Properties of {tp.FullName}");
                foreach(var prop in properties)
                {
                    Console.WriteLine($"\t\t-\t{prop.Name} : {prop.PropertyType.FullName}");
                    if(prop.CanRead) Console.WriteLine($"This property is Readable");
                    if(prop.CanWrite) Console.WriteLine($"This property is Writable");
                    if(prop.GetMethod?.IsStatic == true || prop.SetMethod?.IsStatic == true) Console.WriteLine($"This property is Static");
                    if(prop.GetMethod?.IsPrivate == true) Console.WriteLine($"Getting info from this property is Private");
                    if(prop.GetMethod?.IsPublic == true) Console.WriteLine($"Getting info from this property is Public");
                    if(prop.SetMethod?.IsPrivate == true) Console.WriteLine($"Setting info from this property is Private");
                    if(prop.SetMethod?.IsPublic == true) Console.WriteLine($"Setting info from this property is Public");
                    ShowCustomAttributeInfo(prop);
                }
            }
            MethodInfo[] methods = tp.GetMethods();
            if(methods.Length > 0)
            {
                ShowHeader($"Methods of {tp.FullName}");
                foreach(var method in methods)
                {
                    Console.WriteLine($"\t\t-\t{method.Name} ");
                    if(method.ReturnType != null)
                    {
                        Console.WriteLine($"\t\t\t{method.Name} returns {method.ReturnType.FullName}");
                    }
                    if(method.IsPublic) Console.WriteLine($"This method is Public");
                    if(method.IsPrivate) Console.WriteLine($"This method is Private");
                    if(method.IsStatic) Console.WriteLine($"This method is Static");
                    var parameters = method.GetParameters();
                    if(parameters.Length > 0)
                    {
                        Console.WriteLine($"\t\t\tParameters:");
                        foreach(var param in parameters)
                        {
                            Console.WriteLine($"\t\t\t-\t{param.Name} : {param.ParameterType.FullName}");
                            if(param.IsOut) Console.WriteLine($"This parameter is Out");
                            if(param.IsIn) Console.WriteLine($"This parameter is In");
                            if(param.IsOptional) Console.WriteLine($"This parameter is Optional");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\t\t\tNo parameters");
                    }

                    ShowCustomAttributeInfo(method);
                }
            }
            return true;
        }

        private void ShowCustomAttributeInfo(MemberInfo prop)
        {
            var attributes = prop.GetCustomAttributes().ToArray();
            if(attributes.Length > 0)
            {
                Console.WriteLine($"\t\t\tAttributes:");
                foreach(var attr in attributes)
                {
                    Console.WriteLine($"\t\t\t-\t{attr.GetType().FullName}");
                }
            }
        }

        private Type? FindType(string typeName)
        {
            if(CurrentAssembly != null && !string.IsNullOrWhiteSpace(typeName))
            {
                try
                {
                    Console.WriteLine($"Type information of {CurrentAssembly.FullName}");
                    var type = CurrentAssembly.GetTypes().Where(t => (t.FullName ?? "").ToLower() == typeName.ToLower()).SingleOrDefault();
                    if(type == null)
                    {
                        type = CurrentAssembly.GetTypes().Where(t => t.Name.ToLower() == typeName.ToLower()).SingleOrDefault();
                    }
                    if(type == null)
                    {
                        Console.WriteLine($"Type {typeName} not found in assembly {CurrentAssembly.FullName}");
                    }
                    return type;
                } catch(InvalidOperationException ex)
                {
                    var imbigious = CurrentAssembly.GetTypes().Where(t => t.Name.ToLower() == typeName.ToLower()).ToArray();
                    Console.WriteLine($"Please use the full name of the type, there are {imbigious.Length} implementations of this type ");
                    foreach(var type in imbigious)
                    {
                        Console.WriteLine($"\t\t-\t{type.FullName}");
                    }
                    Console.WriteLine();
                }
            }
            return null;
        }

        private bool ShowTypeInfo(string typeName, bool showDetails)
        {
            var type = FindType(typeName);
            if(type != null)
            {
                return ShowTypeInfo(type, showDetails);
            }
            return false;
        }
        private bool ShowTypeInfo(Type? type, bool showDetails)
        {
            if(type == null)
            {
                Console.WriteLine("Type is null");
                return false;
            }
            Console.WriteLine($"Full name:\t{type.FullName}");
            Console.WriteLine($"Namespace:\t {type.Namespace}");
            Console.WriteLine($"Type Name:\t{type.Name}");
            if(type.BaseType != null)
            {
                Console.WriteLine($"Base Type :\t{type.BaseType.FullName}");
            }
            var ifaces = type.GetInterfaces();
            if(ifaces.Length > 0)
            {
                Console.WriteLine($"Implemented interfaces:");
                foreach(var iface in ifaces)
                {
                    Console.WriteLine($"\t\t-\t{iface.FullName}");
                }
            }
            if(type.IsPublic) Console.WriteLine($"{type.Name} is public");
            if(type.IsClass) Console.WriteLine($"{type.Name} is a class");
            if(type.IsValueType) Console.WriteLine($"{type.Name} is a value type");
            if(type.IsEnum) Console.WriteLine($"{type.Name} is an enum");
            if(type.IsInterface) Console.WriteLine($"{type.Name} is an interface");
            if(type.IsAbstract) Console.WriteLine($"{type.Name} is an abstract type");
            if(type.IsSealed) Console.WriteLine($"{type.Name} is a sealed type");
            if(type.IsGenericType) Console.WriteLine($"{type.Name} is a generic type");
            ShowCustomAttributeInfo(type);

            if(showDetails)
            {
                Console.WriteLine(new string('>', 80));
                Console.WriteLine($"Details of {type.Name}");
                ShowTypeDetails(type);
                Console.WriteLine();
                Console.WriteLine(new string('<', 80));

            }
            return true;
        }

        private void UnloadAssembly()
        {
            if(CurrentContext != null)
            {
                CurrentAssembly = null;
                CurrentContext.Unload();
                CurrentContext = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                Console.WriteLine("Assembly unloaded");
            }
            else
            {
                Console.WriteLine("No assembly loaded");
            }

        }

        private void LoadAssembly(string src)
        {
            if(CurrentContext != null)
            {
                UnloadAssembly();
            }
            if(!string.IsNullOrWhiteSpace(src) && File.Exists(src))
            {
                try
                {
                    CurrentContext = new AssemblyLoadContext(Guid.NewGuid().ToString(), isCollectible: true);
                    CurrentAssembly = CurrentContext.LoadFromAssemblyPath(src);
                    Console.WriteLine($"Assembly {src} loaded");
                    Console.WriteLine();
                } catch(Exception ex)
                {
                    Console.WriteLine($"Error loading assembly {src}: {ex.Message}");
                }
            }
        }
    }
}
