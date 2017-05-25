using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using MySql.Data.MySqlClient;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace IPSv1
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            UpdateInfo frm = new UpdateInfo();
            frm.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            MentorAssign frm = new MentorAssign();
            frm.Show();
        }

        private void ViewReport_Click(object sender, EventArgs e)
        {   
            
            PdfDocument pdf = new PdfDocument();//create pdf document object
            pdf.Info.Title = "My First PDF";//set title
            PdfPage pdfPage = pdf.AddPage();//add page to the object

            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);

            //write content to pdf document
            graph.DrawString("School of Computer Sciences,", font, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.Center);
            
            string pdfFilename = "Mentor Report.pdf";
            pdf.Save(pdfFilename);
            Process.Start(pdfFilename);

        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
    
    
    

    

