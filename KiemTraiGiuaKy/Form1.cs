

//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using KiemTraiGiuaKy.Entity; // Đảm bảo namespace này đúng với Model của bạn

//namespace KiemTraiGiuaKy
//{
//    public partial class Form1 : Form
//    {
//        public Form1()
//        {
//            InitializeComponent();
//        }

//        private void label1_Click(object sender, EventArgs e)
//        {
//            // Có thể bỏ trống hoặc thêm logic xử lý sự kiện click cho label1
//        }

//        // Giả sử bạn có 2 TextBox: txtUsername, txtPassword và 1 Button: btnLogin
//        private void btnLogin_Click(object sender, EventArgs e)
//        {
//            string username = txtUsername.Text.Trim();
//            string password = txtPassword.Text.Trim();

//            // Sử dụng "Model1" như bạn đã định nghĩa trong mã
//            using (var db = new Model1())
//            {
//                var user = db.NHAN_VIEN
//                    .FirstOrDefault(nv => nv.TENDANGNHAP == username && nv.MATKHAU == password && (nv.VOHIEUHOA == false || nv.VOHIEUHOA == null));

//                if (user != null)
//                {
//                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    // Truyền tên đăng nhập của người dùng hiện tại sang Form phân quyền
//                    frm_phanQuyen frm = new frm_phanQuyen(username);
//                    frm.Show();
//                    this.Hide();
//                }
//                else
//                {
//                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void btn_DangKy_Click(object sender, EventArgs e)
//        {

//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KiemTraiGiuaKy.Entity;
using System.Security.Cryptography; // Add this for SHA-256 hashing

namespace KiemTraiGiuaKy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Can be left empty or add logic for label1 click event
        }

        // Helper method to compute SHA-256 hash
        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Hash the entered password before comparing with the database
            string hashedPassword = ComputeSha256Hash(password);

            using (var db = new Model1())
            {
                var user = db.NHAN_VIEN
                    .FirstOrDefault(nv => nv.TENDANGNHAP == username && nv.MATKHAU == hashedPassword && (nv.VOHIEUHOA == false || nv.VOHIEUHOA == null));

                if (user != null)
                {
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Pass the current user's username to the permission form
                    frm_phanQuyen frm = new frm_phanQuyen(username);
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_DangKy_Click(object sender, EventArgs e)
        {
            frm_DangKy registerForm = new frm_DangKy();
            registerForm.ShowDialog(); // Show the registration form
        }
    }
}