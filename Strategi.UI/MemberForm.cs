using Strategi.BusinessLayer.Concrete;
using Strategi.DataAccessLayer.Concrete;
using Strategi.DataAccessLayer.EntityFramework;
using System;
using System.Windows.Forms;

namespace Strategi.UI
{
    public partial class MemberForm : Form
    {
        AppDbContext context;
        MemberManager manager;
        public MemberForm()
        {
            InitializeComponent();
            context = new AppDbContext();
            manager = new MemberManager(new EfMemberDAL(context));
        }

        private void MemberForm_Load(object sender, EventArgs e)
        {
            var data = manager.TGetList();
            m_dataGridViewMembers.DataSource = data;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MemberAdd_UpdateForm form = new MemberAdd_UpdateForm();
            form.Text = "Üye Ekle";
            form.btnAddUpdate.Text = "Ekle";
            form.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (m_dataGridViewMembers.SelectedRows.Count > 0)
            {
                int selectedRowIndex = m_dataGridViewMembers.SelectedRows[0].Index;
                int memberID = Convert.ToInt32(m_dataGridViewMembers.Rows[selectedRowIndex].Cells["MemberID"].Value);
                string firstName = m_dataGridViewMembers.Rows[selectedRowIndex].Cells["FirstName"].Value.ToString();
                string lastName = m_dataGridViewMembers.Rows[selectedRowIndex].Cells["LastName"].Value.ToString();
                string phone = m_dataGridViewMembers.Rows[selectedRowIndex].Cells["Phone"].Value.ToString();
                string mail = m_dataGridViewMembers.Rows[selectedRowIndex].Cells["Mail"].Value.ToString();
                string userName = m_dataGridViewMembers.Rows[selectedRowIndex].Cells["UserName"].Value.ToString();
                string password = m_dataGridViewMembers.Rows[selectedRowIndex].Cells["Password"].Value.ToString();
                bool isAdmin = Convert.ToBoolean(m_dataGridViewMembers.Rows[selectedRowIndex].Cells["Is_Admin"].Value.ToString());

                MemberAdd_UpdateForm form = new MemberAdd_UpdateForm(memberID, firstName, lastName, phone, mail, userName, password, isAdmin);
                form.Text = "Üye Bilgisi Güncelle";
                form.btnAddUpdate.Text = "Güncelle";
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Lütfen güncellemek için bir ürün seçin.", "Seçili Ürün Yok", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_dataGridViewMembers.SelectedRows.Count > 0)
                {
                    var id = m_dataGridViewMembers.SelectedRows[0].Cells["MemberID"].Value.ToString();
                    var data = manager.TGetByID(Convert.ToInt32(id));
                    manager.TDelete(data);
                    context.SaveChanges();

                    MessageBox.Show("Üye başarıyla silindi.", "Üye Silindi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Lütfen geçerli bir satır seçin", "Üye Silinemedi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri tabanı işlemleri sırasında bir sorun ile karşılaşıldı. " + ex.Message.ToString(), "Başarısız İşlem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                ReLoadProducts();
            }
        }

        public void ReLoadProducts()
        {
            AppDbContext context = new AppDbContext();
            MemberManager manager = new MemberManager(new EfMemberDAL(context));

            m_dataGridViewMembers.DataSource = null;

            var data = manager.TGetList();
            m_dataGridViewMembers.DataSource = data;
        }


    }
}
