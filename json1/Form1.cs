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
using System.Drawing.Printing;
using System.Net.NetworkInformation;
using mshtml;


namespace json1
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            
            InitializeComponent();
            //this.Text = "This Is My Title";
         /*   bool status = NetworkInterface.GetIsNetworkAvailable();
            if (status == true)            
                 Text = ("Cetak Struk - Connected -");            
            else Text = ("Internet connections are not available");
         */  
            WebRequest request = WebRequest.Create("http://192.168.15.59/rest");
            try
            {
                request.GetResponse();             
                Text = ("Cetak Struk -- Connected --                                   "+DateTime.Now.ToString("                             dddd, d - MMMM - yyyy"));                
            }
            catch //If exception thrown then couldn't get response from address
            {               
                Text = ("Cetak Struk -- Disconnected --");                              
            }
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
                //string uri = @"http://localhost/rest/api/struq?idpel=" + idpel + "&tgl_bayar=" + tgl;
                string uri = @"http://192.168.15.59/rest/api/struq?idpel=" + idpel + "&tgl_bayar=" + tgl;
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
                    string kodetrx = (string)rss[0]["kode_trx"];
                    //nontag
                    string jenis = (string)rss[0]["jenis_trx"];
                    string noreg = (string)rss[0]["noreg"];
                    string tglreg = (string)rss[0]["tgl_reg"];
                    string info1 = (string)rss[0]["infopln1"];
                    string lain1 = (string)rss[0]["lain1"];
                    string ref2 = (string)rss[0]["ref2"];
                    //token
                    string stoken = (string)rss[0]["lain2"];
                    //lain
                    string struklain = (string)rss[0]["struk"];

                    /* //sum code
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

                    string ID = "IDPEL";
                    string NM = "NAMA";
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
                    string AG = "PLN menyatakan struk ini sebagai bukti pembayaran yang sah";
                    string TL = "TAGIHAN LISTRIK";
                    //NONTAG
                    string NON = "STRUK PEMBAYARAN NON TAGIHAN LISTRIK";
                    string NR = "NO REG     :";
                    string NR2 = "NO REGISTRASI";
                    string TR = "TGL REG    :";
                    string TR2 = "TGL REGISTRASI";
                    string JT = "TRANSAKSI";
                    string BP = "BIAYA PLN";
                    string AD2 = "ADMIN BANK :RP.    ";


                    if (comboBox1.Text == "PLN TAGIHAN - POSTPAID")
                    {
                        webBrowser1.Hide();
                        richTextBox1.Show();

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

                        list.Add(string.Concat(string.Format("{0,10}\t\t{1,10}\t\t{2,10}\t{3,10}", agen.PadRight(35), agen, TB2, tglbyr)));
                        list.Add(string.Concat(string.Format("{0,10}\t\t\t\t\t\t\t{1,10}{2,1}", tglbyr, SP, ENT)));
                        list.Add(string.Concat(string.Format("{0,5}{1,5}\t\t\t{2,5}{3,5}\t\t\t\t{4,5}{5,1}", ID, idp, ID, idp, BT, bln)));
                        list.Add(string.Concat(string.Format("{0,5}{1,5}\t\t\t{2,5}{3,5}\t\t\t{4,5}{5,1}-{6,1}", NM, nama.PadRight(15), NM, nama.PadRight(20), ST, st0, st1)));
                        list.Add(string.Concat(string.Format("{0,5}{1,1}\t\t\t\t\t{2,5}{3,1}", TD, tarif, TD, tarif)));
                        list.Add(string.Concat(string.Format("{0,5}{1,1}\t\t\t\t{2,5}{3,5}", BT, bln, RP, rptag)));
                        list.Add(string.Concat(string.Format("{0,5}{1,5}\t\t{2,5}{3,5}", TB, tglbyr, RF, reff)));
                        list.Add(string.Concat(string.Format("{0,5}{1,1}-{2,1}", ST, st0, st1)));
                        list.Add(string.Concat(string.Format("\t\t\t\t\t\t\t\t{0,5}", AG)));
                        list.Add(string.Concat(string.Format("{0,5}{1,5}\t\t\t{2,5}{3,5}", JT, TL, AD, adm)));
                        list.Add(string.Concat(string.Format("{0,5}{1,5}\t\t\t{2,5}{3,5}", RP, rptag, TT, total)));
                        list.Add(string.Concat(string.Format("{0,5}{1,5}\t\t\t\t\t\t\t{2,5}", AD, adm, TQ)));
                        list.Add(string.Concat(string.Format("{0,5}{1,5}\t\t\t{2,5}", TT, total, info1)));
                        list.Add(string.Concat(string.Format("{0,5}{1,5}\t\t\t{2,5}|{3,5}|{4,5}", RF, reff, lain1, ref2, kodetrx)));
                        list.Add(ENT);

                        /* list.Add(string.Concat(agen));
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
                        webBrowser1.Hide();
                        richTextBox1.Show();


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
                        list.Add(string.Concat(string.Format("\t\t\t\t\t\t\t\t\t\t\t{0,1}{1,1}", TB2, tglbyr)));
                        list.Add(string.Concat(string.Format("{0,1}\t{1,1}", agen.PadRight(35), agen)));
                        list.Add(string.Concat(string.Format("{0,1}\t\t\t\t\t\t\t{1,1}{2,1}", tglbyr, NON,ENT)));
                        list.Add(string.Concat(string.Format("{0,1}{1,1}\t\t\t{2,1}:{3,1}", NR, noreg, JT.PadRight(15), jenis)));
                        list.Add(string.Concat(string.Format("{0,1}{1,1}\t\t\t\t{2,1}:{3,1}", TR, tglreg, NR2.PadRight(15), noreg)));
                        list.Add(string.Concat(string.Format("{0,1}:{1,1}\t\t\t{2,1}:{3,1}", ID.PadRight(11), idp.PadRight(15), TR2.PadRight(15), tglreg)));
                        list.Add(string.Concat(string.Format("{0,1}:{1,1}\t{2,1}:{3,1}", NM.PadRight(11), nama.PadRight(25), NM.PadRight(15), nama)));
                        list.Add(string.Concat(string.Format("{0,1}:\t\t\t\t\t{1,1}:{2,1}", JT.PadRight(11), ID.PadRight(15),idp)));
                        list.Add(string.Concat(string.Format("{0,5}\t\t\t\t{1,1}:{2,1}", jenis, BP.PadRight(15), rptag)));
                        list.Add(ENT);

                       
                       

                        richTextBox1.Text += string.Join(Environment.NewLine, list.ToArray());
                        /*
                        list.Add(string.Concat(string.Format("{0,10}\t\t{1,10}\t\t{2,15}\t{3,10}", agen, agen, TB2, tglbyr)));
                        list.Add(string.Concat(string.Format("{0,10}\t\t\t\t\t\t\t{1,10}", tglbyr, NON)));
                        list.Add(string.Concat(string.Format("\t\t\t\t\t\t{0,10}{1,1}", JT, jenis)));
                        list.Add(string.Concat(string.Format("{0,5}{1,5}\t\t\t{2,5}{3,5}", NR, noreg, NR, noreg)));
                        list.Add(string.Concat(string.Format("{0,5}{1,5}\t\t\t\t{2,5}{3,5}", TR, tglreg, TR, tglreg)));
                        list.Add(string.Concat(string.Format("{0,5}{1,5}\t\t\t{2,5}{3,5}", ID, idp, NM, nama)));
                        list.Add(string.Concat(string.Format("{0,5}{1,5}\t\t\t{2,5}{3,5}", NM, nama.PadRight(15), ID, idp)));
                        list.Add(string.Concat(string.Format("{0,5}{1,5}\t\t\t{2,5}{3,5}", JT, jenis, RP, rptag)));
                        list.Add(string.Concat(string.Format("{0,5}{1,5}\t\t\t{2,5}{3,5}", RP, rptag, RF, reff)));
                        list.Add(string.Concat(string.Format("{0,5}{1,5}\t\t\t\t{2,5}", AD2, adm, AG)));
                        list.Add(string.Concat(string.Format("{0,5}{1,5}\t\t\t{2,5}{3,5}", TT, total, AD2, adm)));
                        list.Add(string.Concat(string.Format("{0,5}{1,5}\t{2,5}{3,5}", RF, reff, TT, total)));
                        list.Add(string.Concat(string.Format("\t\t\t\t\t\t{0,5}", info1)));
                        list.Add(string.Concat(string.Format("\t\t\t\t\t\t\t{0,5}|{1,5}|{2,5}",lain1, ref2, kodetrx)));                        
                        list.Add(ENT);
                        */


                        //richTextBox1.Text += string.Join(Environment.NewLine, list.ToArray());

                        /* list.Add(NON);
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
                         */

                    }
                    else if (comboBox1.Text == "PLN TOKEN - PREPAID")
                    {
                        webBrowser1.Hide();
                        richTextBox1.Show();
                        //parsing html
                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                        doc.LoadHtml(stoken);
                        foreach (HtmlTextNode node in doc.DocumentNode.SelectNodes("//text()"))
                        {
                            richTextBox1.AppendText(Environment.NewLine + node.InnerText.Trim());
                        }


                    }
                    else if (comboBox1.Text == "TELKOM GROUP")
                    {
                        richTextBox1.Hide();
                        webBrowser1.Show();                      
                        webBrowser1.DocumentText = struklain + "<br/>";

                        /************Disable alert/print function()**********************
                        HtmlElement head = webBrowser1.Document.GetElementsByTagName("head")[0];
                        HtmlElement scriptEl = webBrowser1.Document.CreateElement("script");
                        IHTMLScriptElement element = (IHTMLScriptElement)scriptEl.DomElement;
                        string alertBlocker = @"window.alert = function () { }; 
                        window.print=function () { };
                        window.confirm=function () { };";
                        element.text = alertBlocker;
                        head.AppendChild(scriptEl);
                        webBrowser1.ScriptErrorsSuppressed = true;
                        /****************************************************************/

                        /*
                                                     HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                                                     doc.LoadHtml(struklain);

                                                     var TempString = new StringBuilder();
                                                     foreach (HtmlNode style in doc.DocumentNode.Descendants("style").ToArray())
                                                     {
                                                         style.Remove();
                                                     }
                                                     foreach (HtmlNode script in doc.DocumentNode.Descendants("script").ToArray())
                                                     {
                                                         script.Remove();
                                                     }
                                                     foreach (HtmlNode head in doc.DocumentNode.Descendants("head").ToArray())
                                                     {
                                                         head.Remove();
                                                     }
                                                     foreach (HtmlNode comment in doc.DocumentNode.Descendants("comment()").ToArray())
                                                     {
                                                         comment.Remove();
                                                     }
                                                     foreach (HtmlTextNode node in doc.DocumentNode.SelectNodes("//text()"))
                                                     {
                                                      richTextBox1.AppendText(Environment.NewLine+ node.InnerText);                            
                                                     }
                             */



                    }
                    else if (comboBox1.Text == "MULTIFINANCE")
                    {
                        /* richTextBox1.Hide();
                           webBrowser1.Show();
                           webBrowser1.Visible = true;
                           webBrowser1.DocumentText = Environment.NewLine + struklain; */
                        
                        
                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                        doc.LoadHtml(struklain);

                        var TempString = new StringBuilder();
                        foreach (HtmlNode style in doc.DocumentNode.Descendants("style").ToArray())
                        {
                            style.Remove();
                        }
                        foreach (HtmlNode script in doc.DocumentNode.Descendants("script").ToArray())
                        {
                            script.Remove();
                        }
                        foreach (HtmlNode head in doc.DocumentNode.Descendants("head").ToArray())
                        {
                            head.Remove();
                        }
                        foreach (HtmlNode comment in doc.DocumentNode.Descendants("comment()").ToArray())
                        {
                            comment.Remove();
                        }
                        
                        var paragraphs = doc.DocumentNode.Descendants("p").ToList();
                        foreach (var item in paragraphs)
                        {
                            if (item.InnerHtml == "&nbsp;") item.Remove();
                        }
                        var followingText = doc.DocumentNode
                            .SelectNodes(".//following-sibling::text()")
                            .ToList();
                        for (int i = 0; i < followingText.Count - 1; ++i)
                        {
                            followingText[i].Remove();
                        }
                        
                        foreach (HtmlTextNode node in doc.DocumentNode.SelectNodes("//text()"))
                        {
                            /*string regex1 = Regex.Replace(node.InnerText.Trim(), @"\s", "").Trim();
                              string regex2 = Regex.Replace(regex1, @"<[^>]+>|&nbsp;", "").Trim();
                              string regex3 = Regex.Replace(regex2, @"^\s+", "");
                              string regex4 = Regex.Replace(regex3, @"\s+$", "");
                              */
                            string regex2 = Regex.Replace(node.InnerText, @"<[^>]+>|&nbsp;", "");
                            richTextBox1.AppendText(regex2);
                            //webBrowser1.DocumentText = regex2;
                        }

                    }
                    else if (comboBox1.Text == "PDAM")
                    {
                        richTextBox1.Hide();
                        webBrowser1.Show();

                        webBrowser1.Visible = true;
                        webBrowser1.DocumentText = Environment.NewLine + struklain;
                    }
                    else if (comboBox1.Text == "TV TAGIHAN")
                    {
                        richTextBox1.Hide();
                        webBrowser1.Show();

                        webBrowser1.Visible = true;
                        webBrowser1.DocumentText = Environment.NewLine + struklain;
                    }
                    else if (comboBox1.Text == "TAGIHAN SELULER")
                    {
                        richTextBox1.Hide();
                        webBrowser1.Show();

                        webBrowser1.Visible = true;
                        webBrowser1.DocumentText = Environment.NewLine + struklain;
                    }
                    else if (comboBox1.Text == "BPJS KESEHATAN")
                    {
                        richTextBox1.Hide();
                        webBrowser1.Show();

                        webBrowser1.Visible = true;
                        webBrowser1.DocumentText = Environment.NewLine + struklain;
                    }
                    /*  webBrowser1.Navigate("about:blank");
                       while (webBrowser1.Document == null || webBrowser1.Document.Body == null)
                           Application.DoEvents();
                       webBrowser1.Document.OpenNew(true).Write(result);*/
                }


            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            // printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("custom", 925, 1155);
            //printPreviewDialog1.ShowDialog();
            printDialog1.Document = printDocument1;
            if(printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Text, new Font("Courier New", 8, FontStyle.Regular), Brushes.Black, new PointF(5, 3));
        }
    }
}


