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
using MySql.Data;
using MySql.Data.MySqlClient;
using GoogleMaps.LocationServices;

namespace IPSv1
{
    public partial class AddMentor : Form
    {
        //create connection to database
        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;");

        public AddMentor()
        {
            InitializeComponent();
            state.Items.Add("Perlis");
            state.Items.Add("Perak");
            state.Items.Add("Kedah");
            state.Items.Add("Penang");
            state.Items.Add("Perlis");
            state.Items.Add("Selangor");
            state.Items.Add("Kuala Lumpur");
            state.Items.Add("Negeri Sembilan");
            state.Items.Add("Melaka");
            state.Items.Add("Pahang");
            state.Items.Add("Johor");
            state.Items.Add("Kelantan");
            state.Items.Add("Terengganu");
            state.Items.Add("Sabah");
            state.Items.Add("Sarawak");

            studentAssigned.Items.Add("1");
            studentAssigned.Items.Add("2");
            studentAssigned.Items.Add("3");
            studentAssigned.Items.Add("4");
            studentAssigned.Items.Add("5");
            studentAssigned.Items.Add("6");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            mentorId.Clear();
            mentorName.Clear();
            district.Clear();
            state.DataSource = null;
            studentAssigned.DataSource = null;
        }

        private void AddMentor_Load(object sender, EventArgs e)
        {

        }

        private void state_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var address = district.Text+","+state.Text+",Malaysia";

            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(address);

            var latitude = point.Latitude;
            var longitude = point.Longitude;
            string markerinsert = "";
            //create insert query to insert data to mentors table
            string insertQuery = "INSERT INTO ipsv1.mentors(mentor_name,mentor_id,mentor_district,mentor_state,number_student_assign,company_assign,company_assign2,company_assigned3,company_assign4,lat,lng) VALUES('" + mentorName.Text + "','" + mentorId.Text + "','" + district.Text + "','" + state.Text + "','" + studentAssigned.Text + "','','','','','" + latitude + "','" + longitude + "')";
            //run query with connection to database
            MySqlCommand command = new MySqlCommand(insertQuery, connection);
            //let the command continue execute while connection is opened
            MySqlDataReader myReader;
            try
            {
                connection.Open();
                myReader = command.ExecuteReader();
                MessageBox.Show("Information Saved.");
                while (myReader.Read())
                {

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }
    }
}
