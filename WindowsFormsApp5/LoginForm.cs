using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace WindowsFormsApp5
{
    public partial class LoginForm : Form
    {
        DBHelper dBHelper = new DBHelper();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hashtable par = new Hashtable();
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                par.Add("fio", textBox1.Text);
                par.Add("login", textBox2.Text);


                if (!dBHelper.insertInto(DBNames.USERTABLE, par))
                {
                    regStatus regStatus = new regStatus("User has been already created!");
                    regStatus.Show();
                }
                else
                {
                    regStatus regStatus = new regStatus("User has been successfully created !");
                    regStatus.Show();
                }
            }
            else
            {
                MessageBox.Show("Enter user data!");
            }
        }

        private void login_Click(object sender, EventArgs e)
        {
            Hashtable par = new Hashtable();
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                par.Add("fio", textBox1.Text);
                par.Add("login", textBox2.Text);


                if (dBHelper.findUser(par))
                {
                    LessonsForm f2 = new LessonsForm();
                    f2.Tag = textBox1.Text;
                    this.Hide();
                    f2.Show();
                }
                else
                {
                    MessageBox.Show("Cannot identify this user!");
                }
            }
            else
            {
                MessageBox.Show("Enter user data!");
            }
        }
    }
}