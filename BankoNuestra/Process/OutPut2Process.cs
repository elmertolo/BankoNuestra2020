using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using BankoNuestra.Models;
using BankoNuestra.Services;
using System.Windows.Forms;
using System.Data.OleDb;

namespace BankoNuestra.Process
{
    class OutPut2Process
    {
        string outputForlder = Application.StartupPath + "\\Output";
        public void ProcessCheck(List<ChequeModel> _checkm, frmMain _mainForm)
        {
            string doBlockPath;
            StreamWriter file;
            DBConnections db = new DBConnections();
            db.GetAllData(_checkm, _mainForm.batchFile);
            var chkList = _checkm.Select(e => e.ChequeType).Distinct().ToList();
            foreach (string chk in chkList)
            {
                doBlockPath = Application.StartupPath + "\\Output\\Regular_Checks\\BlockP.txt";

                if (chk == "A")
                {
                    if (File.Exists(doBlockPath))
                        File.Delete(doBlockPath);

                    file = File.CreateText(doBlockPath);
                    file.Close();

                    var chkA = _checkm.Where(e => e.ChequeType == chk).ToList();

                    using (file = new StreamWriter(File.Open(doBlockPath, FileMode.Append)))
                    {
                        string output = OutPutProcess.ConvertToBlockText(chkA, "PERSONAL", _mainForm.batchFile, _mainForm.deliveryDate, frmLogIn._userName, _mainForm.fileName);

                        file.WriteLine(output);
                    }

                }

            }
            foreach (string chk in chkList)
            {

                if (chk == "B")
                {

                    var chkB = _checkm.Where(e => e.ChequeType == chk).ToList();
                    doBlockPath = Application.StartupPath + "\\Output\\Regular_Checks\\BlockC.txt";
                    if (File.Exists(doBlockPath))
                        File.Delete(doBlockPath);

                    file = File.CreateText(doBlockPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(doBlockPath, FileMode.Append)))
                    {
                        string output = OutPutProcess.ConvertToBlockText(chkB, "COMMERCIAL", _mainForm.batchFile, _mainForm.deliveryDate, frmLogIn._userName, _mainForm.fileName);

                        file.WriteLine(output);
                    }
                }

            }
            foreach (string chk in chkList)
            {

                if (chk == "CS")
                {

                    var chkB = _checkm.Where(e => e.ChequeType == chk).ToList();
                    doBlockPath = Application.StartupPath + "\\Output\\Charge_Slip\\BlockA.txt";
                    if (File.Exists(doBlockPath))
                        File.Delete(doBlockPath);

                    file = File.CreateText(doBlockPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(doBlockPath, FileMode.Append)))
                    {
                        string output = OutPutProcess.ConvertToBlockText(chkB, "CHARGE-SLIP", _mainForm.batchFile, _mainForm.deliveryDate, frmLogIn._userName, _mainForm.fileName);

                        file.WriteLine(output);
                    }
                }

            }

        }
        public void ProcessCheck2(List<ChequeModel> _checkm, Main2 _mainForm)
        {
            string doBlockPath;
            StreamWriter file;
            DBConnections db = new DBConnections();
          //  db.GetAllData(_checkm, _mainForm.batchfile);
            var chkList = _checkm.Select(e => e.ChequeType).Distinct().ToList();
            foreach (string chk in chkList)
            {
                doBlockPath = Application.StartupPath + "\\Output\\Regular_Checks\\BlockP.txt";

                if (chk == "A")
                {
                    if (File.Exists(doBlockPath))
                        File.Delete(doBlockPath);

                    file = File.CreateText(doBlockPath);
                    file.Close();

                    var chkA = _checkm.Where(e => e.ChequeType == chk).ToList();

                    using (file = new StreamWriter(File.Open(doBlockPath, FileMode.Append)))
                    {
                        string output = OutPutProcess.ConvertToBlockText(chkA, "PERSONAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn._userName, _mainForm.filename);

                        file.WriteLine(output);
                    }

                }

            }
            foreach (string chk in chkList)
            {

                if (chk == "B")
                {

                    var chkB = _checkm.Where(e => e.ChequeType == chk).ToList();
                    doBlockPath = Application.StartupPath + "\\Output\\Regular_Checks\\BlockC.txt";
                    if (File.Exists(doBlockPath))
                        File.Delete(doBlockPath);

                    file = File.CreateText(doBlockPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(doBlockPath, FileMode.Append)))
                    {
                        string output = OutPutProcess.ConvertToBlockText(chkB, "COMMERCIAL", _mainForm.batchfile, _mainForm.deliveryDate, frmLogIn._userName, _mainForm.filename);

                        file.WriteLine(output);
                    }
                }

            }
            //foreach (string chk in chkList)
            //{

            //    if (chk == "CS")
            //    {

            //        var chkB = _checkm.Where(e => e.ChequeType == chk).ToList();
            //        doBlockPath = Application.StartupPath + "\\Output\\Charge_Slip\\BlockA.txt";
            //        if (File.Exists(doBlockPath))
            //            File.Delete(doBlockPath);

            //        file = File.CreateText(doBlockPath);
            //        file.Close();

            //        using (file = new StreamWriter(File.Open(doBlockPath, FileMode.Append)))
            //        {
            //            string output = OutPutProcess.ConvertToBlockText(chkB, "CHARGE-SLIP", _mainForm.batchFile, _mainForm.deliveryDate, frmLogIn._userName, _mainForm.fileName);

            //            file.WriteLine(output);
            //        }
            //    }

            //}

        }
        //string outputForlder = "\\\\192.168.0.254\\captive\\Auto\\IslaBank\\Test";
        public void PackingText(List<ChequeModel> _checksModel, frmMain _mainForm)
        {

            StreamWriter file;
            DBConnections db = new DBConnections();
            db.GetAllData(_checksModel, _mainForm.batchFile);
            var listofcheck = _checksModel.Select(e => e.ChequeType).ToList();

            foreach (string Scheck in listofcheck)
            {
                if (Scheck == "A")
                {

                    string packkingListPath = outputForlder + "\\Regular_Checks\\PackingA.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checksModel.Where(a => a.ChequeType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutPutProcess.ConvertToPackingList(checks, "PERSONAL", _mainForm.batchFile);

                        file.WriteLine(output);
                    }

                }
            }
            foreach (string Scheck in listofcheck)
            {
                if (Scheck == "B")
                {

                    string packkingListPath = outputForlder + "\\Regular_Checks\\PackingB.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checksModel.Where(a => a.ChequeType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutPutProcess.ConvertToPackingList(checks, "COMMERCIAL", _mainForm.batchFile);

                        file.WriteLine(output);
                    }

                }
            }
          
            
        }
        public void PackingText2(List<ChequeModel> _checksModel, Main2 _mainForm)
        {

            StreamWriter file;
            DBConnections db = new DBConnections();
           // db.GetAllData(_checksModel, _mainForm.batchFile);
            var listofcheck = _checksModel.Select(e => e.ChequeType).ToList();

            foreach (string Scheck in listofcheck)
            {
                if (Scheck == "A")
                {

                    string packkingListPath = outputForlder + "\\Regular_Checks\\PackingA.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checksModel.Where(a => a.ChequeType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutPutProcess.ConvertToPackingList(checks, "PERSONAL", _mainForm.batchfile);

                        file.WriteLine(output);
                    }

                }
            }
            foreach (string Scheck in listofcheck)
            {
                if (Scheck == "B")
                {

                    string packkingListPath = outputForlder + "\\Regular_Checks\\PackingB.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checksModel.Where(a => a.ChequeType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = OutPutProcess.ConvertToPackingList(checks, "COMMERCIAL", _mainForm.batchfile);

                        file.WriteLine(output);
                    }

                }
            }


        }
        public void SaveToPackingDBF(List<ChequeModel> _checks, string _batchNumber, frmMain _mainForm)
        {
            string dbConnection;
            string tempCheckType = "";
            int blockNo = 0, blockCounter = 0;
            DBConnections db = new DBConnections();
            db.GetAllData(_checks, _mainForm.batchFile);

            var listofchecks = _checks.Select(e => e.ChequeType).Distinct().ToList();

            foreach (string checktype in listofchecks)
            {

                if (checktype == "A" || checktype == "B")
                {
                    dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\Regular_Checks\\Packing.dbf" + "; Mode=ReadWrite;";

                    //Check if packing file exists
                    //if (!File.Exists(_filepath))
                    //{
                    OleDbConnection oConnect = new OleDbConnection(dbConnection);
                    OleDbCommand oCommand;
                    oConnect.Open();
                    oCommand = new OleDbCommand("DELETE FROM PACKING", oConnect);
                    oCommand.ExecuteNonQuery();
                    foreach (var check in _checks)
                    {
                        if (tempCheckType != check.ChequeType)
                            blockNo = 1;

                        tempCheckType = check.ChequeType;

                        if (blockCounter < 4)
                            blockCounter++;
                        else
                        {
                            blockCounter = 1;
                            blockNo++;
                        }

                        string sql = "INSERT INTO PACKING (BATCHNO, BLOCK, RT_NO, BRANCH, ACCT_NO, ACCT_NO_P, CHKTYPE, ACCT_NAME1, ACCT_NAME2," +
                         "NO_BKS, CK_NO_P, CK_NO_B, CK_NOE, CK_NO_E, DELIVERTO, M) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.Address1 +
                         "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChequeType + "','" + check.Name1.Replace("'", "''") + "','" + check.Name2.Replace("'", "''") + "',1," +
                         check.StartingSerial + ",'" + check.StartingSerial + "'," + check.StartingSerial + ",'" + check.EndingSerial + "','R', '')";

                        oCommand = new OleDbCommand(sql, oConnect);

                        oCommand.ExecuteNonQuery();
                    }
                    oConnect.Close();
                }

            }
            foreach (string checktype in listofchecks)
            {

                if (checktype == "CS")
                {
                    dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\Charge_Slip\\Packing.dbf" + "; Mode=ReadWrite;";

                    //Check if packing file exists
                    //if (!File.Exists(_filepath))
                    //{
                    OleDbConnection oConnect = new OleDbConnection(dbConnection);
                    OleDbCommand oCommand;
                    oConnect.Open();
                    oCommand = new OleDbCommand("DELETE FROM PACKING", oConnect);
                    oCommand.ExecuteNonQuery();
                    foreach (var check in _checks)
                    {
                        if (tempCheckType != check.ChequeType)
                            blockNo = 1;

                        tempCheckType = check.ChequeType;

                        if (blockCounter < 4)
                            blockCounter++;
                        else
                        {
                            blockCounter = 1;
                            blockNo++;
                        }

                        string sql = "INSERT INTO PACKING (BATCHNO, BLOCK, RT_NO, BRANCH, ACCT_NO, ACCT_NO_P, CHKTYPE, ACCT_NAME1, ACCT_NAME2," +
                         "NO_BKS, CK_NO_P, CK_NO_B, CK_NOE, CK_NO_E, DELIVERTO, M) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.Address1 +
                         "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChequeType + "','" + check.Name1.Replace("'", "''") + "','" + check.Name2.Replace("'", "''") + "',1," +
                         check.StartingSerial + ",'" + check.StartingSerial + "'," + check.StartingSerial + ",'" + check.EndingSerial + "','R', '')";

                        oCommand = new OleDbCommand(sql, oConnect);

                        oCommand.ExecuteNonQuery();
                    }
                    oConnect.Close();
                }
            }
        }

        public void SaveToPackingDBF2(List<ChequeModel> _checks, string _batchNumber, Main2 _mainForm)
        {
            string dbConnection;
            string tempCheckType = "";
            int blockNo = 0, blockCounter = 0;
            DBConnections db = new DBConnections();
           // db.GetAllData(_checks, _mainForm.batchfile);

            var listofchecks = _checks.Select(e => e.ChequeType).Distinct().ToList();

            foreach (string checktype in listofchecks)
            {

                if (checktype == "A" || checktype == "B")
                {
                    dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\Regular_Checks\\Packing.dbf" + "; Mode=ReadWrite;";

                    //Check if packing file exists
                    //if (!File.Exists(_filepath))
                    //{
                    OleDbConnection oConnect = new OleDbConnection(dbConnection);
                    OleDbCommand oCommand;
                    oConnect.Open();
                    oCommand = new OleDbCommand("DELETE FROM PACKING", oConnect);
                    oCommand.ExecuteNonQuery();
                    foreach (var check in _checks)
                    {
                        if (tempCheckType != check.ChequeType)
                            blockNo = 1;

                        tempCheckType = check.ChequeType;

                        if (blockCounter < 4)
                            blockCounter++;
                        else
                        {
                            blockCounter = 1;
                            blockNo++;
                        }

                        string sql = "INSERT INTO PACKING (BATCHNO, BLOCK, RT_NO, BRANCH, ACCT_NO, ACCT_NO_P, CHKTYPE, ACCT_NAME1, ACCT_NAME2," +
                         "NO_BKS, CK_NO_P, CK_NO_B, CK_NOE, CK_NO_E, DELIVERTO, M) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.Address1 +
                         "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChequeType + "','" + check.Name1.Replace("'", "''") + "','" + check.Name2.Replace("'", "''") + "',1," +
                         check.StartingSerial + ",'" + check.StartingSerial + "'," + check.StartingSerial + ",'" + check.EndingSerial + "','R', '')";

                        oCommand = new OleDbCommand(sql, oConnect);

                        oCommand.ExecuteNonQuery();
                    }
                    oConnect.Close();
                }

            }
            //foreach (string checktype in listofchecks)
            //{

            //    if (checktype == "CS")
            //    {
            //        dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\Charge_Slip\\Packing.dbf" + "; Mode=ReadWrite;";

            //        //Check if packing file exists
            //        //if (!File.Exists(_filepath))
            //        //{
            //        OleDbConnection oConnect = new OleDbConnection(dbConnection);
            //        OleDbCommand oCommand;
            //        oConnect.Open();
            //        oCommand = new OleDbCommand("DELETE FROM PACKING", oConnect);
            //        oCommand.ExecuteNonQuery();
            //        foreach (var check in _checks)
            //        {
            //            if (tempCheckType != check.ChequeType)
            //                blockNo = 1;

            //            tempCheckType = check.ChequeType;

            //            if (blockCounter < 4)
            //                blockCounter++;
            //            else
            //            {
            //                blockCounter = 1;
            //                blockNo++;
            //            }

            //            string sql = "INSERT INTO PACKING (BATCHNO, BLOCK, RT_NO, BRANCH, ACCT_NO, ACCT_NO_P, CHKTYPE, ACCT_NAME1, ACCT_NAME2," +
            //             "NO_BKS, CK_NO_P, CK_NO_B, CK_NOE, CK_NO_E, DELIVERTO, M) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.Address1 +
            //             "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChequeType + "','" + check.Name1.Replace("'", "''") + "','" + check.Name2.Replace("'", "''") + "',1," +
            //             check.StartingSerial + ",'" + check.StartingSerial + "'," + check.StartingSerial + ",'" + check.EndingSerial + "','R', '')";

            //            oCommand = new OleDbCommand(sql, oConnect);

            //            oCommand.ExecuteNonQuery();
            //        }
            //        oConnect.Close();
            //    }
            //}
        }
        public void PrinterFile(List<ChequeModel> _checkModel, frmMain _mainForm)
        {

            DBConnections db = new DBConnections();
            db.GetAllData(_checkModel, _mainForm.batchFile);
            StreamWriter file;

            var listofchecks = _checkModel.Select(e => e.ChequeType).ToList();
           

            foreach (string checktype in listofchecks)
            {
                if (checktype == "A")
                {
                    string printerFilePathA = Application.StartupPath + "\\Output\\Regular_Checks\\BNU" + _mainForm.batchFile.Substring(0, 4) + "P.txt";
                    var check = _checkModel.Where(e => e.ChequeType == checktype).ToList();
                    if (File.Exists(printerFilePathA))
                        File.Delete(printerFilePathA);

                    file = File.CreateText(printerFilePathA);
                    file.Close();
                   
                    for (int a = 0; a < check.Count; a++)
                    {


                        using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                        {
                            string output = OutPutProcess.ConvertToPrinterFormat1(check[a].BRSTN, check[a].AccountNo, long.Parse(check[a].StartingSerial), check[a].PcsPerBook, check[a].Name1, check[a].Name2, check[a].Address1, check[a].Address2, check[a].Address3, check[a].Address4, check[a].Address5, check[a].Address6, check[a].Address1, "A");

                            file.WriteLine(output);
                        }
                    }
                    ZipCopyProcess.CopyPrinterFile(checktype, _mainForm);
                    ZipCopyProcess.CopyPackingDBF(checktype, _mainForm);
                }
              
            }
            

            foreach (string checktype in listofchecks)
            {
                if (checktype == "B")
                {
                    string printerFilePath = Application.StartupPath + "\\Output\\Regular_Checks\\BNU" + _mainForm.batchFile.Substring(0, 4) + "C.txt";
                    var check = _checkModel.Where(e => e.ChequeType == checktype).ToList();
                    if (File.Exists(printerFilePath))
                        File.Delete(printerFilePath);

                    file = File.CreateText(printerFilePath);
                    file.Close();
                    for (int a = 0; a <check.Count; a++)
			        {
			 
			
                    using (file = new StreamWriter(File.Open(printerFilePath, FileMode.Append)))
                      {
                          string output = OutPutProcess.ConvertToPrinterFormat1(check[a].BRSTN, check[a].AccountNo, long.Parse(check[a].StartingSerial), check[a].PcsPerBook, check[a].Name1, check[a].Name2, check[a].Address1, check[a].Address2, check[a].Address3, check[a].Address4, check[a].Address5, check[a].Address6, check[a].Address1, "B");

                          file.WriteLine(output);
                        }
                    }
                    ZipCopyProcess.CopyPrinterFile(checktype, _mainForm);
                    ZipCopyProcess.CopyPackingDBF(checktype, _mainForm);
                }  
            }
     
            foreach (string checktype in listofchecks)
            {
                if (checktype == "CS")
                {
                    string printerFilePath = Application.StartupPath + "\\Output\\Charge_Slip\\BNU" + _mainForm.batchFile.Substring(0, 4) + "CS.txt";
                    var check = _checkModel.Where(e => e.ChequeType == checktype).ToList();
                    if (File.Exists(printerFilePath))
                        File.Delete(printerFilePath);

                    file = File.CreateText(printerFilePath);
                    file.Close();
                    for (int a = 0; a < check.Count; a++)
                    {


                        using (file = new StreamWriter(File.Open(printerFilePath, FileMode.Append)))
                        {
                            string output = OutPutProcess.ConvertToPrinterFormat1(check[a].BRSTN, check[a].AccountNo, long.Parse(check[a].StartingSerial), check[a].PcsPerBook, check[a].Name1, check[a].Name2, check[a].Address1, check[a].Address2, check[a].Address3, check[a].Address4, check[a].Address5, check[a].Address6, check[a].Address1, "CS");

                            file.WriteLine(output);
                        }
                    }
                    ZipCopyProcess.CopyPrinterFile(checktype, _mainForm);
                    ZipCopyProcess.CopyPackingDBF(checktype, _mainForm);
                }
            }
           
        }
        public void PrinterFile2(List<ChequeModel> _checkModel, Main2 _mainForm)
        {

            DBConnections db = new DBConnections();
            db.GetAllData(_checkModel, _mainForm.batchfile);
            StreamWriter file;

            var listofchecks = _checkModel.Select(e => e.ChequeType).ToList();


            foreach (string checktype in listofchecks)
            {
                if (checktype == "A")
                {
                    string printerFilePathA = Application.StartupPath + "\\Output\\Regular_Checks\\BNU" + _mainForm.batchfile.Substring(0, 4) + "P.txt";
                    var check = _checkModel.Where(e => e.ChequeType == checktype).ToList();
                    if (File.Exists(printerFilePathA))
                        File.Delete(printerFilePathA);

                    file = File.CreateText(printerFilePathA);
                    file.Close();

                    for (int a = 0; a < check.Count; a++)
                    {


                        using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                        {
                            string output = OutPutProcess.ConvertToPrinterFormat1(check[a].BRSTN, check[a].AccountNo, long.Parse(check[a].StartingSerial), check[a].PcsPerBook, check[a].Name1, check[a].Name2, check[a].Address1, check[a].Address2, check[a].Address3, check[a].Address4, check[a].Address5, check[a].Address6, check[a].Address1, "A");

                            file.WriteLine(output);
                        }
                    }
                    ZipCopyProcess.CopyPrinterFile2(checktype, _mainForm);
                    ZipCopyProcess.CopyPackingDBF2(checktype, _mainForm);
                }

            }


            foreach (string checktype in listofchecks)
            {
                if (checktype == "B")
                {
                    string printerFilePath = Application.StartupPath + "\\Output\\Regular_Checks\\BNU" + _mainForm.batchfile.Substring(0, 4) + "C.txt";
                    var check = _checkModel.Where(e => e.ChequeType == checktype).ToList();
                    if (File.Exists(printerFilePath))
                        File.Delete(printerFilePath);

                    file = File.CreateText(printerFilePath);
                    file.Close();
                    for (int a = 0; a < check.Count; a++)
                    {


                        using (file = new StreamWriter(File.Open(printerFilePath, FileMode.Append)))
                        {
                            string output = OutPutProcess.ConvertToPrinterFormat1(check[a].BRSTN, check[a].AccountNo, long.Parse(check[a].StartingSerial), check[a].PcsPerBook, check[a].Name1, check[a].Name2, check[a].Address1, check[a].Address2, check[a].Address3, check[a].Address4, check[a].Address5, check[a].Address6, check[a].Address1, "B");

                            file.WriteLine(output);
                        }
                    }
                    ZipCopyProcess.CopyPrinterFile2(checktype, _mainForm);
                    ZipCopyProcess.CopyPackingDBF2(checktype, _mainForm);
                }
            }

            //foreach (string checktype in listofchecks)
            //{
            //    if (checktype == "CS")
            //    {
            //        string printerFilePath = Application.StartupPath + "\\Output\\Charge_Slip\\BNU" + _mainForm.batchFile.Substring(0, 4) + "CS.txt";
            //        var check = _checkModel.Where(e => e.ChequeType == checktype).ToList();
            //        if (File.Exists(printerFilePath))
            //            File.Delete(printerFilePath);

            //        file = File.CreateText(printerFilePath);
            //        file.Close();
            //        for (int a = 0; a < check.Count; a++)
            //        {


            //            using (file = new StreamWriter(File.Open(printerFilePath, FileMode.Append)))
            //            {
            //                string output = OutPutProcess.ConvertToPrinterFormat1(check[a].BRSTN, check[a].AccountNo, long.Parse(check[a].StartingSerial), check[a].PcsPerBook, check[a].Name1, check[a].Name2, check[a].Address1, check[a].Address2, check[a].Address3, check[a].Address4, check[a].Address5, check[a].Address6, check[a].Address1, "CS");

            //                file.WriteLine(output);
            //            }
            //        }
            //        ZipCopyProcess.CopyPrinterFile(checktype, _mainForm);
            //        ZipCopyProcess.CopyPackingDBF(checktype, _mainForm);
            //    }
            //}

        }
        public void SavePackingDBF(List<ChequeModel> _list, frmMain _mainForm)
        {
            DBConnections db = new DBConnections();
            db.GetAllData(_list, _mainForm.batchFile);
        }
    }
}