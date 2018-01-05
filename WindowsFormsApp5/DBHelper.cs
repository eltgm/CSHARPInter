using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Collections;
using Newtonsoft.Json;

namespace WindowsFormsApp5
{
    class DBHelper
    {
        SQLiteConnection dbConnection;

        public void createDB()
        {
            SQLiteConnection.CreateFile(DBNames.DBNAME);
            dbConnection = new SQLiteConnection("Data Source=" + DBNames.DBNAME + ";Version=3;");
            dbConnection.Open();

            string usersTable = String.Format("CREATE TABLE {0} (_id INTEGER PRIMARY KEY AUTOINCREMENT," +
                                              "fio TEXT," +
                                              "login TEXT," +
                                              "progress TEXT)", DBNames.USERTABLE);

            SQLiteCommand command = new SQLiteCommand(usersTable, dbConnection);

            command.ExecuteNonQuery();

            string lectionTable = String.Format("CREATE TABLE {0} (_id INTEGER PRIMARY KEY AUTOINCREMENT," +
                                                "name TEXT," +
                                                "text TEXT," +
                                                "lesson INTEGER)", DBNames.LECTIONTABLE);

            SQLiteCommand command1 = new SQLiteCommand(lectionTable, dbConnection);

            command1.ExecuteNonQuery();

            string SeminarTable = String.Format("CREATE TABLE {0} (_id INTEGER PRIMARY KEY AUTOINCREMENT," +
                                                "name TEXT," +
                                                "text TEXT," +
                                                "lesson TEXT" +
                                                "answer TEXT)", DBNames.SEMINARTABLE);

            SQLiteCommand command2 = new SQLiteCommand(SeminarTable, dbConnection);

            command2.ExecuteNonQuery();

            string ExamTable = String.Format("CREATE TABLE {0} (_id INTEGER PRIMARY KEY AUTOINCREMENT," +
                                             "name TEXT," +
                                             "lesson TEXT" +
                                             "test TEXT" +
                                             "answer TEXT)", DBNames.EXAMTABLE);

            SQLiteCommand command3 = new SQLiteCommand(ExamTable, dbConnection);

            command3.ExecuteNonQuery();

            string LessonTable = String.Format("CREATE TABLE {0} (_id INTEGER PRIMARY KEY AUTOINCREMENT," +
                                               "name TEXT)", DBNames.LESSIONTABLE);

            SQLiteCommand command4 = new SQLiteCommand(LessonTable, dbConnection);

            command4.ExecuteNonQuery();

            string ImgsTable = String.Format("CREATE TABLE {0} (_id INTEGER PRIMARY KEY AUTOINCREMENT," +
                                             "lesson INTEGER," +
                                             "lec INTEGER, " +
                                             "filepath TEXT)", DBNames.IMGSTABLE);

            SQLiteCommand command5 = new SQLiteCommand(ImgsTable, dbConnection);

            command5.ExecuteNonQuery();

            dbConnection.Close();
        }


        public bool insertInto(string tableName, Hashtable par)
        {
            switch (tableName)
            {
                case DBNames.USERTABLE:
                    dbConnection = new SQLiteConnection("Data Source=" + DBNames.DBNAME + ";Version=3;");
                    dbConnection.Open();

                    SQLiteCommand search =
                        new SQLiteCommand(
                            String.Format("SELECT * from '{0}' WHERE fio='{1}' AND login='{2}'", DBNames.USERTABLE,
                                par["fio"], par["login"]), dbConnection);
                    SQLiteDataReader reader = search.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        string sql = String.Format("insert into {0} (fio, login) values ('{1}', '{2}')", tableName,
                            par["fio"], par["login"]);

                        SQLiteCommand command = new SQLiteCommand(sql, dbConnection);

                        if (command.ExecuteNonQuery() > 0)
                        {
                            reader.Close();
                            dbConnection.Close();
                            return true;
                        }
                    }
                    else
                    {
                        reader.Close();
                        dbConnection.Close();
                        return false;
                    }


                    break;
            }

            return false;
        }


        public void initLec(Hashtable par)
        {
            dbConnection = new SQLiteConnection("Data Source" + DBNames.DBNAME + ";Version=3;");
            dbConnection.Open();

            string sqllec = String.Format("insert into {0} (name, text, lesson)  values ('{1}', '{2}', '{3}')",
                DBNames.LECTIONTABLE, par["name"], par["text"], par["lesson"]);
            SQLiteCommand command1 = new SQLiteCommand(sqllec, dbConnection);
            command1.ExecuteNonQuery();
            dbConnection.Close();
        }

        public bool findUser(Hashtable par)
        {
            dbConnection = new SQLiteConnection("Data Source=" + DBNames.DBNAME + ";Version=3;");
            dbConnection.Open();

            SQLiteCommand search =
                new SQLiteCommand(
                    String.Format("SELECT * from '{0}' WHERE fio='{1}' AND login='{2}'", DBNames.USERTABLE, par["fio"],
                        par["login"]), dbConnection);
            SQLiteDataReader reader = search.ExecuteReader();

            if (reader.HasRows)
            {
                dbConnection.Close();
                return true;
            }
            else
            {
                dbConnection.Close();
                return false;
            }
        }

        public Progress getUserProgress(string login)
        {
            string prog = null;
            List<string> ImportedFiles = new List<string>();
            Progress userProgress = new Progress();

            dbConnection = new SQLiteConnection("Data Source=" + DBNames.DBNAME + ";Version=3;");
            dbConnection.Open();

            SQLiteCommand search =
                new SQLiteCommand(String.Format("SELECT progress from '{0}' WHERE fio='{1}'", DBNames.USERTABLE, login),
                    dbConnection);
            SQLiteDataReader reader = search.ExecuteReader();

            while (reader.Read())
            {
                prog = Convert.ToString(reader["progress"]);
            }

            dbConnection.Close();

            if (!prog.Equals(""))
                return userProgress = JsonConvert.DeserializeObject<Progress>(prog);


            return new Progress();
        }

        public void setUserProgress(string usProg, string username)
        {
            dbConnection = new SQLiteConnection("Data Source=" + DBNames.DBNAME + ";Version=3;");
            dbConnection.Open();

            SQLiteCommand insert =
                new SQLiteCommand(
                    String.Format("update '{0}' set progress='{1}' where fio='{2}'", DBNames.USERTABLE, usProg,
                        username), dbConnection);
            insert.ExecuteNonQuery();

            dbConnection.Close();
        }
    }
}