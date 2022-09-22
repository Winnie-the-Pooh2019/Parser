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

        public List<CompactDisk> Storage { get => _disks; set { _disks = value; } }

        public Catalog(List<CompactDisk> data)
        {
            var dataArray = new CompactDisk[data.Count];
            data.CopyTo(dataArray);
            _disks = dataArray.ToList();
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
        public IStorage<CompactDisk> FormatParse(string filepath)
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

            return new Catalog(result);
        }
    }
}
