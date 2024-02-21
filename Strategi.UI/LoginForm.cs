using Strategi.BusinessLayer.Concrete;
using Strategi.DataAccessLayer.Concrete;
using Strategi.DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Strategi.UI
{
    public partial class LoginForm : Form
    {
        AppDbContext context;
        MemberManager manager;
        public LoginForm()
        {
            InitializeComponent();
            context = new AppDbContext();
            manager = new MemberManager(new EfMemberDAL(context));
        }

        public void btnLogin_Click(object sender, EventArgs e)
        {
            var userName = txtBoxUserName.Text;
            var password = txtBoxPassword.Text;

            bool credential = manager.TCheckCredentials(userName, password);
            bool admin = manager.TIsAdmin(userName);
            if (credential)
            {
                MainForm form = new MainForm(userName);
                if (admin)
                {
                    form.btnMembers.Enabled = true;
                }
                else
                {
                    form.btnMembers.Enabled=false;
                    form.btnMembers.Text = "Üye yönetimine erişebilmeniz için yönetici hesabınız olması gerek.";
                }
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Giriş sırasında bir hata ile karşılaşıldı. Lütfen daha sonra tekrar deneyin.");
            }
        }
    }
}
