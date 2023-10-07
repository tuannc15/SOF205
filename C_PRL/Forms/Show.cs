using A_DAL.Models;
using B_BUS.Services;
using B_BUS.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_PRL.Forms
{
    public partial class Show : Form
    {
        ChildServices _service = new ChildServices();
        int selectedID = -1; // Tạo 1 thuộc tính ID để khi chọn vào 1 row bất kì ta lấy được ID
        public Show()
        {
            _service = new ChildServices();
            InitializeComponent();
        }

        private void btn_Show_Click(object sender, EventArgs e)
        {
            LoadData(_service.GetAllChild());
        }

        public void LoadData(dynamic data) // dynamic là kiểu dữ liệu linh hoạt cho phép nhận mọi dữ liệu
        {
            dtg_Show.Rows.Clear();
            dtg_Show.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Auto kích cỡ
            int stt = 1; // đánh số thứ tự tự tăng
            dtg_Show.ColumnCount = 6; // đánh xem datagrid view sẽ có bao nhiêu cột
            // ĐỊnh dạng datagridview (Tên cột, Text hiển thị của cột
            dtg_Show.Columns[0].Name = "stt"; dtg_Show.Columns[0].HeaderText = "Số thứ tự";
            dtg_Show.Columns[1].Name = "name"; dtg_Show.Columns[1].HeaderText = "Tên";
            dtg_Show.Columns[2].Name = "age"; dtg_Show.Columns[2].HeaderText = "Tuổi";
            dtg_Show.Columns[3].Name = "address"; dtg_Show.Columns[3].HeaderText = "Địa chỉ";
            dtg_Show.Columns[4].Name = "sex"; dtg_Show.Columns[4].HeaderText = "Giới tính";
            dtg_Show.Columns[5].Name = "id";
            dtg_Show.Columns[5].Visible = false; // ẩn cột ID để không thấy
            // dtg_Show.DataSource = _service.GetAllChild();
            // Đẩy data vào dtg
            foreach (var item in data)
            {
                dtg_Show.Rows.Add(stt++, item.Name, item.Age, item.Address,
                    item.Sex ? "Nam" : "Nữ", item.ChildId); // Dùng toán tử 3 ngôi để định giới tính
            }
            // Dùng toán tử 3 ngôi để định giới tính
        }

        private void tbt_Search_TextChanged(object sender, EventArgs e)
        {
            LoadData(_service.GetChildByName(tbt_Search.Text));
        }

        private void dtg_Show_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Lấy được data từ Cell và Lấy được ID của đối tượng mà mình chọn lựa
            int index = e.RowIndex;
            var selectedChild = dtg_Show.Rows[index]; // Lấy data từ index được chọn
            tbt_Name.Text = selectedChild.Cells[1].Value.ToString();
            tbt_Age.Text = selectedChild.Cells[2].Value.ToString();
            tbt_Address.Text = selectedChild.Cells[3].Value.ToString();
            if (selectedChild.Cells[4].Value.ToString() == "Nam")
            {
                rb_Male.Checked = true; rb_Female.Checked = false;
            }
            else
            {
                rb_Female.Checked = true; rb_Male.Checked = false;
            }
            // cbb_Parent.Text = selectedChild.Cells[4].Value.ToString(); Tính sau vì cần join bảng
        }

        private void dtg_Show_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Lấy được data từ Cell và Lấy được ID của đối tượng mà mình chọn lựa
            int index = e.RowIndex;
            var selectedChild = dtg_Show.Rows[index]; // Lấy data từ index được chọn
            tbt_Name.Text = selectedChild.Cells[1].Value.ToString();
            tbt_Age.Text = selectedChild.Cells[2].Value.ToString();
            tbt_Address.Text = selectedChild.Cells[3].Value.ToString();
            selectedID = Convert.ToInt32(selectedChild.Cells[5].Value); // Lấy ID khi select 1 row mình chọn
            if (selectedChild.Cells[4].Value.ToString() == "Nam")
            {
                rb_Male.Checked = true; rb_Female.Checked = false;
            }
            else
            {
                rb_Female.Checked = true; rb_Male.Checked = false;
            }
            // cbb_Parent.Text = selectedChild.Cells[4].Value.ToString(); Tính sau vì cần join bảng
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            // Lấy data từ form sao cho phù hợp với service  
            string name = tbt_Name.Text;
            string age = tbt_Age.Text;
            string address = tbt_Address.Text;
            bool sex = rb_Male.Checked;
            int parentID = 1; // gán cứng tạm hôm sau tính tiếp
            // Validate trước khi thêm
            if(!Validates.ValidateInt(age) || !Validates.ValidateString(name))
            {
                MessageBox.Show("Hãy kiểm tra lại dữ liệu"); 
                return; // return để dừng luôn hành động
            }
            bool add = _service.AddNewChild(name, age, address, sex, parentID);
            if (add) MessageBox.Show("Thêm thành công!");
            else MessageBox.Show("Thất bại");
            LoadData(_service.GetAllChild());
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Bạn có muốn xóa không?", "Xác nhận",
                MessageBoxButtons.YesNoCancel);
            if(DialogResult == DialogResult.Yes)
            {
                // Lấy được ID cần xóa => Cột đã bị ẩn của datagridview 
                var deleteResult = _service.DeleteChild(selectedID);
                if (deleteResult) MessageBox.Show("Xóa thành công");
                else MessageBox.Show("Thất bại");
                LoadData(_service.GetAllChild());
            }else
            {
                return;
            }        
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            string name = tbt_Name.Text;
            string age = tbt_Age.Text;
            string address = tbt_Address.Text;
            bool sex = rb_Male.Checked;
            int Id = selectedID; // id đã được lấy ra mỗi lần cell click
            if (_service.UpdateChild(Id, name, age, address, sex, 1))// fix cứng parenID là 1
            {
                MessageBox.Show("Sửa thành công");
                LoadData(_service.GetAllChild());
            }
            else
            {
                MessageBox.Show("Sửa thất bại, mời kiểm tra lại");
            }
        }
    }
}
