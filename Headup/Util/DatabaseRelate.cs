using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Headup.Util
{
    class DatabaseRelate
    {
        public DatabaseRelate(string dbPath)
        {
            DbPath = dbPath;
            Conn = new SQLiteConnection("Data Source=" + DbPath + ";journal mode=Off;PRAGMA synchronous = OFF;PRAGMA cache_size = 800000");
            Conn.Open(); //open을 안하면 파일이 생성이 되지 않는다.
        }
        
        public bool CreateTable(string tableName, Dictionary<string, string> colNameAndType)
        {
            try
            {
                SQLiteCommand cmd = new SQLiteCommand(Conn);
                string query = "Create table " + tableName + "(";
                foreach (KeyValuePair<string, string> tmp in colNameAndType)
                {
                    query = query + tmp.Key + " " + tmp.Value + ", ";
                }
                query = query.Substring(0, query.Length - 2); //문자열 끝에 ", " 삭제를 한다.
                query = query + ")";

                cmd.CommandText = query;
                cmd.ExecuteNonQuery();

                cmd.Dispose();
            }
            catch (Exception ex)
            {

            }
            return true;
        }
        public void dropTable(string tableName)
        {
            SQLiteCommand cmd = new SQLiteCommand(Conn);
            cmd.CommandText = "DROP TABLE IF EXISTS " + tableName;
            cmd.ExecuteNonQuery();
        }
        //public DataSet GetAllDataFromDb()
        //{
        //    DataSet ds = new DataSet();
        //    return ds;
        //}
        public DataSet GetDataSetFromDb(string tableName, string[] colList)
        {
            DataSet ds = new DataSet();

            string query = "SELECT ";
            foreach (string st in colList)
            {
                query = query + st + ", ";
            }
            query = query.Substring(0, query.Length - 2); //문자열 끝에 ", " 삭제를 한다.
            query = query + " from " + tableName;

            //SQLiteDataAdapter 클래스를 이용 비연결 모드로 데이타 읽기
            try
            {
                var adpt = new SQLiteDataAdapter(query, Conn);
                adpt.Fill(ds, tableName);
                adpt.Dispose();
            }
            catch (Exception ex) //대부분 marformed 에러가 여기에서 걸린다.
            {
                return null; //아무것도 들어가있지 않은 객체 반환
            }

            return ds;
        }
        public DataTable GetDataTableFromDb(string tableName, string[] colList)
        {
            DataTable dt = new DataTable();

            string query = "SELECT ";
            foreach (string st in colList)
            {
                query = query + st + ", ";
            }
            query = query.Substring(0, query.Length - 2); //문자열 끝에 ", " 삭제를 한다.
            query = query + " from " + tableName;

            //SQLiteDataAdapter 클래스를 이용 비연결 모드로 데이타 읽기
            try
            {
                var adpt = new SQLiteDataAdapter(query, Conn);
                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex) //대부분 marformed 에러가 여기에서 걸린다.
            {
                return null; //아무것도 들어가있지 않은 객체 반환
            }
            dt.TableName = tableName;
            return dt;
        }
        public List<string> GetTableList()
        {
            List<string> result = new List<string>();
            string sql = "SELECT * FROM sqlite_master WHERE type='table'";

            //SQLiteDataReader를 이용하여 연결 모드로 데이타 읽기
            SQLiteCommand cmd = new SQLiteCommand(sql, Conn);
            SQLiteDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                result.Add(rdr["name"].ToString());
            }
            rdr.Close();
            return result;
        }
        //public DataSet SendQuery(string query)
        //{
        //    DataSet ds = new DataSet();
        //    var adpt = new SQLiteDataAdapter(query, Conn);
        //    adpt.Fill(ds, "result");
        //    adpt.Dispose();
        //    return ds;
        //}
        public void InsertQuery(string query)
        {
            SQLiteCommand cmd = new SQLiteCommand(query, Conn);
            cmd.ExecuteNonQuery();
        }
        public void InsertAllDataSetToDb(DataSet ds, string tableName, List<string> colList)
        {
            using (var tr = Conn.BeginTransaction()) //속도를 위한 코드
            {
                var command = Conn.CreateCommand();
                command.Transaction = tr;
                string query;
                query = "INSERT INTO " + tableName + " (";
                foreach (string tmp in colList)
                {
                    query = query + tmp + ", ";
                }
                query = query.Substring(0, query.Length - 2); //문자열 끝에 ", " 삭제를 한다.
                query = query + ") VALUES (";
                foreach (string tmp in colList)
                {
                    query = query + "@" + tmp + ", ";
                }
                query = query.Substring(0, query.Length - 2); //문자열 끝에 ", " 삭제를 한다.
                query = query + ")";

                command.CommandText = query;

                foreach (DataRow myRow in ds.Tables[tableName].Rows)
                {
                    foreach (DataColumn myColumn in ds.Tables[tableName].Columns)
                    {
                        command.Parameters.Add(new SQLiteParameter("@" + myColumn, myRow[myColumn]));
                    }
                    command.ExecuteNonQuery();
                }
                tr.Commit();
            }
        }

        public void SqlClose()
        {
            Conn.Close();
        }

        private SQLiteConnection conn;
        private string dbPath;

        public string DbPath
        {
            get { return dbPath; }
            set { dbPath = value; }
        }
        public SQLiteConnection Conn
        {
            get { return conn; }
            set { conn = value; }
        }
    }
}
