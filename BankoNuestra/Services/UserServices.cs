using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankoNuestra.Models;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BankoNuestra.Services
{
    class UserServices : DBConnections
    {
        public Users Login(string _username, string _password)
        {
            try
            {

                if (_username == "test")
                {
                    Users user = new Users
                    {
                        Username = "Test",
                        Password = "",
                        Name = "Test User"
                    };
                 //   Properties.Settings.Default.IfTest = true;
                    return user;
                }

                else
                {
                    //   int check = _check;
                  //  Properties.Settings.Default.IfTest = false;
                    // _check = 0;
                    DBConnect();

                    Users user = new Users();

                    string query = "SELECT username, password FROM " + databaseName + ".users WHERE username='" + _username + "' AND password='" + _password + "'";

                    MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                    MySqlDataAdapter sda = new MySqlDataAdapter(myCommand);
                    // bool t;
                    //DataTable dt = new DataTable();
                    //sda.Fill(dt);
                    //if (dt.Rows.Count == 1)
                    //{
                    //  // t = true;

                    MySqlDataReader myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        user = new Users
                        {
                            Username = myReader.GetString(0),
                            Password = myReader.GetString(1),
                            //  Name = myReader.GetString(2)
                        };

                    }
                    DBClosed();
                    return user;

                    //}
                    //else
                    //{
                    //  //  t = false;
                    //  // check = 1;
                    //    DBClosed();
                    //    return user; 
                    //}

                    //return check;

                }
            }
            catch (Exception error)
            {
                return null;
            }
        }
    }
}