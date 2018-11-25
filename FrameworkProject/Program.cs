using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using StandardModel;

namespace FrameworkProject
{
    class Program
    {
        static void Main(string[] args)
        {
            StandardModel.MyModel fwObject = new MyModel();
            fwObject.count1 = 2;
            fwObject.count2 = 1;
            fwObject.string1 = "fw2";
            fwObject.string2 = "fw";
            IFormatter formatter = new BinaryFormatter();

            Stream stream = new FileStream(@"E:\Repos\dotNet\stream\serialized.bin",
                FileMode.OpenOrCreate,FileAccess.Write,FileShare.None);
            formatter.Serialize(stream, fwObject);
            stream.Close();
            Console.WriteLine("work done");
            Console.ReadKey();
        }
    }
}
