//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using KiemTraiGiuaKy.Entity; // Ensure this namespace is correct for your Model
//using System.Security.Cryptography; // Add this for SHA-256 hashing

//namespace KiemTraiGiuaKy
//{
//    public partial class frm_DangKy : Form
//    {
//        public frm_DangKy()
//        {
//            InitializeComponent();
//            // Initialize ComboBox for GioiTinh (Gender)
//            cbxGioiTinh.Items.Add("Nam");
//            cbxGioiTinh.Items.Add("Nữ");
//            cbxGioiTinh.Items.Add("Khác");
//            cbxGioiTinh.SelectedIndex = 0; // Set a default selection
//        }

//        private void frm_DangKy_Load(object sender, EventArgs e)
//        {
//            // Any specific load logic for the registration form
//        }

//        // Helper method to compute SHA-256 hash
//        private string ComputeSha256Hash(string rawData)
//        {
//            using (SHA256 sha256Hash = SHA256.Create())
//            {
//                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

//                StringBuilder builder = new StringBuilder();
//                for (int i = 0; i < bytes.Length; i++)
//                {
//                    builder.Append(bytes[i].ToString("x2"));
//                }
//                return builder.ToString();
//            }
//        }

//        private void btnDangKy_Click(object sender, EventArgs e)
//        {
//            // Input validation
//            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) ||
//                string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
//                string.IsNullOrWhiteSpace(txtXacNhanMatKhau.Text) ||
//                string.IsNullOrWhiteSpace(txtTenNhanVien.Text))
//            {
//                MessageBox.Show("Vui lòng điền đầy đủ các trường bắt buộc (Tên đăng nhập, Mật khẩu, Tên nhân viên).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            if (txtMatKhau.Text != txtXacNhanMatKhau.Text)
//            {
//                MessageBox.Show("Mật khẩu xác nhận không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                return;
//            }

//            string username = txtTenDangNhap.Text.Trim();
//            string password = txtMatKhau.Text.Trim();
//            string employeeName = txtTenNhanVien.Text.Trim();
//            string address = txtDiaChi.Text.Trim();
//            string phoneNumber = txtSDT.Text.Trim();
//            string gender = cbxGioiTinh.SelectedItem?.ToString();
//            DateTime dob = dtpNgaySinh.Value;
//            string cmndCccd = txtCMND_CCCD.Text.Trim();

//            // Hash the password
//            string hashedPassword = ComputeSha256Hash(password);

//            using (var db = new Model1())
//            {
//                // Check if username already exists
//                if (db.NHAN_VIEN.Any(nv => nv.TENDANGNHAP == username))
//                {
//                    MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên đăng nhập khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }

//                // Generate a new unique MANHANVIEN (simplified for demonstration)
//                // In a real application, consider a more robust ID generation strategy
//                string newMaNhanVien;
//                int lastId = 0;
//                var lastEmployee = db.NHAN_VIEN.OrderByDescending(nv => nv.MANHANVIEN).FirstOrDefault();
//                if (lastEmployee != null && lastEmployee.MANHANVIEN.StartsWith("NV"))
//                {
//                    // Try to parse the numeric part and increment
//                    if (int.TryParse(lastEmployee.MANHANVIEN.Substring(2), out int parsedId))
//                    {
//                        lastId = parsedId;
//                    }
//                }
//                newMaNhanVien = "NV" + (lastId + 1).ToString("D2"); // Ensures "NV01", "NV02", etc.

//                var newEmployee = new NHAN_VIEN
//                {
//                    MANHANVIEN = newMaNhanVien, // You might need a more robust ID generation
//                    TENNHANVIEN = employeeName,
//                    DIACHI = address,
//                    SDT = phoneNumber,
//                    GIOITINH = gender,
//                    NGAYSINH = dob,
//                    CMND_CCCD = cmndCccd,
//                    TENDANGNHAP = username,
//                    MATKHAU = hashedPassword, // Store the hashed password
//                    VOHIEUHOA = false, // Default to not disabled
//                    LAQUANLY = false // Default to regular employee
//                };

//                db.NHAN_VIEN.Add(newEmployee);
//                db.SaveChanges();

//                MessageBox.Show("Đăng ký tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                this.Close(); // Close the registration form after successful registration
//            }
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
using System.Security.Cryptography;
using System.Data.Entity.Validation; // Add this namespace

