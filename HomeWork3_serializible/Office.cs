using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork3_serializible
{
    [Serializable]
    public class Office
    {
        private List<Employee> _listEmpl;

        public List<Employee> ListEmpl
        {
            get
            {
                return _listEmpl;
            }
        }

        public Office()
        {
            _listEmpl = new List<Employee>();
        }

        public string ShowEmpl(int index)
        {
            return _listEmpl[index].ToString();
        }

        public string ShowEmpl(string firstName, string secondName)
        {
            Employee emp = new Employee(firstName, secondName);
            if (_listEmpl.Contains(emp))
            {
                return _listEmpl[FindEmp(emp) - 1].ToString();
            }
            else
            {
                return firstName + " " + secondName + " отсутствует в базе, возможно стоит проверить регистр";
            }
        }

        private int FindEmp(Employee emp)
        {
            for (int i = 0; i < _listEmpl.Count; i++)
            {
                if (_listEmpl[i].Equals(emp))
                {
                    return i + 1;
                }
            }
            return 0;
        }

        public void AddEmpl(string firstName, string secondName, int age)
        {
            _listEmpl.Add(new Employee(firstName, secondName, age));
        }

        public bool DelEmpl(int index)
        {
            if (_listEmpl.Count < index + 1)
            {
                return false;
            }
            else
            {
                _listEmpl.RemoveAt(index);
                return true;
            }
        }

        public bool DelEmpl(string firstName, string secondName)
        {
            Employee emp = new Employee(firstName, secondName);
            if (_listEmpl.Contains(emp))
            {
                _listEmpl.Remove(emp);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DelAll()
        {
            _listEmpl.Clear();
        }

        public override string ToString()
        {
            string result = null;
            int i = 1;
            foreach (Employee emp in _listEmpl)
            {
                result += i + ". " + emp.ToString() + "\n";
                i++;
            }
            return result;
        }
    }
}
