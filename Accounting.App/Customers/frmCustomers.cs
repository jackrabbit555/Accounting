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

namespace Accounting.App
{
    public partial class frmCustomers : Form
    {
        public frmCustomers()
        {
            InitializeComponent();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dgCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        void BindGrid()
        {
            using (UnitOFWork db = new UnitOFWork())
            {
                dgCustomers.AutoGenerateColumns = false;
                dgCustomers.DataSource = db.CustomerRepository.GetAllCustomers();


            }

        }
        private void frmCustomers_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void btnRefreshCustomer_Click(object sender, EventArgs e)
        {
            txtFilter.Text = "";
            BindGrid();
        }

        private void txtFilter_Click(object sender, EventArgs e)
        {

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            using (UnitOFWork db = new UnitOFWork())
            {
                dgCustomers.DataSource = db.CustomerRepository.GetCustomersByFilter(txtFilter.Text);

            }
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {

            if (dgCustomers.CurrentRow != null)
            {
                using (UnitOFWork db = new UnitOFWork())
                {
                    string name = dgCustomers.CurrentRow.Cells[1].Value.ToString();
                    if (RtlMessageBox.Show($"مطمعن هستید؟{name}ایا از حذف ", "توجه",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        int customerId = int.Parse(dgCustomers.CurrentRow.Cells[0].Value.ToString());
                        db.CustomerRepository.DeleteCustomer(customerId);
                        db.Save();
                        BindGrid();
                    }
                }
            }
            else
            {
                RtlMessageBox.Show("لطفا شخصی را انتخاب کنید ");
            }
        }

        private void btnAddNewCustomer_Click(object sender, EventArgs e)
        {
            frmAddOrEditCustomer frmAdd = new frmAddOrEditCustomer();
            if (frmAdd.ShowDialog() == DialogResult.OK)
            {
                BindGrid();
            }
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            if (dgCustomers.CurrentRow !=null)
            {
                int CustomerId = int.Parse(dgCustomers.CurrentRow.Cells[0].Value.ToString());
                frmAddOrEditCustomer frmAddOrEdit = new frmAddOrEditCustomer();
                frmAddOrEdit.isEdit = CustomerId;
                if (frmAddOrEdit.ShowDialog() == DialogResult.OK)
                {
                    BindGrid();
                }
            }
        }
    }
}
