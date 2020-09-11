using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using BankoNuestra.Models;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.IO;


namespace BankoNuestra.Services
{
    class DBConnections
    {
        public MySqlConnection myConnect;
        public string databaseName = "";
        public string tableName = "";
        public void DBConnect()
        {
            try
            {
            string DBConnection = "";

            if (frmLogIn._userName == "test")
            {
                DBConnection = "datasource=localhost;port=3306;username=root;password=corpcaptive; convert zero datetime=True;";
                //MessageBox.Show("Hello Test!!!");
                databaseName = "islabank";
              //  tableName;
            }
            else
            {
                //  DBConnection = "";
                DBConnection = "datasource=192.168.0.254;port=3306;username=root;password=CorpCaptive; convert zero datetime=True;";
                // MessageBox.Show("HELLO");
                databaseName = "captive_database";

            }

            myConnect = new MySqlConnection(DBConnection);
            myConnect.Open();

            }
            catch (Exception Error)
            {

                MessageBox.Show(Error.Message, "System Error");
            }
        }// end of function

        public void DBClosed()
        {
            myConnect.Close();
        }// end of function
 
        public DataTable GetBranch(DataTable _branches)
        {
            DBConnect();
            string sql = "Select Address1 from "+databaseName + ".banko_nuestra_branches";
            MySqlDataAdapter cmd = new MySqlDataAdapter(sql, myConnect);
            cmd.Fill(_branches);
            return _branches;
        }
        public BranchesModel GetBrstn(BranchesModel _refmodel, string _branch)
        {
            DBConnect();
            string sql = "Select brstn from "+databaseName +".banko_nuestra_branches where Address1 = '" + _branch + "'";
            MySqlCommand cmd = new MySqlCommand(sql, myConnect);
            MySqlDataReader myReader = cmd.ExecuteReader();

            while (myReader.Read())
            {
                _refmodel.BRSTN = myReader.GetString(0);
            }

            DBClosed();

            return _refmodel;
        }
        public DataTable LoadDataToGridView(DataTable data)
        {
            string sql = "Select Distinct(BRSTN), CheckType as ChequeType, ChequeTypeName as ChequeName, AccountNumber, Name1 as Name, Name2 as SecondaryName, BranchName as BranchName, Quantity from "+databaseName +".banko_nuestra_temp";
            DBConnect();
            MySqlDataAdapter adp = new MySqlDataAdapter(sql, myConnect);
            adp.Fill(data);
            return data;
        }// end of function

        public ChequeModel SaveToTempTable(ChequeModel _check)
        {

            string sql = "INSERT INTO " + databaseName + ".banko_nuestra_temp (BRSTN,AccountNumber,CheckType,ChequeTypeName,Name1,Name2,Quantity,BranchName,Address2, Address3, Address4, StartingSerial, EndingSerial,PcsPerBook)VALUES(" +
                        "'" + _check.BRSTN + "'," +
                        "'" + _check.AccountNo + "'," +
                        "'" + _check.ChequeType + "'," +
                        "'" + _check.ChequeName + "'," +
                        "'" + _check.Name1 + "'," +
                        "'" + _check.Name2 + "'," +
                        "'" + _check.Quantity + "'," +
                        "'" + _check.Address1 + "'," +
                        "'" + _check.Address2 + "'," +
                        "'" + _check.Address3 + "'," +
                        "'" + _check.Address4 + "'," +
                        "'" + _check.StartingSerial + "'," +
                        "'" + _check.EndingSerial + "'," +
                        "'" + _check.PcsPerBook + "')";
                    


            DBConnect();
            MySqlCommand myCommand = new MySqlCommand(sql, myConnect);

            myCommand.ExecuteNonQuery();
            DBClosed();
            return _check;
        }// end of function


