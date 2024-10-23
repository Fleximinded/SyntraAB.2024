using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO.Demo.src
{
    public class BasicStreamDemo :IDisposable
    {
        Stream? _stream;
        Stream? DataStream {  get=>_stream; set { _stream = value; } }    
        public BasicStreamDemo()       {                    }
        public BasicStreamDemo(string filePath)
        {
            _stream = new FileStream(filePath, FileMode.Open,FileAccess.ReadWrite);        
        }
        public BasicStreamDemo(Stream stream)
        {           
                _stream = stream;
                   }
        public void Dispose()
        {
            _stream?.Flush();
            _stream?.Dispose();
        }
        public byte[] ReadAll(bool byteByByte=false,bool returnBuffer=false)
        {
            if(_stream != null && _stream.CanRead)
            {
                MemoryStream buffer = new MemoryStream();
                byte[] block= new byte[500];
                int readBuf = 0;
                if(byteByByte==false)
                {
                    do
                    {
                        readBuf = _stream.Read(block, 0, block.Length);
                        buffer.Write(block, 0, readBuf);
                    } while(readBuf == block.Length);
                }
                else
                {
                    while(true)
                    {
                        readBuf = _stream.ReadByte();
                        if(readBuf < 0) break;
                        buffer.WriteByte((byte)readBuf);
                    }
                }
                if(returnBuffer) return buffer.GetBuffer();
                return buffer.ToArray();    
            
            }
            return [];
        }
    }
}
