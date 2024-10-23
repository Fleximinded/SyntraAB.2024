using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO.Demo.src
{
    public class BinaryStreamDemo
    {
        DummyData Data { get; set; } = new DummyData();
        public class DummyData
        {
            Random random = new Random();
            public DummyData()
            {
                NumberData = random.Next();
                DoubleData = random.NextDouble();
                TextData = Guid.NewGuid().ToString();
            }
            public int NumberData { get; set; }
            public double DoubleData { get; set; }
            public string TextData { get; set; }
            public override string ToString()
            {
                return $"Number: {NumberData} Double: {DoubleData} Text: {TextData}";
            }
        }
        public bool WriteData(string filePath)
        {
            if(Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                using FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                using BinaryWriter writer = new BinaryWriter(fileStream);
                writer.Write(Data.TextData);
                writer.Write(Data.NumberData);
                writer.Write(Data.DoubleData);
                return true;
            }
            return false;
        }
        public DummyData? ReadData(string filePath)
        {
            if(!File.Exists(filePath)) return null;
            using(FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using(BinaryReader reader = new BinaryReader(fileStream))
                {
                    Data.TextData = reader.ReadString();
                    Data.NumberData = reader.ReadInt32();
                    Data.DoubleData = reader.ReadDouble();
                }
                return Data;
            }
        }
    }
}
