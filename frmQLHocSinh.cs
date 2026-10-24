using System;
using System.Linq;
using System.Windows.Forms;

namespace CSLab08_LINQ
{
    public partial class frmQLHocSinh : Form
    {
        QLHSDataContext db = new QLHSDataContext();

        public frmQLHocSinh()
        {
            InitializeComponent();
        }

        private void frmQLHocSinh_Load(object sender, EventArgs e)
        {
            cboLop.DataSource = db.Lops;
            cboLop.DisplayMember = "TenLop";
            cboLop.ValueMember = "MaLop";
            fillHocSinhByLop();
        }

        private void fillHocSinhByLop()
        {
            if (cboLop.SelectedValue != null)
            {
                var dsHocSinh = db.HocSinhs.Where(p => p.MaLop == cboLop.SelectedValue.ToString() && p.TenHS.Contains(txtTenHS.Text.Trim()));
                dgvHocSinh.DataSource = dsHocSinh.Select(p => new { p.MaHS, p.TenHS, p.Lop.TenLop, p.DTB });
            }
        }

        private void cboLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillHocSinhByLop();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            fillHocSinhByLop();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            HocSinh hs = new HocSinh
            {
                MaHS = txtMaHS.Text,
                TenHS = txtTenHS.Text,
                NgaySinh = dtpNgaySinh.Value,
                DiaChi = txtDiaChi.Text,
                MaLop = cboLop.SelectedValue.ToString(),
                DTB = float.Parse(txtDiemTB.Text)
            };

            db.HocSinhs.InsertOnSubmit(hs);
            db.SubmitChanges();
            fillHocSinhByLop();
            MessageBox.Show("Thêm học sinh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            HocSinh hs = db.HocSinhs.SingleOrDefault(p => p.MaHS == txtMaHS.Text);
            if (hs != null)
            {
                db.HocSinhs.DeleteOnSubmit(hs);
                db.SubmitChanges();
                fillHocSinhByLop();
                MessageBox.Show("Xóa học sinh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không tìm thấy học sinh cần xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            HocSinh hs = db.HocSinhs.SingleOrDefault(p => p.MaHS == txtMaHS.Text);
            if (hs != null)
            {
                hs.MaLop = cboLop.SelectedValue.ToString();
                hs.TenHS = txtTenHS.Text;
                hs.NgaySinh = dtpNgaySinh.Value;
                hs.DiaChi = txtDiaChi.Text;
                hs.DTB = float.Parse(txtDiemTB.Text);

                db.SubmitChanges();
                fillHocSinhByLop();
                MessageBox.Show("Cập nhật học sinh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không tìm thấy học sinh cần sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}