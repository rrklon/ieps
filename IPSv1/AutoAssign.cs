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
    public partial class AutoAssign : Form
    {
        public AutoAssign()
        {
            InitializeComponent();
        }

        private void AutoAssign_Load(object sender, EventArgs e)
        {

            string[] company;
            int arrayinc = 0;

            string companyname, companyaddress, mentorname, hometown, studentcompany, companyname2, studentcompany2, studentcompanyminimum;
            string studentassign;
            int storeassign = 0, studentcompanyInt, studentassignInt, studentcompanyInt2, studentcompanyIntminimum;
            string checkexist1, checkexist2, cmdText1, cmdText2, cmdText3, cmdText4;
            string cmdQuery, cmdQuery2;
            int increment = 1, increment2 = 1, breakloop = 0, checkincrement = 0, checkincrement2 = 0;
            string distanceresult, distanceresultkm;
            double newdistanceresult;
            string url;
            string requesturl;
            string content;
            long count = 0, count2 = 0, countSpecific = 0, countdiscard = 0, countremove = 0;
            string liststring, stringminimum;
            int checkValue = 0, checkValidation = 0;
            string stringvalidation, stringvalidation2;
            bool indexvalidation, indexvalidation2, index;
            string minimum = null;

            string ConnectionString = "server=localhost;Database=ipsv1;Uid=root;Pwd=;"; //create a connection string to database
            MySqlConnection con = new MySqlConnection("server=localhost;Database=ipsv1;Uid=root;Pwd=;");

            //this section will assign the mentor to a company based on the preferred location (district)
            //-------------------------------------------------------------------------------------------------------------
            string commandline = "SELECT COUNT(*) FROM mentors";
            using (MySqlConnection connection = new MySqlConnection("server=localhost;Database=ipsv1;Uid=root;Pwd=;"))
            using (MySqlCommand cmdCount = new MySqlCommand(commandline, connection))
            {
                connection.Open(); // must open connection first
                count = (long)cmdCount.ExecuteScalar();
            }

            //this section to get total number of company
            string commandline2 = "SELECT COUNT(*) FROM company";
            using (MySqlConnection connection2 = new MySqlConnection("server=localhost;Database=ipsv1;Uid=root;Pwd=;"))
            using (MySqlCommand cmdCount2 = new MySqlCommand(commandline2, connection2))
            {
                connection2.Open(); // must open connection first
                count2 = (long)cmdCount2.ExecuteScalar();

            }
            //-------------------------------------------------------------------------------------------------------------
            //this section will calculate the distance from each company to mentor location
            do
            {

                do
                {


                    if (minimum == null) //minimum is the string that store the shortest distance of the company name travel to hometown
                    {

                        do
                        {
                            con.Open();
                            MySqlCommand cmdstudent = new MySqlCommand();
                            cmdstudent.CommandText = string.Format("SELECT number_student_assign FROM mentors WHERE mentor_no ='" + increment2 + "'");
                            cmdstudent.Connection = con;
                            MySqlDataReader readerstudent = cmdstudent.ExecuteReader();

                            while (readerstudent.Read())
                            {
                                studentassign = (string)readerstudent.GetString(0);  //studentassign is a string to get the number of student assign from the mentors database table
                            }
                            con.Close();
                            studentassignInt = Convert.ToInt32(studentassign);



                            con.Open();
                            MySqlCommand cmd = new MySqlCommand();
                            cmd.CommandText = string.Format("SELECT company_name,company_address,number_of_students FROM companys WHERE legend_no ='" + increment + "'");
                            cmd.Connection = con;
                            MySqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                companyaddress = (string)reader.GetString("company_address"); //store the company address 
                                companyname = (string)reader.GetString("company_name");  //store the company name
                                studentcompany = (string)reader.GetString("number_of_students");  //store the number of students assign in the company
                            }
                            con.Close();
                            studentcompanyInt = Convert.ToInt32(studentcompany); //convert the studentcompany string into integer


                            con.Open();
                            MySqlCommand cmd2 = new MySqlCommand();
                            cmd2.CommandText = string.Format("SELECT mentor_district FROM mentors WHERE mentor_id ='" + increment2 + "'");
                            cmd2.Connection = con;
                            MySqlDataReader reader2 = cmd2.ExecuteReader();
                            while (reader2.Read())
                            {
                                hometown = (string)reader2.GetString(0); //store the mentor's district address
                            }
                            con.Close();
                            checkincrement = increment2;
                            checkincrement--;

                            if (increment2 > 1)
                            {
                                while (checkincrement >= 1)
                                {
                                    con.Open();
                                    MySqlCommand cmdvalidation = new MySqlCommand();
                                    cmdvalidation.CommandText = string.Format("SELECT company_assign FROM mentors WHERE mentor_id ='" + checkincrement + "'");
                                    cmdvalidation.Connection = con;
                                    MySqlDataReader readervalidation = cmdvalidation.ExecuteReader();
                                    while (readervalidation.Read())
                                    {
                                        stringvalidation = (string)readervalidation.GetString(0); //store the string company_assign from the mentors database table
                                    }
                                    con.Close();
                                    indexvalidation = stringvalidation.Contains(companyname); //check the string company_assign contains the company name or not
                                    if (indexvalidation == true)
                                        checkincrement = -1;  //break the loop
                                    else
                                        checkincrement--; //continue loop
                                }
                            }



                            if ((storeassign + studentcompanyInt) > studentassignInt || (indexvalidation == true)) //check the number of student assign is exceed the student assign for mentors and the company name whether contains in the company_assign column or not
                            {
                                increment++; //continue loop with increase the row number by 1
                            }
                            else if ((storeassign + studentcompanyInt) <= studentassignInt)
                            {
                                breakloop = 1; //break the loop
                            }

                            if (increment > count)
                                breakloop = 1; //break the loop



                        }// end do

                        while ((storeassign + studentcompanyInt) > studentassignInt || breakloop != 1 || indexvalidation == true);

                        System.Threading.Thread.Sleep(200);
                        url = "http://maps.googleapis.com/maps/api/directions/json?origin=" + companyaddress + "&destination=" + hometown + "&sensor=false"; //the url that store company address and hometown from mentor which will generate the distance result
                        requesturl = url;
                        //string requesturl = @"http://maps.googleapis.com/maps/api/directions/json?origin=" + from + "&alternatives=false&units=imperial&destination=" + to + "&sensor=false";
                        content = fileGetContents(requesturl); //store the html page that generate from url into a file form

                        Newtonsoft.Json.Linq.JObject o = Newtonsoft.Json.Linq.JObject.Parse(content); //using json extension method to get the content in the file

                        distanceresult = (string)o.SelectToken("routes[0].legs[0].distance.text"); //store the distance result into string 
                        if (distanceresult != null)
                        {
                            distanceresultkm = (string)distanceresult.Replace(" km", ""); //remove the "km" behind the string
                            newdistanceresult = Double.Parse(distanceresultkm); //convert the distanceresultkm string into double variable
                        }
                        else
                        {
                            newdistanceresult = 0; //if the distance result is null or empty, it will generate 0 and you will need to check back the database to see where is the company address or hometown address mistake
                        }

                        //create insert query to insert data to mentors table
                        cmdQuery = "INSERT INTO visits(mentor_id,company_name,visit_distance)VALUES(@mentor_id,@company_name,@visit_distance)";
                        MySqlCommand commandQuery = new MySqlCommand(cmdQuery, connection);
                        MySqlCommand commandQuery2 = new MySqlCommand(cmdQuery2, connection);
                        commandQuery.Parameters.AddWithValue("@company_name", companyname);
                        commandQuery.Parameters.AddWithValue("@company_address", companyaddress);
                        commandQuery.Parameters.AddWithValue("@visit_distance", newdistanceresult);


                        MySqlDataReader myReader, myReader2;
                        try
                        {
                            connection.Open();
                            myReader = commandQuery.ExecuteReader(); //run the insert query using executereader method
                                                                     //MessageBox.Show("SAVED");
                            while (myReader.Read())
                            {

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        connection.Close();

                    } //end if

                    else
                    {

                        do
                        {
                            checkValue = 0;
                            string commandlineSpecific = "SELECT COUNT(*) FROM companys where company_name LIKE @company_name";
                            using (MySqlConnection connect = new MySqlConnection("server=localhost;Database=ipsv1;Uid=root;Pwd=;"))
                            using (MySqlCommand cmdCount = new MySqlCommand(commandlineSpecific, connect))
                            {
                                connect.Open();
                                cmdCount.Parameters.AddWithValue("@company_name", "%" + minimum);
                                countSpecific = (long)cmdCount.ExecuteScalar();
                            }


                            con.Open();
                            MySqlCommand cmdcompare1 = new MySqlCommand();
                            cmdcompare1.CommandText = string.Format("SELECT company_address FROM companys WHERE company_name = @minimum");
                            cmdcompare1.Parameters.AddWithValue("@minimum", minimum);
                            cmdcompare1.Connection = con;
                            MySqlDataReader readercompare1 = cmdcompare1.ExecuteReader();
                            while (readercompare1.Read())
                            {
                                companyaddress = (string)readercompare1.GetString("company_address"); //store the company address from company database table
                            }
                            con.Close();



                            con.Open();
                            MySqlCommand cmdcompare2 = new MySqlCommand();
                            cmdcompare2.CommandText = string.Format("SELECT company_name,company_address,number_of_students FROM companys WHERE legend_no ='" + increment + "'");
                            cmdcompare2.Connection = con;
                            MySqlDataReader readercompare2 = cmdcompare2.ExecuteReader();
                            while (readercompare2.Read())
                            {
                                companyname2 = (string)readercompare2.GetString("company_name");
                                hometown = (string)readercompare2.GetString("company_address");
                                studentcompany2 = (string)readercompare2.GetString("Number_of_students");
                            }
                            studentcompanyInt2 = Convert.ToInt32(studentcompany2);
                            con.Close();

                            index = stringminimum.Contains(companyname2);


                            checkincrement2 = increment2;
                            checkincrement2--;

                            if (increment2 > 1)
                            {
                                while (checkincrement2 >= 1)
                                {
                                    con.Open();
                                    MySqlCommand cmdvalidation2 = new MySqlCommand();
                                    cmdvalidation2.CommandText = string.Format("SELECT company_assign FROM mentors WHERE mentor_id ='" + checkincrement2 + "'");
                                    cmdvalidation2.Connection = con;
                                    MySqlDataReader readervalidation2 = cmdvalidation2.ExecuteReader();
                                    while (readervalidation2.Read())
                                    {
                                        stringvalidation2 = (string)readervalidation2.GetString(0);
                                    }
                                    con.Close();
                                    indexvalidation2 = stringvalidation2.Contains(companyname2);

                                    if (indexvalidation2 == true)
                                        checkincrement2 = -1;
                                    else
                                        checkincrement2--;

                                }
                            }



                            if ((storeassign + studentcompanyInt2) > studentassignInt || (indexvalidation2 == true) || index == true)
                            {
                                increment++;
                                countremove++;

                            }
                            else if ((storeassign + studentcompanyInt2) <= studentassignInt)
                            {
                                breakloop = 1;
                            }

                            if (increment > count)
                                breakloop = 1;

                        } while ((storeassign + studentcompanyInt2) > studentassignInt || breakloop != 1 || (index == true) || (indexvalidation2 == true));



                        checkValue = 0;
                        if (increment > count)
                            breakloop = 1;
                        System.Threading.Thread.Sleep(200);
                        //string from = origin.Text;
                        //string to = destination.Text;
                        url = "http://maps.googleapis.com/maps/api/directions/json?origin=" + companyaddress + "&destination=" + hometown + "&sensor=false";
                        requesturl = url;
                        //string requesturl = @"http://maps.googleapis.com/maps/api/directions/json?origin=" + from + "&alternatives=false&units=imperial&destination=" + to + "&sensor=false";
                        content = fileGetContents(requesturl);

                        Newtonsoft.Json.Linq.JObject o = Newtonsoft.Json.Linq.JObject.Parse(content);

                        distanceresult = (string)o.SelectToken("routes[0].legs[0].distance.text");
                        if (distanceresult != null)
                        {
                            distanceresultkm = (string)distanceresult.Replace(" km", "");
                            newdistanceresult = Double.Parse(distanceresultkm);
                        }
                        else
                        {
                            newdistanceresult = 0;
                        }

                        //create insert query to insert data to mentors table
                        cmdQuery = "INSERT INTO distance(company_name,company_address,total_distance)VALUES(@company_name,@company_address,@total_distance)";
                        MySqlCommand commandQuery = new MySqlCommand(cmdQuery, connection);
                        MySqlCommand commandQuery2 = new MySqlCommand(cmdQuery2, connection);
                        commandQuery.Parameters.AddWithValue("@company_name", companyname2);
                        commandQuery.Parameters.AddWithValue("@company_address", hometown);
                        //commandQuery2.Parameters.AddWithValue("@company_name2", mentorname);
                        //commandQuery2.Parameters.AddWithValue("@company_address2", hometown);
                        commandQuery.Parameters.AddWithValue("@total_distance", newdistanceresult);
                        //commandQuery2.Parameters.AddWithValue("@total_distance", newdistanceresult);

                        //run query with connection to database
                        //MySqlCommand command = new MySqlCommand(insertQuery, connection);
                        //let the command continue execute while connection is opened
                        MySqlDataReader myReader, myReader2;
                        try
                        {
                            connection.Open();
                            myReader2 = commandQuery.ExecuteReader();
                            //MessageBox.Show("SAVED");
                            while (myReader2.Read())
                            {

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        connection.Close();
                    }

                    countdiscard = count - countSpecific - countremove;

                    if (increment > count)
                        breakloop = 1;

                    increment++;
                    if (increment > countdiscard)
                        break;
                }//end do2

                while (increment <= count || increment <= countdiscard);

                con.Open();
                MySqlCommand cmdminimum = new MySqlCommand();
                cmdminimum.CommandText = string.Format("SELECT company_name FROM distance WHERE total_distance=(select min(total_distance) from distance)");  //select query with condition to search the minimum from the column 
                cmdminimum.Connection = con;
                MySqlDataReader readerminimum = cmdminimum.ExecuteReader();
                //int countminimum = 0;
                //string [] sp = new string [countminimum];
                CommaDelimitedStringCollection list = new CommaDelimitedStringCollection(); //implement comma reference and initialize
                while (readerminimum.Read())
                {
                    minimum = (string)readerminimum.GetString(0);

                }
                con.Close();
                stringminimum += minimum + Environment.NewLine;
                //textBox1.Text = stringminimum;

                using (MySqlConnection cnn = new MySqlConnection("server=localhost;Database=internship2;Uid=root;Pwd=;"))
                using (MySqlCommand cmddeleterow = new MySqlCommand("TRUNCATE TABLE distance", cnn))
                {
                    cnn.Open();
                    cmddeleterow.ExecuteNonQuery();
                }

                //countminimum++;
                //textBox1.Text = companyaddress;
                increment = 1;


                con.Open();
                MySqlCommand cmdgetNumber = new MySqlCommand();
                cmdgetNumber.CommandText = string.Format("SELECT Number_of_students FROM companys WHERE company_name ='" + minimum + "'");
                cmdgetNumber.Connection = con;
                MySqlDataReader readergetNumber = cmdgetNumber.ExecuteReader();
                while (readergetNumber.Read())
                {

                    studentcompanyminimum = (string)readergetNumber.GetString("Number_of_students");
                }
                studentcompanyIntminimum = Convert.ToInt32(studentcompanyminimum);
                con.Close();
                storeassign = storeassign + studentcompanyIntminimum;
                if (storeassign >= studentassignInt)
                {
                    breakloop = 1;
                }

            }//end do1


            while (storeassign < studentassignInt || breakloop != 1);
            //create insert query to insert data to mentors table
            string cmdQuery3 = "UPDATE mentors SET company_assign = @company_assign WHERE mentor_id = @mentor_id";

            MySqlCommand commandInsertQuery = new MySqlCommand(cmdQuery3, connection);

            commandInsertQuery.Parameters.AddWithValue("@company_assign", stringminimum);
            commandInsertQuery.Parameters.AddWithValue("@mentor_id", increment2);

            //run query with connection to database

            MySqlDataReader myReader3;
            try
            {
                connection.Open();
                myReader3 = commandInsertQuery.ExecuteReader();
                //MessageBox.Show("SAVED");
                while (myReader3.Read())
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();

            minimum = null;

            storeassign = 0;

            stringminimum = null;
            checkincrement = 1;
            checkincrement2 = 1;
            increment = 1;
            increment2 = increment2 + 1;


            while (increment2 <= count2) ;
            t.Abort();


        }







        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            progressBar1.Maximum = 100000;
            progressBar1.Step = 1;

            for (int j = 0; j < 100000; j++)
            {
                Calculate(j);
                progressBar1.PerformStep();
            }

        }

        private void Calculate(int i)
        {
            double pow = Math.Pow(i, i);
        }
    }
}