        public List<ChequeModel> GetAllData(List<ChequeModel> check, string _batch)
        {
            DBConnect();
            string sql = "SELECT A.BRSTN,AccountNumber,CheckType,ChequeTypeName,Name1,Name2,Quantity,BranchName," 
            + "B.Address2, B.Address3, B.Address4, StartingSerial, EndingSerial, PcsPerBook FROM "+databaseName+".banko_nuestra_temp as A left join "+databaseName+".banko_nuestra_branches as B on  A.BRSTN = B.BRSTN";
            MySqlCommand cmd = new MySqlCommand(sql, myConnect);
            MySqlDataReader dr = cmd.ExecuteReader();

            // List<CheckModel> check = new List<CheckModel>();

            while (dr.Read())
            {
                ChequeModel model = new ChequeModel
                {
                    BRSTN = dr.GetString(0),
                    AccountNo = dr.GetString(1),
                    ChequeType = dr.GetString(2),
                    ChequeName = dr.GetString(3),
                    Name1 = dr.GetString(4),
                    Name2 = dr.GetString(5),
                    Quantity = int.Parse(dr.GetString(6)),
                    Address1 = dr.GetString(7),
                    Address2 = dr.GetString(8),
                    Address3 = dr.GetString(9),
                    Address4 = dr.GetString(10),
                    StartingSerial = dr.GetString(11),
                    EndingSerial = dr.GetString(12),
                    PcsPerBook = dr.GetInt16(13),
                  

                };

                check.Add(model);
            }

            DBClosed();
            return check;

        }// end of function

        public BranchesModel GetLastNO(BranchesModel _refmodel, string _Brstn)
        {
            DBConnect();
            string sql = "SELECT  DateEncoded,BRSTN,Address1,LastNo_C,LastNo_CS,LastNo_P FROM "+databaseName+".banko_nuestra_branches where BRSTN ='" + _Brstn + "'";

            MySqlCommand cmd = new MySqlCommand(sql, myConnect);

            MySqlDataReader myReader = cmd.ExecuteReader();


            while (myReader.Read())
            {
               // _refmodel.ID = int.Parse(myReader.GetString(0));
                _refmodel.Date = DateTime.Parse(myReader.GetString(0));
                _refmodel.BRSTN = myReader.GetString(1);
                _refmodel.Address1 = myReader.GetString(2);
                _refmodel.LastNo_C = myReader.GetInt64(3);
                _refmodel.LastNo_CS = myReader.GetInt64(4);
                _refmodel.LastNo_P = myReader.GetInt64(5);
                //_refmodel.C_Before = int.Parse(myReader.GetString(7));
            }

            DBClosed();
            return _refmodel;

        }// end of function
        public BranchesModel UpdateRef(BranchesModel _ref, string _brstn)
        {
            DBConnect();
            string sql = "Update " + databaseName + ".banko_nuestra_branches SET LastNo_C = '" + _ref.LastNo_C + "',LastNo_CS = '" + _ref.LastNo_CS + "' ,LastNo_P = '" + _ref.LastNo_P + "', DateUpdated = '" + _ref.DateUpdated.ToString("yyyy-MM-dd") + "' where BRSTN = '" + _brstn + "'";
            MySqlCommand cmd = new MySqlCommand(sql, myConnect);

            MySqlDataReader myReader = cmd.ExecuteReader();
            DBClosed();
            return _ref;

        }// end of function

