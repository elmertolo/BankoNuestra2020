using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BankoNuestra.Models;
using BankoNuestra.Services;
using BankoNuestra.Process;

namespace BankoNuestra
{
    public partial class frmMain : Form
    {
        DBConnections db = new DBConnections();
        ChequeModel cheque = new ChequeModel();
          BranchesModel br = new BranchesModel();
        public string batchFile = "";
        public DateTime deliveryDate;
        public string fileName;
        DateTime dateTime;
        Int64 chargeSlipSerial = 0;
        string stringchargeSlipSerial = "";
        int LastNoP = 0;
        int LastNoC = 0;
        int LastNoCS = 0;
        //private int endserial = 0;
        public frmMain()
        {
            InitializeComponent();
            dateTime = dateTimePicker1.MinDate = DateTime.Now.Date; //Disable selection of backdated dates to prevent errors     
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Color color = System.Drawing.ColorTranslator.FromHtml("#FFA542");
            //   Color result = Color.FromArgb(color.R, color.G, color.B);
            this.BackColor = color;
            lblVersion.Text = "v1.0";
            //groupBox1.BackColor = Color.DarkGray;
            txtAccountNumber.MaxLength = 12;
            txtAccountName1.MaxLength = 50;
            txtAccountName2.MaxLength = 50;
            DisplayBranch();
            lblUsername.Text = frmLogIn._userName;
            dateTimePicker1.Text = DateTime.Now.ToString();
            txtAccountNumber.BackColor = System.Drawing.Color.LightGray;
            txtAccountName1.BackColor = System.Drawing.Color.LightGray;
            txtAccountName2.BackColor = System.Drawing.Color.LightGray;
           
            txtOrQty.BackColor = System.Drawing.Color.LightGray;
            deliveryDate = dateTime;
            MessageBox.Show(db.databaseName);
            
          //  MessageBox.Show(cheque.BRSTN);
            CheckLoadData();
        }
        private void DisplayBranch()
        {
            db.DBConnect();
            DataTable dt = new DataTable();
            db.GetBranch(dt);
            foreach (DataRow row in dt.Rows)
            {
                cmbBranch.Items.Add(row[0]);
            }
            cmbBranch.SelectedIndex = 0;
        }
        private void CheckLoadData()
        {
            DataTable dt = new DataTable();
            
            db.LoadDataToGridView(dt);
            dvgDatalist.DataSource = dt;

        }

