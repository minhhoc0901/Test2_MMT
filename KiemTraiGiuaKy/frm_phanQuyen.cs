

////using System;
////using System.Collections.Generic;
////using System.Data;
////using System.Drawing;
////using System.Linq;
////using System.Windows.Forms;
////using KiemTraiGiuaKy.Entity;

////namespace KiemTraiGiuaKy
////{
////    public partial class frm_phanQuyen : Form
////    {
////        private string currentUser = ""; // Lưu tên đăng nhập hiện tại

////        public frm_phanQuyen(string username = "")
////        {
////            InitializeComponent();
////            currentUser = username;
////        }

////        private void frm_phanQuyen_Load(object sender, EventArgs e)
////        {
////            InitDataGridView(dataGridView1, isManager: true);
////            InitDataGridView(dataGridView2, isManager: false);
////            LoadData();
////        }

////        private void InitDataGridView(DataGridView dgv, bool isManager)
////        {
////            dgv.AutoGenerateColumns = false;
////            dgv.Columns.Clear();

////            // Số thứ tự
////            var sttCol = new DataGridViewTextBoxColumn
////            {
////                Name = "STT",
////                HeaderText = "STT",
////                Width = 40,
////                ReadOnly = true
////            };
////            dgv.Columns.Add(sttCol);

////            // Cột icon thay đổi
////            var iconCol = new DataGridViewImageColumn
////            {
////                Name = "ThayDoi",
////                HeaderText = "THAY ĐỔI",
////                Width = 50,
////                ImageLayout = DataGridViewImageCellLayout.Zoom
////            };
////            // 1. Thêm file icon (ví dụ: edit_icon.png) vào thư mục Resources của project (chuột phải vào project > Properties > Resources > Add Resource > Add Existing File).
////            // 2. Đảm bảo tên resource là edit_icon (không có đuôi .png).
////            // 3. Trong code, dòng này sẽ lấy icon từ resource:
////            iconCol.Image = Properties.Resources.edit_icon;
////            dgv.Columns.Add(iconCol);

////            dgv.Columns.Add(new DataGridViewTextBoxColumn
////            {
////                DataPropertyName = "TenDangNhap",
////                Name = "TenDangNhap",
////                HeaderText = "TÊN ĐĂNG NHẬP",
////                ReadOnly = true
////            });
////            dgv.Columns.Add(new DataGridViewTextBoxColumn
////            {
////                DataPropertyName = "TenNhanVien",
////                Name = "TenNhanVien",
////                HeaderText = "TÊN NHÂN VIÊN",
////                ReadOnly = true
////            });
////            dgv.Columns.Add(new DataGridViewTextBoxColumn
////            {
////                DataPropertyName = "SoDienThoai",
////                Name = "SoDienThoai",
////                HeaderText = "SỐ ĐIỆN THOẠI",
////                ReadOnly = true
////            });
////            dgv.Columns.Add(new DataGridViewTextBoxColumn
////            {
////                DataPropertyName = "GioiTinh",
////                Name = "GioiTinh",
////                HeaderText = "GIỚI TÍNH",
////                ReadOnly = true
////            });
////            dgv.Columns.Add(new DataGridViewTextBoxColumn
////            {
////                DataPropertyName = "NgaySinh",
////                Name = "NgaySinh",
////                HeaderText = "NGÀY SINH",
////                ReadOnly = true,
////                DefaultCellStyle = { Format = "dd/MM/yyyy" }
////            });

////            dgv.RowPostPaint += DataGridView_RowPostPaint;

////            if (isManager)
////                dgv.CellClick += DataGridView1_CellClick;
////            else
////                dgv.CellClick += DataGridView2_CellClick;
////        }

////        private void LoadData()
////        {
////            using (var db = new Model1())
////            {
////                // Quản lý
////                var dsQuanLy = db.NHAN_VIEN
////                    .Where(nv => (nv.VOHIEUHOA == false || nv.VOHIEUHOA == null) && nv.LAQUANLY == true)
////                    .Select(nv => new
////                    {
////                        TenDangNhap = nv.TENDANGNHAP,
////                        TenNhanVien = nv.TENNHANVIEN,
////                        SoDienThoai = nv.SDT,
////                        GioiTinh = nv.GIOITINH,
////                        NgaySinh = nv.NGAYSINH,
////                        IsAdmin = nv.TENDANGNHAP.ToLower() == "admin"
////                    })
////                    .ToList();

