

////using System;
////using System.Collections.Generic;
////using System.Data;
////using System.Drawing;
////using System.Linq;
////using System.Windows.Forms;
////using KiemTraiGiuaKy.Entity; // Đảm bảo namespace này đúng với Model của bạn

////namespace KiemTraiGiuaKy
////{
////    public partial class frm_phanQuyen : Form
////    {


//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Windows.Forms;
//using KiemTraiGiuaKy.Entity;
//using System.Security.Cryptography; // Add this namespace for hashing
//using System.Text; // Add this namespace for StringBuilder

//namespace KiemTraiGiuaKy
//{
//    public partial class frm_phanQuyen : Form
//    {
//        // Lưu tên đăng nhập của người dùng hiện tại (người đang thao tác trên form phân quyền)
//        private string currentUser = "";

//        public frm_phanQuyen(string username = "")
//        {
//            InitializeComponent();
//            currentUser = username; // Nhận tên đăng nhập từ form đăng nhập
//        }

//        private void frm_phanQuyen_Load(object sender, EventArgs e)
//        {
//            InitDataGridView(dataGridView1, isManager: true);
//            InitDataGridView(dataGridView2, isManager: false);
//            LoadData();
//        }

//        private void InitDataGridView(DataGridView dgv, bool isManager)
//        {
//            dgv.AutoGenerateColumns = false;
//            dgv.Columns.Clear();

//            // Số thứ tự
//            var sttCol = new DataGridViewTextBoxColumn
//            {
//                Name = "STT",
//                HeaderText = "STT",
//                Width = 40,
//                ReadOnly = true
//            };
//            dgv.Columns.Add(sttCol);

//            // Cột icon thay đổi
//            var iconCol = new DataGridViewImageColumn
//            {
//                Name = "ThayDoi",
//                HeaderText = "THAY ĐỔI",
//                Width = 100,
//                ImageLayout = DataGridViewImageCellLayout.Zoom
//            };
//            // Đảm bảo bạn đã thêm 'edit_icon' vào Resources của project
//            // Chuột phải vào project > Properties > Resources > Add Resource > Add Existing File
//            iconCol.Image = Properties.Resources.edit_icon; //
//            dgv.Columns.Add(iconCol);

//            dgv.Columns.Add(new DataGridViewTextBoxColumn
//            {
//                DataPropertyName = "TenDangNhap",
//                Name = "TenDangNhap",
//                HeaderText = "TÊN ĐĂNG NHẬP",
//                Width = 150,
//                ReadOnly = true
//            });
//            dgv.Columns.Add(new DataGridViewTextBoxColumn
//            {
//                DataPropertyName = "TenNhanVien",
//                Name = "TenNhanVien",
//                HeaderText = "TÊN NHÂN VIÊN",
//                Width = 200,
//                ReadOnly = true
//            });
//            dgv.Columns.Add(new DataGridViewTextBoxColumn
//            {
//                DataPropertyName = "SoDienThoai",
//                Name = "SoDienThoai",
//                HeaderText = "SỐ ĐIỆN THOẠI",
//                Width = 150,
//                ReadOnly = true
//            });
//            dgv.Columns.Add(new DataGridViewTextBoxColumn
//            {
//                DataPropertyName = "GioiTinh",
//                Name = "GioiTinh",
//                HeaderText = "GIỚI TÍNH",
//                Width = 70,
//                ReadOnly = true
//            });
//            dgv.Columns.Add(new DataGridViewTextBoxColumn
//            {
//                DataPropertyName = "NgaySinh",
//                Name = "NgaySinh",
//                HeaderText = "NGÀY SINH",
//                ReadOnly = true,
//                Width = 150,
//                DefaultCellStyle = { Format = "dd/MM/yyyy" }
//            });

//            dgv.RowPostPaint += DataGridView_RowPostPaint;

//            if (isManager)
//                dgv.CellClick += DataGridView1_CellClick; // Quản lý
//            else
//                dgv.CellClick += DataGridView2_CellClick; // Nhân viên
//        }

//        private void LoadData()
//        {
//            using (var db = new Model1()) // Sử dụng "Model1"
//            {
//                // Danh sách Quản lý
//                var dsQuanLy = db.NHAN_VIEN
//                    .Where(nv => (nv.VOHIEUHOA == false || nv.VOHIEUHOA == null) && nv.LAQUANLY == true)
//                    .Select(nv => new
//                    {
//                        TenDangNhap = nv.TENDANGNHAP,
//                        TenNhanVien = nv.TENNHANVIEN,
//                        SoDienThoai = nv.SDT,
//                        GioiTinh = nv.GIOITINH,
//                        NgaySinh = nv.NGAYSINH,
//                        IsAdmin = nv.TENDANGNHAP.ToLower() == "admin" // Thêm cột IsAdmin để dễ kiểm tra
//                    })
//                    .ToList();

