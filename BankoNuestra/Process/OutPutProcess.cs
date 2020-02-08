using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoNuestra.Models;
using BankoNuestra.Services;
using System.Windows.Forms;
using System.Drawing;

namespace BankoNuestra.Process
{
    class OutPutProcess
    {
        private static string GenerateSpace(int _noOfSpaces)
        {
            string output = "";

            for (int x = 0; x < _noOfSpaces; x++)
            {
                output += " ";
            }

            return output;

        }//END OF FUNCTION

        private static string Seperator()
        {
            return "";
        }
        public static string ConvertToBlockText(List<ChequeModel> _check, string _ChkType, string _batchNumber, DateTime _deliveryDate, string _preparedBy, string _fileName)
        {
            int page = 1, lineCount = 14, blockCounter = 1, blockContent = 1;
            string date = DateTime.Now.ToString("MMM. dd, yyyy");
            bool noFooter = true;
            string countText = "";
            string output = "";

            //Sort Check List
            var sort = (from c in _check
                        orderby c.BRSTN
                        ascending
                        select c).ToList();

            output += "\n" + GenerateSpace(8) + "Page No. " + page.ToString() + "\n" +
            GenerateSpace(8) + date +
            "\n" +
            GenerateSpace(21) + "Banko Nuestra - SUMMARY OF BLOCK - " + _ChkType.ToUpper() + "\n" +
            GenerateSpace(27) + "(There should be an A prefix " + "\n" +
            GenerateSpace(27) + "printed before the CHECK NUMEBRS)" + "\n\n" +
            GenerateSpace(21) + "A L L  M A N U A L  E N C O D E D ! ! !" + "\n\n" +
            GenerateSpace(18) + "Branch Name and Address is already in the Basestock\n\n"+
            GenerateSpace(21) + "Branch Name AND BRSTN is fixed: 06137-0012" + "\n\n\n" +
            GenerateSpace(8) + "BLOCK RT_NO" + GenerateSpace(5) + "M ACCT_NO" + GenerateSpace(9) + "START_NO." + GenerateSpace(2) + "END_NO.\n\n";
            int checkTypeCount = 0;
            foreach (var check in sort)
            {


                //if (_ChkType == "PERSONAL")
                //{
                //    checkTypeCount = check.Quantity;
                //    while (check.StartingSerial.Length < 7)
                //        check.StartingSerial = "0" + check.StartingSerial;

                //    while (check.EndingSerial.Length < 7)
                //        check.EndingSerial = "0" + check.EndingSerial;
                //}
                //else
                //{

                    while (check.StartingSerial.Length < 10)
                        check.StartingSerial = "0" + check.StartingSerial;

                    while (check.EndingSerial.Length < 10)
                        check.EndingSerial = "0" + check.EndingSerial;
                //}


                if (blockContent == 1)
                {
                    output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";
                    lineCount += 2;
                }

                if (blockContent == 5)
                {
                    blockContent = 2;

                    blockCounter++;

                    output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";

                    output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                    GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";
                }
                else
                {
                    output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                    GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";

                    lineCount += 1;

                    blockContent++;
                }
            }
            //if (lineCount >=61 )
            //{
            if (noFooter) //ADD FOOTER
            {
                output += "\n " + _batchNumber + GenerateSpace(46) + "DLVR: " + _deliveryDate.ToString("MM-dd(ddd)") + "\n\n" +
                    " A = " + checkTypeCount + GenerateSpace(20) + _fileName + ".txt\n" +
                    countText +
                    GenerateSpace(4) + "Prepared By" + GenerateSpace(3) + ": " + _preparedBy + "\t\t\t\t RECHECKED BY:\n" +
                    GenerateSpace(4) + "Updated By" + GenerateSpace(4) + ": " + _preparedBy + "\n" +
                    GenerateSpace(4) + "Time Start" + GenerateSpace(4) + ": " + DateTime.Now.ToShortTimeString() + "\n" +
                    GenerateSpace(4) + "Time Finished :\n" +
                    GenerateSpace(4) + "File rcvd" + GenerateSpace(5) + ":\n";

                noFooter = false;
            }

            // output += Seperator();

            lineCount = 1;
            //}

            return output;

        }
        public static string ConvertToPackingList(List<ChequeModel> _checks, string _checkType, frmMain _mainForm)
        {
            var listofbrstn = _checks.Select(e => e.BRSTN).Distinct().ToList();
            int page = 1;
            string date = DateTime.Now.ToShortDateString();
            string output = "";
            int i = 0;

            foreach (string brstn in listofbrstn)
            {

                output += "\n Page No. " + page.ToString() + "\n " +
                                  date + "\n" +
                                  GenerateSpace(29) + "CAPTIVE PRINTING CORPORATION\n" +
                                  GenerateSpace(24) + "Banko Nuestra - " + _checkType + " Checks Summary\n\n" +
                                  GenerateSpace(2) + "ACCT_NO" + GenerateSpace(9) + "ACCOUNT NAME" + GenerateSpace(21) + "QTY CT START #" + GenerateSpace(4) + "END #\n\n\n";
                var listofchecks = _checks.Where(e => e.BRSTN == brstn).ToList();
                output += " ** ORDERS OF BRSTN " + _checks[i].BRSTN + " " + _checks[i].Address1 + "\n\n" +
                              " * BATCH #: " + _mainForm.batchFile + "\n\n";



                foreach (var check in listofchecks)
                {

                    //if (_checkType == "PERSONAL")
                    //{
                    //    while (check.StartingSerial.Length < 7)
                    //        check.StartingSerial = "0" + check.StartingSerial;

                    //    while (check.EndingSerial.Length < 7)
                    //        check.EndingSerial = "0" + check.EndingSerial;
                    //}
                    //else
                    //{
                        while (check.StartingSerial.Length < 10)
                            check.StartingSerial = "0" + check.StartingSerial;

                        while (check.EndingSerial.Length < 10)
                            check.EndingSerial = "0" + check.EndingSerial;
                    //}//END OF ADDING ZERO IN SERIES NUMBER

                    output += GenerateSpace(2) + check.AccountNo + GenerateSpace(4);

                    if (check.Name1.Length < 33)
                        output += check.Name1 + GenerateSpace(33 - check.Name1.Length);
                    else if (check.Name1.Length > 33)
                        output += check.Name2.Substring(0, 33);

                    output += "  1 " + check.ChequeType + GenerateSpace(2) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + " \n";
                    if (check.Name2 != "")
                        output += GenerateSpace(18) + check.Name2 + "\n";
                }

                output += "\n";
                output += "  * * * Sub Total * * * " + listofchecks.Count + "\n";

                page++;
                i++;

            }
            output += "  * * * Grand Total * * * " + _checks.Count + "\n";
            return output;

        }// end of function


