using GoogleMaps.LocationServices;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPSv1
{
    public partial class CompanyAdd : Form
    { 
        //create connection to database
        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;");

        public CompanyAdd()
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        private void formtitle_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var address = txtAddress.Text + ",Malaysia";

            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(address);

            var lat = point.Latitude;
            var lng = point.Longitude;
            string inserquery = "INSERT INTO ipsv1.companys( company_name,company_address,company_district,company_state,number_of_students,lat,lng)VALUES " +
                "('" + txtName.Text + "','" + txtAddress.Text + "','" + txtName.Text + "','" + txtName.Text + "',  1,'" + lat + "','" + lng + "');";
            //run query with connection to database
            MySqlCommand command = new MySqlCommand(inserquery, connection);
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

        private void state_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CompanyAdd_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
