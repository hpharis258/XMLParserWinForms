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
            // New List Box
            ListBox testListBox = new ListBox();
            // Set size and location of list Box
            testListBox.Size = new System.Drawing.Size(800, 800);
            testListBox.Location = new System.Drawing.Point(0, 0);
            // Add list Box to the Form
            this.Controls.Add(testListBox);
            //
            string RemoveNonNumeric(string value) => Regex.Replace(value, "[^0-9]", "");
            string memFullLink = "https://www.theyworkforyou.com/mp/";
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Ignore;
            XmlReader reader = XmlReader.Create("https://www.theyworkforyou.com/pwdata/scrapedxml/regmem/regmem2021-12-13.xml", settings);
            //List<Regmem> regmem = new List<Regmem>();
            List <String> regmemDetails = new List<String>();
            List <String> regMemDate = new List<String>();
            List <String> LinkList = new List<String>();
            List <String> Received = new List<String>();    
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "regmem")
                {
                    string? MemberName = reader.GetAttribute("membername");
                    string? Date = reader.GetAttribute("date");
                    string? PersonID = reader.GetAttribute("personid");
                    // Check that they are not Null
                    if (MemberName != null && Date != null && PersonID != null)
                    {
                        string clean = RemoveNonNumeric(PersonID);
                        regmemDetails.Add(MemberName);
                        regMemDate.Add(Date);
                        LinkList.Add(memFullLink + clean);
                        //regmemDetails.Add(Date);
                        //regmemDetails.Add(memFullLink + clean);

                        // Create and Save New Regmem
                        //Regmem created = new Regmem();
                        //created.Membername = MemberName;
                        //created.Date = Date;
                        //created.Personid = memFullLink + clean;
                        //regmem.Add(created);
                        //Console.WriteLine("Member Name: " + created.Membername + "\nDate Record Created: " + created.Date + "\nLink:  " + created.Personid);
                        //Console.WriteLine("//////////////////////////////////////////////////////////////////////////////////////////");
                    }
                }
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "category")
                {
                    string? type = reader.GetAttribute("type");
                    string? name = reader.GetAttribute("name");
                    if (type != null && name != null)
                    {
                        //Console.WriteLine(name + " " + type);
                        Console.WriteLine("Category Type:  " + type);
                        Console.WriteLine("Category Name: " + name);
                        Console.WriteLine("/////////////////////////////////////////////////");
                    }
                }
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "item")
                {
                    //
                    string Item = reader.ReadInnerXml();
                    if (Item.Contains("received") || Item.Contains("Payment") && Item.Contains("£"))
                    {
                        //Console.WriteLine(Item); Clean
                        string cleanItem = RemoveNonNumeric(Item);
                        Received.Add(cleanItem); 
                    }
                }
                //testListBox.MultiColumn = true;
                //testListBox.ColumnWidth = 100;
                //testListBox.C
                System.Diagnostics.Debug.WriteLine(regmemDetails.Count());
                int memCount = regmemDetails.Count();
                if(memCount == 647) 
                {
                    testListBox.BeginUpdate();
                    for (int i = 0; i < memCount; i++)
                    {
                        testListBox.Items.Add(regmemDetails[i] + "          " + regMemDate[i] + "          " + LinkList[i]  + "     " + Received[i]);
                    }
                    testListBox.EndUpdate();
                    //
                   

                }

            }
        }
    }
}