//                // Danh sách Nhân viên
//                var dsNhanVien = db.NHAN_VIEN
//                    .Where(nv => (nv.VOHIEUHOA == false || nv.VOHIEUHOA == null) && (nv.LAQUANLY == false || nv.LAQUANLY == null))
//                    .Select(nv => new
//                    {
//                        TenDangNhap = nv.TENDANGNHAP,
//                        TenNhanVien = nv.TENNHANVIEN,
//                        SoDienThoai = nv.SDT,
//                        GioiTinh = nv.GIOITINH,
//                        NgaySinh = nv.NGAYSINH
//                    })
//                    .ToList();

//                dataGridView1.DataSource = dsQuanLy;
//                dataGridView2.DataSource = dsNhanVien;
//            }
//        }

//        // Hiển thị số thứ tự
//        private void DataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
//        {
//            var dgv = sender as DataGridView;
//            if (dgv != null)
//            {
//                dgv.Rows[e.RowIndex].Cells["STT"].Value = (e.RowIndex + 1).ToString();
//            }
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

//        // Yêu cầu nhập mật khẩu xác nhận của người dùng HIỆN TẠI (currentUser)
//        private bool ConfirmPassword()
//        {
//            using (var inputForm = new frmNhapMatKhau())
//            {
//                if (inputForm.ShowDialog() == DialogResult.OK)
//                {
//                    string inputPassword = inputForm.Password;
//                    string hashedPassword = ComputeSha256Hash(inputPassword); // Hash the input password

//                    using (var db = new Model1()) // Sử dụng "Model1"
//                    {
//                        // Kiểm tra mật khẩu nhập vào (đã được hash) có đúng với mật khẩu của currentUser không
//                        var user = db.NHAN_VIEN.FirstOrDefault(x =>
//                            x.TENDANGNHAP == currentUser &&
//                            x.MATKHAU == hashedPassword && // Compare with hashed password
//                            (x.VOHIEUHOA == false || x.VOHIEUHOA == null)
//                        );
//                        if (user != null)
//                            return true;
//                    }
//                    MessageBox.Show("Mật khẩu xác nhận không đúng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                }
//            }
//            return false;
//        }

//        // Click icon ở dataGridView2: Chuyển Nhân viên -> Quản lý
//        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
//        {
//            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView2.Columns["ThayDoi"].Index)
//            {
//                string tenDangNhapToChange = dataGridView2.Rows[e.RowIndex].Cells["TenDangNhap"].Value?.ToString();
//                if (string.IsNullOrEmpty(tenDangNhapToChange)) return;

//                // Bất kỳ quản lý nào cũng có thể chuyển nhân viên thành quản lý (bao gồm admin)
//                // Yêu cầu xác nhận mật khẩu của người dùng hiện tại (currentUser)
//                if (!ConfirmPassword()) return;

//                using (var db = new Model1()) // Sử dụng "Model1"
//                {
//                    var nv = db.NHAN_VIEN.FirstOrDefault(x => x.TENDANGNHAP == tenDangNhapToChange);
//                    if (nv != null)
//                    {
//                        nv.LAQUANLY = true;
//                        db.SaveChanges();
//                        MessageBox.Show("Đã phân quyền nhân viên thành quản lý!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                        LoadData(); // Tải lại dữ liệu sau khi cập nhật
//                    }
//                }
//            }
//        }

//        // Click icon ở dataGridView1: Chuyển Quản lý -> Nhân viên (chỉ admin mới được)
//        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
//        {
//            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["ThayDoi"].Index)
//            {
//                string tenDangNhapToChange = dataGridView1.Rows[e.RowIndex].Cells["TenDangNhap"].Value?.ToString();
//                if (string.IsNullOrEmpty(tenDangNhapToChange)) return;

//                // Kiểm tra nếu đang cố gắng thay đổi tài khoản admin
//                bool isTargetAdmin = tenDangNhapToChange.ToLower() == "admin";
//                if (isTargetAdmin)
//                {
//                    MessageBox.Show("Không thể thay đổi quyền của tài khoản admin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }

//                // Chỉ tài khoản admin mới được phép chuyển quản lý khác thành nhân viên
//                if (currentUser.ToLower() != "admin")
//                {
//                    MessageBox.Show("Chỉ tài khoản admin mới có quyền chuyển quản lý thành nhân viên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }

