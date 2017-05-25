using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace IPSv1
{
    public partial class UpdateInfo : Form
    {
        MySqlConnection connection = new MySqlConnection("Server=localhost;Database=ipsv1;Uid=root;Pwd=;");
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        MySqlCommandBuilder scb;
        DataTable dt = new DataTable();

        public UpdateInfo()
        {
            InitializeComponent();
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Cannot connect to server. Contact administrator");
                return false;
            }
        }
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //delete the data that show in textbox
            try
            {
                string deletequery = "DELETE * FROM mentors WHERE mentor_id = " + staffid.Text;
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand(deletequery, connection);
                if (cmd.ExecuteNonQuery() == 1)
                    MessageBox.Show("User deleted!");
                else
                    MessageBox.Show("User not delete!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            CloseConnection();
        }

        private void UpdateInfo_Load(object sender, EventArgs e)
        {
            
            //load datagridview with the database
            connection.Open();
            cmd = new MySqlCommand("SELECT * from mentors", connection);
            //MySqlDataAdapter adr = new MySqlDataAdapter(cmd, connection);
            adapter = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt); //auto open/close connection
            dataGridView1.DataSource = dt;

        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            //search mentor name from the search textbox and update display on datagridview
            string select = "SELECT * FROM mentors WHERE CONCAT(`mentor_name`) like '%" + searchbox.Text + "%'";
            cmd = new MySqlCommand(select, connection);
            OpenConnection();
            adapter = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;

            CloseConnection();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // update the mentor name and mentor hometown into database
            string updateQuery = "UPDATE mentors SET mentor_name='" + staffname.Text + "',mentor_id='" + staffid.Text + "',mentor_state='" + state.Text + "', mentor_district='" + district.Text + "'  WHERE mentor_id =" + staffid.Text;
            try
            {
                OpenConnection();
                cmd = new MySqlCommand(updateQuery, connection);
                MySqlDataReader MyReader;

                MyReader = cmd.ExecuteReader();
                MessageBox.Show("Data Updated");
                while (MyReader.Read())
                {
                }
                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Query Executed");
                }

                else
                {
                    MessageBox.Show("Query Not Executed");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            CloseConnection();

            // populate the datagridview
            string selectQuery = "SELECT * FROM mentors";
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(selectQuery, connection);
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

       
        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //load the textbox after click on the row of datagridview
            staffid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            staffname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            state.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            district.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            //companyassignBox.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }
    }
    
}
