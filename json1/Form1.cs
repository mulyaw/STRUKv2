﻿using System;
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
using System.Threading;
using System.Management;
using System.Runtime.InteropServices;
namespace json1
{
    public partial class Form1 : Form
    {

        public Form1()
        {

            InitializeComponent();
            PrinterList();
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
                Text = ("Cetak Struk -- Connected --                                   " + DateTime.Now.ToString("                             dddd, d MMMM yyyy"));
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
                try
                {
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
                        string TD = "TARIF/DAYA";
                        string BT = "BL/TH";
                        string TB = "TGL BAYAR";
                        string TB2 = "Tanggal Bayar ";
                        string ST = "STAND METER";
                        string RP = "RP TAG PLN : RP.";
                        string RP2 = "RP TAG PLN  : RP.";
                        string AD = "ADMIN BANK : RP.";
                        string TT = "TOTAL BAYAR: RP.";
                        string T1 = "TOTAL BAYAR : RP.";
                        string RF = "REFF";
                        string IF0 = "Informasi Hubungi Call Center 123 Atau Hub PLN Terdekat :. Powered by Bukopinet.";
                        string TQ = "TERIMAKASIH";
                        string SP = "STRUK PEMBAYARAN TAGIHAN LISTRIK";
                        string AG = "PLN menyatakan struk ini sebagai bukti pembayaran yang sah";
                        string TL = "TAGIHAN LISTRIK";
                        //NONTAG
                        string NON = "STRUK PEMBAYARAN NON TAGIHAN LISTRIK";
                        string NR = "NO REG";
                        string NR2 = "NO REGISTRASI";
                        string TR = "TGL REG";
                        string TR2 = "TGL REGISTRASI";
                        string JT = "TRANSAKSI";
                        string BP = "BIAYA PLN";
                        string AD2 = "ADMIN BANK  : RP.";


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
                            list.Add(string.Concat(string.Format("\t\t\t\t\t\t\t\t\t\t\t\t{0,1}{1,1}", TB2, tglbyr)));
                            list.Add(string.Concat(string.Format("{0,1}\t{1,1}", agen.PadRight(35), agen)));
                            list.Add(string.Concat(string.Format("{0,1}\t\t\t\t\t\t\t{1,1}{2,1}", tglbyr, SP, ENT)));
                            list.Add(string.Concat(string.Format("{0,1}: {1,1}\t\t{2,1}: {3,1}\t\t{4,1}: {5,1}", ID.PadRight(12), idp.PadRight(15), ID.PadRight(11), idp.PadRight(15), BT.PadRight(12), bln)));
                            list.Add(string.Concat(string.Format("{0,1}: {1,1}\t{2,1}: {3,1}\t{4,1}: {5,1}-{6,1}", NM.PadRight(12), nama.PadRight(23), NM.PadRight(11), nama.PadRight(23), ST.PadRight(12), st0, st1)));
                            list.Add(string.Concat(string.Format("{0,1}: {1,1}/{2,1}\t{3,1}: {4,1}/{5,1}", TD.PadRight(12), tarif, daya.PadRight(18), TD.PadRight(11), tarif, daya)));
                            list.Add(string.Concat(string.Format("{0,1}: {1,1}\t{2,1} {3,1}", BT.PadRight(12), bln.PadRight(23), RP.PadRight(11), rptag)));
                            list.Add(string.Concat(string.Format("{0,1}: {1,1}\t{2,1}: {3,1}", TB.PadRight(12), tglbyr.PadRight(21), RF.PadRight(11), reff)));
                            list.Add(string.Concat(string.Format("{0,1}: {1,1}-{2,1}", ST.PadRight(12), st0, st1)));
                            list.Add(string.Concat(string.Format("{0,1} {1,1}\t\t\t{2,1}", RP2.PadRight(20), rptag.PadLeft(10), AG)));
                            list.Add(string.Concat(string.Format("{0,1} {1,1}\t\t{2,1}{3,1}", AD2.PadRight(12), adm.PadLeft(13), AD.PadRight(11), adm.PadLeft(10))));
                            list.Add(string.Concat(string.Format("{0,25}\t\t\t{1,1}{2,1}", TL, TT.PadRight(11), total.PadLeft(10))));
                            list.Add(string.Concat(string.Format("{0,1}{1,1}\t\t\t\t\t{2,1}", T1.PadRight(15), total.PadLeft(14), TQ)));
                            list.Add(string.Concat(string.Format("{0,1}:\t\t\t\t\t{1,1}", RF, IF0.PadLeft(83))));
                            list.Add(string.Concat(string.Format("{0,1}\t\t\t{1,5}|{2,5}|{3,5}", reff, lain1, ref2, kodetrx)));
                            list.Add(ENT);
                            richTextBox1.Text += string.Join(Environment.NewLine, list.ToArray());
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
                               list.Add(string.Concat(IF0)); 
                               richTextBox1.Lines = list.ToArray();                        
                               richTextBox1.Text += string.Join(Environment.NewLine, list.ToArray());
                             */

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
                            list.Add(string.Concat(string.Format("\t\t\t\t\t\t\t\t\t\t\t\t{0,1}{1,1}", TB2, tglbyr)));
                            list.Add(string.Concat(string.Format("{0,1}\t{1,1}", agen.PadRight(35), agen)));
                            list.Add(string.Concat(string.Format("{0,1}\t\t\t\t\t\t\t{1,1}{2,1}", tglbyr, NON, ENT)));
                            list.Add(string.Concat(string.Format("{0,1}: {1,1}\t\t{2,1}: {3,1}", NR.PadRight(11), noreg.PadRight(15), JT.PadRight(15), jenis)));
                            list.Add(string.Concat(string.Format("{0,1}: {1,1}\t{2,1}: {3,1}", TR.PadRight(11), tglreg.PadRight(25), NR2.PadRight(15), noreg)));
                            list.Add(string.Concat(string.Format("{0,1}: {1,1}\t{2,1}: {3,1}", ID.PadRight(11), idp.PadRight(25), TR2.PadRight(15), tglreg)));
                            list.Add(string.Concat(string.Format("{0,1}: {1,1}\t{2,1}: {3,1}", NM.PadRight(11), nama.PadRight(25), NM.PadRight(15), nama)));
                            list.Add(string.Concat(string.Format("{0,1}:\t\t\t\t\t{1,1}: {2,1}", JT.PadRight(11), ID.PadRight(15), idp)));
                            list.Add(string.Concat(string.Format("{0,25}\t\t\t{1,1}: {2,1}", jenis, BP.PadRight(15), rptag)));
                            list.Add(string.Concat(string.Format("\t\t\t\t\t\t{0,1}: {1,1}", RF.PadRight(15), reff)));
                            list.Add(string.Concat(string.Format("{0,1}{1,1}\t\t\t\t{2,1}", RP, rptag.PadLeft(15).PadRight(11), AG)));
                            list.Add(string.Concat(string.Format("{0,1}{1,1}\t\t{2,1}{3,1}", AD, adm.PadLeft(15), AD, adm.PadLeft(10))));
                            list.Add(string.Concat(string.Format("{0,1}{1,1}\t\t{2,1}{3,1}", TT, total.PadLeft(15), TT, total.PadLeft(10))));
                            list.Add(string.Concat(string.Format("{0,1}:\t\t\t\t\t{1,83}", RF.PadRight(5), info1)));
                            list.Add(string.Concat(string.Format("{0,1}\t\t\t{1,5}|{2,5}|{3,5}", reff, lain1, ref2, kodetrx)));
                            list.Add(ENT);
                            richTextBox1.Text += string.Join(Environment.NewLine, list.ToArray());
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
                               richTextBox1.Text += string.Join(Environment.NewLine, list.ToArray());
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
                            /* richTextBox1.Hide();
                             webBrowser1.Show();                      
                             webBrowser1.DocumentText = struklain + "<br/>";*/

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
                            foreach (var text in followingText)
                            {
                                text.Remove();
                            }

                            foreach (HtmlTextNode node in doc.DocumentNode.SelectNodes("//text()"))
                            {
                                string regex2 = Regex.Replace(node.InnerHtml, @"<[^>]+>|&nbsp;", "");
                                //string regex3 = Regex.Replace(regex2, @"\s+", "\n");                               
                                richTextBox1.AppendText(regex2);


                            }
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
                            ****************************************************************/




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
                            webBrowser1.DocumentText = Environment.NewLine + struklain;
                        }
                        else if (comboBox1.Text == "TV TAGIHAN")
                        {
                            richTextBox1.Hide();
                            webBrowser1.Show();
                            webBrowser1.DocumentText = Environment.NewLine + struklain;
                        }
                        else if (comboBox1.Text == "TAGIHAN SELULER")
                        {
                            richTextBox1.Hide();
                            webBrowser1.Show();
                            webBrowser1.DocumentText = Environment.NewLine + struklain;
                        }
                        else if (comboBox1.Text == "BPJS KESEHATAN")
                        {
                            /*  
                              richTextBox1.Hide();                       
                              webBrowser1.Visible = true;
                              webBrowser1.DocumentText = Environment.NewLine + struklain;
                              */

                            richTextBox1.Hide();
                            webBrowser1.Visible = true;
                            byte[] bytes = Encoding.UTF8.GetBytes(struklain);
                            MemoryStream ms = new MemoryStream();
                            ms.Write(bytes, 0, bytes.Length);
                            ms.Position = 0;
                            webBrowser1.DocumentStream = ms;                                              

                           /*  webBrowser1.Navigate("about:blank");
                              while (webBrowser1.Document == null || webBrowser1.Document.Body == null)
                                  Application.DoEvents();
                              webBrowser1.Document.OpenNew(true).Write(result);*/
                        }
                    }
                }
                catch (Exception err)
                {

                    MessageBox.Show("DATA TIDAK DITEMUKAN");
                    File.WriteAllText("log.txt", tbidpel.Text+err);

                }
            }
        }
        






        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                printDocument1.DefaultPageSettings.PaperSize = new PaperSize("custom", 925, 1155);
                printDocument1.Print();
            }            
            if (cb2.Text == "1")
            {
                printDocument1.DefaultPageSettings.PaperSize = new PaperSize("custom", 925, 240);
                printDocument1.Print();
            }
            else if (cb2.Text == "2")
            {
                printDocument1.DefaultPageSettings.PaperSize = new PaperSize("custom", 925, 460);
                printDocument1.Print();
            }
            /*
            printDialog1.Document = printDocument1;
            if(printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }*/
        }
        public static class printer
        {
            [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool SetDefaultPrinter(string printer);

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {

            if (checkBox1.Checked)
            {
                e.Graphics.DrawString(richTextBox1.Text, new Font("Courier New", 9, FontStyle.Regular), Brushes.Black, new PointF(5, 3));
            } else if (checkBox1.Checked == false)
            {
                e.Graphics.DrawString(richTextBox1.Text, new Font("Courier New", 8, FontStyle.Regular), Brushes.Black, new PointF(5, 3));
            }
            
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pname = comboBox3.SelectedItem.ToString();
            printer.SetDefaultPrinter(pname);
        }
        private void PrinterList()
        {
            foreach (string sPrinters in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                comboBox3.Items.Add(sPrinters);
            }
        }

        private void tbidpel_TextChanged(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.IsBalloon = true;
            tt.Show(string.Empty, tbidpel, 0);
            int x = 1500;
            tt.Show("Untuk cetak beberapa IDPEL, Silahkan pisah dengan ENTER", tbidpel, x);

            if (Regex.IsMatch(tbidpel.Text, "[^0-9\r\n]+", RegexOptions.IgnoreCase | RegexOptions.Multiline))
            {
                MessageBox.Show("Only Number Allowed.");
                tbidpel.Text = tbidpel.Text.Remove(tbidpel.Text.Length - 1);
                
            }
        }
    }
}