////                // Nhân viên
////                var dsNhanVien = db.NHAN_VIEN
////                    .Where(nv => (nv.VOHIEUHOA == false || nv.VOHIEUHOA == null) && (nv.LAQUANLY == false || nv.LAQUANLY == null))
////                    .Select(nv => new
////                    {
////                        TenDangNhap = nv.TENDANGNHAP,
////                        TenNhanVien = nv.TENNHANVIEN,
////                        SoDienThoai = nv.SDT,
////                        GioiTinh = nv.GIOITINH,
////                        NgaySinh = nv.NGAYSINH
////                    })
////                    .ToList();

////                dataGridView1.DataSource = dsQuanLy;
////                dataGridView2.DataSource = dsNhanVien;
////            }
////        }

////        // Hiển thị số thứ tự
////        private void DataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
////        {
////            var dgv = sender as DataGridView;
////            if (dgv != null)
////            {
////                dgv.Rows[e.RowIndex].Cells["STT"].Value = (e.RowIndex + 1).ToString();
////            }
////        }

////        // Click icon ở dataGridView2: Nhân viên -> Quản lý
////        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
////        {
////            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView2.Columns["ThayDoi"].Index)
////            {
////                string tenDangNhap = dataGridView2.Rows[e.RowIndex].Cells["TenDangNhap"].Value?.ToString();
////                if (string.IsNullOrEmpty(tenDangNhap)) return;

////                using (var db = new Model1())
////                {
////                    var nv = db.NHAN_VIEN.FirstOrDefault(x => x.TENDANGNHAP == tenDangNhap);
////                    if (nv != null)
////                    {
////                        nv.LAQUANLY = true;
////                        db.SaveChanges();
////                        MessageBox.Show("Đã phân quyền nhân viên thành quản lý!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
////                        LoadData();
////                    }
////                }
////            }
////        }

////        // Click icon ở dataGridView1: Quản lý -> Nhân viên (chỉ admin mới được)
////        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
////        {
////            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["ThayDoi"].Index)
////            {
////                string tenDangNhap = dataGridView1.Rows[e.RowIndex].Cells["TenDangNhap"].Value?.ToString();
////                bool isAdminRow = tenDangNhap.ToLower() == "admin";
////                if (isAdminRow)
////                {
////                    MessageBox.Show("Không thể thay đổi quyền của tài khoản admin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
////                    return;
////                }

////                if (currentUser.ToLower() != "admin")
////                {
////                    MessageBox.Show("Chỉ tài khoản admin mới có quyền chuyển quản lý thành nhân viên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
////                    return;
////                }

////                using (var db = new Model1())
////                {
////                    var nv = db.NHAN_VIEN.FirstOrDefault(x => x.TENDANGNHAP == tenDangNhap);
////                    if (nv != null)
////                    {
////                        nv.LAQUANLY = false;
////                        db.SaveChanges();
////                        MessageBox.Show("Đã chuyển quản lý thành nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
////                        LoadData();
////                    }
////                }
////            }
////        }
////    }
////}


//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Windows.Forms;
//using KiemTraiGiuaKy.Entity;

//namespace KiemTraiGiuaKy
//{
//    public partial class frm_phanQuyen : Form
//    {
//        private string currentUser = ""; // Lưu tên đăng nhập hiện tại

//        public frm_phanQuyen(string username = "")
//        {
//            InitializeComponent();
//            currentUser = username;
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
//                Width = 50,
//                ImageLayout = DataGridViewImageCellLayout.Zoom
//            };
//            iconCol.Image = Properties.Resources.edit_icon;
//            dgv.Columns.Add(iconCol);

