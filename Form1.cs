using System.Text.RegularExpressions;
using System.Xml;

namespace XMLParserV1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Read Local CSV 
            CSVLocalFileReader reader = new CSVLocalFileReader();
            reader.ReadData("C:\\Users\\Haroldas\\source\\repos\\XMLParserV1\\mps.csv");
            XMLParserFromURL XMLParser = new XMLParserFromURL("https://www.theyworkforyou.com/pwdata/scrapedxml/regmem/regmem2021-12-13.xml");
            List<MemberOfParliament> List = XMLParser.GetAllData();
            System.Diagnostics.Debug.WriteLine(List.Count);
            for (int i = 0; i < List.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(List[i].FullLink);
            }
            dataGridView1.DataSource = List;
    }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}