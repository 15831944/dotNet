using StandardModel;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CoreProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Stream stream = new FileStream(
                @"E:\Repos\dotNet\stream\serialized.bin",
                FileMode.Open, FileAccess.Read, FileShare.None);

            IFormatter formatter = new BinaryFormatter();
            
            StandardModel.MyModel coreObject = (MyModel)formatter.Deserialize(stream);
            Console.WriteLine($"Object deserialized: {coreObject.ToString()}");
            Console.WriteLine($"Object count1: {coreObject.count1.ToString()}");
            Console.WriteLine($"Object string1: {coreObject.string1}");
            Console.ReadKey();
        }
    }
}
