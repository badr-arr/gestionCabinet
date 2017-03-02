using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projet
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Erreur.Visibility = Visibility.Hidden;
        }

        private void MotDePasseOublie(object sender, RoutedEventArgs e)
        {
            string mot = Interaction.InputBox("Entrer votre adresse email", "Mot de passe oublié", "");
            if (mot.Equals("") || mot.Trim(' ').Equals(""))
                System.Windows.MessageBox.Show("Chaine vide");
            else if (!Regex.IsMatch(mot, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}"))
                System.Windows.MessageBox.Show("Adresse email invalide");
            else if (Connexion.EmailExists(mot))
            {
                try
                {
                    string msg = "Bonjour,\n\nVotre adresse email : " + mot + " est validée.\nVotre mot de passe est :" + Connexion.RetourneMotDePasse(mot) + "\n\nCordialement.";
                    var smtpServerName = ConfigurationManager.AppSettings["SmtpServer"];
                    var port = ConfigurationManager.AppSettings["Port"];
                    var senderEmailId = ConfigurationManager.AppSettings["SenderEmailId"];
                    var senderPassword = ConfigurationManager.AppSettings["SenderPassword"];

                    var smptClient = new SmtpClient(smtpServerName, Convert.ToInt32(port))
                    {
                        Credentials = new NetworkCredential(senderEmailId, senderPassword),
                        EnableSsl = true
                    };

                    smptClient.Send(senderEmailId, mot, "Mot de passe oublié", msg);
                    MessageBox.Show("Message envoyé avec succès");
                }
                catch (Exception)
                {
                    MessageBox.Show("Message non envoyé");
                }
            }
            else
                System.Windows.MessageBox.Show("Email n'existe pas dans notre base de donnée");
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            Connexion c = new Connexion();
            if ((!email.Text.Equals(null)) && (!password.Password.Equals(null)))
                if (c.verifierLogin(email.Text, password.Password))
                {
                    Page p = new Page(email.Text);
                    p.Show();
                    this.Close();

                }
                else
                    Erreur.Visibility = Visibility.Visible;
        }
    }
}