//            dgv.Columns.Add(new DataGridViewTextBoxColumn
//            {
//                DataPropertyName = "TenDangNhap",
//                Name = "TenDangNhap",
//                HeaderText = "TÊN ĐĂNG NHẬP",
//                ReadOnly = true
//            });
//            dgv.Columns.Add(new DataGridViewTextBoxColumn
//            {
//                DataPropertyName = "TenNhanVien",
//                Name = "TenNhanVien",
//                HeaderText = "TÊN NHÂN VIÊN",
//                ReadOnly = true
//            });
//            dgv.Columns.Add(new DataGridViewTextBoxColumn
//            {
//                DataPropertyName = "SoDienThoai",
//                Name = "SoDienThoai",
//                HeaderText = "SỐ ĐIỆN THOẠI",
//                ReadOnly = true
//            });
//            dgv.Columns.Add(new DataGridViewTextBoxColumn
//            {
//                DataPropertyName = "GioiTinh",
//                Name = "GioiTinh",
//                HeaderText = "GIỚI TÍNH",
//                ReadOnly = true
//            });
//            dgv.Columns.Add(new DataGridViewTextBoxColumn
//            {
//                DataPropertyName = "NgaySinh",
//                Name = "NgaySinh",
//                HeaderText = "NGÀY SINH",
//                ReadOnly = true,
//                DefaultCellStyle = { Format = "dd/MM/yyyy" }
//            });

//            dgv.RowPostPaint += DataGridView_RowPostPaint;

//            if (isManager)
//                dgv.CellClick += DataGridView1_CellClick;
//            else
//                dgv.CellClick += DataGridView2_CellClick;
//        }

//        private void LoadData()
//        {
//            using (var db = new Model1())
//            {
//                // Quản lý
//                var dsQuanLy = db.NHAN_VIEN
//                    .Where(nv => (nv.VOHIEUHOA == false || nv.VOHIEUHOA == null) && nv.LAQUANLY == true)
//                    .Select(nv => new
//                    {
//                        TenDangNhap = nv.TENDANGNHAP,
//                        TenNhanVien = nv.TENNHANVIEN,
//                        SoDienThoai = nv.SDT,
//                        GioiTinh = nv.GIOITINH,
//                        NgaySinh = nv.NGAYSINH,
//                        IsAdmin = nv.TENDANGNHAP.ToLower() == "admin"
//                    })
//                    .ToList();

//                // Nhân viên
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

//        // Yêu cầu nhập mật khẩu xác nhận
//        private bool ConfirmPassword()
//        {
//            using (var inputForm = new frmNhapMatKhau())
//            {
//                if (inputForm.ShowDialog() == DialogResult.OK)
//                {
//                    string inputPassword = inputForm.Password;
//                    using (var db = new Model1())
//                    {
//                        var user = db.NHAN_VIEN.FirstOrDefault(x => x.TENDANGNHAP == currentUser && x.MATKHAU == inputPassword && (x.VOHIEUHOA == false || x.VOHIEUHOA == null));
//                        if (user != null)
//                            return true;
//                    }
//                    MessageBox.Show("Mật khẩu xác nhận không đúng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                }
//            }
//            return false;
//        }

//        // Click icon ở dataGridView2: Nhân viên -> Quản lý
//        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
//        {
//            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView2.Columns["ThayDoi"].Index)
//            {
//                string tenDangNhap = dataGridView2.Rows[e.RowIndex].Cells["TenDangNhap"].Value?.ToString();
//                if (string.IsNullOrEmpty(tenDangNhap)) return;

//                if (!ConfirmPassword()) return;

//                using (var db = new Model1())
//                {
//                    var nv = db.NHAN_VIEN.FirstOrDefault(x => x.TENDANGNHAP == tenDangNhap);
//                    if (nv != null)
//                    {
//                        nv.LAQUANLY = true;
//                        db.SaveChanges();
//                        MessageBox.Show("Đã phân quyền nhân viên thành quản lý!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                        LoadData();
//                    }
//                }
//            }
//        }

//        // Click icon ở dataGridView1: Quản lý -> Nhân viên (chỉ admin mới được)
//        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
//        {
//            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["ThayDoi"].Index)
//            {
//                string tenDangNhap = dataGridView1.Rows[e.RowIndex].Cells["TenDangNhap"].Value?.ToString();
//                bool isAdminRow = tenDangNhap.ToLower() == "admin";
//                if (isAdminRow)
//                {
//                    MessageBox.Show("Không thể thay đổi quyền của tài khoản admin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }

//                if (currentUser.ToLower() != "admin")
//                {
//                    MessageBox.Show("Chỉ tài khoản admin mới có quyền chuyển quản lý thành nhân viên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }

//                if (!ConfirmPassword()) return;

