using Strategi.BusinessLayer.Concrete;
using Strategi.DataAccessLayer.Concrete;
using Strategi.DataAccessLayer.EntityFramework;
using Strategi.DataAccessLayer.Helpers;
using Strategi.EntityLayer;
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
    public partial class ProductForm : Form
    {
        AppDbContext context;
        ProductManager manager;

        public ProductForm()
        {
            InitializeComponent();
            context = new AppDbContext();
            manager = new ProductManager(new EfProductDAL(context));
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            var data = manager.TGetList();
            m_dataGridViewProducts.DataSource = data;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductAdd_UpdateForm form = new ProductAdd_UpdateForm();
            form.Text = "Ürün Ekle";
            form.btnAddUpdate.Text = "Ekle";
            form.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (m_dataGridViewProducts.SelectedRows.Count > 0)
            {
                int selectedRowIndex = m_dataGridViewProducts.SelectedRows[0].Index;
                int productID = Convert.ToInt32(m_dataGridViewProducts.Rows[selectedRowIndex].Cells["ProductID"].Value);
                string productName = m_dataGridViewProducts.Rows[selectedRowIndex].Cells["ProductName"].Value.ToString();
                int categoryID = Convert.ToInt32(m_dataGridViewProducts.Rows[selectedRowIndex].Cells["CategoryID"].Value);
                int unitPrice = Convert.ToInt32(m_dataGridViewProducts.Rows[selectedRowIndex].Cells["UnitPrice"].Value);
                int unitsInStock = Convert.ToInt32(m_dataGridViewProducts.Rows[selectedRowIndex].Cells["UnitsInStock"].Value);
                int reorderLevel = Convert.ToInt32(m_dataGridViewProducts.Rows[selectedRowIndex].Cells["ReorderLevel"].Value);
                string description = m_dataGridViewProducts.Rows[selectedRowIndex].Cells["Description"].Value.ToString();

                ProductAdd_UpdateForm updateForm = new ProductAdd_UpdateForm(productID, productName, categoryID, unitPrice, unitsInStock, reorderLevel, description);
                updateForm.Text = "Ürün Bilgisi Güncelle";
                updateForm.btnAddUpdate.Text = "Güncelle";
                updateForm.ShowDialog();
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
                if (m_dataGridViewProducts.SelectedRows.Count > 0)
                {
                    var id = m_dataGridViewProducts.SelectedRows[0].Cells["ProductID"].Value.ToString();
                    var data = manager.TGetByID(Convert.ToInt32(id));
                    manager.TDelete(data);
                    context.SaveChanges();

                    MessageBox.Show("Ürün başarıyla silindi.", "Ürün Silindi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Lütfen geçerli bir satır seçin", "Ürün Silinemedi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri tabanı işlemleri sırasında bir sorun ile karşılaşıldı", "Başarısız İşlem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                ReLoadProducts();
            }
        }

        public void ReLoadProducts()
        {
            AppDbContext context = new AppDbContext();
            ProductManager manager = new ProductManager(new EfProductDAL(context));

            m_dataGridViewProducts.DataSource = null;

            var data = manager.TGetList();
            m_dataGridViewProducts.DataSource = data;
        }

    }
}
