using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace lab5
{
    public partial class Auth : Form
    {
        public Auth()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (UserContext db = new UserContext())
                {
                foreach (User user in db.Users)
                    {
                   
                    if (textBoxLog.Text == user.Login &&
                    this.GetHashString(textBoxPass.Text) == user.Password)
                        {
                        MessageBox.Show("Вход успешен!");
                         UserForm userForm = new UserForm();
                        this.Hide();
                        MessageBox.Show("Вы вошли под учётной записью " + user.Login);
                        userForm.Show();
                        return;
                        }
                     }
                MessageBox.Show("Логин или пароль указан неверно!");
                 }


        }
        //шифрование
        private string GetHashString(string s)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(s);
            MD5CryptoServiceProvider CSP = new
            MD5CryptoServiceProvider();
            byte[] byteHash = CSP.ComputeHash(bytes);
            string hash = "";
            foreach (byte b in byteHash)
            {
                hash += string.Format("{0:x2}", b);
            }
            return hash;
        }

        // создание акка
        private void label4_Click(object sender, EventArgs e)
        {
            Reg form = new Reg();       
            this.Hide();
            form.Show();
        }
        // восстановлене акка
        private void label5_Click(object sender, EventArgs e)
        {
            RecoverAccount RA = new RecoverAccount();   
            this.Hide();
            RA.Show();
        }

        private void Auth_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                textBoxPass.UseSystemPasswordChar = true;
            }
            else { textBoxPass.UseSystemPasswordChar = false; }
        }
    }
}
