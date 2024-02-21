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
    public partial class CategoryAdd_UpdateForm : Form
    {
        AppDbContext context;
        CategoryManager manager;
        Categories c;
        LOGHelper helper;
        CategoryForm form = Application.OpenForms.OfType<CategoryForm>().FirstOrDefault();

        public CategoryAdd_UpdateForm()
        {
            InitializeComponent();
            context = new AppDbContext();
            manager = new CategoryManager(new EfCategoryDAL(context));
            c = new Categories();
            helper = new LOGHelper();
        }

        public CategoryAdd_UpdateForm(int categoryID, string categoryName, string description)
        {
            InitializeComponent();
            context = new AppDbContext();
            manager = new CategoryManager(new EfCategoryDAL(context));
            c = new Categories();
            helper = new LOGHelper();

            lblCategoryID.Text = categoryID.ToString();
            txtBoxCategoryName.Text = categoryName;
            txtBoxDescription.Text = description;
        }

        private void btnAddUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAddUpdate.Text == "Ekle")
                {
                    c.CategoryName = txtBoxCategoryName.Text;
                    c.Description = txtBoxDescription.Text;

                    manager.TAdd(c);
                    context.SaveChanges();

                    MessageBox.Show("Kategori başarıyla eklendi. Kategori Adı: " + c.CategoryName, "Kategori Eklendi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    int categoryID = Convert.ToInt32(lblCategoryID.Text);
                    string categoryName = txtBoxCategoryName.Text;
                    string description = txtBoxDescription.Text;

                    var existingCategory = manager.TGetByID(categoryID);

                    if (existingCategory != null)
                    {
                        existingCategory.CategoryName = categoryName;
                        existingCategory.Description = description;

                        manager.TUpdate(existingCategory);
                        context.SaveChanges();

                        MessageBox.Show("Kategori başarıyla güncellendi. Kategori Adı: " + existingCategory.CategoryName, "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Kategori bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
