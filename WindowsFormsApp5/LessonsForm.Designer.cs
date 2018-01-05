using Newtonsoft.Json;
using System;
using System.IO;
using WindowsFormsApp5.parser;
using System.Windows.Forms;
using System.Text;

namespace WindowsFormsApp5
{
    partial class LessonsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Button exit;
            this.listView1 = new System.Windows.Forms.ListView();
            exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // exit
            // 
            exit.Location = new System.Drawing.Point(136, 526);
            exit.Name = "exit";
            exit.Size = new System.Drawing.Size(75, 23);
            exit.TabIndex = 1;
            exit.Text = "Exit";
            exit.UseVisualStyleBackColor = true;
            exit.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(353, 2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(328, 556);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // LessonsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 561);
            this.ControlBox = false;
            this.Controls.Add(exit);
            this.Controls.Add(this.listView1);
            this.Name = "LessonsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "LessonsForm";
            this.Load += new System.EventHandler(this.LessonsForm_Load);
            this.ResumeLayout(false);

        }
        void check_ClickedEventHandler(object sender, EventArgs e)
        {
            this.listView1.Controls.Clear();

            int j = 0;
            if (((lesson)((LinkLabel)sender).Tag).lections != null)
            {
                foreach (lection lec in ((lesson)((LinkLabel)sender).Tag).lections) {
                LinkLabel linkLabel = new LinkLabel();
                
                linkLabel.Text = lec.name;
                linkLabel.Location = new System.Drawing.Point(0, j );
                linkLabel.AutoSize = true;

                linkLabel.Tag = lec;

                linkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(lectionPressed);
                
                this.listView1.Controls.Add(linkLabel);
                j+=15;
            }
            }
            else
            {
                MessageBox.Show("Лекций или семинаров ещё нет!");
            }

            if (((lesson)((LinkLabel)sender).Tag).exam != null)
            {
                foreach (exam ex in ((lesson)((LinkLabel)sender).Tag).exam)
                {
                    LinkLabel linkLabel = new LinkLabel();

                    linkLabel.Text = ex.name;
                    linkLabel.Location = new System.Drawing.Point(0, j);
                    linkLabel.AutoSize = true;

                    linkLabel.Tag = ex;

                    linkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(lectionPressed);

                    this.listView1.Controls.Add(linkLabel);
                    j += 15;


                }
            }
            else
            {
                MessageBox.Show("Экзаменов ещё нет!");
            }
        }
        #endregion
        void lectionPressed(object sender, EventArgs e) {

            if (((LinkLabel)sender).Tag.GetType().Name.Equals("lection"))
                switch (((lection)((LinkLabel)sender).Tag).type)
                {
                    case "lec":
                        LectionForm f2 = new LectionForm(userProgress);
                        f2.Tag = (lection)((LinkLabel)sender).Tag;

                        f2.Show();
                        break;
                    case "sem":
                        SeminarForm f1 = new SeminarForm(userProgress);
                        f1.Tag = (lection)((LinkLabel)sender).Tag;
                        f1.Show();
                        break;
                }
            else
            {
                if (((exam)((LinkLabel)sender).Tag).lesson == 1)
                {
                    ExamForm f3 = new ExamForm(userProgress);
                    f3.Tag = (exam)((LinkLabel)sender).Tag;
                    f3.Show();
                }
                else
                {

                    if (userProgress.getExamProgress(((exam)((LinkLabel)sender).Tag).lesson - 1) > 3 && ((exam)((LinkLabel)sender).Tag).lesson > 1)
                    {
                        ExamForm f3 = new ExamForm(userProgress);
                        f3.Tag = (exam)((LinkLabel)sender).Tag;
                        f3.Show();
                    }
                    else
                    {
                        MessageBox.Show("Вы не сдали предыдущий экзамен!");
                    }
                }

            }
                

        }

        



        private ListView listView1;
    }
}