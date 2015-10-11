using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace HomeWork3_serializible
{
    class Menu
    {
        public void Show()
        {
            Office of = DeSerialize();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("введите команду:");
                string[] command = Console.ReadLine().Split(' ');
                int flag;
                if (command.Length == 1)
                {
                    switch (command[0].ToLower())
                    {
                        case "add":
                            Console.Clear();
                            Console.WriteLine("Введите имя нового сотрудника:");
                            string firstName = Console.ReadLine();
                            if (firstName != "")
                            {
                                Console.Clear();
                                Console.WriteLine("Введите фамилию для сотрудника " + firstName);
                                string secName = Console.ReadLine();
                                if (secName != "")
                                {
                                    while (true)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Введите возраст нового сотрудника " + firstName + " " + secName);
                                        string age = Console.ReadLine();
                                        Int32.TryParse(age, out flag);
                                        if (flag != 0)
                                        {
                                            of.AddEmpl(firstName, secName, Convert.ToInt32(age));
                                            Console.WriteLine("Сотрудник " + firstName + " добавлен");
                                            Console.WriteLine("Нажмите любую клавишу для продолжения...");
                                            Console.Read();
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                        case "exit":
                            Serialize(of);
                            return;
                        case "help":
                            Help();
                            break;
                    }
                }
                else if (command.Length == 2)
                {
                    Int32.TryParse(command[1], out flag);
                    switch (command[0].ToLower())
                    {
                        case "del":
                            if (flag != 0)
                            {
                                if (!of.DelEmpl(Convert.ToInt32(command[1]) - 1))
                                {
                                    Console.Write("Вы указали несуществующий индекс сотрудника");
                                }
                            }
                            else if (command[1].ToLower() == "all")
                            {
                                of.DelAll();
                                Console.Write("Список сотрудников очищен.\nНажмите любую клавишу для продолжения...");
                                Console.Read();
                            }
                            break;
                        case "show":
                            if (flag != 0)
                            {
                                Console.WriteLine(of.ShowEmpl(Convert.ToInt32(command[1]) - 1));
                                Console.Write("Нажмите любую клавишу для продолжения...");
                                Console.Read();
                            }
                            else if (command[1] == "all")
                            {
                                Console.WriteLine(of.ToString());
                                Console.Write("Нажмите любую клавишу для продолжения...");
                                Console.Read();
                            }
                            break;
                    }
                }
                else if (command.Length == 3)
                {
                    switch (command[0].ToLower())
                    {
                        case "del":
                            if (!of.DelEmpl(command[1], command[2]))
                            {
                                Console.Write("Вы ввели несуществующего сотрудника. Регистр важен");
                            }
                            break;
                        case "show":
                            Console.WriteLine(of.ShowEmpl(command[1], command[2]));
                            Console.Write("Нажмите любую клавишу для продолжения...");
                            Console.Read();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public string CheckOptions()
        {
            if (File.Exists("options.ini"))
            {
                using (StreamReader sr = new StreamReader("options.ini"))
                {
                    string serializationType = sr.ReadToEnd().ToLower();
                    return serializationType;
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter("options.ini"))
                {
                    sw.Write("bin");
                }
                return "bin";
            }
        }

        public Office DeSerialize()
        {
            
            switch (CheckOptions())
            {
                case "bin":
                    if (File.Exists("data.dat"))
                    {
                        using (FileStream fs = new FileStream("data.dat", FileMode.OpenOrCreate))
                        {
                            BinaryFormatter bf = new BinaryFormatter();
                            //Console.WriteLine("данные из файла data.dat подгружены");
                            return (Office)bf.Deserialize(fs);
                        }
                    }
                        break;
                case "xml":
                        if (File.Exists("data.xml"))
                        {
                            using (FileStream fs = new FileStream("data.xml", FileMode.OpenOrCreate))
                            {
                                XmlSerializer xs = new XmlSerializer(typeof(Office));
                                //Console.WriteLine("данные из файла data.xml подгружены");
                                return (Office)xs.Deserialize(fs);
                            }
                        }
                        break;
            }
            return new Office();
        }

        public void Serialize(Office of)
        {
            switch (CheckOptions())
            {
                case "bin":
                    using (FileStream fs = new FileStream("data.dat", FileMode.OpenOrCreate))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(fs, of);
                    }
                    break;
                case "xml":
                    using (FileStream fs = new FileStream("data.xml", FileMode.OpenOrCreate))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(Office));
                        xs.Serialize(fs, of);
                    }
                    break;
            }
        }

        public void Help()
        {
            Console.WriteLine("\tadd");
            Console.WriteLine("\tdel employeeNumber");
            Console.WriteLine("\tdel employeeFirstName employeeSecondName");
            Console.WriteLine("\tshow employeeFirstName employeeSecondName");
            Console.WriteLine("\tshow employeeNumber");
            Console.WriteLine("\tshow all");
            Console.WriteLine("\tdel all");
            Console.WriteLine("\thelp");
            Console.WriteLine("\texit");
            Console.Write("Нажмите любую клавишу для продолжения...");
            Console.Read();
        }
    }
}
