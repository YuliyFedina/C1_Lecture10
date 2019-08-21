using System.Xml;
using System.Xml.Linq;

namespace Ex2
{
    internal class Program
    {
        private static void Main()
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
            xDoc.Load(@"..\..\..\Card.xml");

            var xRoot = xDoc.DocumentElement;

            foreach (XmlNode xNode in xRoot)
                if (xNode.Name == "Contacts")
                    foreach (XmlNode childNode in xNode.ChildNodes)
                        if (childNode.Attributes.Count > 0)
                        {
                            var attr = childNode.Attributes.GetNamedItem("IsPromotional");
                            if (attr.InnerText == "true")
                            {
                                var contact = new XElement($"{childNode.Name}");
                                var contactValue = new XAttribute("Contact_Value",
                                    $"{childNode.Attributes.GetNamedItem("Value")?.Value}");
                                var description = new XAttribute("Description",
                                    $"{childNode.Attributes.GetNamedItem("Description")?.Value}");

                                contact.Add(contactValue);
                                contact.Add(description);

                                cardWithPromotionalContacts.Add(contact);
                            }

                            if (attr.InnerText == "false")
                            {
                                var contact = new XElement($"{childNode.Name}");
                                var contactValue = new XAttribute("Contact_Value",
                                    $"{childNode.Attributes.GetNamedItem("Value")?.Value}");
                                var description = new XAttribute("Description",
                                    $"{childNode.Attributes.GetNamedItem("Description")?.Value}");

                                contact.Add(contactValue);
                                contact.Add(description);

                                cardWithNotPromotionalContacts.Add(contact);
                            }
                        }

            promotionalContacts.Add(cardWithPromotionalContacts);
            promotionalContacts.Save(@"..\..\..\PromotionalContacts.xml");

            notPromotionalContacts.Add(cardWithNotPromotionalContacts);
            notPromotionalContacts.Save(@"..\..\..\NotPromotionalContacts.xml");
        }
    }
}