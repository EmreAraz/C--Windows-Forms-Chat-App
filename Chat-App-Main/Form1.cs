using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Data.SqlClient;
using Guna.UI2.WinForms;
using System.Data;
using System.Text.RegularExpressions;
using Xamarin.Essentials;
using ChatApp.Properties;


namespace ChatApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Aktif renk ve pasif renk tanýmlarý
        Color activeColor = Color.SpringGreen; // Aktif olan öðe için renk
        Color inactiveColor = Color.Gray; // Pasif olan öðe için renk

        string constring = "Data Source=;";

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            // Login panelini öne getir
            panel1.BringToFront();

            // Login butonunu aktif yap, Register butonunu pasif yap
            ButtonLogin.FillColor = activeColor;
            ButtonRegester.FillColor = inactiveColor;

            // Panellerin arka plan rengini deðiþtir
            panel3.BackColor = activeColor;
            panel4.BackColor = activeColor;
        }

        private void ButtonRegester_Click(object sender, EventArgs e)
        {
            // Register panelini öne getir
            panel2.BringToFront();

            // Register butonunu aktif yap, Login butonunu pasif yap
            ButtonRegester.FillColor = activeColor;
            ButtonLogin.FillColor = inactiveColor;

            // Panellerin arka plan rengini deðiþtir
            panel3.BackColor = activeColor;
            panel4.BackColor = activeColor;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Program baþladýðýnda giriþ ekranýný aktif yap
            panel1.BringToFront(); // Login panelini öne getir
            ButtonLogin.FillColor = activeColor; // Login butonu aktif
            ButtonRegester.FillColor = inactiveColor; // Register butonu pasif

            // Panellerin arka plan rengini ayarla
            panel3.BackColor = activeColor;
            panel4.BackColor = activeColor;
        }



        private void guna2Button4_Click(object sender, EventArgs e)
        {
            // Fotoðraf seçildi mi kontrolü
            if (guna2CirclePictureBox1.Image == null)
            {
                MessageBox.Show("Fotoðraf Seçin");
                return;
            }

            // Alanlarýn kontrolü

            // Ad kontrolü
            if (string.IsNullOrEmpty(firstname.Text.Trim()))
            {
                errorProvider1.SetError(firstname, "Adýnýz Gereklidir");
                return;
            }
            else
            {
                errorProvider1.SetError(firstname, string.Empty);
            }

            // Soyad kontrolü
            if (string.IsNullOrEmpty(lastname.Text.Trim()))
            {
                errorProvider1.SetError(lastname, "Soyadýnýz Gereklidir");
                return;
            }
            else
            {
                errorProvider1.SetError(lastname, string.Empty);
            }

            // E-posta kontrolü
            if (string.IsNullOrEmpty(email.Text.Trim()))
            {
                errorProvider1.SetError(email, "E-posta adresiniz gereklidir");
                return;
            }
            else
            {
                errorProvider1.SetError(email, string.Empty);
            }

            // E-posta formatý kontrolü
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(email.Text.Trim(), emailPattern))
            {
                errorProvider1.SetError(email, "Geçerli bir e-posta adresi giriniz");
                return;
            }

            // Þifre kontrolü
            if (string.IsNullOrEmpty(password.Text.Trim()))
            {
                errorProvider1.SetError(password, "Þifreniz Gereklidir");
                return;
            }
            else
            {
                errorProvider1.SetError(password, string.Empty);
            }

            // Þifre doðrulama kontrolü
            if (string.IsNullOrEmpty(confirm.Text.Trim()))
            {
                errorProvider1.SetError(confirm, "Þifrenizi Doðrulamanýz Gereklidir");
                return;
            }
            else
            {
                errorProvider1.SetError(confirm, string.Empty);
            }

            // Þifreler uyuþuyor mu kontrolü
            if (password.Text != confirm.Text)
            {
                MessageBox.Show("Þifreler Uyuþmuyor!");
                return;
            }

            // Veritabanýna ekleme iþlemi
            try
            {
                SqlConnection con = new SqlConnection(constring);

                // E-posta kontrolü
                string checkEmailQuery = "SELECT COUNT(*) FROM Login WHERE email = @checkEmail";
                SqlCommand checkEmailCmd = new SqlCommand(checkEmailQuery, con);
                checkEmailCmd.Parameters.AddWithValue("@checkEmail", email.Text.Trim());

                con.Open();
                int emailExists = (int)checkEmailCmd.ExecuteScalar();
                con.Close();

                if (emailExists > 0)
                {
                    MessageBox.Show("Bu e-posta adresi zaten kayýtlý!");
                    return;
                }

                // Yeni kullanýcý ekleme
                string q = "INSERT INTO Login (firstname, lastname, email, password, confirmpass, image) " +
                           "VALUES (@firstname, @lastname, @email, @password, @confirmpass, @image)";
                SqlCommand cmd = new SqlCommand(q, con);

                // Resmi byte dizisine dönüþtür
                MemoryStream me = new MemoryStream();
                if (guna2CirclePictureBox1.Image != null)
                {
                    guna2CirclePictureBox1.Image.Save(me, guna2CirclePictureBox1.Image.RawFormat);
                }
                else
                {
                    me = null; // Resim yoksa null gönder
                }

                // Parametreleri ekle
                cmd.Parameters.AddWithValue("@firstname", firstname.Text);
                cmd.Parameters.AddWithValue("@lastname", lastname.Text);
                cmd.Parameters.AddWithValue("@email", email.Text);
                cmd.Parameters.AddWithValue("@password", password.Text); // Þifreyi ekle
                cmd.Parameters.AddWithValue("@confirmpass", confirm.Text); // Þifreyi doðrulamak için
                cmd.Parameters.AddWithValue("@image", me.ToArray()); // Resmi byte dizisi olarak ekle

                // Veritabanýna baðlan ve sorguyu çalýþtýr
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                // Kayýt baþarýlý mesajý
                MessageBox.Show("Kayýt Baþarýlý...");

                // Formu temizle
                firstname.Clear();
                lastname.Clear();
                email.Clear();
                password.Clear();
                confirm.Clear();
                guna2CirclePictureBox1.Image = Resources.login ;

            }
            catch (Exception ex)
            {
                // Hata mesajý
                MessageBox.Show("Bir hata oluþtu: " + ex.Message);
            }
        }




        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "select image(*Jpg; *.png; *Gif| *.Jpg; *.png; *Gif)";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                guna2CirclePictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

		private void LoginBB_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(emaillogin.Text.Trim()))
			{
				errorProvider1.SetError(emaillogin, "Emailinizi Girmeniz Gereklidir");
				return;
			}
			else
			{
				errorProvider1.SetError(emaillogin, string.Empty);
			}

			if (string.IsNullOrEmpty(passwordlogin.Text.Trim()))
			{
				errorProvider1.SetError(passwordlogin, "Þifrenizi Girmeniz Gereklidir");
				return;
			}
			else
			{
				errorProvider1.SetError(passwordlogin, string.Empty);
			}

			// ?? Eðer kullanýcý adý ve þifre admin ise direkt giriþ yap
			if (emaillogin.Text == "admin" && passwordlogin.Text == "admin")
			{
				// Aktif kullanýcý bilgilerini ata
				ActiveUser.FirstName = "Admin";
				ActiveUser.LastName = "";
				ActiveUser.Email = "admin@system.local";
				ActiveUser.Password = "admin";
				ActiveUser.ProfileImage = null;

				// Form2'yi aç
				Form2 form2 = new Form2();
				form2.SetUserData("Admin", "", "admin@system.local", "admin", null);
				form2.Show();
				this.Hide();
				return;
			}

			// Normal veritabaný giriþ kontrolü
			SqlConnection con = new SqlConnection(constring);
			con.Open();

			string q = "SELECT * FROM Login WHERE email = @Email AND password = @Password";
			SqlCommand cmd = new SqlCommand(q, con);
			cmd.Parameters.AddWithValue("@Email", emaillogin.Text);
			cmd.Parameters.AddWithValue("@Password", passwordlogin.Text);
			SqlDataReader dataReader = cmd.ExecuteReader();


			if (dataReader.HasRows)
            {
                // Veritabanýndan kullanýcý bilgilerini al
                string firstname = "";
                string lastname = "";
                string email = "";
                string password = "";
                byte[] imageBytes = null; // Resim verisi burada tutulacak
                int userId = 0; // Kullanýcý ID'si için yeni deðiþken


                while (dataReader.Read())
                {
                    firstname = dataReader["firstname"].ToString();
                    lastname = dataReader["lastname"].ToString();
                    email = dataReader["email"].ToString();
                    password = dataReader["password"].ToString();
                    if (dataReader["image"] != DBNull.Value)
                    {
                        imageBytes = (byte[])dataReader["image"]; // Resim verisini al
                    }
                    userId = Convert.ToInt32(dataReader["ID"]); // ID bilgisi alýnýyor

                }

                // Giriþ yapan kullanýcýyý ActiveUser sýnýfýnda sakla
                ActiveUser.UserId = userId;  // ID'yi ActiveUser'a kaydediyoruz

                ActiveUser.FirstName = firstname;
                ActiveUser.LastName = lastname;
                ActiveUser.Email = email;
                ActiveUser.Password = password;
                ActiveUser.ProfileImage = imageBytes;

                // Form2'yi aç ve bilgileri gönder
                Form2 form2 = new Form2();
                form2.SetUserData(firstname, lastname, email, password, imageBytes);  // Kullanýcý bilgilerini ve resmi gönder
                form2.Show();  // Form2'yi aç

                // Giriþ baþarýlý olduktan sonra metin kutularýný temizle
                emaillogin.Clear();
                passwordlogin.Clear();
            }
            else
            {
                MessageBox.Show("Lütfen Bilgilerinizi Kontrol Ediniz.");
            }

            con.Close();
        }




        private void timer1_Tick(object sender, EventArgs e)
        {
            if (guna2CircleProgressBar1.Value < 100)
            {
                guna2CircleProgressBar1.Value += 10;
            }
            else
            {
                timer1.Stop();
                Form2 f2 = new Form2();
                f2.emailname = emaillogin.Text;
                this.Hide();
                f2.Show();

            }
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (confirm.PasswordChar == '*' && password.PasswordChar == '*')
            {
                confirm.PasswordChar = '\0';
                password.PasswordChar = '\0';
            }
            else
            {
                confirm.PasswordChar = '*';
                password.PasswordChar = '*';
            }
        }

        private void guna2CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (passwordlogin.PasswordChar == '*')
            {
                passwordlogin.PasswordChar = '\0';
            }
            else
            {
                passwordlogin.PasswordChar = '*';
            }
        }

        private void emaillogin_TextChanged(object sender, EventArgs e)
        {

            // Login butonunu aktif yap, Register butonunu pasif yap
            ButtonLogin.FillColor = activeColor;
            ButtonRegester.FillColor = inactiveColor;

            // Panellerin arka plan rengini deðiþtir
            panel3.BackColor = activeColor;
            panel4.BackColor = activeColor;
        }

        private void passwordlogin_TextChanged(object sender, EventArgs e)
        {

            // Login butonunu aktif yap, Register butonunu pasif yap
            ButtonLogin.FillColor = activeColor;
            ButtonRegester.FillColor = inactiveColor;

            // Panellerin arka plan rengini deðiþtir
            panel3.BackColor = activeColor;
            panel4.BackColor = activeColor;

        }
    }
}
