using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoSeleniumFromExcel
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtUserName.Text.Trim()=="admin" && txtPassword.Text.Trim()== "w642424a")
            {
                this.Visible = false;
                new Form1().Show();
            }
            else
            {
                MessageBox.Show("User or Password Invalid !");
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
        }
    }
}
