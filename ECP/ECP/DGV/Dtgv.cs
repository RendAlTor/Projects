using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ECP.DGV
{
    public class Dtgv
    {
        private SqlConnection conn = new SqlConnection("Data Source=А-Optimus;Initial Catalog=активы;Integrated Security=True");

        public void OpenConnection()
        {
            try
            {
                conn.Open();
            }
            catch (Exception) {}
        }
        public void CloseConnections()
        {
            try
            {
                conn.Close();
            }
            catch (Exception) { }
        }
        public string SelectFile()
        {
            try
            {
                Crypto qw = new Crypto();
                string tmpp = "";
                SqlCommand cmd = new SqlCommand("SelectFile", this.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    String tmp = reader[0].ToString();
                    tmp += " ";
                    tmp += qw.Decrypt(reader[1].ToString());
                    tmp += " ";
                    tmp += qw.Decrypt(reader[2].ToString());
                    tmpp = tmp;
                }
                return tmpp;
            }
            catch (Exception)
            {
                throw new System.ArgumentException("Database query failed");
            }
        }
        /*public void InsertClient(string name, string address)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].InsertClient", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Name_client", name);
                cmd.Parameters.AddWithValue("Address_client", address);
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { }

        }*/
        public DataTable SelectClient1()
        {
            try
            {
                Crypto qw = new Crypto();
                string tmpp = "";

                SqlCommand cmd = new SqlCommand("SelectClient", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet Client = new DataSet();
                adapter.Fill(Client, "SelectClient");
                DataTable dt = Client.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    String tmp1 = qw.Decrypt(dr.ItemArray[1].ToString());
                    dr["Name_client"] = tmp1;
                    tmpp = dr.ItemArray[2].ToString();
                    dr["Address_client"] = qw.Decrypt(tmpp);
                }
                return dt;
            }
            catch (Exception) { }
            return null;
        }
    }
}