using Accounting.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidationComponents;

namespace Accounting.App
{
    public partial class frmNewAccounting : Form
    {
        private UnitOFWork db;
        public int AccountIDchanging = 0;

        public frmNewAccounting()
        {
            InitializeComponent();
        }

        private void frmNewAccounting_Load(object sender, EventArgs e)
        {
            db = new UnitOFWork();
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = db.CustomerRepository.GetNameCustomers();
            if (AccountIDchanging != 0)
            {
                var account = db.AccountingRepository.GetById(AccountIDchanging);
                txtAmount.Text = account.Amount.ToString();
                txtDescription.Text = account.Description.ToString();
                txtName.Text = db.CustomerRepository.GetCustomerNameByID(account.CustomerID);
                if (account.TyprID == 1)
                {
                    rbRecive.Checked = true;


                }
                else { rbPay.Checked = true; }
                this.Text = "ویرایش";
                btnSave.Text = "ویرایش";
                db.Dispose();
            }
            //using (UnitOFWork db2 = new UnitOFWork()) 
            //{
            
            //}

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = db.CustomerRepository.GetNameCustomers(txtFilter.Text);
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dgvCustomers.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))

            {
                db = new UnitOFWork();
                if (rbPay.Checked || rbRecive.Checked) { }

                DataLayer.Accounting accounting = new DataLayer.Accounting()
                {
                    Amount = int.Parse(txtAmount.Value.ToString()),
                    CustomerID = db.CustomerRepository.GetCustomerIdByName(txtName.Text),
                    TyprID = (rbRecive.Checked) ? 1 : 2,
                    DateTime = DateTime.Now,
                    Description = txtDescription.Text,

                };
                if (AccountIDchanging == 0)
                {
                    db.AccountingRepository.Insert(accounting);
                    
                }
                else
                {
                    accounting.ID = AccountIDchanging;
                    db.AccountingRepository.Update(accounting);


                    //using (UnitOFWork db2 = new UnitOFWork())
                    //{
                    //    accounting.ID = AccountIDchanging;
                    //    db2.AccountingRepository.Update(accounting);
                    //    db2.Save();
                    //}
                    
                }

                db.Save();
                DialogResult = DialogResult.OK;
                db.Dispose();
            }

            

            else { RtlMessageBox.Show("لطفا نوع تراکنش را انتخاب کنید "); }


        }


        

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }

       
    }
}
