using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WireLibrary;

namespace WireTestingProject
{
    public partial class Login : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public Login()
        {
            
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
        public void CheckUser()
        {
            WireTestSoftwareDBEntities db = new WireTestSoftwareDBEntities();
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            var objUser = db.tb_Employee.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (objUser != null)
            {
                if (objUser.Username == "Superadmin" || objUser.Username == "superadmin")
                {
                    MasterPage f1 = new MasterPage(1);
                    f1.Show();
                    this.Hide();
                }
                else
                {
                    MasterPage f1 = new MasterPage(0);
                    f1.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Wrong ID or Password!");
            }
        }
        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e) {
            e.Graphics.FillRectangle(new SolidBrush(Color.Gray), e.ClipRectangle);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            label1.Parent = pictureBox1;
            label1.BackColor = Color.Transparent;

            label2.Parent = pictureBox1;
            label2.BackColor = Color.Transparent;

            label3.Parent = pictureBox1;
            label3.BackColor = Color.Transparent;

        }
        private void Login_Load(object sender, EventArgs e)
        {
            //label1.Parent = pictureBox1;
            //label1.BackColor = Color.Transparent;
            //label1.ForeColor = Color.Violet;

            //label2.Parent = pictureBox1;
            //label2.BackColor = Color.Transparent;

            //label3.Parent = pictureBox1;
            //label3.BackColor = Color.Transparent;
            
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckUser();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
