


using Fleximinded.Core.Extensions;
using Fleximinded.Core.Parts.CLI;
using IO.Demo.src;
using System.Text;

namespace IO.Demo
{
    public class IOLib : ICliExecutable
    {
        public string Name { get => "IOExample"; }
        public string Description { get => "This is a IO test"; }
        
        public bool Execute(ICliRuntime owner, ICliCommand prm)
        {
            string path = "";
            string? dirName="";
            string? fileName;
            string extra = ""; 

            path = prm.FindOption("p")?.Value ?? "";
            if(path.IsEmpty())
            {
                path = prm.RawCommandParameter;
            }
            if(path.IsEmpty())
            {
                Console.WriteLine("No valid path was given");
            }
            switch(prm.Command)
            {
                case "file.exist":
                    if(prm.ContainsOption("p"))
                    {
                        path = prm.FindOption("p")?.Value ?? "";
                        Console.WriteLine($"Bestand {path} bestaat {(File.Exists(path) ? "" : "niet")}");
                    }
                    return true;
                case "dir.exist":
                    if(prm.ContainsOption("p"))
                    {
                        path = prm.FindOption("p")?.Value ?? "";
                        Console.WriteLine($"Map {path} bestaat {(Directory.Exists(path) ? "" : "niet")}");
                    }
                    return true;
                case "path.exist":
                    if(prm.ContainsOption("p"))
                    {
                        path = prm.FindOption("p")?.Value ?? "";
                        Console.WriteLine($"Map {path} bestaat {(Path.Exists(path) ? "" : "niet")}");
                    }
                    return true;
                case "path.info":
                    if(prm.ContainsOption("p"))
                    {
                        path = prm.FindOption("p")?.Value ?? "";
                        Console.WriteLine($"Path '{path}' heeft {(Path.HasExtension(path) ? "" : "g")}een extentie");
                        if(Path.HasExtension(path))
                        {
                            Console.WriteLine($"De extentie van het path is {Path.GetExtension(path)}");
                        }
                        fileName = Path.GetFileName(path);
                        if(fileName.NotEmpty())
                        {
                            Console.WriteLine($"De totale file name is {fileName}, de naam is {Path.GetFileNameWithoutExtension(path)} en de extentie is {Path.GetExtension(path)}");
                        }
                        dirName = Path.GetDirectoryName(path);
                        if(dirName.NotEmpty())
                        {
                            Console.WriteLine($"De directory name is {dirName}");
                        }
                    }
                    return true;
                case "dir.list":
                   
                    dirName = Path.GetDirectoryName(path);
                    if(Directory.Exists(path))
                    {

                        var files = Directory.GetFileSystemEntries(path);
                        if(files?.Length > 0)
                        {
                            Console.WriteLine($"Files in {path}:");
                            foreach(var file in files)
                            {
                                if(Directory.Exists(file))
                                {
                                    extra = $"(DIR)\t";
                                }
                                else {
                                    extra = $"(FILE)\t";
                                }
                                Console.WriteLine($"{extra}{file}");
                            }
                        }
                    }
                    return true;
                case "file.type":
                    if(File.Exists(path))
                    {
                       extra=File.ReadAllText(path);
                        Console.WriteLine($"File {path} contains: ");
                        Console.WriteLine(extra);
                        Console.WriteLine();
                    }
                    return true;
                case "to.unicode":
                    dirName = Path.GetDirectoryName(path) ?? "";
                    var coding = Encoding.Unicode;
                    if(prm.ContainsOption("c"))
                    {
                        try
                        {
                            coding = Encoding.GetEncoding(prm.FindOption("c")?.Value ?? "UTF-8");
                        }catch(Exception ex)
                        {
                            Console.WriteLine($"Error: {prm.FindOption("c")?.Value} is not a valid encoding");
                        }
                    }
                    if(prm.ContainsOption("to"))
                    {
                        fileName = Path.GetFileName(prm.FindOption("to")?.Value ?? "");
                    }
                    else
                    {
                        fileName =$"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}.json";
                    }
                    if(File.Exists(path))
                    {
                        extra = File.ReadAllText(path);
                        if(extra.Length > 0 && dirName?.Length>0)
                        {
                            File.WriteAllText($"{dirName.TrimEnd('\\')}\\{fileName}",extra,coding   );
                        }
                    }
                    return true;
                case "xml.demo":
                    XmlDemo.Demo1(path);
                    return true;
                case "xml.load":
                    XmlDemo.Demo2(path);
                    return true;

            }
            return false;
        }

    }
}