//                // Yêu cầu xác nhận mật khẩu của người dùng hiện tại (currentUser - lúc này là admin)
//                if (!ConfirmPassword()) return;

//                using (var db = new Model1()) // Sử dụng "Model1"
//                {
//                    var nv = db.NHAN_VIEN.FirstOrDefault(x => x.TENDANGNHAP == tenDangNhapToChange);
//                    if (nv != null)
//                    {
//                        nv.LAQUANLY = false;
//                        db.SaveChanges();
//                        MessageBox.Show("Đã chuyển quản lý thành nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                        LoadData(); // Tải lại dữ liệu sau khi cập nhật
//                    }
//                }
//            }
//        }
//    }

//    // Form nhập mật khẩu xác nhận
//    public class frmNhapMatKhau : Form
//    {
//        public string Password { get; private set; }
//        private TextBox txtPassword;
//        private Button btnOK, btnCancel;

//        public frmNhapMatKhau()
//        {
//            this.Text = "Xác nhận mật khẩu";
//            this.FormBorderStyle = FormBorderStyle.FixedDialog;
//            this.StartPosition = FormStartPosition.CenterParent;
//            this.Width = 300;
//            this.Height = 140;
//            this.MaximizeBox = false;
//            this.MinimizeBox = false;

//            Label lbl = new Label() { Text = "Nhập mật khẩu:", Left = 10, Top = 20, Width = 100 };
//            txtPassword = new TextBox() { Left = 120, Top = 18, Width = 150, PasswordChar = '*' };

//            btnOK = new Button() { Text = "OK", Left = 60, Width = 70, Top = 60, DialogResult = DialogResult.OK };
//            btnCancel = new Button() { Text = "Hủy", Left = 150, Width = 70, Top = 60, DialogResult = DialogResult.Cancel };

//            btnOK.Click += (s, e) => { Password = txtPassword.Text; this.Close(); };
//            btnCancel.Click += (s, e) => { this.Close(); };

//            this.Controls.Add(lbl);
//            this.Controls.Add(txtPassword);
//            this.Controls.Add(btnOK);
//            this.Controls.Add(btnCancel);

//            this.AcceptButton = btnOK;
//            this.CancelButton = btnCancel;
//        }
//    }
//}



using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using KiemTraiGiuaKy.Entity;
using System.Security.Cryptography; // Add this namespace for hashing
using System.Text; // Add this namespace for StringBuilder

namespace KiemTraiGiuaKy
{
    public partial class frm_phanQuyen : Form
    {
        // Lưu tên đăng nhập của người dùng hiện tại (người đang thao tác trên form phân quyền)
        private string currentUser = "";

        // DataSource cho mỗi DataGridView để dễ dàng lọc
        private List<object> _quanLyDataSource;
        private List<object> _nhanVienDataSource;

        public frm_phanQuyen(string username = "")
        {
            InitializeComponent();
            currentUser = username; // Nhận tên đăng nhập từ form đăng nhập
            // Đặt các sự kiện TextChanged cho các textbox tìm kiếm (cần thêm các textbox này vào designer)
            // Nếu bạn đã đặt tên textbox là txtSearchQuanLy và txtSearchNhanVien
            txtSearchQuanLy.TextChanged += TxtSearchQuanLy_TextChanged;
            txtSearchNhanVien.TextChanged += TxtSearchNhanVien_TextChanged;
        }

        private void frm_phanQuyen_Load(object sender, EventArgs e)
        {
            InitDataGridView(dataGridView1, isManager: true);
            InitDataGridView(dataGridView2, isManager: false);
            LoadData();
        }

        private void InitDataGridView(DataGridView dgv, bool isManager)
        {
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Clear();

            // Số thứ tự
            var sttCol = new DataGridViewTextBoxColumn
            {
                Name = "STT",
                HeaderText = "STT",
                Width = 40,
                ReadOnly = true
            };
            dgv.Columns.Add(sttCol);

            // Cột icon thay đổi
            var iconCol = new DataGridViewImageColumn
            {
                Name = "ThayDoi",
                HeaderText = "THAY ĐỔI",
                Width = 100, // Tăng chiều rộng để hiển thị rõ hơn
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };
            iconCol.Image = Properties.Resources.edit_icon; // Đảm bảo bạn đã thêm 'edit_icon' vào Resources
            dgv.Columns.Add(iconCol);

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenDangNhap",
                Name = "TenDangNhap",
                HeaderText = "TÊN ĐĂNG NHẬP",
                Width = 150, // Điều chỉnh độ rộng cột
                ReadOnly = true
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenNhanVien",
                Name = "TenNhanVien",
                HeaderText = "TÊN NHÂN VIÊN",
                Width = 200, // Điều chỉnh độ rộng cột
                ReadOnly = true
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoDienThoai",
                Name = "SoDienThoai",
                HeaderText = "SỐ ĐIỆN THOẠI",
                Width = 120, // Điều chỉnh độ rộng cột
                ReadOnly = true
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GioiTinh",
                Name = "GioiTinh",
                HeaderText = "GIỚI TÍNH",
                Width = 90, // Điều chỉnh độ rộng cột
                ReadOnly = true
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NgaySinh",
                Name = "NgaySinh",
                HeaderText = "NGÀY SINH",
                ReadOnly = true,
                Width = 120, // Điều chỉnh độ rộng cột
                DefaultCellStyle = { Format = "dd/MM/yyyy" }
            });

