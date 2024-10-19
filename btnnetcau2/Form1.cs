using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
namespace btnnetcau2
{
    public partial class Form1 : Form
    {
        private List<thaodao> nv = new List<thaodao>();
        private const string filePath = "nv.json";
        public Form1()
        {
            InitializeComponent();
            LoadthaodaoData();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this);
            form2.ShowDialog();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        dataGridView1.Rows.Remove(row);
                        
                    }
                    UpdateJsonFile();
                    richTextBox1.Clear();
                    pictureBoxAvatar.Image = null;
                    MessageBox.Show("xoa thanh cong");
                }
                
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa.");
            }
        }

        public void Addthaodao(thaodao emp)
        {
            nv.Add(emp);
            dataGridView1.Rows.Add(emp.ID, emp.hoten, emp.namsinh.ToShortDateString(), emp.gioitinh, emp.bophan, emp.chucvu, emp.hopdong, emp.diachi, emp.ngayra.ToShortDateString(), emp.ngayvao.ToShortDateString(), emp.dantoc, emp.cccd, emp.sdt, emp.email,emp.Avatar);
             SavethaodaoData();
        }

        private void LoadthaodaoData()
        {
            string filePath = "nv.json";
            List<thaodao> nv = new List<thaodao>();

            if (File.Exists(filePath))
            {
                try
                {
                    MessageBox.Show("Đang kiểm tra dữ liệu...");

                    string jsonData = File.ReadAllText(filePath);


                    if (!string.IsNullOrEmpty(jsonData))
                    {
                        nv = JsonConvert.DeserializeObject<List<thaodao>>(jsonData);

                        if (nv != null)
                        {
                          
                            foreach (var emp in nv)
                            {
                                dataGridView1.Rows.Add(emp.ID, emp.hoten, emp.namsinh.ToShortDateString(), emp.gioitinh, emp.bophan, emp.chucvu, emp.hopdong, emp.diachi, emp.ngayra.ToShortDateString(), emp.ngayvao.ToShortDateString(), emp.dantoc, emp.cccd, emp.sdt, emp.email,emp.Avatar);
                            }

                        }
                       
                    }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra khi tải dữ liệu: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("File dữ liệu nv.json không tồn tại.");
            }
        }

        private void SavethaodaoData()
        {
            string json = JsonConvert.SerializeObject(nv, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                StringBuilder rowData = new StringBuilder();
                rowData.AppendLine("ID: " + row.Cells["ID"].Value?.ToString());
                rowData.AppendLine("Họ tên: " + row.Cells["hoten"].Value?.ToString());
                rowData.AppendLine("Năm sinh: " + row.Cells["namsinh"].Value?.ToString());
                rowData.AppendLine("Giới tính: " + row.Cells["gioitinh"].Value?.ToString());
                rowData.AppendLine("Bộ phận: " + row.Cells["bophan"].Value?.ToString());
                rowData.AppendLine("Chức vụ: " + row.Cells["chucvu"].Value?.ToString());
                rowData.AppendLine("Hợp đồng: " + row.Cells["hopdong"].Value?.ToString());
                rowData.AppendLine("Địa chỉ: " + row.Cells["diachi"].Value?.ToString());
                rowData.AppendLine("Ngày ra: " + row.Cells["ngayra"].Value?.ToString());
                rowData.AppendLine("Ngày vào: " + row.Cells["ngayvao"].Value?.ToString());
                rowData.AppendLine("Dân tộc: " + row.Cells["dantoc"].Value?.ToString());
                rowData.AppendLine("CCCD: " + row.Cells["cccd"].Value?.ToString());
                rowData.AppendLine("Số điện thoại: " + row.Cells["sdt"].Value?.ToString());
                rowData.AppendLine("Email: " + row.Cells["email"].Value?.ToString());

                richTextBox1.Text = rowData.ToString();
                string avatar = row.Cells["Avatar"].Value?.ToString(); 
                if (!string.IsNullOrEmpty(avatar) && File.Exists(avatar))
                {
                    pictureBoxAvatar.Image = Image.FromFile(avatar);
                }
                else
                {
                    pictureBoxAvatar.Image = null; 
                }
            }
        }

        private void btntrc_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;

                if (index > 0)
                {


                    
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[index - 1].Selected = true;

                    dataGridView1_CellClick_1(null, new DataGridViewCellEventArgs(0, index - 1));

                    SavethaodaoData();
                }
                else
                {
                    MessageBox.Show("Đây là hàng đầu tiên, không thể di chuyển lên nữa.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng .");
            }
        }

        private void btnsau_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
        
                if (index < nv.Count - 1)
                {

                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[index + 1].Selected = true;

                    dataGridView1_CellClick_1(null, new DataGridViewCellEventArgs(0, index + 1));

                    SavethaodaoData();
                }
                else
                {
                    MessageBox.Show("Đây là hàng cuối cùng, không thể di chuyển xuống nữa.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng để di chuyển.");
            }

        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void UpdateJsonFile()
        {
            string filePath = "nv.json";
            List<thaodao> currentData = new List<thaodao>();

            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                if (!string.IsNullOrEmpty(jsonData))
                {
                    currentData = JsonConvert.DeserializeObject<List<thaodao>>(jsonData);
                }
            }

            // Cập nhật danh sách currentData từ DataGridView
            currentData.Clear();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue; 

                thaodao emp = new thaodao
                {
                    ID = row.Cells["ID"].Value?.ToString(),
                    hoten = row.Cells["hoten"].Value?.ToString(),
                    namsinh = Convert.ToDateTime(row.Cells["namsinh"].Value),
                    gioitinh = row.Cells["gioitinh"].Value?.ToString(),
                    bophan = row.Cells["bophan"].Value?.ToString(),
                    chucvu = row.Cells["chucvu"].Value?.ToString(),
                    hopdong = row.Cells["hopdong"].Value?.ToString(),
                    diachi = row.Cells["diachi"].Value?.ToString(),
                    ngayra = Convert.ToDateTime(row.Cells["ngayra"].Value),
                    ngayvao = Convert.ToDateTime(row.Cells["ngayvao"].Value),
                    dantoc = row.Cells["dantoc"].Value?.ToString(),
                    cccd = row.Cells["cccd"].Value?.ToString(),
                    sdt = row.Cells["sdt"].Value?.ToString(),
                    email = row.Cells["email"].Value?.ToString(),
                    Avatar = row.Cells["Avatar"].Value?.ToString()
                };

                currentData.Add(emp);
            }

            string updatedJson = JsonConvert.SerializeObject(currentData, Formatting.Indented);
            File.WriteAllText(filePath, updatedJson);
        }

    }
} 



