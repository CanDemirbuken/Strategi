using Strategi.BusinessLayer.Concrete;
using Strategi.DataAccessLayer.Concrete;
using Strategi.DataAccessLayer.EntityFramework;
using Strategi.DataAccessLayer.Helpers;
using Strategi.EntityLayer;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Strategi.UI
{
    public partial class MemberAdd_UpdateForm : Form
    {
        AppDbContext context;
        MemberManager manager;
        Members m;
        LOGHelper helper;
        MemberForm form = Application.OpenForms.OfType<MemberForm>().FirstOrDefault();

        public MemberAdd_UpdateForm()
        {
            InitializeComponent();
            context = new AppDbContext();
            manager = new MemberManager(new EfMemberDAL(context));
            m = new Members();
            helper = new LOGHelper();
        }

        public MemberAdd_UpdateForm(int memberID, string firstName, string lastName, string phone, string mail, string userName, string password, bool isAdmin)
        {
            InitializeComponent();
            context = new AppDbContext();
            manager = new MemberManager(new EfMemberDAL(context));
            m = new Members();
            helper = new LOGHelper();

            lblMemberID.Text = memberID.ToString();
            txtBoxFirstName.Text = firstName;
            txtBoxLastName.Text = lastName;
            txtBoxPhone.Text = phone;
            txtBoxMail.Text = mail;
            txtBoxUserName.Text = userName;
            txtBoxPassword.Text = password;
            chkBoxYes.Checked = isAdmin;
        }

        private void btnAddUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAddUpdate.Text == "Ekle")
                {
                    m.FirstName = txtBoxFirstName.Text;
                    m.LastName = txtBoxLastName.Text;
                    m.Phone = txtBoxPhone.Text;
                    m.Mail = txtBoxMail.Text;
                    m.UserName = txtBoxUserName.Text;
                    m.Password = txtBoxPassword.Text;
                    m.Is_Admin = chkBoxYes.Checked;

                    manager.TAdd(m);
                    context.SaveChanges();

                    MessageBox.Show("Üye başarıyla eklendi. Üye Adı: " + m.UserName, "Kategori Eklendi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    int memberID = Convert.ToInt32(lblMemberID.Text);
                    string firstName = txtBoxFirstName.Text;
                    string lastName = txtBoxLastName.Text;
                    string phone = txtBoxPhone.Text;
                    string mail = txtBoxMail.Text;
                    string userName = txtBoxUserName.Text;
                    string password = txtBoxPassword.Text;
                    bool isAdmin = chkBoxYes.Checked;

                    var existingMember = manager.TGetByID(memberID);

                    if (existingMember != null)
                    {
                        existingMember.FirstName = firstName;
                        existingMember.LastName = lastName;
                        existingMember.Phone = phone;
                        existingMember.Mail = mail;
                        existingMember.UserName = userName;
                        existingMember.Password = password;
                        existingMember.Is_Admin = isAdmin;

                        manager.TUpdate(existingMember);
                        context.SaveChanges();

                        MessageBox.Show("Üye bilgisi başarıyla güncellendi. Üye Adı: " + existingMember.UserName, "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Üye bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri tabanı işlemleri sırasında bir sorun ile karşılaşıldı", "Başarısız İşlem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                helper.LogError($"Hata: {ex.Message.ToString()}" + "-->" + DateTime.Now.ToString());
            }
            finally
            {
                form.ReLoadProducts();
                this.Close();
            }
        }
    }
}
