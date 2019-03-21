using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using HtmlAgilityPack;

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
            //string idpel = tbidpel.Text;
            string tgl = dtp.Value.Date.ToString("yyyy-MM-dd");
            string[] allLines = tbidpel.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            //int count = 0; //hitung jumlah data
            foreach (string idpel in allLines)
            {


                string uri = @"http://localhost/rest/api/struq?idpel=" + idpel + "&tgl_bayar=" + tgl;


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
                    string tarif = (string)rss[0]["tarif"];
                    string daya = (string)rss[0]["daya"];
                    string bln = (string)rss[0]["bln"];
                    string st0 = (string)rss[0]["stand0"];
                    string st1 = (string)rss[0]["stand1"];
                    string rptag = (string)rss[0]["rp_tag"];
                    string adm = (string)rss[0]["admin_bank"];
                    string reff = (string)rss[0]["ref1"];
                    //nontag
                    string jenis = (string)rss[0]["jenis_trx"];
                    string noreg = (string)rss[0]["noreg"];
                    string tglreg = (string)rss[0]["tgl_reg"];
                    //token
                    string stoken = (string)rss[0]["lain2"];
                    //lain
                    string struklain = (string)rss[0]["struk"];

                    /*   //sum code
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
                       //end sum
       */
                    string ENT = Environment.NewLine;

                    string ID = "IDPEL      :";
                    string NM = "NAMA       :";
                    string TD = "TARIF/DAYA :";
                    string SL = "/";
                    string BT = "BL/TH      :";
                    string TB = "TGL BAYAR  :";
                    string TB2 = "Tanggal Bayar ";
                    string ST = "STAND METER:";
                    string RP = "RP TAG PLN :RP.    ";
                    string AD = "ADMIN BANK :RP.     ";
                    string TT = "TOTAL BAYAR:RP.    ";
                    string RF = "REFF       :";
                    string IF0 = "Informasi Hub Call Center 123 Atau PLN Terdekat";
                    string TQ = "TERIMAKASIH";
                    string SP = "STRUK PEMBAYARAN TAGIHAN LISTRIK";
                    string AG = "PLN menyatakan struk ini sebagai pembayaran yang sah";
                    //NONTAG
                    string NON = "STRUK PEMBAYARAN NON TAGIHAN LISTRIK";
                    string NR = "NO REG     :   ";
                    string TR = "TGL REG    :   ";
                    string JT = "TRANSAKSI  :   ";

                    if (comboBox1.Text == "PLN TAGIHAN - POSTPAID")
                    {

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
                        //end sum


                        List<String> list = new List<String>();
                        //list.Add(SP);
                        //list.Add(string.Concat(string.Format("{0,85}", SP)));
                        list.Add(string.Concat(string.Format("{0,10}\t\t{1,10}\t\t{2,10}\t{3,10}", agen, agen, TB2, tglbyr)));
                        list.Add(string.Concat(string.Format("{0,10}\t\t\t\t\t\t\t{1,10}", tglbyr, SP)));
                        list.Add(string.Concat(string.Format("{0,5}{1,5}\t\t\t{2,5}{3,5}\t\t{4,5}{5,1}", ID, idp, ID, idp, BT, bln)));
                        list.Add(string.Concat(string.Format("{0,5}{1,1}\t\t\t\t{2,5}{3,1}\t\t\t{4,1}{5,1}{6,1}{7,1}", NM, nama, NM, nama, ST, st0, SL, st1)));
                        list.Add(string.Concat(string.Format("{0,5}{1,1}\t\t\t\t\t{2,5}{3,1}", TD, tarif, TD, tarif)));
                        list.Add(string.Concat(string.Format("{0,5}{1,1}\t\t\t\t\t{2,5}{3,5}", BT, bln, RP, rptag)));
                        list.Add(string.Concat(string.Format("{0,5}{1,5}\t\t\t{2,5}{3,5}", TB, tglbyr, RF, reff)));
                        list.Add(string.Concat(string.Format("{0,5}{1,1}{2,1}{3,1}\t\t\t\t\t{4,5}", ST, st0, SL, st1, AG)));
                        list.Add(string.Concat(string.Format("{0,5}{1,5}\t\t\t{2,5}{3,5}", RP, rptag, AD, adm)));
                        list.Add(string.Concat(string.Format("{0,5}{1,5}\t\t\t{2,5}{3,5}", AD, adm, TT, total)));
                        list.Add(string.Concat(string.Format("{0,5}{1,5}\t\t\t\t\t\t{2,5}", TT, total, TQ)));
                        list.Add(string.Concat(string.Format("{0,5}{1,5}\t\t\t{2,5}", RF, reff, IF0)));
                        list.Add(ENT);
                        /*   list.Add(string.Concat(agen));
                           list.Add(string.Concat(tglbyr,ENT));
                           list.Add(string.Concat(ID, idp));
                           list.Add(string.Concat(NM, nama));
                           list.Add(string.Concat(TD, tarif));
                           list.Add(string.Concat(BT, bln));
                           list.Add(string.Concat(TB, tglbyr));
                           list.Add(string.Concat(ST, st0, SL, st1));
                           list.Add(string.Concat(RP, rptag));
                           list.Add(string.Concat(AD, adm));
                           list.Add(string.Concat(TT, total));
                           list.Add(string.Concat(RF, reff));
                           list.Add(string.Concat(IF0));  */

                        //richTextBox1.Lines = list.ToArray();
                        
                        richTextBox1.Text += string.Join(Environment.NewLine, list.ToArray());


    }
                    else if (comboBox1.Text == "PLN NON TAGIHAN")
                    {


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
                        //end sum


                        List<String> list = new List<String>();
                        list.Add(NON);
                        list.Add(agen);
                        list.Add(string.Concat(tglbyr, ENT));
                        list.Add(string.Concat(NR, noreg));
                        list.Add(string.Concat(TR, tglreg));
                        list.Add(string.Concat(ID, idp));
                        list.Add(string.Concat(NM, nama));
                        list.Add(string.Concat(JT, jenis));
                        list.Add(string.Concat(RP, rptag));
                        list.Add(string.Concat(AD, adm));
                        list.Add(string.Concat(TT, total));
                        list.Add(string.Concat(RF, reff));
                        list.Add(string.Concat(IF0));

                        richTextBox1.Lines = list.ToArray();

                    }
                    else if (comboBox1.Text == "PLN TOKEN - PREPAID")
                    {
                        //richTextBox1.Visible = false;
                        //parsing html
                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                        doc.LoadHtml(stoken);
                        foreach (HtmlTextNode node in doc.DocumentNode.SelectNodes("//text()"))
                        {

                            richTextBox1.AppendText(Environment.NewLine + node.InnerText.Trim());                           

                        }
                 

                    }
                    else if (comboBox1.Text == "BPJS KESEHATAN")
                    {
                        richTextBox1.Visible = false;
                        //webBrowser1.DocumentText = Environment.NewLine + struklain;

                        
                            webBrowser1.ScriptErrorsSuppressed = true;
                            webBrowser1.Navigate("about:blank");
                            while (webBrowser1.Document == null || webBrowser1.Document.Body == null)
                                Application.DoEvents();
                            webBrowser1.Document.OpenNew(true).Write(Environment.NewLine +struklain);                        

                    }







                    /*  webBrowser1.Navigate("about:blank");
                       while (webBrowser1.Document == null || webBrowser1.Document.Body == null)
                           Application.DoEvents();
                       webBrowser1.Document.OpenNew(true).Write(result);*/
                }


            }


        }
    }
}

