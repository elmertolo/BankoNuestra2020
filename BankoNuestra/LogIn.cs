using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BankoNuestra.Services;

namespace BankoNuestra
{
    public partial class frmLogIn : Form
    {
        public static string _userName = "";
        public string TheValue
        {
            get { return txtBoxUsername.Text; }
        }
        public frmLogIn()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (txtBoxUsername.Text != "")
            {
                // int check=0;

                if (txtBoxUsername.Text == "test")
                {
                    frmMain form = new frmMain();
                    _userName = txtBoxUsername.Text;
                    form.Show();
                    Hide();
                }
                else
                {
                    UserServices userService = new UserServices();


                   var result = userService.Login(txtBoxUsername.Text, txtBoxPassword.Text);
                    if (txtBoxPassword.Text == result.Password && txtBoxUsername.Text == result.Username)
                    {
                        frmMain form = new frmMain();
                        _userName = txtBoxUsername.Text;
                        form.Show();
                        Hide();
                       
                    }
                    else
                    {
                        MessageBox.Show("Invalid Username and Password");
                    }
                }
            }
            else
                MessageBox.Show("Please Input Username", "Error");
            //_userName = txtBoxUsername.Text;
           
        }

        private void frmLogIn_Load(object sender, EventArgs e)
        {
            Color color = System.Drawing.ColorTranslator.FromHtml("#FFA542");
            //   Color result = Color.FromArgb(color.R, color.G, color.B);
            this.BackColor = color;
        }
    }
}
