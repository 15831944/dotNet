using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using StandardModel;

namespace FrameworkProject
{
    class Program
    {
        private static bool _shallUpdate = false;
        private static IFormatter _formatter = new BinaryFormatter();
        private static MyModel _coreObject;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to serialization exercise program.");
            Console.WriteLine(@"Path for binary file used for comunication is fixed and is C:\temp\serialized.bin");
            Console.WriteLine("If this is the first run pres 'f' key");
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            if (consoleKeyInfo.KeyChar == 'f')
            {
                System.IO.Directory.CreateDirectory(@"C:\temp\");
                StandardModel.MyModel fwObject = new MyModel();
                fwObject.count1 = 1;
                fwObject.count2 = 0;
                fwObject.string1 = "test1";
                fwObject.string2 = "test2";
                IFormatter formatter = new BinaryFormatter();

                Stream stream = new FileStream(@"C:\temp\serialized.bin",
                    FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, fwObject);
                stream.Close();
            }
            CheckingLoop();
            Console.ReadKey();
        }

        static void CheckCurrentValue()
        {
            using (Stream stream = new FileStream(@"C:\temp\serialized.bin",
                FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                _coreObject = (MyModel)_formatter.Deserialize(stream);
            }
            
            if (_coreObject.count1 % 2 == 0)
            {
                _shallUpdate = true;
            }
            else
            {
                _shallUpdate = false;
            }
        }

        static void IncrementObjectField()
        {
            _coreObject.count1++;
            using (Stream stream = new FileStream(@"C:\temp\serialized.bin",
                            FileMode.Open, FileAccess.Write, FileShare.None))
            {
                _formatter.Serialize(stream, _coreObject);
            }
            Console.WriteLine(string.Format(".Net Framework program incremented to: {0}", _coreObject.count1.ToString()));
        }

        static void CheckingLoop()
        {
            Console.WriteLine("Checking started. Press ESC to stop");
            do
            {
                while (!Console.KeyAvailable)
                {
                    Thread.Sleep(500);
                    CheckCurrentValue();
                    if (_shallUpdate)
                    {
                        IncrementObjectField();
                    }
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            Console.WriteLine("Checking stoped");
        }
    }
}
