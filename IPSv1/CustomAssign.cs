using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace IPSv1
{
    public partial class CustomAssign : Form
    {
        public string companyname = null;
        public string companyassign = null;
        public int increment = 1, increment2 = 1, loop = 1;
        public bool index;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            increment2 = comboBox1.SelectedIndex + 1;
            increment = comboBox2.SelectedIndex + 1;
            using (MySqlConnection connection = new MySqlConnection("server=localhost;Database=ipsv1;Uid=root;Pwd=;"))
            {
                using (MySqlCommand cmdSelect = new MySqlCommand("SELECT Number_of_students FROM companys WHERE legend_no=@legend_no", connection))
                {
                    connection.Open();
                    cmdSelect.Parameters.AddWithValue("@legend_no", increment);
                    MySqlDataReader readerSelect = cmdSelect.ExecuteReader();
                    while (readerSelect.Read())
                    {
                        studentnum.Text = (string)readerSelect.GetString(0);
                    }
                    connection.Close();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            increment2 = comboBox1.SelectedIndex + 1;
            increment = comboBox2.SelectedIndex + 1;


            using (MySqlConnection connection = new MySqlConnection("server=localhost;Database=ipsv1;Uid=root;Pwd=;"))
            {
                using (MySqlCommand cmdCount = new MySqlCommand("SELECT COUNT(*) FROM mentors", connection))
                {

                    connection.Open();
                    count = (long)cmdCount.ExecuteScalar();
                    connection.Close();

                }
                using (MySqlCommand cmdSelect = new MySqlCommand("SELECT company_name FROM companys WHERE legend_no=@legend_no", connection))
                {
                    connection.Open();
                    cmdSelect.Parameters.AddWithValue("@legend_no", increment);
                    MySqlDataReader readerSelect = cmdSelect.ExecuteReader();
                    while (readerSelect.Read())
                    {
                        companyname = (string)readerSelect.GetString(0);
                    }
                    connection.Close();
                }
                do
                {
                    using (MySqlCommand cmdSelect = new MySqlCommand("SELECT company_assign FROM mentors WHERE mentor_id=@mentor_id", connection))
                    {
                        connection.Open();
                        cmdSelect.Parameters.AddWithValue("@mentor_id", loop);
                        MySqlDataReader readerSelect = cmdSelect.ExecuteReader();
                        while (readerSelect.Read())
                        {
                            companyassign = (string)readerSelect.GetString(0);
                        }
                        connection.Close();
                    }
                    index = companyassign.Contains(companyname);
                    if (index == false)
                    {
                        loop++;

                    }
                    if (loop == increment2)
                        loop++;

                } while (loop <= count && index == false);
                if (index == true)
                {
                    MessageBox.Show("The company you have choosen had already been selected by other mentors. Please choose other companys!");
                }
                else
                {
                    using (MySqlCommand cmdinsert = new MySqlCommand("UPDATE mentors SET company_assign = @company_assign, number_student_assign = @studentAssign WHERE mentor_id = @mentor_id", connection))
                    {
                        connection.Open();
                        cmdinsert.Parameters.AddWithValue("@company_assign", companyname);
                        cmdinsert.Parameters.AddWithValue("@mentor_id", increment2);
                        cmdinsert.Parameters.AddWithValue("@studentAssign", studentnum.Text);
                        MySqlDataReader reader = cmdinsert.ExecuteReader();

                        while (reader.Read())
                        {

                        }
                        connection.Close();
                        MessageBox.Show("SAVED");
                        loop = 1;

                    }
                }
            }
        }

                
            

        public long count;

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        public CustomAssign()
        {
            InitializeComponent();
        }

        private void CustomAssign_Load(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection("server=localhost;Database=ipsv1;Uid=root;Pwd=;"))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT mentor_name FROM mentors", connection))
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader[0]);

                    }
                    connection.Close();
                }
            }
            using (MySqlConnection connection = new MySqlConnection("server=localhost;Database=ipsv1;Uid=root;Pwd=;"))
            {
                using (MySqlCommand cmdcompany = new MySqlCommand("SELECT company_name FROM companys", connection))
                {
                    connection.Open();
                    MySqlDataReader reader = cmdcompany.ExecuteReader();

                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader[0]);

                    }
                    connection.Close();
                }
            }
        }
    }
}
