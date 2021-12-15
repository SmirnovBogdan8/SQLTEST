using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;

namespace SQLTEST
{
    class Person
    {
        private string name;
        public string Name
        { 
            
            get {
                return name; 
            }
            private set {
                name = value;
            }
        }
        private string surname;
        public string Surname {
            get
            {
                return surname;
            }
            private set
            {
                surname = value;
            }
        }
        private string patronymic;
        public string Patronymic {
            get
            {
                return patronymic;
            }
            private set
            {
                patronymic = value;
            }
        }
        private DateTime birthDate;
        public DateTime BirthDate {
            get
            {
                return birthDate;
            }
            private set
            {
                birthDate = value;
            }
        }

        public Person(string name, string surname, string patronymic, DateTime birthDate) 
        {
            this.name = name;
            this.surname = surname;
            this.patronymic = patronymic;
            this.birthDate = birthDate;
        }
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<string> data = new List<string>();
            Console.WriteLine("введите путь к файлу:");//поправка 2
            string path = Console.ReadLine() ;
            try
            {
                StreamReader sr = new StreamReader(path);
                string line = sr.ReadLine();
                data.Add(line);
                while (line != null)
                {
                    line = sr.ReadLine();
                    data.Add(line);
                }
                //close the file
                sr.Close();
            }
            catch (FileNotFoundException e) 
            {
                Console.WriteLine("Файл не найден!");
                return;
            }
            
            
            string query;
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=testbd;";
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase;
            MySqlDataReader reader;
            try 
            { 
                databaseConnection.Open(); 
            }
            catch(Exception e)
            {
                Console.WriteLine("Не удалось подключиться к БД!");
                return;
            }
            foreach (string item in data)
            {
                if (item == null) continue;
                string[] arr = item.Split(' ');
                Person person = new Person(arr[0],arr[1],arr[2], DateTime.Parse(arr[3]));
                query = $"INSERT INTO person(name,surname,patronymic,birthDate) VALUES(\"{ person.Name }\",\"{ person.Surname }\",\"{ person.Patronymic }\",\"{ person.BirthDate.ToString("yyyy-MM-dd") }\"); ";
                commandDatabase = new MySqlCommand(query, databaseConnection);
                try {
                    reader = commandDatabase.ExecuteReader();
                    reader.Close();
                }
                catch(MySqlException e)
                {
                   Console.WriteLine($"Получена ошибка {e}!");
                    return;
                }
                
            }

        }
    }
}
