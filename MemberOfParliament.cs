using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace XMLParserV1
{
    public class MemberOfParliament : IPerson, IFinancialData,IPoliticalData
    {
        // Person Details
        public string? Name { get; set; }

        public string? Id { get; set; }
        // Aditional
        public string FullLink { get; set; } = "https://www.theyworkforyou.com/mp/";
        // Image 
        private string ImageURL = "https://www.theyworkforyou.com/people-images/mps/";

        public Image? MemberPhoto { get; set; }

        
        // Financial Details
        public List<string>? PaymentDateReceived { get; set; }
        public List<int>? PaymentsReceived { get; set; }
        public List<string>? DonnorNames { get; set; }
        // Display String
        public string DisplayDonnorNames { get; set; }
        public int FullAmountReceived{ get; set; }
        public string Party { get; set; }
        public string Constituency { get; set; }

        // Constructor
        public MemberOfParliament(string Name, string Id, List<string> PaymentDateReceived, List<int> PaymentsReceived, List<string> DonnorNames)
        {
            this.Name = Name;
            this.Id = Id;
            this.PaymentDateReceived= PaymentDateReceived;
            this.PaymentsReceived = PaymentsReceived;
            this.DonnorNames = DonnorNames;
            this.DisplayDonnorNames = string.Join(",", DonnorNames);
            System.Diagnostics.Debug.WriteLine("Donor Names Added to MEM " + Name);
            System.Diagnostics.Debug.WriteLine(DonnorNames.Count);
            this.FullLink += Id;
            FullAmountReceived = CalculateFullAmmount(PaymentsReceived);
            this.ImageURL += Id + ".jpg";
            System.Diagnostics.Debug.WriteLine(this.ImageURL);
            // Get Image
            if(this.ImageURL != null || this.ImageURL != "")
            {
                WebClient wClient = new WebClient();
                try
                {
                    byte[] imageByte = wClient.DownloadData(ImageURL);
                    MemoryStream stream = new MemoryStream(imageByte);
                    MemberPhoto = Image.FromStream(stream);
                }
                catch(Exception ex)
                {
                    byte[] imageByte = wClient.DownloadData("https://cdn-icons-png.flaticon.com/128/2748/2748558.png");
                    MemoryStream stream = new MemoryStream(imageByte);
                    MemberPhoto = Image.FromStream(stream);
                }
                
                //MemberPhoto = Image.FromStream(stream);
            }
            
        }
        public int CalculateFullAmmount(List<int> PaymentsReceived)
        {
            foreach(int payment in PaymentsReceived)
            {
                FullAmountReceived += payment;
            }
            return FullAmountReceived; 
        }
    }
}
