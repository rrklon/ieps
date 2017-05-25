using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace IPSv1
{
    


    public partial class MentorAssign : Form
    {        //create connection to database
        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;");
        public MentorAssign()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            CustomAssign frm = new CustomAssign();
            frm.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DataTable mentorDt = new DataTable();
            DataTable companyDt = new DataTable();

            string allMentors = "SELECT * FROM ipsv1.mentors;";
            MySqlCommand command = new MySqlCommand(allMentors, connection);
            //let the command continue execute while connection is opened
            MySqlDataReader myReader;
            try
            {
                connection.Open();
                MySqlDataAdapter adr = new MySqlDataAdapter(allMentors, connection);
                adr.SelectCommand.CommandType = CommandType.Text;
                adr.Fill(mentorDt); //opens and closes the DB connection automatically !! (fetches from pool)

                foreach (DataRow item in mentorDt.Rows)
                {

                    string query = "SELECT legend_no,lat, lng, SQRT(POW(69.1 * (lat - " + item["lat"].ToString() + "), 2) + POW(69.1 * (" + item["lng"].ToString() + " - lng) * COS(lat / 57.3), 2)) AS distance FROM ipsv1.companys  ORDER BY distance;";
                    adr = new MySqlDataAdapter(query, connection);
                    adr.SelectCommand.CommandType = CommandType.Text;
                    adr.Fill(companyDt); //opens and closes the DB connection automatically !! (fetches from pool)
                    foreach (DataRow dtrow in companyDt.Rows)
                    {
                        string insert = "INSERT INTO ipsv1.companymentorassign(CompanyId,MentorId) VALUES('" + dtrow["legend_no"] + "','" + item["mentor_no"] + "');";
                        command = new MySqlCommand(insert, connection);
                        //let the command continue execute while connection is opened
                        try
                        {                     
                            myReader = command.ExecuteReader();
                            MessageBox.Show("Assigned!!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;
                    }
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
            
        
    

