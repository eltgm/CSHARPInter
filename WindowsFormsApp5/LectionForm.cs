using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp5.parser;

namespace WindowsFormsApp5
{
    public partial class LectionForm : Form
    {
        Progress userProgress;

        public LectionForm(Progress prog)
        {
            userProgress = prog;
            InitializeComponent();
        }

        private void LectionForm_Load(object sender, EventArgs e)
        {
            lection lec = (lection) ((LectionForm) sender).Tag;
            if (userProgress.lections.Count > 0)
            {
                if (userProgress.getLectionProgress(lec.id))
                {
                    MessageBox.Show("Лекция прочитана");
                }
                else
                {
                    MessageBox.Show("Лекция не прочитана");
                    userProgress.setLectionProgress(lec.id);
                }
            }
            else
            {
                MessageBox.Show("Лекция не прочитана");
                userProgress.setLectionProgress(lec.id);
            }


            this.mainText.Width = 900;
            this.title.Text = lec.name;
            this.mainText.Text = lec.text;
            int i = 1;
            if (lec.photos != null)
                foreach (photo img in lec.photos)
                {
                    PictureBox pictureBox = new PictureBox();

                    pictureBox.ImageLocation = @img.filepath;

                    pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                    pictureBox.Dock = DockStyle.Bottom;

                    Label label = new Label();
                    label.Dock = DockStyle.Bottom;
                    label.Font = new Font(FontFamily.GenericMonospace, 16, FontStyle.Bold);


                    label.Text = "Рисунок " + i;
                    i++;


                    this.Controls.Add(label);
                    this.Controls.Add(pictureBox);
                }
        }
    }
}