        public ChequeModel SavedDatatoDatabase(ChequeModel _check, string _batch,DateTime _deliveryDate)
        {

            string sql = "INSERT INTO " + databaseName + ".banko_nuestra(Date,Time,DeliveryDate,ChkType,ChequeName,BRSTN,AccountNo,Name1,Name2,Address1,Address2,Address3,Address4,Address5,Address6,Batch,StartingSerial, EndingSerial)VALUES(" +

                        "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                        "'" + DateTime.Now.ToString("HH:mm:ss") + "'," +
                        "'" + _deliveryDate.ToString("yyyy-MM-dd") + "'," +
                        "'" + _check.ChequeType + "'," +
                        "'" + _check.ChequeName + "'," +
                        "'" + _check.BRSTN + "'," +
                        "'" + _check.AccountNo + "'," +
                        "'" + _check.Name1 + "'," +
                        "'" + _check.Name2 + "'," +
                        "'" + _check.Address1 + "'," +
                        "'" + _check.Address2 + "'," +
                        "'" + _check.Address3 + "'," +
                        "'" + _check.Address4 + "'," +
                        "'" + _check.Address5 + "'," +
                        "'" + _check.Address6 + "'," +
                        "'" + _batch + "'," +
                        "'" + _check.StartingSerial + "'," +
                        "'" + _check.EndingSerial + "')";



            DBConnect();
            MySqlCommand myCommand = new MySqlCommand(sql, myConnect);

            myCommand.ExecuteNonQuery();
            DBClosed();
            return _check;
        }// end of function
        public void DeleteTempData()
        {
                
            string sql = "Delete from " + databaseName + ".banko_nuestra_temp";
            MySqlCommand cmd = new MySqlCommand(sql, myConnect);
            myConnect.Open();
            cmd.ExecuteNonQuery();
            myConnect.Close();
        }// end of function

        public ChequeModel UpdateTemp(ChequeModel _cheque, Int64 _startingSerial, Int64 _endingSerial, string _accountNo)
        {
            DBConnect();
            string sql = "Update " +databaseName + ".banko_nuestra_temp Set StartingSerial = '" + _startingSerial +"' , EndingSerial = '" + _endingSerial + "' where AccountNumber = '" +_accountNo+ "'"  ;
            MySqlCommand cmd = new MySqlCommand(sql, myConnect);
            MySqlDataReader myReader = cmd.ExecuteReader();
            DBClosed();
            return _cheque;

        }

