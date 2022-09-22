using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Parser
{
    class Catalog : IStorage<CompactDisk>
    {
        private List<CompactDisk> _disks;
        private IParse<CompactDisk> _parse;

        public List<CompactDisk> Storage { get => _disks; set { _disks = value; } }
        public IParse<CompactDisk> Parser { get => _parse; set { _parse = value; } }

        public Catalog(string text)
        {
            _parse = new CatalogParser();
            _disks = _parse.formatParse(text);
        }

        public override string ToString()
        {
            string str = "";

            Storage.ForEach(it => str += $"{it}\n");

            return str;
        }
    }

    class CompactDisk : IItem 
    {
        public string Title { get; init; }
        public string Artist { get; init; }
        public string Country { get; init; }
        public string Company { get; init; }
        public double Price { get; init; }
        public int Year { get; init; }

        public CompactDisk(string title, string artist, string country, string company, double price, int year)
        {
            Title = title;
            Artist = artist;
            Country = country;
            Company = company;
            Price = price;
            Year = year;
        }

        public override string ToString()
        {
            return "{\n\t" +
            Title + "\n\t" +
            Artist + "\n\t" +
            Country + "\n\t" +
            Company + "\n\t" +
            Price.ToString() + "\n\t" +
            Year.ToString() + "\n}";
        }
    }

    class CatalogParser : IParse<CompactDisk>
    {
        public List<CompactDisk> formatParse(string filepath)
        {
            XDocument xdoc = XDocument.Load(filepath);
            XElement? firstElement = xdoc.Element("CATALOG");

            if (firstElement == null)
                throw new NullReferenceException("Failture while document parsing");

            List<CompactDisk> result = new List<CompactDisk>();

            foreach (XElement cd in firstElement.Elements("CD"))
            {
                CompactDisk disk = new CompactDisk(
                    cd.Element("TITLE")!.Value,
                    cd.Element("ARTIST")!.Value,
                    cd.Element("COUNTRY")!.Value,
                    cd.Element("COMPANY")!.Value,
                    double.Parse(cd.Element("PRICE")!.Value),
                    int.Parse(cd.Element("YEAR")!.Value)
                    );

                result.Add(disk);
            }

            return result;
        }
    }

    interface IStorage<R> where R : IItem
    {
        public IParse<R> Parser { get; set; }
        public List<R> Storage { get; set; }
    }

    interface IItem {}

    interface IParse<R> where R : IItem
    {
        public List<R> formatParse(string text);
    }
}
