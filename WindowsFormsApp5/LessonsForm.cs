using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp5.parser;

namespace WindowsFormsApp5
{
    public partial class LessonsForm : Form
    {
        Progress userProgress;
        DBHelper dBHelper = new DBHelper();
        string username;

        public LessonsForm()
        {
            InitializeComponent();
        }

        public class CustomEventArgs : EventArgs
        {
            public CustomEventArgs(exam[] exam)
            {
                this.exam = exam;
            }

            private exam[] exam { get; set; }
        }

        private void LessonsForm_Load(object sender, EventArgs e)
        {
            string s = "";
            string buff = "";
            string path = @"files\lessons.txt";
            using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding(1251)))
            {
                while ((s = sr.ReadLine()) != null)
                {
                    buff += s;
                }
            }

            Console.WriteLine(buff);

            Unit unit = JsonConvert.DeserializeObject<Unit>(buff);
            username = (string) ((LessonsForm) sender).Tag;

            userProgress = dBHelper.getUserProgress(username);

            int i = 0;
            int j = 0;

            foreach (lesson les in unit.lessons)
            {
                LinkLabel lesLabel = new LinkLabel();

                lesLabel.Text = les.name;

                lesLabel.Location = new System.Drawing.Point(0, i);
                lesLabel.AutoSize = true;

                lesLabel.Tag = les;
                lesLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(check_ClickedEventHandler);

                i += 20;


                this.Controls.Add(lesLabel);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string serProg = JsonConvert.SerializeObject(userProgress);
            dBHelper.setUserProgress(serProg, username);
            System.Environment.Exit(0);
        }
    }
}