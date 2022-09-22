namespace Parser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = openFileDialog1.FileName;
            Catalog catalog = (Catalog)FromXmlToList<CompactDisk>.ToList(filename, new CatalogParser());

            MakeTree(catalog);
        }

        private void MakeTree(Catalog catalog)
        {
            treeView1.Nodes.Clear();

            foreach (CompactDisk disk in catalog.Storage)
            {
                TreeNode parentNode = new TreeNode(disk.Title);

                TreeNode artist = new TreeNode("Artist");
                TreeNode artistValue = new TreeNode(disk.Artist);
                artist.Nodes.Add(artistValue);
                parentNode.Nodes.Add(artist);

                TreeNode country = new TreeNode("Country");
                TreeNode countryValues = new TreeNode(disk.Country);
                country.Nodes.Add(countryValues);
                parentNode.Nodes.Add(country);

                TreeNode company = new TreeNode("Company");
                TreeNode companyValues = new TreeNode(disk.Company);
                company.Nodes.Add(companyValues);
                parentNode.Nodes.Add(company);

                TreeNode price = new TreeNode("Price");
                TreeNode priceValue = new TreeNode(disk.Price.ToString());
                price.Nodes.Add(priceValue);
                parentNode.Nodes.Add(price);

                TreeNode year = new TreeNode("Year");
                TreeNode yearValue = new TreeNode(disk.Year.ToString());
                year.Nodes.Add(yearValue);
                parentNode.Nodes.Add(year);

                treeView1.Nodes.Add(parentNode);
            }
        }
    }
}