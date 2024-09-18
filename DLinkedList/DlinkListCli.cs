using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLinkedList.Test;
using Fleximinded.Core.Parts.CLI;

namespace DLinkedList
{
    public class DlinkListCli : ICliExecutable
    {
        public string Name => "DLink tester"; 
        public string Description  => "test the DLinkList library"; 

        public bool Execute(ICliRuntime owner, ICliCommand command)
        {
            switch(command.Command)
            {
                case "test":
                    TestLinkedList();
                    break;
                case "find":
                    if(command.ContainsOption("name"))
                    {
                        TestMethods.FindText = command.FindOption("name")?.Value ?? "";
                    }
                    if(command.ContainsValue("-e"))
                    {
                        TestMethods.TestFind(true);
                    }
                    else
                    {
                        TestMethods.TestFind();
                    }
                    break;
            }
            return true;
        }


        public bool TestLinkedList() {
            TestMethods.Test1();
            return true;
        
        
        }
    }
}
