using Accounting.DataLayer;
using Accounting.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidationComponents;

namespace Accounting.App
{
    public partial class frmAddOrEditCustomer : Form
    {
        public int isEdit = 0;
        UnitOFWork db = new UnitOFWork();
        public frmAddOrEditCustomer()
        {
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSelectPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pcCustomer.ImageLocation = openFileDialog.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(pcCustomer.ImageLocation);
                string imagelocation = pcCustomer.ImageLocation;
                string path = Application.StartupPath + "/Images/" + imageName;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //pcCustomer.Image.Save(Application.StartupPath+ "/Images/"+imageName);
                pcCustomer.Image.Save(path + imageName);

                Customers customer = new Customers()

                {


                    FullName = txtName.Text,
                    Mobile = txtMobile.Text,
                    Email = txtEmail.Text,
                    Addresse = txtAddress.Text,
                    CustomerImage = imageName




                };
                if (isEdit == 0)
                {
                    db.CustomerRepository.InsertCustomer(customer);
                }
                else
                {
                    customer.CustomerID = isEdit;
                    db.CustomerRepository.UpdateCustomer(customer);
                }

                db.Save();
                DialogResult = DialogResult.OK;
            }
        }

        private void frmAddOrEditCustomer_Load(object sender, EventArgs e)
        {
            if (isEdit != 0)
            {
                this.Text = "ویرایش شخص";
                btnSave.Text = "ویرایش";
                var customer = db.CustomerRepository.GetCustomersbyId(isEdit);
                txtEmail.Text = customer.Email;
                txtMobile.Text = customer.Mobile;
                txtName.Text = customer.FullName;
                txtAddress.Text = customer.Addresse;
                pcCustomer.ImageLocation = Application.StartupPath + "/Images/" + customer.CustomerImage;
            }
        }

        private void pcCustomer_Click(object sender, EventArgs e)
        {

        }
    }
}
