using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms; // Guna UI bileşenlerini kullanıyoruz
using Microsoft.Data.SqlClient; // Microsoft SQL Server bağlantısı

namespace ChatApp
{
    public partial class UserControl1 : UserControl
    {
        // Kullanıcı bilgilerini tutan özel değişkenler
        private string _userOne;  // UserOne'ın adı
        private string _userTwo;  // UserTwo'nun adı
        private string _title;    // Başlık
        private Image _icon;      // Profil resmi (ikon)

        // Guna2CirclePictureBox bileşenlerini tanımlıyoruz
        private Guna2CirclePictureBox profilePictureBox;
        private Guna2CirclePictureBox notificationIndicator;  // Yeni mesaj göstergesi (bildirim)
        private Label titleLabel; // Başlık etiketi
        private System.Windows.Forms.Timer messageTimer;
        private System.ComponentModel.IContainer components;
        public Label unreadCountLabel; // Okunmamış mesaj sayısı etiketi

        public UserControl1()
        {
            InitializeComponent(); // Bileşenleri başlatıyoruz
            this.Click += UserControl1_Click;  // Kullanıcı kontrolüne tıklanma olayını ekliyoruz

            // Timer'ı başlatıyoruz
            messageTimer = new System.Windows.Forms.Timer();
            messageTimer.Interval = 1000; // 10 saniye (10000 ms)
            messageTimer.Tick += messageTimer_Tick;
            messageTimer.Start(); // Timer'ı başlatıyoruz

        }

        // Title özelliği (Başlık) için property
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                titleLabel.Text = value; // Başlık değiştiğinde label'ı güncelliyoruz
            }
        }

        // Icon özelliği (Profil resmi) için property
        public Image Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                profilePictureBox.Image = value; // Profil resmini güncelliyoruz
            }
        }

        // NotificationIndicator (bildirim göstergesi) görünürlük özelliği
        public bool IsHighlighted
        {
            get => notificationIndicator.Visible;  // Görünürlük durumu
            set => notificationIndicator.Visible = value;  // Görünürlük değerini ayarlıyoruz
        }

        // Kullanıcı bilgileri için UserOne ve UserTwo properties
        public string UserOne
        {
            get => _userOne;
            set
            {
                _userOne = value;
            }
        }

        public string UserTwo
        {
            get => _userTwo;
            set
            {
                _userTwo = value;
            }
        }

        // Bileşenlerin başlangıç ayarlarını yapıyoruz
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl1));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            profilePictureBox = new Guna2CirclePictureBox();
            notificationIndicator = new Guna2CirclePictureBox();
            titleLabel = new Label();
            unreadCountLabel = new Label();
            messageTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)profilePictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)notificationIndicator).BeginInit();
            SuspendLayout();
            // 
            // profilePictureBox
            // 
            profilePictureBox.ImageRotate = 0F;
            profilePictureBox.Location = new Point(10, 15);
            profilePictureBox.Name = "profilePictureBox";
            profilePictureBox.ShadowDecoration.CustomizableEdges = customizableEdges1;
            profilePictureBox.Size = new Size(80, 80);
            profilePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            profilePictureBox.TabIndex = 0;
            profilePictureBox.TabStop = false;
            // 
            // notificationIndicator
            // 
            notificationIndicator.Image = (Image)resources.GetObject("notificationIndicator.Image");
            notificationIndicator.ImageRotate = 0F;
            notificationIndicator.Location = new Point(340, 25);
            notificationIndicator.Name = "notificationIndicator";
            notificationIndicator.ShadowDecoration.CustomizableEdges = customizableEdges2;
            notificationIndicator.Size = new Size(50, 50);
            notificationIndicator.SizeMode = PictureBoxSizeMode.StretchImage;
            notificationIndicator.TabIndex = 2;
            notificationIndicator.TabStop = false;
            notificationIndicator.Visible = false;
            // 
            // titleLabel
            // 
            titleLabel.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(100, 20);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(220, 30);
            titleLabel.TabIndex = 1;
            // 
            // unreadCountLabel
            // 
            unreadCountLabel.AutoSize = true;
            unreadCountLabel.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, 162);
            unreadCountLabel.ForeColor = Color.White;
            unreadCountLabel.Location = new Point(100, 66);
            unreadCountLabel.Name = "unreadCountLabel";
            unreadCountLabel.Size = new Size(24, 25);
            unreadCountLabel.TabIndex = 3;
            unreadCountLabel.Text = "0";
            unreadCountLabel.Click += unreadCountLabel_Click;
            // 
            // messageTimer
            // 
            messageTimer.Tick += messageTimer_Tick;
            // 
            // UserControl1
            // 
            BackColor = Color.DarkSlateGray;
            Controls.Add(profilePictureBox);
            Controls.Add(titleLabel);
            Controls.Add(notificationIndicator);
            Controls.Add(unreadCountLabel);
            Name = "UserControl1";
            Size = new Size(420, 110);
            ((System.ComponentModel.ISupportInitialize)profilePictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)notificationIndicator).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        // Yeni mesaj göstergesi (bildirim) görünür hale gelir
        public void ShowNewMessageIndicator()
        {
            IsHighlighted = true;  // Bildirim göstergesini aktif yapıyoruz
        }

        // Yeni mesaj göstergesi (bildirim) gizlenir
        public void HideNewMessageIndicator()
        {
            IsHighlighted = false;  // Bildirim göstergesini pasif yapıyoruz
        }

        // UserControl'a tıklanma olayını tanımlıyoruz
        private void UserControl1_Click(object sender, EventArgs e)
        {
            HideNewMessageIndicator();  // Tıklanırsa bildirim göstergesini gizle
        }

        // Okunmamış mesaj sayısını güncelleyen metod
        // Okunmamış mesaj sayısını güncelleyen metod
        // Okunmamış mesaj sayısını güncelleyen metod
        // UpdateMessageCount metodunu public yapıyoruz
        // Okunmamış mesaj sayısını güncelleyen metod
        // Bu metot, label'a tıklama olayını yönetir.
        private void unreadCountLabel_Click(object sender, EventArgs e)
        {
            // Label tıklandığında, veritabanından okuma mesaj sayısını çekiyoruz
        }

        // Veritabanından mesaj sayısını çekme fonksiyonu



        private void messageTimer_Tick(object sender, EventArgs e)
        {
            // Timer her tetiklendiğinde, UpdateMessageCount fonksiyonunu çağırıyoruz

        }
    }
}
