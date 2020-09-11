using BankoNuestra.Models;
using BankoNuestra.Process;
using BankoNuestra.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace BankoNuestra
{
    public partial class Main2 : Form
    {
        public string batchfile = "";
        public DateTime deliveryDate;
        DateTime dateTime;
        int totalA = 0;
        int totalB = 0;
        public string filename = "";
        public static string outputFolder = "";
        DBConnections db = new DBConnections();
        List<ChequeModel> checks = new List<ChequeModel>();
        public Main2()
        {
            InitializeComponent();
         
            dateTime = dateTimePicker1.MinDate = DateTime.Now; //Disable selection of backdated dates to prevent errors  
        }

        private void checkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deliveryDate = dateTimePicker1.Value;
            if (deliveryDate == dateTime)
            {
                MessageBox.Show("Please set Delivery Date!");
            }
         
            else
            {
                if (txtBatch.Text == "")
                {
                    MessageBox.Show("Please Enter Batch Number!!!");
                }
                else
                {
                    batchfile = txtBatch.Text;

                   
                    if (Directory.GetFiles(Application.StartupPath + "\\Head\\").Length == 0) // if the path folder is empty
                        MessageBox.Show("No files found in directory path", "***System Error***");
                    else
                    {
                        string[] list = Directory.GetFiles(Application.StartupPath + "\\Head\\");

                        for (int i = 0; i < list.Length; i++)
                        {
                           
                            // _fileName = Path.GetFileName(list[i]);
                            Excel.Application xlApp = new Excel.Application();
                            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(list[i], 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

                            int SheetsCount = xlWorkbook.Sheets.Count;
                            for (int b = 0; b < SheetsCount; b++)
                            {
                                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[b + 1];
                                Excel.Range xlRange = xlWorksheet.UsedRange;

                                int rowCount = xlRange.Rows.Count;
                                int colCount = xlRange.Columns.Count;
                                string SheetName = xlWorksheet.Name.ToUpper();
                                //  int rowCounter = 0;
                                string chequeName = xlWorksheet.Name;
                                string accountname = "";
                                //  MessageBox.Show();
                                string SN = "";
                                //  string EN = "";
                                
                                string qty="";
                                for (int a = 0; a < rowCount; a++)
                                {
                                    string cell = xlRange.Cells[a +8, 1].Text;
                                    if (cell.Trim() == "")
                                    {
                                        a++;
                                    }

                                    else
                                    {
                                        qty = xlRange.Cells[a + 8, 4].Text;
                                        ChequeModel check = new ChequeModel();
                                        check.AccountNoHypen = xlRange.Cells[a + 8, 1].Text;
                                        check.AccountNoHypen = check.AccountNoHypen.Trim();
                                        check.AccountNo = check.AccountNoHypen.Replace("-", "");
                                        accountname = xlRange.Cells[a + 8, 2].Text;
                                        accountname = accountname.Trim();
                                        //   if(accountname.Length > 50)
                                        // For OR
                                        if (accountname.Length > 36)
                                        {
                                            int LoopCount5 = accountname.Length;
                                            while (LoopCount5 > 0)
                                            {
                                                if (check.Name1 == null && check.Name2 == null && LoopCount5 < accountname.Length - 6)
                                                {
                                                     
                                                    if (accountname.Substring(LoopCount5, 4) == " Or " || accountname.Substring(LoopCount5, 4) == " or " || accountname.Substring(LoopCount5, 5) == " AND ")
                                                    {
                                                        if (accountname.Substring(LoopCount5, 5) != " AND ")
                                                        {
                                                            check.Name1 = accountname.Substring(0, LoopCount5 + 3).ToUpper();
                                                            check.Name2 = accountname.Substring(LoopCount5 + 4, accountname.Length - LoopCount5 - 4).ToUpper();
                                                        }
                                                        else
                                                        {
                                                            check.Name1 = accountname.Substring(0, LoopCount5 + 4).ToUpper();
                                                            check.Name2 = accountname.Substring(LoopCount5 + 5, accountname.Length - LoopCount5 - 5).ToUpper();
                                                        }
                                                    }
                                                    //else
                                                    //{
                                                    //    check.Name1 = accountname;
                                                    //    check.Name2 = "";
                                                    //}
                                                }
                                                LoopCount5 = LoopCount5 - 1;
                                            }
                                        }
                                        else
                                        {
                                            check.Name1 = accountname.ToUpper();
                                            check.Name2 = "";
                                        }
                                        // End For OR


                                        //  check.Name1 = accountname;
                                        SN = xlRange.Cells[a+8, 3].Text;
                                        SN = SN.Trim();
                                        check.BRSTN = "061370012";
                                        check.Address1 = "SAN SIMON PAMPANGA BRANCH";
                                        check.Address2 = "678 MACARTHUR HIGHWAY, STA. MONICA,";
                                        check.Address3 = "SAN SIMON, PAMPANGA";
                                        check.StartingSerial = SN.Substring(0,5);
                                        check.EndingSerial = SN.Substring(6, 5);
                                        check.Quantity = int.Parse(xlRange.Cells[a + 8, 4].Text) ;
                                        check.ChequeName = xlWorksheet.Name;
                                        if(chequeName.Contains("PERSONAL"))
                                        {
                                            check.ChequeName = "Personal Check";
                                            check.ChequeType = "A";
                                            check.PcsPerBook = 50;
                                            filename = "BNU" +batchfile.Substring(0, 4) + "P";
                                            totalA++;
                                            outputFolder = "Regular_Checks";
                                        }
                                        else
                                        {
                                            check.ChequeName = "Commercial Check";
                                            check.ChequeType = "B";
                                            check.PcsPerBook = 100;
                                            filename = "BNU" + batchfile.Substring(0, 4) + "C";
                                            outputFolder = "Regular_Checks";
                                            totalB++;
                                        }

                                        checks.Add(check);
                                       // MessageBox.Show(qty);
                                    }
                                    
                                }

                                //string rbBrstn;

                            }
                        }
                        //BindingSource checkBind = new BindingSource();
                        //checkBind.DataSource = checks;
                        //dataGridView1.DataSource = checkBind;
                        lblQtyC.Text = totalB.ToString();
                        lblQtyP.Text = totalA.ToString();
                        lblTotal.Text = (totalA + totalB).ToString();
                        MessageBox.Show("No Errors found!!!");
                        generateToolStripMenuItem.Enabled = true;
                    }
                }
            }
        }

        private void Main2_Load(object sender, EventArgs e)
        {
            db.DBConnect();
            Color color = System.Drawing.ColorTranslator.FromHtml("#FFA542");
            //   Color result = Color.FromArgb(color.R, color.G, color.B);
            this.BackColor = color;
            lblBVerion.Text = "2.0"; // Adding form for reading order file and generate data 
            deliveryDate = dateTime;
        }

        private void encodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMain main = new frmMain();
           // this.Hide();
            main.Show();
        }

        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZipCopyProcess zip = new ZipCopyProcess();
            zip.DeleteFiles(".txt");
            //  Main2 main2 = new Main2();
     
            OutPut2Process process = new OutPut2Process();
          
            process.ProcessCheck2(checks, this);
            process.PackingText2(checks, this);
            process.PrinterFile2(checks, this);
            process.SaveToPackingDBF2(checks,batchfile, this);
            for (int i = 0; i < checks.Count; i++)
            {
                db.SavedDatatoDatabase(checks[i], batchfile, deliveryDate);
            }
            db.DumpMySQL2();
            zip.ZipFileS2(frmLogIn._userName,this);
            zip.DeleteHead(".xlsx");
            MessageBox.Show("Data has been process!!!");

            Environment.Exit(0);
        }
    }
}
