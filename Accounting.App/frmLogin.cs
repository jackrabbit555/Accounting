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
    public partial class frmLogin : Form
    {
        public bool isEdit = false;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                using (UnitOFWork db = new UnitOFWork())
                {
                    if (isEdit) 
                    {
                        var login = db.LoginRopository.Get().First();
                        login.UserName = txtUserName.Text;
                        login.PassWord = txtPassWord.Text;
                        db.LoginRopository.Update(login);
                        db.Save();
                        Application.Restart();

                    }
                    else
                    {
                        if (db.LoginRopository.Get(l => l.UserName == txtUserName.Text && l.PassWord == txtPassWord.Text).Any())
                        {
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            RtlMessageBox.Show("اطلاعات وارد شده صحیح نمیباشد", "هشدار", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                        }
                    }




                };
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (isEdit)
            {

                this.Text = "تنظیمات ورود به برنامه";
                btnLogin.Text = "ذخیره تغییرات";
                using (UnitOFWork db = new UnitOFWork())
                {
                    var login = db.LoginRopository.Get().First();
                    txtUserName.Text = login.UserName;
                    txtPassWord.Text = login.PassWord;

                }

            }
        }
    }
}