        private void rdbPersonal_CheckedChanged(object sender, EventArgs e)
        {
            lblPcsperbook.Text = "50 Pcs. / Bkt";
            txtAccountNumber.Text = "";
            txtAccountName1.Enabled = true;
            txtAccountName2.Enabled = true;
            txtAccountNumber.Enabled = true;
            txtOrQty.Enabled = true;
            txtAccountNumber.BackColor = System.Drawing.Color.Empty;
            txtAccountName1.BackColor = System.Drawing.Color.Empty;
            txtAccountName2.BackColor = System.Drawing.Color.Empty;
            txtOrQty.BackColor = System.Drawing.Color.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            //int totalcheque = 0;
          
             cheque.Address1 = cmbBranch.Text;
            cheque.Name1 = txtAccountName1.Text;
            cheque.Name2 = txtAccountName2.Text;
            cheque.AccountNo = txtAccountNumber.Text;
            cheque.Quantity = int.Parse(txtOrQty.Text);

           
            if (rdbPersonal.Checked == true || rdbCommercial.Checked == true || rdbChargeSlip.Checked == true)
            {
                if (rdbPersonal.Checked == true)
                {
                   // lblPcsperbook.Text = "50 Pcs. / Bkt";
                    
                    cheque.ChequeType = "A";
                    cheque.PcsPerBook = 50; 
                    cheque.ChequeName = rdbPersonal.Text;
                    //totalcheque = cheque.Quantity * 50;
                   // cheque.EndingSerial = (LastNo  + totalcheque).ToString();
                }
                if (rdbCommercial.Checked == true)
                {
                    //lblPcsperbook.Text = "100 Pcs. / Bkt";
                   
                    cheque.ChequeType = "B";
                    cheque.PcsPerBook = 100; 
                    cheque.ChequeName = rdbCommercial.Text;
                   // totalcheque = cheque.Quantity * 100;
                    
                 //   cheque.StartingSerial = (LastNo + 1).ToString();
                    
                }
                if (rdbChargeSlip.Checked == true)
                {
                    //  lblPcsperbook.Text = "50 Pcs. / Bkt";
                    OutPutProcess.InputBox("", "Input Serial Number :", ref stringchargeSlipSerial);
                    cheque.ChequeType = "CS";
                    cheque.PcsPerBook = 50; 
                    cheque.ChequeName = rdbChargeSlip.Text;
                   // totalcheque = cheque.Quantity * 50;
                    
               //     cheque.StartingSerial = (LastNo + 1).ToString();
                   
                }

                
                
                
          //  cheque.StartingSerial = (LastNo + 1).ToString();
            br.DateUpdated =DateTime.Now;
            //cheque.PcsPerBook = int.Parse(lblPcsperbook.Text);
           
          //  MessageBox.Show(cheque.StartingSerial + " - + " + cheque.EndingSerial);
            for (int i = 0; i < cheque.Quantity; i++)
            {
             
                
                    if (cheque.ChequeType == "A")
                    {

                        //LastNoP += 50;
                       
                        cheque.StartingSerial = (LastNoP + 1).ToString();
                        LastNoP += 50;
                         cheque.EndingSerial = LastNoP.ToString();
                         db.SaveToTempTable(cheque);
                         br.LastNo_P = Int64.Parse(cheque.EndingSerial);
                    }
                    else if (cheque.ChequeType == "B")
                    {
                        cheque.EndingSerial = (LastNoC + 100).ToString();
                        cheque.StartingSerial = (LastNoC + 1).ToString();
                        db.SaveToTempTable(cheque);
                        //br.LastNo_C = Int64.Parse(cheque.EndingSerial);
                    }
                    else if (cheque.ChequeType == "CS")
                    {
                        //  cheque.EndingSerial = 
                        chargeSlipSerial = Int64.Parse(stringchargeSlipSerial);
                        cheque.StartingSerial = chargeSlipSerial.ToString();
                        chargeSlipSerial += 50;
                        cheque.EndingSerial = (chargeSlipSerial -1).ToString();
                     //   cheque.StartingSerial = (LastNoCS + 1).ToString();
                         db.SaveToTempTable(cheque);
                        //br.LastNo_CS = Int64.Parse(cheque.EndingSerial);
                        stringchargeSlipSerial = chargeSlipSerial.ToString();
                       // MessageBox.Show(cheque.EndingSerial + " - " + cheque.StartingSerial);
                    }

                }
               // db.SaveToTempTable(cheque);
            
          
            MessageBox.Show("Data has been saved!");
            ClearForm();
            CheckLoadData();
         
            }
            else
                MessageBox.Show("Please Choose Cheque Type!!!");
        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  DataTable dt = new DataTable();
            BranchesModel brmodel = new BranchesModel();
            if (cmbBranch.Text == "SAN SIMON PAMPANGA BRANCH")
            {

                db.GetBrstn(brmodel, cmbBranch.Text);
               
                txtBrstn.Text = brmodel.BRSTN.ToString();
                cheque.BRSTN = txtBrstn.Text;
                db.GetLastNO(br, cheque.BRSTN);
                LastNoP = int.Parse(br.LastNo_P.ToString());
                LastNoC = int.Parse(br.LastNo_C.ToString());
              //  LastNoCS = int.Parse(br.LastNo_CS.ToString());
            }
        }

        private void rdbCommercial_CheckedChanged(object sender, EventArgs e)
        {
            lblPcsperbook.Text = "100 Pcs. / Bkt";
            txtAccountNumber.Text = "";
            txtAccountName1.Enabled = true;
            txtAccountName2.Enabled = true;
            txtAccountNumber.Enabled = true;
            txtOrQty.Enabled = true;
            txtAccountNumber.BackColor = System.Drawing.Color.Empty;
            txtAccountName1.BackColor = System.Drawing.Color.Empty;
            txtAccountName2.BackColor = System.Drawing.Color.Empty;
            txtOrQty.BackColor = System.Drawing.Color.Empty;
        }

