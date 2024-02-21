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
    public partial class CategoryForm : Form
    {
        AppDbContext context;
        CategoryManager manager;

        public CategoryForm()
        {
            InitializeComponent();
            context = new AppDbContext();
            manager = new CategoryManager(new EfCategoryDAL(context));
        }

        private void CategoryForm_Load(object sender, EventArgs e)
        {
            var data = manager.TGetList();
            m_dataGridViewCategories.DataSource = data;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CategoryAdd_UpdateForm form = new CategoryAdd_UpdateForm();
            form.Text = "Kategori Ekle";
            form.btnAddUpdate.Text = "Ekle";
            form.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (m_dataGridViewCategories.SelectedRows.Count > 0)
            {
                int selectedRowIndex = m_dataGridViewCategories.SelectedRows[0].Index;
                int categoryID = Convert.ToInt32(m_dataGridViewCategories.Rows[selectedRowIndex].Cells["CategoryID"].Value);
                string categoryName = m_dataGridViewCategories.Rows[selectedRowIndex].Cells["CategoryName"].Value.ToString();
                string description = m_dataGridViewCategories.Rows[selectedRowIndex].Cells["Description"].Value.ToString();

                CategoryAdd_UpdateForm form = new CategoryAdd_UpdateForm(categoryID, categoryName, description);
                form.Text = "Kategori Bilgisi Güncelle";
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
                if (m_dataGridViewCategories.SelectedRows.Count > 0)
                {
                    var id = m_dataGridViewCategories.SelectedRows[0].Cells["CategoryID"].Value.ToString();
                    var data = manager.TGetByID(Convert.ToInt32(id));
                    manager.TDelete(data);
                    context.SaveChanges();

                    MessageBox.Show("Kategori başarıyla silindi.", "Kategori Silindi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Lütfen geçerli bir satır seçin", "Kategori Silinemedi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            CategoryManager manager = new CategoryManager(new EfCategoryDAL(context));

            m_dataGridViewCategories.DataSource = null;

            var data = manager.TGetList();
            m_dataGridViewCategories.DataSource = data;
        }
    }
}