namespace KiemTraiGiuaKy
{
    public partial class frm_DangKy : Form
    {
        public frm_DangKy()
        {
            InitializeComponent();
            // Assuming your ComboBox for gender is named cbxGioiTinh
            // Make sure these are the correct names as per your designer
            cbxGioiTinh.Items.Add("Nam");
            cbxGioiTinh.Items.Add("Nữ");
            cbxGioiTinh.Items.Add("Khác");
            cbxGioiTinh.SelectedIndex = 0; // Set a default selection
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

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            // Input validation based on your UI (image_81ea0e.png)
            // You have fields for "Ten dang nhap", "Ho va ten", "Mat khau", "Nhap lai mat khau"
            // and others like "Ngay sinh", "Gioi tinh", "CCCD", "So dien thoai", "Dia chi".
            // Make sure your textboxes are correctly named. Let's assume:
            // txtTenDangNhap, txtHoVaTen (for TENNHANVIEN), txtMatKhau, txtNhapLaiMatKhau
            // dtpNgaySinh, cbxGioiTinh, txtCCCD, txtSoDienThoai, txtDiaChi

            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
                string.IsNullOrWhiteSpace(txtXacNhanMatKhau.Text) || // This was txtXacNhanMatKhau previously, adjust to your UI
                string.IsNullOrWhiteSpace(txtTenNhanVien.Text)) // This was txtTenNhanVien previously, adjust to your UI
            {
                MessageBox.Show("Vui lòng điền đầy đủ các trường bắt buộc (Tên đăng nhập, Họ và tên, Mật khẩu).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtMatKhau.Text != txtXacNhanMatKhau.Text)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = txtTenDangNhap.Text.Trim();
            string password = txtMatKhau.Text.Trim();
            string employeeName = txtTenNhanVien.Text.Trim(); // Use txtHoVaTen for TENNHANVIEN
            string address = txtDiaChi.Text.Trim();
            string phoneNumber = txtSDT.Text.Trim();
            string gender = cbxGioiTinh.SelectedItem?.ToString();
            DateTime dob = dtpNgaySinh.Value;
            string cmndCccd = txtCMND_CCCD.Text.Trim(); // Use txtCCCD for CMND_CCCD

            // Hash the password
            string hashedPassword = ComputeSha256Hash(password);

            using (var db = new Model1())
            {
                try
                {
                    // Check if username already exists
                    if (db.NHAN_VIEN.Any(nv => nv.TENDANGNHAP == username))
                    {
                        MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên đăng nhập khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Generate a new unique MANHANVIEN
                    // This is a more robust way to generate the ID by finding the max numeric part
                    string newMaNhanVien = "";
                    var lastEmployee = db.NHAN_VIEN.AsEnumerable() // Use AsEnumerable to do client-side sorting if MANHANVIEN isn't purely numeric
                                    .Where(nv => nv.MANHANVIEN.StartsWith("NV"))
                                    .OrderByDescending(nv => int.TryParse(nv.MANHANVIEN.Substring(2), out int num) ? num : 0)
                                    .FirstOrDefault();

                    int lastIdNumber = 0;
                    if (lastEmployee != null)
                    {
                        string numericPart = lastEmployee.MANHANVIEN.Substring(2);
                        int.TryParse(numericPart, out lastIdNumber);
                    }
                    newMaNhanVien = "NV" + (lastIdNumber + 1).ToString("D2"); // Ensures "NV01", "NV02", etc.

                    // Check if the generated MANHANVIEN already exists (unlikely with this logic, but good practice)
                    while (db.NHAN_VIEN.Any(nv => nv.MANHANVIEN == newMaNhanVien))
                    {
                        lastIdNumber++;
                        newMaNhanVien = "NV" + (lastIdNumber + 1).ToString("D2");
                    }


                    var newEmployee = new NHAN_VIEN
                    {
                        MANHANVIEN = newMaNhanVien,
                        TENNHANVIEN = employeeName,
                        DIACHI = address,
                        SDT = phoneNumber,
                        GIOITINH = gender,
                        NGAYSINH = dob,
                        CMND_CCCD = cmndCccd,
                        TENDANGNHAP = username,
                        MATKHAU = hashedPassword, // Store the hashed password
                        VOHIEUHOA = false, // Default to not disabled
                        LAQUANLY = false // Default to regular employee
                    };

                    db.NHAN_VIEN.Add(newEmployee);
                    db.SaveChanges(); // This is where the validation exception occurs

                    MessageBox.Show("Đăng ký tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Close the registration form after successful registration
                }
                catch (DbEntityValidationException ex)
                {
                    // This block will catch the specific validation error
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Lỗi xác thực dữ liệu khi đăng ký:");
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            sb.AppendLine($"- Thuộc tính: {validationError.PropertyName}, Lỗi: {validationError.ErrorMessage}");
                        }
                    }
                    MessageBox.Show(sb.ToString(), "Lỗi Xác Thực", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    // Catch any other general exceptions
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_Huy_Click(object sender, EventArgs e)
        {
            Form1 loginForm = new Form1();
            loginForm.Show(); // Show the login form
            Close();
        }
    }
}