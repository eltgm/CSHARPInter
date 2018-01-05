using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp5.parser;

namespace WindowsFormsApp5
{
    public partial class ExamForm : Form
    {
        Progress userProgress;
        ranswers[] answer;
        exam ex;

        public ExamForm(Progress progress)
        {
            userProgress = progress;
            InitializeComponent();
        }

        private void ExamForm_Load(object sender, EventArgs e)
        {
            ex = (exam) ((ExamForm) sender).Tag;


            string[] substrings = ex.text.Split('\n');

            answer = ex.ranswers;

            if (userProgress.getExamProgress(ex.lesson) > 0)
            {
                List<ExAnswer> list = new List<ExAnswer>();
                list = userProgress.getExamAnswers(ex.lesson);
                if (userProgress.getExamProgress(ex.lesson) > 3)
                {
                    MessageBox.Show("Экзамен сдан. Ваша оценка: " + userProgress.getExamProgress(ex.lesson).ToString());
                }
                else
                {
                    MessageBox.Show("Экзамен не сдан. Количество правильных ответов: " +
                                    userProgress.getExamProgress(ex.lesson).ToString());
                }

                int i = 0;
                int y = 60;
                foreach (string answer in substrings)
                {
                    Label label = new Label();
                    label.Location = new Point(0, y);
                    label.AutoSize = true;
                    label.Text = answer;

                    this.Controls.Add(label);
                    y += 20;

                    TextBox text = new TextBox();
                    text.Name = "ans";
                    text.Text = list[i].ans;
                    text.Location = new Point(0, y);
                    text.Size = new Size(200, 20);
                    this.Controls.Add(text);
                    y += 30;
                    i++;
                }
            }
            else
            {
                int y = 60;
                foreach (string answer in substrings)
                {
                    Label label = new Label();
                    label.Location = new Point(0, y);
                    label.AutoSize = true;
                    label.Text = answer;

                    this.Controls.Add(label);
                    y += 20;

                    TextBox text = new TextBox();
                    text.Name = "ans";
                    text.Size = new Size(200, 20);
                    text.Location = new Point(0, y);
                    this.Controls.Add(text);
                    y += 30;
                }
            }

            Button button = new Button();
            button.Location = new Point(430, 630);
            button.Text = "Show results";
            button.AutoSize = true;


            button.Click += new EventHandler(this.Btn_Click);
            this.Controls.Add(button);

            Button but = new Button();
            but.AutoSize = true;
            but.Text = "Close";
            but.Location = new Point(830, 630);


            but.Click += new EventHandler(this.Home_Click);
            this.Controls.Add(but);
        }

        void Btn_Click(Object sender, EventArgs e)
        {
            Control[] answers = this.Controls.Find("ans", false);
            int i = 0;
            int count = 0;
            List<ExAnswer> exansw = new List<ExAnswer>();


            foreach (Control textBox in answers)
            {
                ExAnswer ex = new ExAnswer();
                string a = ((TextBox) textBox).Text;
                ex.ans = a;
                a = a.Trim();
                a = a.ToLower();


                exansw.Add(ex);

                if (a.Equals(answer[i].ra))
                {
                    count++;
                }

                i++;
            }

            userProgress.setExamProgress(ex.lesson, count, exansw);


            if (count > 3)
            {
                MessageBox.Show("Зачет сдан. Ваша оценка: " + count.ToString());
            }
            else
            {
                MessageBox.Show("Зачет не сдан. Количество правильных ответов: " + count.ToString());
            }
        }

        void Home_Click(Object sender, EventArgs e)
        {
            this.Close();
        }
    }
}