        public static string ConvertToPrinterFile(List<ChequeModel> _checkModels)
        {

          //  var listofcheck = _checkModels.Select(e => e.BRSTN).OrderBy(e => e).Distinct().ToList();

            string output = "";
            var sort = (from c in _checkModels
                        orderby c.BRSTN, c.AccountNo
                        ascending
                        select c).ToList();


            foreach (var check in sort)
            {
                Int64 Series = 0;
                if (check.ChequeType == "B")
                {
                    Series = Int64.Parse(check.EndingSerial) + 1;
                }
                else
                {
                    Series = Int64.Parse(check.EndingSerial) + 1 ;
                }
                Int64 endSeries = Series - 1;

                string outputStartSeries = check.StartingSerial.ToString();

                string outputEndSeries = endSeries.ToString();

                //   string brstnFormat = "";

                string txtSeries = Series.ToString();

                if (check.ChequeType == "A")
                {
                    while (check.StartingSerial.Length < 7)
                        check.StartingSerial = "0" + check.StartingSerial;

                    while (check.EndingSerial.Length < 7)
                        check.EndingSerial = "0" + check.EndingSerial;
                }
                else
                {
                    while (check.StartingSerial.Length < 10)
                        check.StartingSerial = "0" + check.StartingSerial;

                    while (check.EndingSerial.Length < 10)
                        check.EndingSerial = "0" + check.EndingSerial;
                }
                 output = "2\n" + //1 (FIXED)
                          check.BRSTN + "\n" + //2  (BRSTN)
                          check.AccountNo + "\n" + //3 (ACCT NUMBER)
                           Series.ToString() + "\n" + //4 (Start Series + PCS per Book)
                          "A\n" + //5 (FIXED)
                          "\n" + //6 (BLANK)
                          check.BRSTN.Substring(0, 5) + "\n" + " " +//7 BRSTN FORMATTED
                          check.BRSTN.Substring(5,3) +
                          "\n" + //8 (BLANK)
                          check.AccountNo.Substring(0, 5) + "-" + check.AccountNo.Substring(5, 5) + "-" + check.AccountNo.Substring(10, 1) + "\n" + //17 (ACCT NUMBER) + "\n" + //9 (ACCT NUMBER)
                          check.Name1 + "\n" + //10 (NAME 1)
                          "SN\n" + //11 (FIXED)
                           "\n" + //12 (BLANK)
                          check.Name2 + "\n" + //13 (NAME 2)
                           "C\n" + //14 (FIXED)
                           "\n" + //15 (BLANK)
                           "\n" +//16 (BLANK)
                          check.Address1 + "\n" + //17 (BRANCH NAME)
                          check.Address2 + "\n" + //18 (ADDRESS 1)
                          check.Address3 + "\n" + //19 (ADDRESS 2)
                          check.Address4 + "\n" + //20 (ADDRESS 3)
                          check.Address5 + "\n" + //21 (ADDRESS 4)
                          check.Address6 + "\n" + //22 (ADDRESS 5)
                          "BANGKO NUESTRA \n" +//23 (FIXED)
                          "\n" + //24 (BLANK)//
                          "\n" + //25 (BLANK)
                          "\n" + //26 (BLANK)
                          "\n" + //27 (BLANK)
                          "\n" + //28 (BLANK)
                          "\n" + //29 (BLANK)
                          "\n" + //30 (BLANK)
                          check.StartingSerial + "\n" + //31 (STARTING SERIES)
                          check.EndingSerial; //32 (ENDING SERIES)
                 //return output;
            }
            return output;
        }
        public static string ConvertToPrinterFormat1(string _BRSTN, string _AcctNo, Int64 _StartSeries, int _pcsPerbook, string _name1, string _name2,
            string _Address1, string _Address2, string _Address3, string _Address4, string _Address5, string _Address6, string _branchName, string _checkType = "")
        {
            //try
            //{
                Int64 Series = _StartSeries + _pcsPerbook;

                Int64 endSeries = Series - 1;

                string outputStartSeries = _StartSeries.ToString();

                string outputEndSeries = endSeries.ToString();

               // string brstnFormat = "";
               // string accountNO = "0" + _AcctNo;
                string txtSeries = Series.ToString();

                //if (_checkType == "A" || _checkType == "CS")
                //{
                    while (outputStartSeries.Length < 10)
                { 
                        outputStartSeries = "0" + outputStartSeries;
                    txtSeries = "0" + txtSeries;
                    }
                    while (outputEndSeries.Length < 10)
                { 
                        outputEndSeries = "0" + outputEndSeries;
                    txtSeries = "0" + txtSeries;
                }

                //}

                //if (_checkType == "MC")
                //{
                //    brstnFormat = string.Format("{0:####-####}", Convert.ToInt64(_BRSTN));

                //    while (txtSeries.Length < 10) //Add Zero Format to complete 10 digits
                //        txtSeries = "0" + txtSeries;

                //    var temp = brstnFormat;

                //    while (temp.Length < 10)
                //        temp = "0" + temp;

                //    if (_checkType == "MC")
                //        brstnFormat = temp.Substring(0, 5) + "\n" + temp.Substring(5, temp.Length - 5);
                //    else
                //        brstnFormat = temp + "\n";
                //}
                //else
                //{
                //    brstnFormat = string.Format("{0:####-###-#}", Convert.ToInt64(_BRSTN));

                //    if (_checkType == "B")
                //        while (txtSeries.Length < 10) //Add Zero Format to complete 10 digits
                //            txtSeries = "0" + txtSeries;

                //    while (brstnFormat.Length < 11)
                //        brstnFormat = "0" + brstnFormat;

                //    while (txtSeries.Length < 7) //Add Zero Format to complete 7 digits
                //        txtSeries = "0" + txtSeries;

                //    brstnFormat += "\n";
                //}
                 //.//output = "";

            string output = "2\n" + //1 (FIXED)
                                _BRSTN + "\n" + //2  (BRSTN)
                                _AcctNo + "\n" + //3 (ACCT NUMBER)
                                txtSeries + "\n" + //4 (Start Series + PCS per Book)
                                "A\n" + //5 (FIXED)
                                "\n" + //6 (BLANK)
                                _BRSTN.Substring(0,5) + //7 BRSTN FORMATTED
                                "\n" + " " + _BRSTN.Substring(5, 4) + "\n" + //8 BRSTN FORMATTED
                                _AcctNo.Substring(0, 4) + "-" + _AcctNo.Substring(4, 2) + "-" + _AcctNo.Substring(6, 5) + "-" + _AcctNo.Substring(11, 1) + "\n" + //9 (ACCT NUMBER)
                                _name1 + "\n" + //10 (NAME 1)
                                "SN\n" + //11 (FIXED)
                                "\n" + //12 (BLANK)
                                _name2 + "\n" + //13 (NAME 2)
                                "C\n" + //14 (FIXED)
                                "XXXX\n" + //15 (BLANK)
                                "\n" +//16 (BLANK)
                                _branchName + "\n" + //17 (BRANCH NAME)
                                _Address2 + "\n" + //18 (ADDRESS 1)
                                _Address3 + "\n" + //19 (ADDRESS 2)
                                _Address4 + "\n" + //20 (ADDRESS 3)
                                _Address5 + "\n" + //21 (ADDRESS 4)
                                _Address6 + "\n" + //22 (ADDRESS 5)
                                "BANKO NUESTRA\n" +//23 (FIXED)
                                "\n" + //24 (BLANK)//
                                "\n" + //25 (BLANK)
                                "\n" + //26 (BLANK)
                                "\n" + //27 (BLANK)
                                "\n" + //28 (BLANK)
                                "\n" + //29 (BLANK)
                                "\n" + //30 (BLANK)
                                outputStartSeries + "\n" + //31 (STARTING SERIES)
                                outputEndSeries; //32 (ENDING SERIES)

                return output;
          //  }
            //catch
            //{
            //    return "";
            //}
        }//END OF FUNCTION
   
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {

            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
    }
}
