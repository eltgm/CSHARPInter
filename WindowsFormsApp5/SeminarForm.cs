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
    public partial class SeminarForm : Form
    {
        int[] ans;
        Progress userProgress;
        lection lec;

        public SeminarForm(Progress prog)
        {
            userProgress = prog;
            InitializeComponent();
        }

        private void SeminarForm_Load(object sender, EventArgs e)
        {
            lec = (lection) ((SeminarForm) sender).Tag;

            ans[] answer = lec.ans;
            int[] rightAns = new int[5];
            int o = 0;
            foreach (ans a in answer)
            {
                rightAns[o] = a.num;
                o++;
            }

            //answer a = lec.answers;
            ans = rightAns;


            String[] substrings = lec.text.Split('\n');
            HashSet<String> options = new HashSet<string>();

            int i = 0;
            int t = 1;
            int q = 0;
            foreach (String ans in substrings)
            {
                opt opt = lec.opt[q];
                String[] subOpt = opt.text.Split(',');

                Label label = new Label();
                label.Text = ans;
                label.Location = new Point(0, i);
                label.AutoSize = true;
                this.Controls.Add(label);
                int j = i + 10;

                GroupBox group = new GroupBox();
                group.Location = new Point(0, i + 10);
                group.Size = new Size(150, 70);
                group.Name = "opt";
                int start = 0;
                foreach (String optO in subOpt)
                {
                    RadioButton radio = new RadioButton();
                    radio.Text = optO;
                    radio.Location = new Point(0, start);
                    radio.Name = "radio";
                    group.Controls.Add(radio);

                    this.Controls.Add(group);
                    start += 20;
                }

                t++;
                i += 100;
                q++;
            }

            Button button = new Button();
            button.Text = "Finish test";
            button.Name = "finTest";
            button.AutoSize = true;
            button.Tag = rightAns;

            button.Location = new Point(250, i + 10);
            button.Click += new EventHandler(this.Btn_Click);
            this.Controls.Add(button);

            if (userProgress.getSeminarProgress(lec.id) == 1 || userProgress.getSeminarProgress(lec.id) == 2)
            {
                Control[] b = this.Controls.Find("finTest", false);
                b[0].Visible = false;
                List<Answer> list = userProgress.getSeminarAnswers(lec.id);
                bool[] wrong = getWrong(list);


                int[] right = ans;
                int count = 0;

                int[] choosed = new int[list.Count];
                int p = 0;
                foreach (Answer an in list)
                {
                    choosed[p] = an.num;
                    p++;
                }

                for (int a = 0; a < 5; a++)
                {
                    if (right[a] == choosed[a])
                    {
                        wrong[a] = true;
                        count++;
                    }
                }

                isDone(count);

                Control[] groupBox = this.Controls.Find("opt", false);
                int countList = 0;
                foreach (Control con in groupBox)
                {
                    int qwe = 0;
                    Control[] radio = con.Controls.Find("radio", false);
                    foreach (Control rad in radio)
                    {
                        if (qwe + 1 == choosed[countList])
                        {
                            ((RadioButton) rad).Checked = true;

                            break;
                        }
                        else
                        {
                            qwe++;
                            continue;
                        }
                    }

                    countList++;
                }

                setOptColor(list, wrong);
            }
        }

        void Btn_Click(Object sender, EventArgs e)
        {
            Control[] b = this.Controls.Find("finTest", false);
            b[0].Visible = false;
            Control[] groupbox = this.Controls.Find("opt", false);

            foreach (Control con in groupbox)
            {
                Control[] radio = con.Controls.Find("radio", false);
                foreach (Control rad in radio)
                {
                    ((RadioButton) rad).BackColor = Color.FromArgb(255, 240, 240, 240);
                }
            }

            int[] choosedMas = getRadButStatus();

            List<Answer> list = new List<Answer>();
            for (int p = 0; p < choosedMas.Length; p++)
            {
                Answer ans = new Answer();
                ans.num = choosedMas[p];
                list.Add(ans);
            }


            bool[] wrong = getWrong(list);
            setOptColor(list, wrong);

            int[] rightAns = ans;
            int count = 0;
            for (int a = 0; a < 5; a++)
            {
                if (rightAns[a] == choosedMas[a])
                {
                    wrong[a] = true;
                    count++;
                }
            }

            isDone(count);
            if (count < 3)
                userProgress.setSeminarProgress(lec.id, 0, list);
            else
                userProgress.setSeminarProgress(lec.id, 1, list);
        }

        private bool[] getWrong(List<Answer> list)
        {
            int[] choosedMas = new int[list.Count];
            int dick = 0;
            foreach (Answer a in list)
            {
                choosedMas[dick] = a.num;
                dick++;
            }

            int[] rightAns = ans;
            bool isEqual = Enumerable.SequenceEqual(rightAns, choosedMas);

            bool[] wrong = new bool[5];
            for (int z = 0; z < 5; z++)
                wrong[z] = false;

            int count = 0;
            for (int a = 0; a < 5; a++)
            {
                if (rightAns[a] == choosedMas[a])
                {
                    wrong[a] = true;
                    count++;
                }
            }

            return wrong;
        }

        private void setOptColor(List<Answer> list, bool[] wrong)
        {
            int[] choosedMas = new int[list.Count];
            int dick = 0;
            foreach (Answer a in list)
            {
                choosedMas[dick] = a.num;
                dick++;
            }

            Control[] groupBox = this.Controls.Find("opt", false);
            int i = 0;
            int m = 0;
            foreach (Control con in groupBox)
            {
                if (!wrong[m])
                {
                    Control[] radio = con.Controls.Find("radio", false);
                    for (int p = 0; p < 3; p++)
                    {
                        if (p + 1 == choosedMas[m])
                            ((RadioButton) radio[p]).BackColor = Color.Red;
                    }

                    m++;
                }
                else
                {
                    m++;
                    continue;
                }
            }
        }

        private int[] getRadButStatus()
        {
            bool[] status = new bool[15];

            SeminarForm sf = this;
            Control[] groupBox = this.Controls.Find("opt", false);
            int i = 0;
            foreach (Control con in groupBox)
            {
                Control[] radio = con.Controls.Find("radio", false);
                foreach (Control rad in radio)
                {
                    status[i] = ((RadioButton) rad).Checked;
                    i++;
                }
            }

            int k = 0;

            int[] intStat = new int[5];
            for (int q = 0; q < 5; q++)
                intStat[q] = 0;

            for (int p = 0; p < status.Length; p += 3)
            {
                int l = 0;
                for (int j = p; j < p + 3; j++)
                {
                    if (status[j] == true)
                    {
                        intStat[k] = l + 1;
                        //k++;
                    }

                    l++;
                }

                k++;
            }

            return intStat;
        }

        void Home_Click(Object sender, EventArgs e)
        {
            this.Close();
        }

        public void isDone(int count)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox.Dock = DockStyle.Right;

            Button home = new Button();
            home.Location = new Point(250, 520);
            home.Text = "Close";

            home.Click += new EventHandler(this.Home_Click);
            if (count >= 3)

            {
                MessageBox.Show("Зачет сдан!");
                pictureBox.ImageLocation = @"files\images\Zachet.jpg";
            }
            else
            {
                MessageBox.Show("Зачет не сдан!");
                pictureBox.ImageLocation = @"files\images\NeZachet2.jpg";

                /*
                pictureBox.ImageLocation = @"files\images\NeZachet.jpg";
                this.Width += 370;
                */
            }


            this.Controls.Add(pictureBox);
            this.Controls.Add(home);
        }
    }
}