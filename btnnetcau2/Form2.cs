using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;

namespace btnnetcau2
{
    public partial class Form2 : Form
    {
        private Form1 mainForm;
        public Form2(Form1 form1)
        {
            InitializeComponent();
            mainForm = form1;
            this.Load += Form2_Load;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void Form2_Load(object sender, EventArgs e)
        {

            comboBoxgioitinh.Items.Add("Nam");
            comboBoxgioitinh.Items.Add("Nữ");
            comboBoxgioitinh.Items.Add("Khác");

            comboBoxgioitinh.SelectedIndex = 0;

            comboBoxdantoc.Items.Add("Kinh");
            comboBoxdantoc.Items.Add("Tày");
            comboBoxdantoc.Items.Add("Thái");
            comboBoxdantoc.Items.Add("Mường");
            comboBoxdantoc.Items.Add("H'Mông");
            comboBoxdantoc.Items.Add("Khơ Me");
            comboBoxdantoc.Items.Add("Khác");

            comboBoxdantoc.SelectedIndex = 0;
        }

        private void btnluu_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtid.Text) || string.IsNullOrEmpty(txtten.Text) || comboBoxgioitinh.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng điền vào tất cả các thông tin bắt buộc.");
                return;
            }

            thaodao emp = new thaodao
            {
                ID = txtid.Text,
                hoten = txtten.Text,
                namsinh = dateTimePickernamsinh.Value,
                gioitinh = comboBoxgioitinh.SelectedItem.ToString(),
                bophan = txtbophan.Text,
                chucvu = txtchuc.Text,
                hopdong = txthopdong.Text,
                diachi = txtdiachi.Text,
                ngayra = dateTimePickerngayra.Value,
                ngayvao = dateTimePickerngayvao.Value,
                dantoc = comboBoxdantoc.SelectedItem.ToString(),
                cccd = txtcccd.Text,
                sdt = txtsdt.Text,
                email = txtmail.Text,
                Avatar = selectedAvatar,
            };

          
            mainForm.Addthaodao(emp);

            string filePath = "nv.json";
            List<thaodao> currentData = new List<thaodao>();
            MessageBox.Show("Đã thêm thành công!");
            try
            {
                // Kiểm tra nếu file không tồn tại
                if (!File.Exists(filePath))
                {
                    // Nếu file không tồn tại, tạo file mới
                    using (FileStream fs = File.Create(filePath))
                    {
                       
                    }
                }

                // Đọc dữ liệu cũ từ file JSON nếu có
                string jsonData = File.ReadAllText(filePath);
                if (!string.IsNullOrEmpty(jsonData))
                {
                    currentData = JsonConvert.DeserializeObject<List<thaodao>>(jsonData);
                }

                currentData.Add(emp);

                string updatedJson = JsonConvert.SerializeObject(currentData, Formatting.Indented);

                File.WriteAllText(filePath, updatedJson);

                MessageBox.Show("Đã lưu thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message);
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            txtid.Clear();
            txtten.Clear();
            comboBoxgioitinh.SelectedIndex = -1;
            txtbophan.Clear();
            txtchuc.Clear();
            txthopdong.Clear();
            txtdiachi.Clear();
            txtcccd.Clear();
            txtsdt.Clear();
            txtmail.Clear();
            comboBoxdantoc.SelectedIndex = -1;
            pictureBox1.Image = null;

        }

        private void btnmoi_Click(object sender, EventArgs e)
        {
            txtid.Clear();
            txtten.Clear();
            comboBoxgioitinh.SelectedIndex = -1;
            txtbophan.Clear();
            txtchuc.Clear();
            txthopdong.Clear();
            txtdiachi.Clear();
            txtcccd.Clear();
            txtsdt.Clear();
            txtmail.Clear();
            comboBoxdantoc.SelectedIndex = -1;
            pictureBox1.Image = null;

        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load_1(object sender, EventArgs e)
        {

        }
        private string selectedAvatar;
        private void btnChonAvatar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Chọn ảnh đại diện";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Lấy đường dẫn của file hình ảnh đã chọn
                    selectedAvatar = openFileDialog.FileName;

                    // Hiển thị hình ảnh trong PictureBox
                    pictureBox1.Image = Image.FromFile(selectedAvatar);
                }
            }
        }
    }
}
