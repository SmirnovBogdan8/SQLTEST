using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;

namespace SQLTEST
{
    class Person
    {
        public string name;
        public string surname;
        public string patronymic;
        public string birthDate;
        
        public Person(string name, string surname, string patronymic, string birthDate) 
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
            StreamReader sr = new StreamReader("data.txt");
            string line = sr.ReadLine(); 
            data.Add(line);
            while (line != null)
            {
                line = sr.ReadLine();
                data.Add(line);
            }

            //close the file
            sr.Close();
            foreach (string item in data)
            {
                if (item == null) continue;
                string[] arr = item.Split(' ');
                Person person = new Person(arr[0],arr[1],arr[2],arr[3]);
                string query = $"INSERT INTO person(name,surname,patronymic,birthDate) VALUES(\"{ person.name }\",\"{ person.surname }\",\"{ person.patronymic }\",\"{ person.birthDate }\"); ";
                string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=testbd;";
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
                MySqlDataReader reader;
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
            }
            
        }
    }
}
