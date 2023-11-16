using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilites.Convertor;

namespace Accounting.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
         frmCustomers frmCustomers = new frmCustomers();
            frmCustomers.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            this.Hide();
            frmLogin frmLogin = new frmLogin();
            if (frmLogin.ShowDialog() == DialogResult.OK)
            {
                this.Show();
                lblDate.Text = DateTime.Now.ToShamsi();
                lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
            }
            else  { Application.Exit(); }
        }

        private void btnNewAccounting_Click(object sender, EventArgs e)
        {
            frmNewAccounting frmNewAccounting = new frmNewAccounting();
            frmNewAccounting.ShowDialog();
        }

        private void btnReportPay_Click(object sender, EventArgs e)
        {
           frmReport frmReport = new frmReport();
            frmReport.TypeID = 2;
            frmReport.ShowDialog();
        }

        private void btnReportRecive_Click(object sender, EventArgs e)
        {
            frmReport frmReport = new frmReport();
            frmReport.TypeID = 1;
            frmReport.ShowDialog();
        }

        

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnEditLogin_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.isEdit = true;
            frmLogin.ShowDialog();

        }
    }
}