        public List<ChequeModel> GetBatchFile(List<ChequeModel> check) //Checking if the batch is already exists
        {
            DBConnect();

            MySqlCommand myCommand = new MySqlCommand("SELECT Distinct( Batch) FROM " + databaseName + ".banko_nuestra", myConnect);

            MySqlDataReader myReader = myCommand.ExecuteReader();

            // List<CheckModel> check = new List<CheckModel>();

            while (myReader.Read())
            {
                ChequeModel refe = new ChequeModel
                {

                    BatchFile = myReader.GetString(0)
                };

                check.Add(refe);
            }

            DBClosed();

            return check;
        }
        public List<MySQLLocatorModel> GetMySQLLocations()
        {
            MySqlConnection connect = new MySqlConnection("datasource=192.168.0.254 ;port=3306;username=root;password=CorpCaptive");

            connect.Open();

            MySqlCommand myCommand = new MySqlCommand("SELECT * FROM captive_database.mysqldump_location", connect);

            MySqlDataReader myReader = myCommand.ExecuteReader();

            List<MySQLLocatorModel> sqlLocator = new List<MySQLLocatorModel>();

            while (myReader.Read())
            {
                MySQLLocatorModel myLocator = new MySQLLocatorModel
                {
                    PrimaryKey = myReader.GetInt32(0),
                    Location = myReader.GetString(1)
                };

                sqlLocator.Add(myLocator);
            }

            connect.Close();

            return sqlLocator;
        }
        public void DumpMySQL()
        {
            string dbname = "banko_nuestra_branches";
            string outputFolder = Application.StartupPath +"\\Output\\" +frmMain._outputfolder;
            System.Diagnostics.Process proc = new System.Diagnostics.Process();

            proc.StartInfo.FileName = "cmd.exe";

            proc.StartInfo.UseShellExecute = false;

            proc.StartInfo.WorkingDirectory = GetMySqlPath().ToUpper().Replace("MYSQLDUMP.EXE", "");

            proc.StartInfo.RedirectStandardInput = true;

            proc.StartInfo.RedirectStandardOutput = true;

            proc.Start();

            StreamWriter myStreamWriter = proc.StandardInput;

            string temp = "mysqldump.exe --user=root --password=CorpCaptive --host=192.168.0.254 " + databaseName +" " +dbname + " > " +
                outputFolder + "\\" + DateTime.Today.ToShortDateString().Replace("/", ".") + "-" + dbname + ".SQL";

            myStreamWriter.WriteLine(temp);

            dbname = "banko_nuestra";

            temp = "mysqldump.exe --user=root --password=password=CorpCaptive --host=192.168.0.254 " + databaseName +" " +dbname + " > " +
                 outputFolder + "\\" + DateTime.Today.ToShortDateString().Replace("/", ".") + "-" + dbname + ".SQL";

            myStreamWriter.WriteLine(temp);

            myStreamWriter.Close();

            proc.WaitForExit();

            proc.Close();
        }
        public string GetMySqlPath()
        {
            var mySQLocator = GetMySQLLocations();

            foreach (var loc in mySQLocator)
            {
                if (File.Exists(loc.Location))
                    return loc.Location;
            }

            return "";
        }
        public void DumpMySQL2()
        {
            string dbname = "banko_nuestra_branches";
            string outputFolder = Application.StartupPath + "\\Output\\" + Main2.outputFolder;
            System.Diagnostics.Process proc = new System.Diagnostics.Process();

            proc.StartInfo.FileName = "cmd.exe";

            proc.StartInfo.UseShellExecute = false;

            proc.StartInfo.WorkingDirectory = GetMySqlPath().ToUpper().Replace("MYSQLDUMP.EXE", "");

            proc.StartInfo.RedirectStandardInput = true;

            proc.StartInfo.RedirectStandardOutput = true;

            proc.Start();

            StreamWriter myStreamWriter = proc.StandardInput;

            string temp = "mysqldump.exe --user=root --password=CorpCaptive --host=192.168.0.254 " + databaseName + " " + dbname + " > " +
                outputFolder + "\\" + DateTime.Today.ToShortDateString().Replace("/", ".") + "-" + dbname + ".SQL";

            myStreamWriter.WriteLine(temp);

            dbname = "banko_nuestra";

            temp = "mysqldump.exe --user=root --password=password=CorpCaptive --host=192.168.0.254 " + databaseName + " " + dbname + " > " +
                 outputFolder + "\\" + DateTime.Today.ToShortDateString().Replace("/", ".") + "-" + dbname + ".SQL";

            myStreamWriter.WriteLine(temp);

            myStreamWriter.Close();

            proc.WaitForExit();

            proc.Close();
        }
       

        public List<ChequeModel> GetNameifExisting(List<ChequeModel> check)
        {
            DBConnect();
            string sql = "Select BRSTN, ChkType, AccountNo,Name1,Name2,Address1,ChequeName from captive_database.banko_nuestra";


            MySqlCommand cmd = new MySqlCommand(sql, myConnect);

            MySqlDataReader dr = cmd.ExecuteReader();

            // List<CheckModel> check = new List<CheckModel>();

            while (dr.Read())
            {
                ChequeModel model = new ChequeModel
                {
                    BRSTN = dr.GetString(0),
                    ChequeType = dr.GetString(1),
                    AccountNo = dr.GetString(2),
                    Name1 = dr.GetString(3),
                    Name2 = dr.GetString(4),
                    Address1 = dr.GetString(5),
                    ChequeName = dr.GetString(6)
                    // BranchName = dr.GetString(5),
                    //Address2 = dr.GetString(6),
                    //   ChkTypeName = dr.GetString(6),
                    // BranchName = dr.GetString(8),
                    // StartSeries = dr.GetString(7),
                    // EndSeries = dr.GetString(8)
                };

                check.Add(model);
            }

            DBClosed();
            return check;
        }// end of function

    }
}
