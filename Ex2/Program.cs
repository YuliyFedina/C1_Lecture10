using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Ex2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Считать из файла flowCards.Card
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

            notPromotionalContacts.Add(cardWithNotPromotionalContacts);
            notPromotionalContacts.Save("NotPromotionalContacts.xml");
        }
    }
}
