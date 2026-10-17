using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLySinhVien_3Layer.BUL;
using QuanLySinhVien_3Layer.Data.Entity;

namespace QuanLySinhVien_3Layer
{
    public partial class Form1 : Form
    {
        private readonly SinhVienBUL _sinhVienBUL;
        private readonly LopBUS _lopBUS;

        public Form1()
        {
            InitializeComponent();
            _sinhVienBUL = new SinhVienBUL();
            _lopBUS = new LopBUS();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                KhoiTaoDuLieuCombobox();
                CauHinhDataGridView();
                HienThiDanhSachSinhVien();
                XoaTrangThongTin();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu MySQL:\n" + ex.Message + "\n\nVui lòng kiểm tra lại MySQL Server (XAMPP/MySQL service) và chuỗi kết nối trong App.config.", 
                    "Lỗi kết nối CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KhoiTaoDuLieuCombobox()
        {
            try
            {
                // Danh sách lớp cho comboBox1 (thông tin sinh viên)
                var dsLop = _lopBUS.GetAll();
                comboBox1.DisplayMember = "TenLop";
                comboBox1.ValueMember = "MaLop";
                comboBox1.DataSource = new List<Lop>(dsLop);

                // Danh sách lớp cho comboBox3 (tìm kiếm) có thêm tùy chọn Tất cả
                var dsLopTimKiem = new List<Lop>
                {
                    new Lop("ALL", "-- Tất cả lớp --")
                };
                dsLopTimKiem.AddRange(dsLop);
                comboBox3.DisplayMember = "TenLop";
                comboBox3.ValueMember = "MaLop";
                comboBox3.DataSource = dsLopTimKiem;
                comboBox3.SelectedIndex = 0;

                // Trạng thái sinh viên cho comboBox2
                comboBox2.Items.Clear();
                comboBox2.Items.Add("Đang học");
                comboBox2.Items.Add("Bảo lưu");
                comboBox2.Items.Add("Đã tốt nghiệp");
                comboBox2.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo dữ liệu Combobox: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CauHinhDataGridView()
        {
            dgvSinhVien.AutoGenerateColumns = false;
            dgvSinhVien.Columns.Clear();

            dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaSV",
                HeaderText = "Mã SV",
                Name = "MaSV",
                FillWeight = 80
            });

            dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HoTen",
                HeaderText = "Họ và tên",
                Name = "HoTen",
                FillWeight = 140
            });

            dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NgaySinh",
                HeaderText = "Ngày sinh",
                Name = "NgaySinh",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" },
                FillWeight = 90
            });

            dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GioiTinh",
                HeaderText = "Giới tính",
                Name = "GioiTinh",
                FillWeight = 70
            });

            dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Email",
                HeaderText = "Email",
                Name = "Email",
                FillWeight = 130
            });

            dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DienThoai",
                HeaderText = "Điện thoại",
                Name = "DienThoai",
                FillWeight = 90
            });

            dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenLop",
                HeaderText = "Lớp học",
                Name = "TenLop",
                FillWeight = 130
            });

            dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Diem",
                HeaderText = "Điểm",
                Name = "Diem",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N1" },
                FillWeight = 60
            });

            dgvSinhVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TrangThai",
                HeaderText = "Trạng thái",
                Name = "TrangThai",
                FillWeight = 100
            });
        }

        private void HienThiDanhSachSinhVien(List<SinhVien> danhSach = null)
        {
            try
            {
                if (danhSach == null)
                {
                    danhSach = _sinhVienBUL.GetAll();
                }

                dgvSinhVien.DataSource = null;
                dgvSinhVien.DataSource = danhSach;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị danh sách sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XoaTrangThongTin()
        {
            textBox1.Clear();
            textBox4.Clear();
            dateTimePicker1.Value = new DateTime(2003, 1, 1);
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            textBox5.Clear();
            textBox3.Clear();
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
            numericUpDown1.Value = 0;
            if (comboBox2.Items.Count > 0) comboBox2.SelectedIndex = 0;

            textBox1.ReadOnly = false;
            textBox1.Focus();
        }

        private SinhVien ThuThapThongTinTuForm()
        {
            string maSV = textBox1.Text.Trim();
            string hoTen = textBox4.Text.Trim();
            DateTime ngaySinh = dateTimePicker1.Value.Date;
            string gioiTinh = radioButton2.Checked ? "Nữ" : "Nam";
            string email = textBox5.Text.Trim();
            string dienThoai = textBox3.Text.Trim();

            string maLop = string.Empty;
            string tenLop = string.Empty;

            Lop selectedLop = comboBox1.SelectedItem as Lop;
            if (selectedLop != null)
            {
                maLop = selectedLop.MaLop;
                tenLop = selectedLop.TenLop;
            }
            else if (comboBox1.SelectedValue != null)
            {
                maLop = comboBox1.SelectedValue.ToString();
                tenLop = comboBox1.Text;
            }

            if (string.IsNullOrWhiteSpace(maLop) && !string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                var dsLop = _lopBUS.GetAll();
                var foundLop = dsLop.FirstOrDefault(l =>
                    l.MaLop.Equals(comboBox1.Text.Trim(), StringComparison.OrdinalIgnoreCase) ||
                    l.TenLop.Equals(comboBox1.Text.Trim(), StringComparison.OrdinalIgnoreCase));
                if (foundLop != null)
                {
                    maLop = foundLop.MaLop;
                    tenLop = foundLop.TenLop;
                }
            }

            decimal diem = numericUpDown1.Value;
            string trangThai = comboBox2.SelectedItem != null ? comboBox2.SelectedItem.ToString() : (string.IsNullOrWhiteSpace(comboBox2.Text) ? "Đang học" : comboBox2.Text.Trim());

            return new SinhVien(maSV, hoTen, ngaySinh, gioiTinh, email, dienThoai, maLop, tenLop, diem, trangThai);
        }

        // Nút Thêm
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var sv = ThuThapThongTinTuForm();
                _sinhVienBUL.Them(sv);
                MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                HienThiDanhSachSinhVien();
                XoaTrangThongTin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi thêm sinh viên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Nút Sửa
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Vui lòng chọn sinh viên cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var sv = ThuThapThongTinTuForm();
                _sinhVienBUL.Sua(sv);
                MessageBox.Show("Cập nhật thông tin sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                HienThiDanhSachSinhVien();
                XoaTrangThongTin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi cập nhật sinh viên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Nút Xoá
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string maSV = textBox1.Text.Trim();
                if (string.IsNullOrWhiteSpace(maSV))
                {
                    MessageBox.Show("Vui lòng chọn sinh viên cần xoá từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show("Bạn có chắc chắn muốn xoá sinh viên mã '" + maSV + "' không?", 
                    "Xác nhận xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _sinhVienBUL.Xoa(maSV);
                    MessageBox.Show("Xoá sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HienThiDanhSachSinhVien();
                    XoaTrangThongTin();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi khi xoá", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Nút Làm mới
        private void button4_Click(object sender, EventArgs e)
        {
            XoaTrangThongTin();
        }

        // Nút Tìm kiếm
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string tuKhoa = textBox2.Text.Trim();

                string maLop = "ALL";
                Lop selectedLop = comboBox3.SelectedItem as Lop;
                if (selectedLop != null)
                {
                    maLop = selectedLop.MaLop;
                }
                else if (comboBox3.SelectedValue != null)
                {
                    maLop = comboBox3.SelectedValue.ToString();
                }

                decimal? diemTu = null;
                if (!string.IsNullOrWhiteSpace(textBox6.Text))
                {
                    decimal d;
                    if (decimal.TryParse(textBox6.Text.Trim().Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out d))
                    {
                        if (d < 0 || d > 10)
                        {
                            MessageBox.Show("Điểm từ tìm kiếm phải nằm trong khoảng từ 0 đến 10!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        diemTu = d;
                    }
                    else
                    {
                        MessageBox.Show("Điểm từ tìm kiếm phải là số hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                var ketQua = _sinhVienBUL.TimKiem(tuKhoa, maLop, diemTu);
                HienThiDanhSachSinhVien(ketQua);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút Hiển thị tất cả
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                textBox2.Clear();
                if (comboBox3.Items.Count > 0) comboBox3.SelectedIndex = 0;
                textBox6.Clear();
                HienThiDanhSachSinhVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lại danh sách sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Click vào 1 dòng trong DataGridView
        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgvSinhVien.Rows.Count)
                {
                    var row = dgvSinhVien.Rows[e.RowIndex];
                    var sv = row.DataBoundItem as SinhVien;
                    if (sv != null)
                    {
                        textBox1.Text = sv.MaSV;
                        textBox4.Text = sv.HoTen;
                        dateTimePicker1.Value = sv.NgaySinh >= dateTimePicker1.MinDate && sv.NgaySinh <= dateTimePicker1.MaxDate ? sv.NgaySinh : DateTime.Now;
                        
                        if (sv.GioiTinh != null && sv.GioiTinh.Equals("Nữ", StringComparison.OrdinalIgnoreCase))
                        {
                            radioButton2.Checked = true;
                        }
                        else
                        {
                            radioButton1.Checked = true;
                        }

                        textBox5.Text = sv.Email;
                        textBox3.Text = sv.DienThoai;
                        
                        if (!string.IsNullOrEmpty(sv.MaLop))
                        {
                            comboBox1.SelectedValue = sv.MaLop;
                            if (comboBox1.SelectedIndex < 0)
                            {
                                for (int i = 0; i < comboBox1.Items.Count; i++)
                                {
                                    Lop lop = comboBox1.Items[i] as Lop;
                                    if (lop != null &&
                                        (string.Equals(lop.MaLop, sv.MaLop, StringComparison.OrdinalIgnoreCase) ||
                                         string.Equals(lop.TenLop, sv.TenLop, StringComparison.OrdinalIgnoreCase)))
                                    {
                                        comboBox1.SelectedIndex = i;
                                        break;
                                    }
                                }
                            }
                        }

                        numericUpDown1.Value = Math.Max(numericUpDown1.Minimum, Math.Min(numericUpDown1.Maximum, sv.Diem));
                        
                        if (!string.IsNullOrEmpty(sv.TrangThai))
                        {
                            int idx = comboBox2.FindStringExact(sv.TrangThai);
                            if (idx >= 0)
                            {
                                comboBox2.SelectedIndex = idx;
                            }
                            else
                            {
                                comboBox2.Text = sv.TrangThai;
                            }
                        }

                        textBox1.ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị thông tin sinh viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Các sự kiện mặc định giữ lại để không lỗi Designer
        private void label1_Click(object sender, EventArgs e) { }
        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label11_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void radioButton1_CheckedChanged(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void groupBox3_Enter(object sender, EventArgs e) { }
        #endregion
    }
}
