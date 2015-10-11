using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork3_serializible
{
    [Serializable]
    public class Employee
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        private int _age;
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                if (value > 0 && value < 100)
                {
                    _age = value;
                }
            }
        }

        public Employee()
        {

        }

        public Employee (string firstName, string secondName, int age)
        {
            FirstName = firstName;
            SecondName = secondName;
            Age = age;
        }

        public Employee(string firstName, string secondName)
        {
            FirstName = firstName;
            SecondName = secondName;
        }

        public override string ToString()
        {
            return "сотрудник " + FirstName + " " + SecondName + ", возраст " + Age;
        }

        public override bool Equals(object obj)
        {
            if (((Employee)obj).FirstName == FirstName && ((Employee)obj).SecondName == SecondName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
