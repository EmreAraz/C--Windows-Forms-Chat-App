using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChatApp
{
    public partial class UserControl3 : UserControl
    {
        private string _title;
        private Image _icon;
        private Image _messageImage; // Mesaj için resim

        public UserControl3()
        {
            InitializeComponent();
            InitializePictureBox(); // PictureBox'ı başlatma
        }

        // Title (Başlık) özelliği
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                label1.Text = value; // Başlık label1'de göster
                AddHeighttext(); // Başlık eklenince yüksekliği güncelle
                ToggleVisibility(); // Başlık değiştiğinde görünürlüğü güncelle
            }
        }

        // Profil resmi (ikon) özelliği
        public Image Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                guna2CirclePictureBox1.Image = value; // Profil resmi PictureBox'a ekle
                AddHeighttext(); // Yüksekliği güncelle
            }
        }

        // Mesaj resmini gösterecek Image özelliği
        public Image MessageImage
        {
            get { return _messageImage; }
            set
            {
                _messageImage = value;
                DisplayImage(); // Resmi göster
            }
        }


        // PictureBox'ın oluşturulması ve ayarlanması
        private void InitializePictureBox()
        {
            // pictureBox1 (Mesaj resmi) ayarları
            pictureBox1.Dock = DockStyle.None;  // PictureBox'ın kontrol alanını üst kısmını doldurması
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;  // Resmi sığdırmak için zoom kullan
            pictureBox1.Visible = false;  // Başlangıçta görünmez
            pictureBox1.BackColor = Color.LightGray; // Arkaplan rengi eklenebilir
            pictureBox1.Location = new Point(470, 12);  // Mesaj resminin konumu
            Controls.Add(pictureBox1);  // PictureBox'ı kontrolün içerisine ekle

            // guna2CirclePictureBox1 (Profil resmi) ayarları
            guna2CirclePictureBox1.Size = new Size(75, 75);  // Profil resminin boyutu
            guna2CirclePictureBox1.Location = new Point(3, 21); // Profil resminin konumu
            guna2CirclePictureBox1.BackColor = Color.Transparent; // Şeffaf arka plan
            Controls.Add(guna2CirclePictureBox1); // Profil resmini ekle
        }



        // Resmi _pictureBox'a yükle
        private void DisplayImage()
        {
            if (_messageImage != null)
            {
                pictureBox1.Image = _messageImage;  // Resmi pictureBox1'a yükle
                pictureBox1.Visible = true;  // pictureBox1'ı görünür hale getir
                ToggleVisibility(); // Görünürlüğü kontrol et
            }
            else
            {
                pictureBox1.Visible = false;  // Resim yoksa pictureBox'ı gizle
            }
        }


        // Title ve Image durumuna göre görünürlüğü ayarlama
        private void ToggleVisibility()
        {
            if (_messageImage != null && string.IsNullOrEmpty(_title)) // Sadece resim varsa
            {
                label1.Visible = false; // Başlık gizlensin
            }
            else if (!string.IsNullOrEmpty(_title) && _messageImage == null) // Sadece başlık varsa
            {
                label1.Visible = true;
                pictureBox1.Visible = false; // Resim gizlensin
            }
            else
            {
                label1.Visible = true;
                pictureBox1.Visible = true; // Hem başlık hem resim varsa
            }
        }


        // Yükleme işlemi
        private void UserControl3_Load(object sender, EventArgs e)
        {
            DisplayImage();
            AddHeighttext();
        }

        // Yükseklik hesaplama
        private void AddHeighttext()
        {
            label1.Height = Uilist.GeTTextHeight(label1) + 10; // Metin boyutuna göre yüksekliği hesapla
            this.Height = label1.Top + label1.Height + 10; // Toplam yüksekliği güncelle
        }
    }
}
