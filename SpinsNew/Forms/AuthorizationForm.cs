using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
using SpinsNew.Data;
using SpinsNew.Interfaces;
using SpinsNew.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpinsNew.Forms
{
    public partial class AuthorizationForm : Form
    {
        private ITableRegisterUser _tableRegisterUser;
        public AuthorizationForm(ITableRegisterUser tableRegisterUser)
        {
            InitializeComponent();
            _tableRegisterUser = tableRegisterUser;
        }

        private async Task DisplayUsers()
        {
            var displayUsers = await Task.Run(() => _tableRegisterUser.DisplayRegisterModelAsync());
            GridView gridView = gridControl1.MainView as GridView;
            registerUsersViewModelBindingSource.DataSource = displayUsers;
            gridControl1.DataSource = registerUsersViewModelBindingSource;
            gridView.BestFitColumns();
            gridView.OptionsView.ColumnAutoWidth = false;
            gridView.OptionsBehavior.Editable = false;
        }

        private async void AuthorizationForm_Load(object sender, EventArgs e)
        {
            await DisplayUsers();
        }
        private async Task UpdateEf()
        {
            GridView gridView = gridControl1.MainView as GridView;
            RegisterModel row = (RegisterModel)gridView.GetRow(gridView.FocusedRowHandle);
            int id = Convert.ToInt32(row.Id);

            using (var context = new ApplicationDbContext())
            {
                var activateUser = await context.tbl_registered_users
                    .FirstOrDefaultAsync(x => x.Id == id);


                activateUser.IsActive = true;
                await context.SaveChangesAsync();

            }
        }
        private async Task DeleteEf()
        {
            GridView gridView = gridControl1.MainView as GridView;
            RegisterModel row = (RegisterModel)gridView.GetRow(gridView.FocusedRowHandle);
            int id = Convert.ToInt32(row.Id);

            using(var context = new ApplicationDbContext())
            {
                var user = await context.tbl_registered_users.SingleAsync(x => x.Id == id);
                context.Remove(user);
                //context.Remove(context.tbl_registered_users.SingleAsync(x => x.Id == id));
                await context.SaveChangesAsync();
            }
        }
        private async void btnActivate_Click(object sender, EventArgs e)
        {

            GridView gridView = gridControl1.MainView as GridView;
            RegisterModel row = (RegisterModel)gridView.GetRow(gridView.FocusedRowHandle);

            if(row.IsActive == true)
            {
                XtraMessageBox.Show("This user already activated.");
                return;
            }

            DialogResult result = XtraMessageBox.Show($"Are you sure you want to Activate user {row.Username}?", "Confirm Activation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {   

                await UpdateEf();
                XtraMessageBox.Show("Activated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await DisplayUsers();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            GridView gridView = gridControl1.MainView as GridView;
            RegisterModel row = (RegisterModel)gridView.GetRow(gridView.FocusedRowHandle);
            DialogResult result = XtraMessageBox.Show($"Are you sure you want to Delete user {row.Username}?", "Delete User", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                await DeleteEf();
                XtraMessageBox.Show("Deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await DisplayUsers();
            }
        }

        private async void simpleButton1_Click(object sender, EventArgs e)
        {
            simpleButton1.Enabled = false;
            simpleButton1.Text = "Please wait...";
            await DisplayUsers();
            simpleButton1.Enabled = true;
            simpleButton1.Text = "Refresh";
        }
    }
}
