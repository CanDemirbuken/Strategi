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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(string userName)
        {
            InitializeComponent();
            this.label1.Text = userName;
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            ProductForm product = new ProductForm();
            product.Show();
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            CategoryForm category = new CategoryForm();
            category.Show();
        }

        private void btnMembers_Click(object sender, EventArgs e)
        {
            MemberForm member = new MemberForm();
            member.Show();
        }
    }
}
