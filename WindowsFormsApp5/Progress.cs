using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp5
{
    public class Progress
    {
        public Progress()
        {
            lections = new List<Lection>();
            exams = new List<Exam>();
        }

        public List<Lection> lections { get; set; }
        public List<Exam> exams { get; set; }

        public void setLectionProgress(int id)
        {
            lock (lections)
            {
                foreach (Lection lec in lections.ToArray())
                {
                    if (lec.id == id)
                    {
                        lections.Remove(lec);
                    }
                }
            }

            Lection lection = new Lection();
            lection.id = id;
            lection.status = 1;
            lection.answers = null;

            lections.Add(lection);
        }

        public void setSeminarProgress(int id, int status, List<Answer> answers)
        {
            lock (lections)
            {
                foreach (Lection lec in lections.ToArray())
                {
                    if (lec.id == id)
                    {
                        lections.Remove(lec);
                    }
                }

                Lection seminar = new Lection();
                seminar.id = id;
                seminar.answers = answers;
                seminar.status = status;

                lections.Add(seminar);
            }
        }

        public void setExamProgress(int id, int mark, List<ExAnswer> answers)
        {
            foreach (Exam ex in exams.ToArray())
            {
                if (ex.id == id)
                {
                    exams.Remove(ex);
                }
            }

            Exam exam = new Exam();
            exam.id = id;
            exam.mark = mark;
            exam.answers = answers;

            exams.Add(exam);
        }

        public List<Answer> getSeminarAnswers(int id)
        {
            foreach (Lection lec in lections)
            {
                if (lec.id == id)
                {
                    return lec.answers;
                }
            }

            return null;
        }

        public int getSeminarProgress(int id)
        {
            foreach (Lection lec in lections)
            {
                if (lec.id == id)
                {
                    if (lec.status == 1)

                        return 2;
                    else
                        return 1;
                }
            }

            return 0;
        }

        public List<ExAnswer> getExamAnswers(int id)
        {
            foreach (Exam ex in exams)
            {
                if (ex.id == id)
                {
                    return ex.answers;
                }
            }

            return null;
        }

        public int getExamProgress(int id)
        {
            foreach (Exam ex in exams)
            {
                if (ex.id == id)
                {
                    return ex.mark;
                }
            }

            return 0;
        }

        public bool getLectionProgress(int id)
        {
            foreach (Lection lec in lections)
            {
                if (lec.id == id)
                {
                    return true;
                }
            }

            return false;
        }
    }


    public class Answer
    {
        public int num { get; set; }
    }

    public class Lection
    {
        public int id { get; set; }
        public int status { get; set; }
        public List<Answer> answers { get; set; }
    }

    public class Exam
    {
        public int id { get; set; }
        public int mark { get; set; }
        public List<ExAnswer> answers { get; set; }
    }

    public class ExAnswer
    {
        public string ans { get; set; }
    }
}