        private void rdbChargeSlip_CheckedChanged(object sender, EventArgs e)
        {
            lblPcsperbook.Text = "50 Pcs. / Bkt";
            txtAccountNumber.Text = "000000000000";
            txtAccountName1.Enabled = false;
            txtAccountName2.Enabled = false;
            txtAccountNumber.Enabled = false;
            txtOrQty.Enabled = true;
            txtAccountNumber.BackColor = System.Drawing.Color.LightGray;
            txtAccountName1.BackColor = System.Drawing.Color.LightGray;
            txtAccountName2.BackColor = System.Drawing.Color.LightGray;
            
          //  MessageBox.Show(stringchargeSlipSerial);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void txtAccountNumber_TextChanged(object sender, EventArgs e)
        {
             List<ChequeModel> Acheck = new List<ChequeModel>();
            DBConnections dbconnection = new DBConnections();
           // var listofchecksAccount = Acheck.Select(a => a.AccountNo).ToList();
            if (txtAccountNumber.Text.Length == 12)
            {
                dbconnection.GetNameifExisting(Acheck);

                for (int i = 0; i < Acheck.Count; i++)
                {
                    if (txtAccountNumber.Text == Acheck[i].AccountNo)
                    {
                        //CheckBRSTNandChkType();
                        txtAccountName1.Text = Acheck[i].Name1.ToString();
                        txtAccountName2.Text = Acheck[i].Name2.ToString();
                        cmbBranch.Text = Acheck[i].Address1.ToString();
                       // cmbChkType.Text = Acheck[i].ChkTypeName.ToString();

                    }
                }
            }
        }

        private void txtAccountNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtOrQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtOrQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cmbBranch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
        isExist:
          //  deliveryDate = dateTime;
            //batchFile = DateTime.Today.ToString("MMddyyyy");
        if (deliveryDate == dateTime)
        {
            MessageBox.Show("Please set the delivery date!");
        }
        //else
        //deliveryDate = DateTime.Parse(dateTimePicker1.Text);
        //
        else
        {
            
            batchFile = txtbatch.Text;
            if (txtbatch.Text == "")
            {
                MessageBox.Show("Please Input Batch File!!!");
            }
            else
            {
                //  OutPutProcess.InputBox("", "Batch Number :", ref batchFile);
                DialogResult result1 = MessageBox.Show("Are you sure to continue the process?", "", MessageBoxButtons.YesNo);

                List<ChequeModel> bcheck = new List<ChequeModel>();
                if (result1.ToString() == "Yes")
                {

                    // checking if the bacth file does exists! 
                    db.GetBatchFile(bcheck);
                    if (bcheck != null)
                    {
                        for (int b = 0; b < bcheck.Count; b++)
                        {

                            if (bcheck[b].BatchFile == batchFile)
                            {


                                MessageBox.Show("Batch is already exists!!!!!");
                                goto isExist;
                            }
                            else
                                  CheckProcess();
                        }

                      
                    }
                    else
                    {
                        CheckProcess();

                    }


                }
            }
        }
        }
        private void ClearForm()
        {
            txtAccountName1.Text = "";
            txtAccountName2.Text = "";
            txtAccountNumber.Text = "";
            txtAccountNumber.Text = "";
            txtOrQty.Text = "";
            rdbCommercial.Checked = false;
            rdbPersonal.Checked = false;
            rdbChargeSlip.Checked = false;
            txtAccountName1.Enabled = false;
            txtAccountName2.Enabled = false;
            txtAccountNumber.Enabled = false;
            txtOrQty.Enabled = false;
        }
        private void CheckProcess()
        {
            ZipCopyProcess.DeleteTxtFile();
            fileName = "BNU" + batchFile.Substring(0, 4) + "P";
            List<ChequeModel> lcheque = new List<ChequeModel>();
            List<ChequeModel> pcheque = new List<ChequeModel>();
            List<ChequeModel> dbfcheque = new List<ChequeModel>();
            OutPut2Process processOutput = new OutPut2Process();
          //  Int64 endingSeries = 0;
          //  db.GetAllData(lcheque, batchFile);
            
            
           
          
            processOutput.ProcessCheck(lcheque, this);
            processOutput.PackingText(pcheque, this);
            processOutput.SaveToPackingDBF(dbfcheque, batchFile, this);
            processOutput.PrinterFile(lcheque, this);
            
            for (int i = 0; i < lcheque.Count; i++)
            {
                db.SavedDatatoDatabase(lcheque[i], batchFile);       
            }
            
            db.UpdateRef(br, cheque.BRSTN);
          
            ZipCopyProcess z = new ZipCopyProcess();
            z.ZipFileS(frmLogIn._userName, this);
          //  ZipCopyProcess.CopyPrinterFile(this);
            
            db.DeleteTempData();
            CheckLoadData();
            db.DumpMySQL();
            MessageBox.Show("Process Done!!");
            Environment.Exit(0);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            deliveryDate = dateTimePicker1.Value;
        }

        private void txtAccountName1_TextChanged(object sender, EventArgs e)
        {
            txtAccountName1.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtAccountName2_TextChanged(object sender, EventArgs e)
        {
            txtAccountName2.CharacterCasing = CharacterCasing.Upper;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            // System.Drawing.ColorTranslator.FromHtml("#A9A9A9");
           
        }
    }
}