            dgv.RowPostPaint += DataGridView_RowPostPaint;

            if (isManager)
                dgv.CellClick += DataGridView1_CellClick; // Quản lý
            else
                dgv.CellClick += DataGridView2_CellClick; // Nhân viên
        }

        private void LoadData()
        {
            using (var db = new Model1())
            {
                _quanLyDataSource = db.NHAN_VIEN
                    .Where(nv => (nv.VOHIEUHOA == false || nv.VOHIEUHOA == null) && nv.LAQUANLY == true)
                    .Select(nv => new
                    {
                        TenDangNhap = nv.TENDANGNHAP,
                        TenNhanVien = nv.TENNHANVIEN,
                        SoDienThoai = nv.SDT,
                        GioiTinh = nv.GIOITINH,
                        NgaySinh = nv.NGAYSINH,
                        IsAdmin = nv.TENDANGNHAP.ToLower() == "admin"
                    })
                    .ToList<object>(); // Cast to List<object>

                _nhanVienDataSource = db.NHAN_VIEN
                    .Where(nv => (nv.VOHIEUHOA == false || nv.VOHIEUHOA == null) && (nv.LAQUANLY == false || nv.LAQUANLY == null))
                    .Select(nv => new
                    {
                        TenDangNhap = nv.TENDANGNHAP,
                        TenNhanVien = nv.TENNHANVIEN,
                        SoDienThoai = nv.SDT,
                        GioiTinh = nv.GIOITINH,
                        NgaySinh = nv.NGAYSINH
                    })
                    .ToList<object>(); // Cast to List<object>

                ApplyFilterQuanLy();
                ApplyFilterNhanVien();
            }
        }

        // Hiển thị số thứ tự
        private void DataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = sender as DataGridView;
            if (dgv != null)
            {
                dgv.Rows[e.RowIndex].Cells["STT"].Value = (e.RowIndex + 1).ToString();
            }
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

        // Yêu cầu nhập mật khẩu xác nhận của người dùng HIỆN TẠI (currentUser)
        private bool ConfirmPassword()
        {
            using (var inputForm = new frmNhapMatKhau())
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    string inputPassword = inputForm.Password;
                    string hashedPassword = ComputeSha256Hash(inputPassword); // Hash the input password

                    using (var db = new Model1())
                    {
                        // Kiểm tra mật khẩu nhập vào (đã được hash) có đúng với mật khẩu của currentUser không
                        var user = db.NHAN_VIEN.FirstOrDefault(x =>
                            x.TENDANGNHAP == currentUser &&
                            x.MATKHAU == hashedPassword && // Compare with hashed password
                            (x.VOHIEUHOA == false || x.VOHIEUHOA == null)
                        );
                        if (user != null)
                            return true;
                    }
                    MessageBox.Show("Mật khẩu xác nhận không đúng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            return false;
        }

        // Click icon ở dataGridView2: Chuyển Nhân viên -> Quản lý
        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView2.Columns["ThayDoi"].Index)
            {
                string tenDangNhapToChange = dataGridView2.Rows[e.RowIndex].Cells["TenDangNhap"].Value?.ToString();
                if (string.IsNullOrEmpty(tenDangNhapToChange)) return;

                // Yêu cầu xác nhận mật khẩu của người dùng hiện tại (currentUser)
                if (!ConfirmPassword()) return;

                using (var db = new Model1())
                {
                    var nv = db.NHAN_VIEN.FirstOrDefault(x => x.TENDANGNHAP == tenDangNhapToChange);
                    if (nv != null)
                    {
                        nv.LAQUANLY = true;
                        db.SaveChanges();
                        MessageBox.Show("Đã phân quyền nhân viên thành quản lý!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // Tải lại dữ liệu sau khi cập nhật
                    }
                }
            }
        }

        // Click icon ở dataGridView1: Chuyển Quản lý -> Nhân viên (chỉ admin mới được)
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["ThayDoi"].Index)
            {
                string tenDangNhapToChange = dataGridView1.Rows[e.RowIndex].Cells["TenDangNhap"].Value?.ToString();
                if (string.IsNullOrEmpty(tenDangNhapToChange)) return;

                // Kiểm tra nếu đang cố gắng thay đổi tài khoản admin
                bool isTargetAdmin = tenDangNhapToChange.ToLower() == "admin";
                if (isTargetAdmin)
                {
                    MessageBox.Show("Không thể thay đổi quyền của tài khoản admin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Chỉ tài khoản admin mới được phép chuyển quản lý khác thành nhân viên
                if (currentUser.ToLower() != "admin")
                {
                    MessageBox.Show("Chỉ tài khoản admin mới có quyền chuyển quản lý thành nhân viên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Yêu cầu xác nhận mật khẩu của người dùng hiện tại (currentUser - lúc này là admin)
                if (!ConfirmPassword()) return;

                using (var db = new Model1())
                {
                    var nv = db.NHAN_VIEN.FirstOrDefault(x => x.TENDANGNHAP == tenDangNhapToChange);
                    if (nv != null)
                    {
                        nv.LAQUANLY = false;
                        db.SaveChanges();
                        MessageBox.Show("Đã chuyển quản lý thành nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // Tải lại dữ liệu sau khi cập nhật
                    }
                }
            }
        }

        // --- Chức năng tìm kiếm ---
        private void TxtSearchQuanLy_TextChanged(object sender, EventArgs e)
        {
            ApplyFilterQuanLy();
        }

        private void TxtSearchNhanVien_TextChanged(object sender, EventArgs e)
        {
            ApplyFilterNhanVien();
        }

        private void ApplyFilterQuanLy()
        {
            string searchText = txtSearchQuanLy.Text.ToLower().Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                dataGridView1.DataSource = _quanLyDataSource;
            }
            else
            {
                var filteredList = _quanLyDataSource.Where(item =>
                {
                    // Sử dụng dynamic để truy cập các thuộc tính của object ẩn danh
                    dynamic dItem = item;
                    return dItem.TenDangNhap.ToLower().Contains(searchText) ||
                           dItem.TenNhanVien.ToLower().Contains(searchText) ||
                           (dItem.SoDienThoai != null && dItem.SoDienThoai.ToLower().Contains(searchText));
                }).ToList();
                dataGridView1.DataSource = filteredList;
            }
            dataGridView1.Refresh(); // Cập nhật hiển thị STT
        }

        private void ApplyFilterNhanVien()
        {
            string searchText = txtSearchNhanVien.Text.ToLower().Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                dataGridView2.DataSource = _nhanVienDataSource;
            }
            else
            {
                var filteredList = _nhanVienDataSource.Where(item =>
                {
                    dynamic dItem = item;
                    return dItem.TenDangNhap.ToLower().Contains(searchText) ||
                           dItem.TenNhanVien.ToLower().Contains(searchText) ||
                           (dItem.SoDienThoai != null && dItem.SoDienThoai.ToLower().Contains(searchText));
                }).ToList();
                dataGridView2.DataSource = filteredList;
            }
            dataGridView2.Refresh(); // Cập nhật hiển thị STT
        }
    }

    // Form nhập mật khẩu xác nhận (giữ nguyên như cũ)
    public class frmNhapMatKhau : Form
    {
        public string Password { get; private set; }
        private TextBox txtPassword;
        private Button btnOK, btnCancel;

        public frmNhapMatKhau()
        {
            this.Text = "Xác nhận mật khẩu";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Width = 300;
            this.Height = 140;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Label lbl = new Label() { Text = "Nhập mật khẩu:", Left = 10, Top = 20, Width = 100 };
            txtPassword = new TextBox() { Left = 120, Top = 18, Width = 150, PasswordChar = '*' };

            btnOK = new Button() { Text = "OK", Left = 60, Width = 70, Top = 60, DialogResult = DialogResult.OK };
            btnCancel = new Button() { Text = "Hủy", Left = 150, Width = 70, Top = 60, DialogResult = DialogResult.Cancel };

            btnOK.Click += (s, e) => { Password = txtPassword.Text; this.Close(); };
            btnCancel.Click += (s, e) => { this.Close(); };

            this.Controls.Add(lbl);
            this.Controls.Add(txtPassword);
            this.Controls.Add(btnOK);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }
    }
}