using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace XMLParserV1
{

    public class XMLParserFromURL : IFileReader<MemberOfParliament>
    {
        public string URL { get; set; }
        public int GetRecordCount { get; set; }
        public XMLParserFromURL(string URL, int getRecordCount = 0)
        {
            this.URL = URL;
            GetRecordCount = getRecordCount;
        }
        // Validate Schema
        public string ValidateSchema()
        {
            return "Add Code that validates Schema";
        }
        // Helper Method
        public string RemoveNonNumberic(string input)
        {
            string clean = Regex.Replace(input, "[^0-9]", "");
            return clean;
        }
        // Read MP Data 
        public List<MemberOfParliament> ReadMPData()
        {
            if(this.URL== null || this.URL == "" )
            {
                throw new Exception("Supplied Url is Invalid!!!");
            }
            else
            {
                // Create XML Settings Object
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.DtdProcessing = DtdProcessing.Ignore;
                XmlReader reader = XmlReader.Create(this.URL, settings);
                // List Of Members 
                List<MemberOfParliament> members = new List<MemberOfParliament>();
                // Temp variables to read the XML
                string? TempName = "";
                string? tempId = "";
                string? DateRecordAdded = "";
                List<int> payments = new List<int>();
                List<string> paymentDates = new List<string>();
                List<string> donnorName = new List<string>();
                
                while (reader.Read())
                {
                    
                    if(reader.NodeType == XmlNodeType.Element && reader.Name == "regmem")
                    {
                        // Extract Member name and Id
                        string? MemberName = reader.GetAttribute("membername");
                        TempName = MemberName;
                        string? PersonLinkRaw = reader.GetAttribute("personid");
                        tempId = RemoveNonNumberic(PersonLinkRaw);
                        string? date = reader.GetAttribute("date");
                        DateRecordAdded = date;

                    }
                    // Extract Money Received
                    if(reader.NodeType == XmlNodeType.Element && reader.Name == "item")
                    {
                        while(reader.Name == "item" || reader.Name == "category" || reader.Name == "record")
                        {
                            string Item = reader.ReadInnerXml();
                            if (Item.Contains("Name of donor:"))
                            {
                                int indexOf = Item.IndexOf("donor:");
                                int indexOfBR = Item.IndexOf("<br");
                                int diff = indexOfBR - indexOf;
                                string NameOfDonnor = Item.Substring(indexOf, diff);
                                string removeDonorPretext = NameOfDonnor.Replace("donor: ", "");
                                //Console.WriteLine(NameOfDonnor);
                                donnorName.Add(removeDonorPretext);
                            }
                            if (Item.Contains("received") && Item.Contains("£"))
                            {
                                int indexOfPound = Item.IndexOf("£");
                                int indexOfVat = Item.IndexOf("VAT");

                                string sub = Item.Substring(indexOfPound, 10);
                                string clean = RemoveNonNumberic(sub);
                                if (clean != null || clean != "")
                                {
                                    long paymentNum = Int64.Parse(clean);
                                    int converted = (int)paymentNum;
                                    payments.Add(converted);
                                }


                            }
                            if (Item.Contains("Date received"))
                            {
                                int indexOfFirst = Item.IndexOf("received:");
                                int idexOfLast = Item.IndexOf("</br>");
                                string Date = Item.Substring(indexOfFirst, 20);
                                paymentDates.Add(Date);

                            }
                        }
                        
                    }
                    if (TempName != "" && tempId != "" && donnorName.Count > 0) 
                    {
                        // Create Member and add To List
                        MemberOfParliament newMember = new MemberOfParliament(TempName, tempId, paymentDates, payments, donnorName);
                        members.Add(newMember);
                        // Reset Temp Variables
                        TempName = "";
                        tempId = "";
                        paymentDates.Clear();
                        payments.Clear();
                        donnorName.Clear();
                        DateRecordAdded = "";

                    }
                    // Check Count of Records
                    if(GetRecordCount != 0 && members.Count == GetRecordCount)
                    {
                        return members;
                    }
                }
                return members;

            }
            
        }
        //s
        public List<MemberOfParliament> GetAllData()
        {
            return ReadMPData();
        }
    }
}
