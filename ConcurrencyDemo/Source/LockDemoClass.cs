using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyDemo.Source
{
    public class LockDemoClass
    {
       
        object lockObject = new object();
        string _text = "";
        public void SetVis()
        {
            //var m = Mutex.OpenExisting("MyLock");
            //if(Monitor.TryEnter(lockObject,20))
            lock(lockObject)
            //if(m.WaitOne(TimeSpan.FromSeconds(5)))
            {
                _text += "v";
                _text += "i";
                Thread.Sleep(10000);
                _text += "s";
                _text += "-";
            }
        }
        public void SetHond()
        {
            //var m = Mutex.OpenExisting("MyLock");
            //if(Monitor.TryEnter(lockObject, 20))
            lock(lockObject)  
            //if(m.WaitOne(TimeSpan.FromSeconds(5)))
            {
                _text += "h";
                _text += "o";
                _text += "n";
                Thread.Sleep(8);
                _text += "d";
                _text += "-";
            }
        }
        public void Execute() {
            for(int i = 0; i < 40; i++)
            {
                Thread visThread = new Thread(SetVis);
                Thread hondThread = new Thread(SetHond);
                visThread.Start();
                hondThread.Start();
            }
            Thread.Sleep(1000);
            Console.WriteLine(_text);
        }
    }
}
