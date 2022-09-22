using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    internal static class FromXmlToList<R> where R : IItem
    {
        public static IStorage<R> ToList(string filepath, IParse<R> parser)
        {
            IStorage<R> storage;
            try
            {
                storage = parser.FormatParse(filepath);
            } catch (NullReferenceException)
            {
                MessageBox.Show("No file found or incorrect file filling", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } catch (Exception)
            {
                MessageBox.Show("Incorrect data format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return parser.FormatParse(filepath);
        }
    }
}
