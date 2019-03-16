using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using System.Windows.Data;
using Newtonsoft.Json;

namespace json1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result = string.Empty;
            string idpel = tbidpel.Text;
            string tgl = dtp.Value.Date.ToString("yyyy-MM-dd");

            string tipe = tbtipe.Text;
            string uri = @"http://localhost/rest/api/struq?idpel="+idpel+"&tgl_bayar="+tgl;


            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.AutomaticDecompression = DecompressionMethods.GZip;

            //req.ContentType = "application /x-www-form-urlencoded";
            req.ContentType = "text/plain";

            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            using (Stream stream = resp.GetResponseStream())




            using (StreamReader read = new StreamReader(stream, Encoding.UTF8))
            {
                result = read.ReadToEnd();                
                            
                dynamic rss = JArray.Parse(result);

                string agen = (string)rss[0]["agen"];
                string tglbyr = (string)rss[0]["tgl_bayar"];
                string idp = (string)rss[0]["idpel"];
                string nama = (string)rss[0]["nama"];
                string tarif= (string)rss[0]["tarif"];
                string daya = (string)rss[0]["daya"];
                string bln = (string)rss[0]["bln"];
                string st0 = (string)rss[0]["stand0"];
                string st1 = (string)rss[0]["stand1"];
                string rptag = (string)rss[0]["rp_tag"];
                string adm = (string)rss[0]["admin_bank"];
                string reff = (string)rss[0]["ref1"];


                //sum code
                //Convert to int
                int x = Int32.Parse(rptag);
                int xx = Int32.Parse(adm);
                //sum
                List<int> summ = new List<int>();
                summ.Add(x);
                summ.Add(xx);
                int sum2 = summ.Sum();
                //convert to string
                string total = sum2.ToString();
                

                string ENT = Environment.NewLine;
                string ID = "IDPEL      :   ";
                string NM = "NAMA       :   ";
                string TD = "TARIF/DAYA :   ";
                string SL = "/";
                string BT = "BL/TH      :   ";
                string TB = "TGL BAYAR  :   ";
                string ST = "STAND METER:   ";
                string RP = "RP TAG PLN :RP.    ";
                string AD = "ADMIN BANK :RP.      ";
                string TT = "TOTAL BAYAR:RP.    ";
                string RF = "REFF       :   ";
                string IF0 = "Informasi Hub Call Center 123 Atau PLN Terdekat";
                string SP = "STRUK PEMBAYARAN TAGIHAN LISTRIK";

                List<String> list = new List<String>();
                list.Add(SP);
                list.Add(agen);
                list.Add(string.Concat(tglbyr,ENT));
                list.Add(string.Concat(ID, idp));
                list.Add(string.Concat(NM, nama));
                list.Add(string.Concat(TD, tarif, SL, daya));
                list.Add(string.Concat(BT, bln));
                list.Add(string.Concat(TB, tglbyr));
                list.Add(string.Concat(ST,st0, SL, st1));
                list.Add(string.Concat(RP, rptag));
                list.Add(string.Concat(AD, adm));
                list.Add(string.Concat(TT, total));
                list.Add(string.Concat(RF, reff));
                list.Add(string.Concat(IF0));

                richTextBox1.Lines = list.ToArray();
              
             
             

             /*  webBrowser1.Navigate("about:blank");
                while (webBrowser1.Document == null || webBrowser1.Document.Body == null)
                    Application.DoEvents();
                webBrowser1.Document.OpenNew(true).Write(result);*/
            }

           
        }
          
            
        }
    }

