using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Employee_Form
{
    class DataBase
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        public DataBase()
        {
            con = new SqlConnection("Data Source=DESKTOP-OC4GVNP;Initial Catalog=MotorSpaLK;Integrated Security=True");
        }

        public int Save_Update_Delete(string q)
        {
            con.Open();
            cmd = new SqlCommand(q, con);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }

        public DataTable Search(string q)
        {
            con.Open();
            da = new SqlDataAdapter(q, con);
            DataTable dt = new DataTable();
            da.Fill(dt);            
            con.Close();            
            return dt;
        }
        public void AutoIDNumber(string q)
        {
            con.Open();
            cmd = new SqlCommand(q, con);

                   
            con.Close();
            
        }

    }
}
