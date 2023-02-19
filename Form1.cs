using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Xml;

namespace XMLParserV1
{
    public partial class Form1 : Form
    {
        private BackgroundWorker Worker;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Read Local CSV 
            CSVLocalFileReader reader = new CSVLocalFileReader();
            List<string> LocalOutput =  reader.ReadData("C:\\Users\\Haroldas\\source\\repos\\XMLParserV1\\mps.csv", 20);
            // Get and Read XML From URL
            XMLParserFromURL XMLParser = new XMLParserFromURL("https://www.theyworkforyou.com/pwdata/scrapedxml/regmem/regmem2021-12-13.xml");
            XMLParser.GetRecordCount = 20;
            List<MemberOfParliament> List = XMLParser.GetAllData();
            System.Diagnostics.Debug.WriteLine(List.Count);
            // Add CSV Data
            for (int i = 0; i < List.Count; i++)
            {
                string LocalCSVLine = LocalOutput[i + 1];
                var values = LocalCSVLine.Split(',');
                //Party
                List[i].Party = values[3];
                // Constituency 
                List[i].Constituency = values[4];
                System.Diagnostics.Debug.WriteLine(List[i].FullLink);
                System.Diagnostics.Debug.WriteLine(LocalOutput[i]);
            }
            // List Formed Create Data Table 
            DataTable dataTable= new DataTable();
            // Data Table collumns
            dataTable.Columns.Add("Index", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("Party", typeof(string));
            dataTable.Columns.Add("Constituency", typeof(string));
            dataTable.Columns.Add("Donors", typeof(string));
            dataTable.Columns.Add("Amount Received", typeof(int));
            dataTable.Columns.Add("Image", typeof(Image));
            dataTable.Columns.Add("Link", typeof(string));
            // Add Date to DATA TABLE
            for(int i = 0; i < List.Count- 1; i++)
            {
                // data to Add
                int index = i + 1;
                string Name = List[i].Name;
                string ID = List[i].Id;
                string Party = List[i].Party;
                string Constituency = List[i].Constituency;
                List<string> _Donors = List[i].DonnorNames;
                string DisplayDonor;
                System.Diagnostics.Debug.WriteLine(_Donors.Count);
                if (_Donors.Count() > 0)
                {
                    DisplayDonor = _Donors[0];
                }else
                {
                    DisplayDonor = "Not Found";
                }
               
                
                int Donations = List[i].FullAmountReceived;
                Image memPhoto = List[i].MemberPhoto;
                string Link = List[i].FullLink;
                // Clean Political Data
                Party = ReplaceNonAlhpaNumeric(Party);
                Constituency = ReplaceNonAlhpaNumeric(Constituency);
                dataTable.Rows.Add(index, Name, ID, Party, Constituency, DisplayDonor, Donations, memPhoto, Link);
            }
            DataGridViewComboBoxColumn Donors = new DataGridViewComboBoxColumn();

            dataGridView1.DataSource = dataTable;
            //dataTable.Columns.Add(string Name, string ID, LinkLabel Link,  )
            //dataGridView1.DataSource = List;

            // Get All candidates with Background worker
            // 
            this.Worker = new BackgroundWorker();
            this.Worker.DoWork += new DoWorkEventHandler(bw_DoWork);
            this.Worker.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            this.Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            this.Worker.WorkerReportsProgress = true;
            if(!this.Worker.IsBusy)
            {
                 this.Worker.RunWorkerAsync();
            }
            //dataGridView1.DataSource = List;
        }

        private void bw_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Background Worker Completed!!!");

        }

        private void bw_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        // Background Worker Do work
        private void bw_DoWork(object? sender, DoWorkEventArgs e)
        {
            // Read Local CSV Local 
            CSVLocalFileReader reader = new CSVLocalFileReader();
            List<string> LocalOutput = reader.ReadData("C:\\Users\\Haroldas\\source\\repos\\XMLParserV1\\mps.csv");
            // Read XML From URL
            XMLParserFromURL XMLParser = new XMLParserFromURL("https://www.theyworkforyou.com/pwdata/scrapedxml/regmem/regmem2021-12-13.xml");
            //XMLParser.GetRecordCount = 20;
            List<MemberOfParliament> List = XMLParser.GetAllData();
            System.Diagnostics.Debug.WriteLine(List.Count);
            for (int i = 0; i < List.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(List[i].FullLink);
                string LocalCSVLine = LocalOutput[i + 1];
                var values = LocalCSVLine.Split(',');
                //Party
                List[i].Party = values[3];
                // Constituency 
                List[i].Constituency = values[4];
            }
            dataGridView1.Invoke((MethodInvoker)(() => { dataGridView1.DataSource = List; }));
            //dataGridView1.DataSource = List;

            System.Diagnostics.Debug.WriteLine(List.Count);
          

                
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                string SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if(SelectedValue.Contains("http"))
                {
                    System.Diagnostics.Debug.WriteLine(SelectedValue);
                    //System.Diagnostics.Process.Start(SelectedValue);
                    OpenUrl(SelectedValue);
                }
                //MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        // Open URL Method
        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
        // Replace Non AlphaNumerics
        string ReplaceNonAlhpaNumeric(string value)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            string str  = rgx.Replace(value, "");
            return str;
        }
    }
}