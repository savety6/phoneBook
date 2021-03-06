using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace classExample
{
    class Program
    {


        public static string Path = @"C:\Users\vlad\Desktop\PhoneBook.txt";
        class Contact
        {
            public string name;

            public List<string> phone = new List<string>();
            public List<string> email = new List<string>();
        }

        class PhoneBook
        {
            

            private List<Contact> contacts = new List<Contact>();

            public void Start()
            {
                string[] Text = File.ReadAllLines(Path);
                

                for (int i = 0; i < Text.GetLength(0); i += 3)
                {
                    if (Text[i]== "")
                    {
                        return;
                    }
                    this.Add(Text[i], Text[i + 1], Text[i + 2]);
                }
            }

            public void Add(string name, string phone, string email)
            {
                Contact _contact = null;
                for (int i = 0; i < contacts.Count; i++)
                {
                    if (contacts[i].name == name)
                    {
                        _contact = contacts[i];
                        break;
                    }
                }
                if (_contact == null)
                {
                    _contact = new Contact { name = name };
                contacts.Add(_contact);
                }
                if (!String.IsNullOrEmpty(phone))
                {
                    _contact.phone.Add(phone);
                }
                if (!String.IsNullOrEmpty(email))
                {
                    _contact.email.Add(email);
                }
            }

            public bool Find(string name, out string phones, out string emails)
            {
                phones = null;
                emails = null;
                for (int i = 0; i < contacts.Count; i++)
                {
                    if (contacts[i].name == name)
                    {
                        phones = "";
                        for (int j = 0; j < contacts[i].phone.Count; j++)
                        {
                            phones += contacts[i].phone[j] + ", ";
                        }
                        emails = "";
                        for (int j = 0; j < contacts[i].email.Count; j++)
                        {
                            emails += contacts[i].email[j] + ", ";
                        }

                        return true;
                    }
                }
                return false;
            }

            public bool Delete(string name)
            {
                for (int i = contacts.Count - 1; i >= 0; i--)
                {
                    if (contacts[i].name == name)
                    { 
                        contacts.RemoveAt(i);
                        return true;
                    }
                }
                return false;
            }
            public void ListAll()
            {
                for (int i = 0; i < contacts.Count; i++)
                {
                    Console.WriteLine("\nName: " + contacts[i].name);
                    Console.WriteLine("\nPhones: ");
                    for (int j = 0; j < contacts[i].phone.Count; j++)
                    {
                        Console.Write(contacts[i].phone[j] + ", \n");
                    }
                    Console.WriteLine("\nE-mails: ");
                    for (int j = 0; j < contacts[i].email.Count; j++)
                    {
                        Console.Write(contacts[i].email[j] + ", \n");
                    }
                }
            }

            public void Save()
            {
                string[] Text = new string[100];
                for (int i = 0, x = 0; i < contacts.Count; i++, x++)
                {
                    Text[x] = contacts[i].name;
                    x++;
                    for (int j = 0; j < contacts[i].phone.Count; j++)
                    {
                        Text[x] += contacts[i].phone[j] + ", ";
                    }
                    x++;
                    for (int j = 0; j < contacts[i].email.Count; j++)
                    {
                        Text[x] += contacts[i].email[j] + ", ";
                    }            
                }
                File.WriteAllLines(Path, Text);
            }
        }

        


        static void Main()
        {
            var phoneBook = new PhoneBook();
            phoneBook.Start();
            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine("your options are\na = Add; f = Find; d = Delete; l = List; q = Quit");

                char key = Console.ReadKey().KeyChar;

                switch (key)
                {
                    case 'a':
                        Console.Write("Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Phone: ");
                        string phone = Console.ReadLine();
                        
                        Console.Write("E-mail: ");
                        string email = Console.ReadLine();

                        phoneBook.Add(name, phone, email);
                        break;

                    case 'f':
                        Console.Write("Name: ");
                        string f_name = Console.ReadLine();
                        if (phoneBook.Find(f_name, out string f_phone, out string f_email))
                        {
                            Console.WriteLine("Phone: " + f_phone);
                            Console.WriteLine("E-mail: " + f_email);
                        }
                        else
                        {
                            Console.WriteLine("Not Found!!!");
                        }
                        Console.ReadLine();
                        break;

                    case 'd':
                        Console.Write("Name: ");
                        string d_name = Console.ReadLine();
                        if (phoneBook.Delete(d_name))
                        {
                            Console.WriteLine("Success");
                        }
                        else
                        {
                            Console.WriteLine("invalid name!!!");
                        }
                        Console.ReadLine();
                        break;

                    case 'l':
                        phoneBook.ListAll();
                        Console.ReadLine();
                        break;

                    case 'q':
                        phoneBook.Save();
                        run = false;
                        break;
                }
            }

        }
    }
}