//                using (var db = new Model1())
//                {
//                    var nv = db.NHAN_VIEN.FirstOrDefault(x => x.TENDANGNHAP == tenDangNhap);
//                    if (nv != null)
//                    {
//                        nv.LAQUANLY = false;
//                        db.SaveChanges();
//                        MessageBox.Show("Đã chuyển quản lý thành nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                        LoadData();
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
using KiemTraiGiuaKy.Entity; // Đảm bảo namespace này đúng với Model của bạn

namespace KiemTraiGiuaKy
{
    public partial class frm_phanQuyen : Form
    {
        // Lưu tên đăng nhập của người dùng hiện tại (người đang thao tác trên form phân quyền)
        private string currentUser = "";

        public frm_phanQuyen(string username = "")
        {
            InitializeComponent();
            currentUser = username; // Nhận tên đăng nhập từ form đăng nhập
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
                Width = 50,
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };
            // Đảm bảo bạn đã thêm 'edit_icon' vào Resources của project
            // Chuột phải vào project > Properties > Resources > Add Resource > Add Existing File
            iconCol.Image = Properties.Resources.edit_icon;
            dgv.Columns.Add(iconCol);

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenDangNhap",
                Name = "TenDangNhap",
                HeaderText = "TÊN ĐĂNG NHẬP",
                ReadOnly = true
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenNhanVien",
                Name = "TenNhanVien",
                HeaderText = "TÊN NHÂN VIÊN",
                ReadOnly = true
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoDienThoai",
                Name = "SoDienThoai",
                HeaderText = "SỐ ĐIỆN THOẠI",
                ReadOnly = true
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GioiTinh",
                Name = "GioiTinh",
                HeaderText = "GIỚI TÍNH",
                ReadOnly = true
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NgaySinh",
                Name = "NgaySinh",
                HeaderText = "NGÀY SINH",
                ReadOnly = true,
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
            using (var db = new Model1()) // Sử dụng "Model1"
            {
                // Danh sách Quản lý
                var dsQuanLy = db.NHAN_VIEN
                    .Where(nv => (nv.VOHIEUHOA == false || nv.VOHIEUHOA == null) && nv.LAQUANLY == true)
                    .Select(nv => new
                    {
                        TenDangNhap = nv.TENDANGNHAP,
                        TenNhanVien = nv.TENNHANVIEN,
                        SoDienThoai = nv.SDT,
                        GioiTinh = nv.GIOITINH,
                        NgaySinh = nv.NGAYSINH,
                        IsAdmin = nv.TENDANGNHAP.ToLower() == "admin" // Thêm cột IsAdmin để dễ kiểm tra
                    })
                    .ToList();

                // Danh sách Nhân viên
                var dsNhanVien = db.NHAN_VIEN
                    .Where(nv => (nv.VOHIEUHOA == false || nv.VOHIEUHOA == null) && (nv.LAQUANLY == false || nv.LAQUANLY == null))
                    .Select(nv => new
                    {
                        TenDangNhap = nv.TENDANGNHAP,
                        TenNhanVien = nv.TENNHANVIEN,
                        SoDienThoai = nv.SDT,
                        GioiTinh = nv.GIOITINH,
                        NgaySinh = nv.NGAYSINH
                    })
                    .ToList();

                dataGridView1.DataSource = dsQuanLy;
                dataGridView2.DataSource = dsNhanVien;
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

        // Yêu cầu nhập mật khẩu xác nhận của người dùng HIỆN TẠI (currentUser)
        private bool ConfirmPassword()
        {
            using (var inputForm = new frmNhapMatKhau())
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    string inputPassword = inputForm.Password;
                    using (var db = new Model1()) // Sử dụng "Model1"
                    {
                        // Kiểm tra mật khẩu nhập vào có đúng với mật khẩu của currentUser không
                        var user = db.NHAN_VIEN.FirstOrDefault(x =>
                            x.TENDANGNHAP == currentUser &&
                            x.MATKHAU == inputPassword &&
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

                // Bất kỳ quản lý nào cũng có thể chuyển nhân viên thành quản lý (bao gồm admin)
                // Yêu cầu xác nhận mật khẩu của người dùng hiện tại (currentUser)
                if (!ConfirmPassword()) return;

                using (var db = new Model1()) // Sử dụng "Model1"
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

                using (var db = new Model1()) // Sử dụng "Model1"
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
    }

    // Form nhập mật khẩu xác nhận
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