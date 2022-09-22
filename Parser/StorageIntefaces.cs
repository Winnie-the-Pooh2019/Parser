using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    interface IStorage<R> where R : IItem
    {
        public List<R> Storage { get; set; }
    }

    interface IItem { }

    interface IParse<R> where R : IItem
    {
        public IStorage<R> FormatParse(string text);
    }
}
