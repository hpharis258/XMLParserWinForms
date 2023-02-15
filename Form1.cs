using System.ComponentModel;
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
            //CSVLocalFileReader reader = new CSVLocalFileReader();
            //reader.ReadData("C:\\Users\\Haroldas\\source\\repos\\XMLParserV1\\mps.csv");
            XMLParserFromURL XMLParser = new XMLParserFromURL("https://www.theyworkforyou.com/pwdata/scrapedxml/regmem/regmem2021-12-13.xml");
            XMLParser.GetRecordCount = 20;
            List<MemberOfParliament> List = XMLParser.GetAllData();
            System.Diagnostics.Debug.WriteLine(List.Count);
            for (int i = 0; i < List.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(List[i].FullLink);
            }
            dataGridView1.DataSource = List;
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

        private void bw_DoWork(object? sender, DoWorkEventArgs e)
        {
            XMLParserFromURL XMLParser = new XMLParserFromURL("https://www.theyworkforyou.com/pwdata/scrapedxml/regmem/regmem2021-12-13.xml");
            //XMLParser.GetRecordCount = 20;
            List<MemberOfParliament> List = XMLParser.GetAllData();
            System.Diagnostics.Debug.WriteLine(List.Count);
            for (int i = 0; i < List.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(List[i].FullLink);
            }
            dataGridView1.Invoke((MethodInvoker)(() => { dataGridView1.DataSource = List; }));
            //dataGridView1.DataSource = List;

            System.Diagnostics.Debug.WriteLine(List.Count);
          

                
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}