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
    public partial class ProductAdd_UpdateForm : Form
    {
        AppDbContext context;
        ProductManager manager;
        Products p;
        LOGHelper helper;
        ProductForm form = Application.OpenForms.OfType<ProductForm>().FirstOrDefault();


        public ProductAdd_UpdateForm()
        {
            InitializeComponent();
            context = new AppDbContext();
            manager = new ProductManager(new EfProductDAL(context));
            p = new Products();
            helper = new LOGHelper();
        }

        public ProductAdd_UpdateForm(int productID, string productName, int categoryID, int unitPrice, int unitsInStock, int reorderLevel, string description)
        {
            InitializeComponent();
            context = new AppDbContext();
            manager = new ProductManager(new EfProductDAL(context));
            p = new Products();
            helper = new LOGHelper();

            lblProductID.Text = productID.ToString();
            txtBoxProductName.Text = productName;
            comboBoxCategory.Text = categoryID.ToString();
            txtBoxPrice.Text = unitPrice.ToString();
            txtBoxStock.Text = unitsInStock.ToString();
            txtBoxReorder.Text = reorderLevel.ToString();
            txtBoxDescription.Text = description;
        }

        private void ProductAdd_UpdateForm_Load(object sender, EventArgs e)
        {
            var categories = context.Categories.ToList();
            comboBoxCategory.DisplayMember = "CategoryName";
            comboBoxCategory.ValueMember = "CategoryID";
            comboBoxCategory.DataSource = categories;
        }

        private void btnAddUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAddUpdate.Text == "Ekle")
                {
                    p.ProductName = txtBoxProductName.Text;
                    p.CategoryID = Convert.ToInt32(comboBoxCategory.SelectedValue);
                    p.UnitPrice = Convert.ToInt32(txtBoxPrice.Text);
                    p.UnitsInStock = Convert.ToInt32(txtBoxStock.Text);
                    p.ReorderLevel = Convert.ToInt32(txtBoxReorder.Text);
                    p.Description = txtBoxDescription.Text;

                    manager.TAdd(p);
                    context.SaveChanges();

                    MessageBox.Show("Ürün başarıyla eklendi. Ürün Adı: " + p.ProductName, "Ürün Eklendi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    int productID = Convert.ToInt32(lblProductID.Text);
                    string productName = txtBoxProductName.Text;
                    int categoryID = Convert.ToInt32(comboBoxCategory.SelectedValue);
                    int unitPrice = Convert.ToInt32(txtBoxPrice.Text);
                    int unitsInStock = Convert.ToInt32(txtBoxStock.Text);
                    int reorderLevel = Convert.ToInt32(txtBoxReorder.Text);
                    string description = txtBoxDescription.Text;

                    var existingProduct = manager.TGetByID(productID);

                    if (existingProduct != null)
                    {
                        existingProduct.ProductName = productName;
                        existingProduct.CategoryID = categoryID;
                        existingProduct.UnitPrice = unitPrice;
                        existingProduct.UnitsInStock = unitsInStock;
                        existingProduct.ReorderLevel = reorderLevel;
                        existingProduct.Description = description;

                        manager.TUpdate(existingProduct);
                        context.SaveChanges();

                        MessageBox.Show("Ürün başarıyla güncellendi. Ürün Adı: " + existingProduct.ProductName, "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Ürün bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
