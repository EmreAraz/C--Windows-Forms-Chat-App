using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChatApp
{
    public partial class UserControl2 : UserControl
    {
        // Resim ve Başlık özellikleri
        private string _title;

        // Resim özelliği
        public Image Image { get; set; }

        // Başlık özelliği
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                label1.Text = value;
                AddHeighttext(); // Başlık eklenince yüksekliği güncelle
                ToggleVisibility(); // Başlık değiştiğinde görünürlüğü güncelle
            }
        }

        // Resmi kullanıcı kontrolüne eklemek için fonksiyon
        public void DisplayImage()
        {
            if (Image != null)
            {
                pictureBox1.Image = Image; // pictureBox, UserControl2'de bulunan bir PictureBox kontrolü
                ToggleVisibility(); // Resim eklendiğinde görünürlüğü güncelle
            }
        }


        // Başlık içeriğiyle birlikte boyutları ayarlamak
        void AddHeighttext()
        {
            label1.Height = Uilist.GeTTextHeight(label1) + 10;
            this.Height = label1.Top + label1.Height + 10; // Kontrolün toplam yüksekliğini güncelle
        }

        // Title ve Image durumuna göre kontrolün görünürlüğünü ayarlama
        private void ToggleVisibility()
        {
            if (Image != null && string.IsNullOrEmpty(Title)) // Sadece resim varsa
            {
                pictureBox1.Visible = true;
                label1.Visible = false; // Başlık gizlensin
            }
            else if (!string.IsNullOrEmpty(Title) && Image == null) // Sadece başlık varsa
            {
                pictureBox1.Visible = false; // Resim gizlensin
                label1.Visible = true;
            }
            else
            {
                pictureBox1.Visible = true;
                label1.Visible = true; // Hem başlık hem resim varsa
            }
        }

        // Kullanıcı Kontrolüne resim eklemek için yükleme işlemi
        private void UserControl2_Load(object sender, EventArgs e)
        {
            DisplayImage();
            AddHeighttext();
        }

        public UserControl2()
        {
            InitializeComponent();
        }
    }
}
