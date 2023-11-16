using Accounting.DataLayer;
using Accounting.DataLayer.Context;
using Accounting.ViewModels.Customers;
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
    public partial class frmReport : Form
    {
        public int TypeID = 0;
        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            using (UnitOFWork db = new UnitOFWork()) 
            {
                List<ListCustomerViewModel> listOfItems = new List<ListCustomerViewModel>();
                listOfItems.Add(new ListCustomerViewModel()
                {
                    CustomerID = 0,
                    FullName = "انتخاب کنید",


                });
                listOfItems.AddRange(db.CustomerRepository.GetNameCustomers());
                cbCustomer.DataSource = listOfItems;
                cbCustomer.DisplayMember = "FullName";
                cbCustomer.ValueMember = "CustomerID";
            }
                
            if (TypeID == 1)
            {
                this.Text = "گزارش دریافتی ها ";
            }
            else
            {
                this.Text = "گزارش پرداختی ها";
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Filter();
        }


        void Filter()
        {
            using (UnitOFWork db = new UnitOFWork())
            {
                List<DataLayer.Accounting> result = new List<DataLayer.Accounting>();

                DateTime? startDate;
                DateTime? endDate;
                
              


                if ((int)cbCustomer.SelectedValue != 0)
                {
                    int customerId = int.Parse(cbCustomer.SelectedValue.ToString());
                    result.AddRange(db.AccountingRepository.Get(a => a.TyprID == TypeID && a.CustomerID == customerId));
                }
                else 
                {
                    result.AddRange(db.AccountingRepository.Get(a => a.TyprID == TypeID ));
                
                }

                if (txtFromDate.Text != "    /  /")

                {
                    startDate = Convert.ToDateTime(txtFromDate.Text);
                    startDate = DateConvertor.ToMiladi(startDate.Value);
                    result = result.Where(x=>  x.DateTime  >=  startDate.Value).ToList();
                }
                if (txtToDate.Text != "    /  /")
                {
                    endDate = Convert.ToDateTime(txtToDate.Text);
                    endDate = DateConvertor.ToMiladi(endDate.Value);
                    result = result.Where(x=>  x.DateTime  <=  endDate.Value).ToList();
                }



                //dgReport.AutoGenerateColumns = false;
                //dgReport.DataSource = result;
                dgReport.Rows.Clear();
                foreach (var accounting in result)
                {
                    string customerName = db.CustomerRepository.GetCustomerNameByID(accounting.CustomerID);
                    //Customers customer = db.CustomerRepository.GetCustomersbyId(accounting.ID);

                    dgReport.Rows.Add(accounting.ID, customerName, accounting.Amount, accounting.DateTime.ToShamsi(), accounting.Description);

                }
            }




        }

        private void dgReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgReport.CurrentRow != null)
            {
                int id = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                if (RtlMessageBox.Show("ایا از حذف مطمعن هستید؟", "هشدار", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (UnitOFWork db = new UnitOFWork())
                    {
                        db.AccountingRepository.Delete(id);
                        db.Save();
                        Filter();

                    }

                }

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Filter();
        }

        private void txtFromDate_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtToDate_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dgReport.CurrentRow != null)
            {
                int id = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());

                frmNewAccounting frmNew = new frmNewAccounting();
                frmNew.AccountIDchanging = id;
                if (frmNew.ShowDialog() == DialogResult.OK)
                {
                    Filter();
                }
            }
        }

        private void cbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
