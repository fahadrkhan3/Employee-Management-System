using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ofc_MgtSys
{
    public partial class Form1 : Form
    {
        DbConn obj = new DbConn();
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-7E4FKNS\SQLEXPRESS;Initial Catalog=mngsys;");
        private object dt;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtempID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            string empid = txtempID.Text;
            string name = txtname.Text;
            string cell = txtCN.Text;
            string address = txtadrs.Text;

            string query = "INSERT INTO Employee VALUES('" + empid + "','" + name + "','" + cell + "','" + address + "')";
            
            try
            {
               // DbConn obj = new DbConn();
                bool b = obj.UDI(query);
                if (b == true)
                {
                    MessageBox.Show("Inserted");

                }
                else
                {
                    MessageBox.Show("Not Inserted");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Exception:" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string empid = txtempID.Text;

            string query = "Delete from Employee where EmpID ='"+ empid + "'";

            try
            {
           //     DbConn obj = new DbConn();
                bool b = obj.UDI(query);
                MessageBox.Show("Record deleted Successfully");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Exception:" + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string empid = txtempID.Text;
            string name = txtname.Text;
            string cell = txtCN.Text;
            string address = txtadrs.Text;

            string query = "UPDATE Employee SET name='" + name + "', cell='" + cell + "', address='" + address + "' WHERE empid='" + empid + "'";


            try
            {
                /* conn.Open();
                 SqlCommand cmd = new SqlCommand(query, conn);
                 cmd.ExecuteNonQuery(); inki jagah lga hy UDI 3 lines sy 1 line ka code*/
                bool b = obj.UDI(query);
                MessageBox.Show("Record Updated Successfully");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Exception:" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            string empid = txtempID.Text;
            string query = "Select * From Employee where EmpID = '"+empid+"'";
            try
            {
                DataTable dt = obj.search(query);
                if (dt!=null)
                {
                    txtname.Text = dt.Rows[0]["name"].ToString();
                    txtCN.Text = dt.Rows[0]["cell"].ToString();
                    txtadrs.Text = dt.Rows[0]["address"].ToString();
                }
                else
                {
                    MessageBox.Show("No Records Found");
                }

            }
            catch(SqlException ex)
            {
                MessageBox.Show("Exception:" + ex.Message);
            }
            finally
            {
                conn.Close();

            }
           

        }
        
         private void LoadDataIntoDataGridView()
         {
             try
             {
                 // Use a using statement for SqlConnection to ensure proper disposal
                 using (SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-7E4FKNS\SQLEXPRESS;Initial Catalog=mngsys;Integrated Security=True;"))
                 {
                     conn.Open();

                     // Use a using statement for SqlDataAdapter to ensure proper disposal
                     using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Employee", conn))
                     {
                         DataTable dt = new DataTable();
                         sda.Fill(dt);

                         if (dt.Rows.Count > 0)
                         {
                             dataGridView1.DataSource = dt;
                         }
                         else
                         {
                             MessageBox.Show("No data found.");
                         }
                     }
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show("Error: Something is wrong " + ex.Message);
             }
         }
      
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        } 

        private void button1_Click_2(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();
        }
    }
}
