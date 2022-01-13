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

namespace MyLibrary
{
    public partial class Login : Form
    {
        public event EventHandler UserLoggedIn;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private bool UserIsValid()
        {
            XElement korisnici = XElement.Load("korisnici.xml");
            var users = from korisnik in korisnici.Elements() 
                                                    select new { username = (string)korisnik.Element("korisnickoIme"), password = (string)korisnik.Element("lozinka") };

            foreach (var user in users)
            {
                if (string.Compare(user.username, textboxUserName.Text, true) == 0
                    && user.password == textBoxPassword.Text)
                    return true;
            }
            return false;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (UserIsValid())
            {
                if (UserLoggedIn != null)
                    UserLoggedIn(this, EventArgs.Empty);
                Close();
                return;
            }
            MessageBox.Show(this, "Invalid username or password", "User Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
    }
}
