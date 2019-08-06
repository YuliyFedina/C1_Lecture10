using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Ex1
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Какую часть задания хотите выполнить?");
            Console.WriteLine("Если первую, введите 1 ");
            Console.WriteLine("Ввести с консоли N чисел, записать их в файл. Считать числа из файла," +
                              "вывести на консоль. Сделать любым способом. ");
            Console.WriteLine();
            Console.WriteLine("Если вторую, введите что-нибудь другое");
            Console.WriteLine(
                "Считать из файла flowCards.Card (https://gitlab.2gis.ru/commonformat/commonformat/blob/master/CommonFormat/CommonFormat/Example/flowCards.Card.xml)" +
                @"файл лежит: ...\Ex1\bin\Debug\netcoreapp2.2\Card.xml" +
                "все контакты, сохранить в 2 разных файлах:" +
                "рекламные и не рекламные контакты (разделять по атрибуту IsPromotional).Формат файла: < Contact_Value > [< Description >]");
            if (Console.ReadLine() == "1")
            {
                //Ввести с консоли N чисел, записать их в файл. Считать числа из файла,
                //вывести на консоль. Сделать любым способом.

                const string fileForLecture10 =
                    @"C:\Users\yu.belova\source\repos\Lectures\Lecture10\fileForLecture10.txt";
                using (new StreamWriter(fileForLecture10, false, Encoding.Default))
                {
                }

                Console.WriteLine("Вводите числа через enter (если хотите завершить, введите \"-1\"): ");

                int value;
                while ((value = Helper.InputInt()) != -1)
                    using (var sw = new StreamWriter(fileForLecture10, true, Encoding.Default))
                    {
                        sw.WriteLine(value);
                    }

                try
                {
                    Console.WriteLine();
                    Console.WriteLine("Считываем файл...");
                    Console.WriteLine("Числа из файла:");
                    using (var sr = new StreamReader(fileForLecture10))
                    {
                        Console.WriteLine(sr.ReadToEnd());
                    }
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            else
            {
                //Считать из файла flowCards.Card (https://gitlab.2gis.ru/commonformat/commonformat/blob/master/CommonFormat/CommonFormat/Example/flowCards.Card.xml)
                //файл лежит: ...\Ex1\bin\Debug\netcoreapp2.2\Card.xml
                //все контакты, сохранить в 2 разных файлах:
                //рекламные и не рекламные контакты (разделять по атрибуту IsPromotional).
                //Формат файла: < Contact_Value > [< Description >]

                var promotionalContacts = new XDocument();
                var cardWithPromotionalContacts = new XElement("Card");

                var notPromotionalContacts = new XDocument();
                var cardWithNotPromotionalContacts = new XElement("Card");

                var xDoc = new XmlDocument();
                xDoc.Load("Card.xml");
                var xRoot = xDoc.DocumentElement;

                foreach (XmlNode xnode in xRoot)
                    if (xnode.Name == "Contacts")
                        foreach (XmlNode childnode in xnode.ChildNodes)
                            if (childnode.Attributes.Count > 0)
                            {
                                var attr = childnode.Attributes.GetNamedItem("IsPromotional");
                                if (attr.InnerText == "true")
                                {
                                    var contact = new XElement($"{childnode.Name}");
                                    var contactValue = new XAttribute("Contact_Value",
                                        $"{childnode.Attributes.GetNamedItem("Value")?.Value}");
                                    var description = new XAttribute("Description",
                                        $"{childnode.Attributes.GetNamedItem("Description")?.Value}");

                                    contact.Add(contactValue);
                                    contact.Add(description);

                                    cardWithPromotionalContacts.Add(contact);
                                }

                                if (attr.InnerText == "false")
                                {
                                    var contact = new XElement($"{childnode.Name}");
                                    var contactValue = new XAttribute("Contact_Value",
                                        $"{childnode.Attributes.GetNamedItem("Value")?.Value}");
                                    var description = new XAttribute("Description",
                                        $"{childnode.Attributes.GetNamedItem("Description")?.Value}");

                                    contact.Add(contactValue);
                                    contact.Add(description);

                                    cardWithNotPromotionalContacts.Add(contact);
                                }
                            }

                promotionalContacts.Add(cardWithPromotionalContacts);
                promotionalContacts.Save("PromotionalContacts.xml");
                Console.WriteLine("Рекламные контакты:");
                Console.WriteLine(promotionalContacts);
                Console.WriteLine();

                notPromotionalContacts.Add(cardWithNotPromotionalContacts);
                notPromotionalContacts.Save("NotPromotionalContacts.xml");
                Console.WriteLine("Не рекламные контакты:");
                Console.WriteLine(notPromotionalContacts);
                Console.WriteLine();
            }
        }
    }
}