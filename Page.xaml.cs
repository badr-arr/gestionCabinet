using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projet
{
    /// <summary>
    /// Logique d'interaction pour Page.xaml
    /// </summary>
    public partial class Page : Window
    {
        public string id;
        public string id_patient = "0";
        public string type;
        public string user;

        public Page(string u)
        {
            InitializeComponent();
            this.user = u;
            this.hideAllGrids();
            CPNNotification.Visibility = Visibility.Hidden;
            CPoNNotification.Visibility = Visibility.Hidden;
            Int32 rdv = RDVS.NombreTotalPatient();
            TotalPatiente.Content = Convert.ToString(rdv);
            rdv = RDVS.NombreTotalPatientStatut("En attente");
            PatienteEnAttente.Content = rdv.ToString();
            rdv = RDVS.NombreTotalPatientStatut("Effectué");
            ConsultationEffectuees.Content = rdv.ToString();
            rdv = RDVS.NombreTotalPatientType("cpn");
            Prenatale.Content = rdv.ToString();
            rdv = RDVS.NombreTotalPatientType("cpon");
            Postnatale.Content = rdv.ToString();
            rdv = RDVS.NombreTotalPatientType("echo");
            Echographie.Content = rdv.ToString();
            AcceuilGrid.Visibility = Visibility.Visible;
            if (DateTime.Today.Day.ToString() == "1")
            {
                CPNNotification.Visibility = Visibility.Visible;
                CPoNNotification.Visibility = Visibility.Visible;
                RapportCPN.CreerRapport();
                RapportCPoN.CreerRapport();
            }
            
        }

        //Affiche le nombre de chaque type de rdv dans l'acceuil
/*        public void affecterValeurAcceuil() {
            TotalPatientsToday.Content = RDVDAO.NbreRDVToday();
            PatientAttenteToday.Content = RDVDAO.NbreRDVTodayCritere("En attente");
            ConsultationEffectueeToday.Content = RDVDAO.NbreRDVTodayCritere("Effectué");
            RDVAnnuleToday.Content = RDVDAO.NbreRDVTodayCritere("Annulé");
            PatientAbsentToday.Content = RDVDAO.NbreRDVTodayCritere("Patient absent");
        }*/

        //******************* les pages qui s'affiche chaque fois qu'on clique sur un boutton du menu 
        public void hideAllGrids()
        {
            TablePatient.Visibility = Visibility.Hidden;
            ParametreGrid.Visibility = Visibility.Hidden;
            samePasswordError.Visibility = Visibility.Hidden;
            oldPasswordError.Visibility = Visibility.Hidden;
            ModificationReussi.Visibility = Visibility.Hidden;
            mailInvalide.Visibility = Visibility.Hidden;
            RemplirPatientGrid.Visibility = Visibility.Hidden;
            RDVGrid.Visibility = Visibility.Hidden;
            AcceuilGrid.Visibility = Visibility.Hidden;
            RapportCPNGrid.Visibility = Visibility.Hidden;
            RapportCPoNGrid.Visibility = Visibility.Hidden;
            RapportJournalierGrid.Visibility = Visibility.Hidden;
        }

        private void patientClick(object sender, RoutedEventArgs e)
        {
            this.hideAllGrids();
            TablePatient.Visibility = Visibility.Visible;
        }

        private void param_Click(object sender, RoutedEventArgs e)
        {
            this.hideAllGrids();
            Connexion c = new Connexion();
            Email.Text = c.RetourneEmailCompte(this.user);
            ParametreGrid.Visibility = Visibility.Visible;

        }

        private void changeParameters(object sender, RoutedEventArgs e)
        {
            samePasswordError.Visibility = Visibility.Hidden;
            oldPasswordError.Visibility = Visibility.Hidden;
            ModificationReussi.Visibility = Visibility.Hidden;
            mailInvalide.Visibility = Visibility.Hidden;
            Connexion c = new Connexion();
            if ((!Email.Text.Equals(null)) && (!oldPassword.Password.Equals(null)))
            {
                if (c.verifierLogin(this.user, oldPassword.Password))
                {
                    if (newPassword.Password.Equals(newPasswordVerification.Password))
                    {
                        if (Regex.IsMatch(Email.Text, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}"))
                        {
                            if (c.modifierComptePassword(Email.Text.Replace("'", "''"), newPassword.Password))
                            {
                                this.user = Email.Text;
                                Email.Text = this.user;
                                oldPassword.Password = null;
                                newPassword.Password = null;
                                newPasswordVerification.Password = null;
                                ModificationReussi.Visibility = Visibility.Visible;
                            }
                        }
                        else
                            mailInvalide.Visibility = Visibility.Visible;

                    }
                    else
                    {
                        Email.Text = this.user;
                        oldPassword.Password = null;
                        newPassword.Password = null;
                        newPasswordVerification.Password = null;
                        samePasswordError.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    Email.Text = this.user;
                    oldPassword.Password = null;
                    newPassword.Password = null;
                    newPasswordVerification.Password = null;
                    oldPasswordError.Visibility = Visibility.Visible;
                }
            }


        }

        private void Deconnexion(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }

        public void PageWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Projet.BDProjetDataSet bDProjetDataSet = ((Projet.BDProjetDataSet)(this.FindResource("bDProjetDataSet")));
                // Chargez les données dans la table Table_patient. Vous pouvez modifier ce code si nécessaire.
                Projet.BDProjetDataSetTableAdapters.Table_patientTableAdapter bDProjetDataSetTable_patientTableAdapter = new Projet.BDProjetDataSetTableAdapters.Table_patientTableAdapter();
                bDProjetDataSetTable_patientTableAdapter.Fill(bDProjetDataSet.Table_patient);
                System.Windows.Data.CollectionViewSource table_patientViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("table_patientViewSource")));
                table_patientViewSource.View.MoveCurrentToFirst();
                // Chargez les données dans la table rdv. Vous pouvez modifier ce code si nécessaire.
                Projet.BDProjetDataSetTableAdapters.rdvTableAdapter bDProjetDataSetrdvTableAdapter = new Projet.BDProjetDataSetTableAdapters.rdvTableAdapter();
                bDProjetDataSetrdvTableAdapter.Fill(bDProjetDataSet.rdv);
                System.Windows.Data.CollectionViewSource rdvViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("rdvViewSource")));
                rdvViewSource.View.MoveCurrentToFirst();
                // Chargez les données dans la table rapportCPN. Vous pouvez modifier ce code si nécessaire.
                Projet.BDProjetDataSetTableAdapters.rapportCPNTableAdapter bDProjetDataSetrapportCPNTableAdapter = new Projet.BDProjetDataSetTableAdapters.rapportCPNTableAdapter();
                bDProjetDataSetrapportCPNTableAdapter.Fill(bDProjetDataSet.rapportCPN);
                System.Windows.Data.CollectionViewSource rapportCPNViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("rapportCPNViewSource")));
                rapportCPNViewSource.View.MoveCurrentToFirst();
                // Chargez les données dans la table RapportCPoN. Vous pouvez modifier ce code si nécessaire.
                Projet.BDProjetDataSetTableAdapters.RapportCPoNTableAdapter bDProjetDataSetRapportCPoNTableAdapter = new Projet.BDProjetDataSetTableAdapters.RapportCPoNTableAdapter();
                bDProjetDataSetRapportCPoNTableAdapter.Fill(bDProjetDataSet.RapportCPoN);
                System.Windows.Data.CollectionViewSource rapportCPoNViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("rapportCPoNViewSource")));
                rapportCPoNViewSource.View.MoveCurrentToFirst();
                // Chargez les données dans la table RapportJournalier. Vous pouvez modifier ce code si nécessaire.
                Projet.BDProjetDataSetTableAdapters.RapportJournalierTableAdapter bDProjetDataSetRapportJournalierTableAdapter = new Projet.BDProjetDataSetTableAdapters.RapportJournalierTableAdapter();
                bDProjetDataSetRapportJournalierTableAdapter.Fill(bDProjetDataSet.RapportJournalier);
                System.Windows.Data.CollectionViewSource rapportJournalierViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("rapportJournalierViewSource")));
                rapportJournalierViewSource.View.MoveCurrentToFirst();
            }
            catch (Exception)
            {

            }
        }

        private void Agenda_Click(object sender,RoutedEventArgs e)
        {
            this.hideAllGrids();
            this.initialiserRDVChamp();
            RemplirRDVGrid.Visibility = Visibility.Hidden;
            ChoisirPatientPourRDVGrid.Visibility = Visibility.Hidden;
            ListeRDV.Visibility = Visibility.Visible;
            RDVGrid.Visibility = Visibility.Visible;
        }

        private void Accueil_Click(object sender, RoutedEventArgs e)
        {
            this.hideAllGrids();
            Int32 rdv = RDVS.NombreTotalPatient();
            TotalPatiente.Content = Convert.ToString(rdv);
            rdv = RDVS.NombreTotalPatientStatut("En attente");
            PatienteEnAttente.Content = rdv.ToString();
            rdv = RDVS.NombreTotalPatientStatut("Effectué");
            ConsultationEffectuees.Content = rdv.ToString();
            rdv = RDVS.NombreTotalPatientType("cpn");
            Prenatale.Content = rdv.ToString();
            rdv = RDVS.NombreTotalPatientType("cpon");
            Postnatale.Content = rdv.ToString();
            rdv = RDVS.NombreTotalPatientType("echo");
            Echographie.Content = rdv.ToString();
            AcceuilGrid.Visibility = Visibility.Visible;
        }

        private void RapportCPN_Click(object sender, RoutedEventArgs e)
        {
            this.hideAllGrids();
            CPNNotification.Visibility = Visibility.Hidden;
            RapportCPNGrid.Visibility = Visibility.Visible;
        }

        private void RapportCPoN_Click(object sender, RoutedEventArgs e)
        {
            this.hideAllGrids();
            CPoNNotification.Visibility = Visibility.Hidden;
            RapportCPoNGrid.Visibility = Visibility.Visible;
        }

        private void RapportJournalier_Click(object sender, RoutedEventArgs e)
        {
            this.hideAllGrids();
            RapportJournalierGrid.Visibility = Visibility.Visible;
        }

        //****************************Patient 
        private void ModifierPatient_Click(object sender, RoutedEventArgs e)
        {
            this.hideAllGrids();
            this.type = "modifier";
            DataRowView dtr = (DataRowView)table_patientDataGrid.SelectedItem;
            this.id = dtr["id"].ToString();
            Patient p = Projet.Patient.getPatient(dtr["cin"].ToString());
            this.RemplirChampPatientPourModification(p);
            RemplirPatientGrid.Visibility = Visibility.Visible;
        }

        private void RemplirChampPatientPourModification(Projet.Patient pp)
        {

            nomPatient.Text = pp.Nom;
            prenomPatient.Text = pp.Prenom ;
            numDossier.Text = pp.NumDossier;
            cinPatient.Text = pp.Cin ;
            tel.Text = pp.Tel;
            adresse.Text = pp.Adresse;
            DernierePeriode.Text = pp.DDR.ToString();
            dateNaissance.Text = pp.DateNaissance.ToString();
            prenomMari.Text = pp.NomMari;
            nomMari.Text = pp.PrenomMari;
            groupage.Text = pp.Groupage;
            dpa.Text = pp.DPA.ToString();
            assurance.Text = pp.Assurance;
            description.Text = pp.Description;
        }

        private void initialiserPatientInformation()
        {
            nomPatient.Text = "";
            prenomPatient.Text = "";
            numDossier.Text = "";
            cinPatient.Text = "";
            tel.Text = "";
            adresse.Text = "";
            DernierePeriode.Text = "";
            dateNaissance.Text = "";
            prenomMari.Text = "";
            nomMari.Text = "";
            DernierePeriode.Text = "";
            groupage.Text = "";
            dpa.Text = "";
            description.Text = "";
            assurance.Text = "";
            DatePickerPatient.Text = "";
        }

        private void AjouterPatient_Click(object sender, RoutedEventArgs e)
        {
            this.hideAllGrids();
            this.initialiserPatientInformation();
            this.type = "ajouter";
            TablePatient.Visibility = Visibility.Hidden;
            RemplirPatientGrid.Visibility = Visibility.Visible;
        }

        private void EnregistrerPatient_Click(object sender, RoutedEventArgs e)
        {
            Patient p = new Projet.Patient();
            try
            {
                p.Nom = nomPatient.Text.Replace("'","''");
                p.Prenom = prenomPatient.Text.Replace("'", "''");
                p.NumDossier = numDossier.Text.Replace("'", "''");
                p.Cin = cinPatient.Text.Replace("'", "''");
                p.Adresse = adresse.Text.Replace("'", "''");
                p.DateNaissance = (DateTime)dateNaissance.SelectedDate;
                p.PrenomMari = prenomMari.Text.Replace("'", "''");
                p.NomMari = nomMari.Text.Replace("'", "''");
                p.DDR = (DateTime)DernierePeriode.SelectedDate;                
                p.Groupage = groupage.Text.Replace("'", "''");

                p.DPA = (DateTime)dpa.SelectedDate;
                p.DateAjoute = DateTime.Today;
                p.Assurance = assurance.Text.Replace("'", "''");
                p.Description = description.Text.Replace("'", "''");

                if (this.TelIsValid(tel.Text))
                    p.Tel = tel.Text.Replace("'", "''");
                else
                    throw new Exception();
                if (this.type == "ajouter")
                {
                    if (Projet.Patient.verifierUniciteCin(p.Cin) && Projet.Patient.verifierUniciteNumDossier(p.NumDossier))
                    {
                        Projet.Patient.persistPatient(p);
                        Dossier d = new Dossier(p.Cin,this);
                        d.Show();
                    }
                }
                else
                {
                    p.Id = this.id;
                    Projet.Patient.ModifyPatient(p);
                }
                RemplirPatientGrid.Visibility = Visibility.Hidden;
                TablePatient.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Veuillez remplir tous les champs du patient");
            }
            this.PageWindow_Loaded(sender,e);
        }

        public bool TelIsValid(string tt)
        {
            try
            {
                if (tt.Count() != 10)
                    throw new Exception() ;
                double nbre = Convert.ToDouble(tt);
                return true;
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("N° de téléphone invalide ");
            }
            return false;
        }

        private void SupprimerPatient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dtr = (DataRowView)table_patientDataGrid.SelectedItem;
                Patient pa = Projet.Patient.getPatient(dtr["cin"].ToString());
                string message, caption;
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                DataRowView row = (DataRowView)((System.Windows.Controls.Button)e.Source).DataContext;
                message = "ATTENTION !!! \nLa suppression du patient entraînera  la suppression de toutes ses informations.\nContinuer quand même ?";
                caption = "Suppression de patient";
                result = System.Windows.Forms.MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    Projet.Patient.deletePatient(pa);
                    this.PageWindow_Loaded(sender, e);
                }
            }
            catch (Exception) { }
        }

        private void AnnulerPatient_Click(object sender, RoutedEventArgs e)
        {
            RemplirPatientGrid.Visibility = Visibility.Hidden;
            TablePatient.Visibility = Visibility.Visible;

        }

        private void chercherPatient_click(object sender, RoutedEventArgs e)
        {
            string selectedItem = "";
            if (TablePatient.IsVisible)
            {
                switch (comboBoxPatient.SelectionBoxItem.ToString())
                {
                    case "N° du dossier":
                        selectedItem = "num_dossier";
                        break;
                    case "Nom de la patiente":
                        selectedItem = "nom";
                        break;
                    case "Prénom de la patiente":
                        selectedItem = "prenom";
                        break;
                    case "CIN de la patiente":
                        selectedItem = "cin";
                        break;
                    case "Téléphone":
                        selectedItem = "tel";
                        break;
                    case "Trimestre":
                        selectedItem = "trimestre";
                        break;
                }
                try
                {
                    if (DatePickerPatient.SelectedDate.ToString().Equals(""))
                        table_patientDataGrid.ItemsSource = Projet.Patient.chercherPatient(selectedItem, patientSearchText.Text.Replace("'", "''")).DefaultView;
                    else
                        table_patientDataGrid.ItemsSource = Projet.Patient.chercherPatientAvecDate(selectedItem, patientSearchText.Text.Replace("'", "''"), (DateTime)DatePickerPatient.SelectedDate).DefaultView;

                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }
            else if (ChoisirPatientPourRDVGrid.IsVisible)
            {
                switch (comboBoxPatientPourRDV.SelectionBoxItem.ToString())
                {
                    case "N° du dossier":
                        selectedItem = "num_dossier";
                        break;
                    case "Nom de la patiente":
                        selectedItem = "nom";
                        break;
                    case "Prénom de la patiente":
                        selectedItem = "prenom";
                        break;
                    case "CIN de la patiente":
                        selectedItem = "cin";
                        break;
                    case "Téléphone":
                        selectedItem = "tel";
                        break;
                    case "Trimestre":
                        selectedItem = "trimestre";
                        break;
                }
                try
                {
                    if (DatePickerPatientPourRDV.SelectedDate.ToString().Equals(""))
                        table_patientPourRDVDataGrid.ItemsSource = Projet.Patient.chercherPatient(selectedItem, patientSearchTextPourRDV.Text.Replace("'", "''")).DefaultView;
                    else
                    {
                        table_patientPourRDVDataGrid.ItemsSource = Projet.Patient.chercherPatientAvecDate(selectedItem, patientSearchTextPourRDV.Text.Replace("'", "''"), (DateTime)DatePickerPatientPourRDV.SelectedDate).DefaultView;
                    }

                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }

        }

        private void VoirDossierPatient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dtr = (DataRowView)table_patientDataGrid.SelectedItem;
                Dossier d = new Dossier(dtr["cin"].ToString(),this);
                d.Show();
            }
            catch (Exception)
            {

            }
        }

        //***********************RDV
        private void AjouterRDV_Click(object sender,RoutedEventArgs e)
        {
            this.hideAllGrids();
            this.initialiserRDVChamp();
            this.type = "ajouter";
            RDVGrid.Visibility = Visibility.Visible;
            RemplirRDVGrid.Visibility = Visibility.Visible;
            ListeRDV.Visibility = Visibility.Hidden;
            
        }

        private void AnnulerRDV_Click(object sender, RoutedEventArgs e)
        {
            this.hideAllGrids();
            RDVGrid.Visibility = Visibility.Visible;
            ListeRDV.Visibility = Visibility.Visible;
            RemplirRDVGrid.Visibility = Visibility.Hidden;
        }

        private void ChoisirPatientPourRDV_Click(object sender,RoutedEventArgs e)
        {
            this.hideAllGrids();
            ChoisirPatientPourRDVGrid.Visibility = Visibility.Visible;
            RDVGrid.Visibility = Visibility.Visible;
            ListeRDV.Visibility = Visibility.Hidden;
            RemplirRDVGrid.Visibility = Visibility.Hidden;
        }

        private void AnnulerChoisirPatientPourRDV_Click(object sender,RoutedEventArgs e)
        {
            this.hideAllGrids();
            ChoisirPatientPourRDVGrid.Visibility = Visibility.Hidden;
            RemplirRDVGrid.Visibility = Visibility.Visible;
            RDVGrid.Visibility = Visibility.Visible;
            ListeRDV.Visibility = Visibility.Hidden;
        }

        private void initialiserRDVChamp()
        {
            this.id_patient = "0";
            cpn1.IsChecked = false;
            cpn2.IsChecked = false;
            cpn3.IsChecked = false;
            cpn4.IsChecked = false;
            autreType.IsChecked = false;
            echographie.IsChecked = false;
            cpon1.IsChecked = false;
            cpon2.IsChecked = false;
            enAttente.IsChecked = false;
            effectue.IsChecked = false;
            NomPatientRDV.Text = "";
            PrenomPatientRDV.Text = "";
            dateRDV.Text = "";
            comboBoxHeureRDV.Text = "";
            comboBoxMinuteRDV.Text = "00";
            comboBoxDureeHeure.Text = "00";
            comboBoxDureeMinute.Text = "00";
            descriptionRDV.Text = "";
            RDVSearchText.Text = "";
            DatePickerRDV.Text = "";
            patientSearchTextPourRDV.Text = "";
            DatePickerPatientPourRDV.Text = "";
            NomPatientRDV.IsReadOnly = true;
            PrenomPatientRDV.IsReadOnly = true;
        }

        private void ChoisirPatientPourRDV(object sender,RoutedEventArgs e)
        {
            DataRowView dtr = (DataRowView)table_patientPourRDVDataGrid.SelectedItem;
            Projet.Patient p = Projet.Patient.getPatient(dtr["cin"].ToString());
            NomPatientRDV.Text = p.Nom;
            PrenomPatientRDV.Text = p.Prenom;
            NomPatientRDV.IsReadOnly = true;
            PrenomPatientRDV.IsReadOnly = true;
            this.id_patient = p.Id;
            ChoisirPatientPourRDVGrid.Visibility = Visibility.Hidden;
            RemplirRDVGrid.Visibility = Visibility.Visible;

        }

        private void EnregistrerRDV_Click(object sender,RoutedEventArgs e)
        {
            RDVS r = new RDVS();
            try
            {
                if (!((bool)cpn1.IsChecked) && !((bool)cpn2.IsChecked) && !((bool)cpn3.IsChecked) && !((bool)cpn4.IsChecked) && !((bool)autreType.IsChecked) && !((bool)cpon1.IsChecked) && !((bool)cpon2.IsChecked) && !((bool)echographie.IsChecked))
                {
                    System.Windows.MessageBox.Show("Veuillez choisir un type de CPN");
                    throw new Exception();
                }
                else if (((bool)cpn1.IsChecked) && !((bool)cpn2.IsChecked) && !((bool)cpn3.IsChecked) && !((bool)cpn4.IsChecked) && !((bool)autreType.IsChecked) && !((bool)cpon1.IsChecked) && !((bool)cpon2.IsChecked) && !((bool)echographie.IsChecked))
                    r.TypeRDV = "CPN1";
                else if (((bool)cpn2.IsChecked) && !((bool)cpn1.IsChecked) && !((bool)cpn3.IsChecked) && !((bool)cpn4.IsChecked) && !((bool)autreType.IsChecked) && !((bool)cpon1.IsChecked) && !((bool)cpon2.IsChecked) && !((bool)echographie.IsChecked))
                    r.TypeRDV = "CPN2";
                else if (((bool)cpn3.IsChecked) && !((bool)cpn2.IsChecked) && !((bool)cpn1.IsChecked) && !((bool)cpn4.IsChecked) && !((bool)autreType.IsChecked) && !((bool)cpon1.IsChecked) && !((bool)cpon2.IsChecked) && !((bool)echographie.IsChecked))
                    r.TypeRDV = "CPN3";
                else if (((bool)cpn4.IsChecked) && !((bool)cpn2.IsChecked) && !((bool)cpn3.IsChecked) && !((bool)cpn1.IsChecked) && !((bool)autreType.IsChecked) && !((bool)cpon1.IsChecked) && !((bool)cpon2.IsChecked) && !((bool)echographie.IsChecked))
                    r.TypeRDV = "CPN4";
                else if (((bool)autreType.IsChecked) && !((bool)cpn2.IsChecked) && !((bool)cpn3.IsChecked) && !((bool)cpn4.IsChecked) && !((bool)cpn1.IsChecked) && !((bool)cpon1.IsChecked) && !((bool)cpon2.IsChecked) && !((bool)echographie.IsChecked))
                    r.TypeRDV = "Autre";
                else if (((bool)cpon1.IsChecked) && !((bool)cpon2.IsChecked) && !((bool)cpn2.IsChecked) && !((bool)cpn3.IsChecked) && !((bool)cpn4.IsChecked) && !((bool)cpn1.IsChecked) && !((bool)autreType.IsChecked) && !((bool)echographie.IsChecked))
                    r.TypeRDV = "CPoN1";
                else if (((bool)cpon2.IsChecked) && !((bool)cpon1.IsChecked) && !((bool)cpn2.IsChecked) && !((bool)cpn3.IsChecked) && !((bool)cpn4.IsChecked) && !((bool)cpn1.IsChecked) && !((bool)echographie.IsChecked) && !((bool)autreType.IsChecked))
                    r.TypeRDV = "CPoN2";
                else if (((bool)echographie.IsChecked) && !((bool)cpn2.IsChecked) && !((bool)cpn3.IsChecked) && !((bool)cpn4.IsChecked) && !((bool)cpn1.IsChecked) && !((bool)cpon1.IsChecked) && !((bool)cpon2.IsChecked) && !((bool)autreType.IsChecked))
                    r.TypeRDV = "Echographie";
                else
                {
                    System.Windows.MessageBox.Show("Plusieurs champs sont séléctionner dans le type de CPN");
                    throw new Exception();
                }
                r.NomPatient = NomPatientRDV.Text.Replace("'", "''");
                r.PrenomPatient = PrenomPatientRDV.Text.Replace("'", "''");
                r.Date = (DateTime)dateRDV.SelectedDate;
                r.Duree = comboBoxDureeHeure.SelectionBoxItem.ToString() + ":" + comboBoxDureeMinute.ToString();

                if (!(bool)enAttente.IsChecked && !(bool)effectue.IsChecked)
                {
                    System.Windows.MessageBox.Show("Veuillez choisir le statut du RDV");
                    throw new Exception();
                }
                else if ((bool)enAttente.IsChecked && !(bool)effectue.IsChecked)
                    r.Statut = "En attente";
                else if (!(bool)enAttente.IsChecked && (bool)effectue.IsChecked)
                    r.Statut = "Effectué";
                else if ((bool)enAttente.IsChecked && (bool)effectue.IsChecked)
                {
                    System.Windows.MessageBox.Show("Veuillez cocher un seul statut");
                    throw new Exception();
                }

                if (comboBoxHeureRDV.Text.Trim(' ') == "")
                {
                    System.Windows.MessageBox.Show("Veuillez saisir l'heure du RDV");
                    throw new Exception();
                }
                else
                    r.Heure = comboBoxHeureRDV.SelectionBoxItem.ToString() + ":" + comboBoxMinuteRDV.SelectionBoxItem.ToString();

                if ((comboBoxDureeHeure.SelectionBoxItem.ToString().Trim(' ').Equals("00")) && (comboBoxDureeMinute.SelectionBoxItem.ToString().Trim(' ').Equals("00")))
                {
                    System.Windows.MessageBox.Show(comboBoxDureeHeure.SelectionBoxItem.ToString().Trim(' ') + ":" + comboBoxDureeMinute.SelectionBoxItem.ToString().Trim(' '));
                    System.Windows.MessageBox.Show("Veuillez saisir la durée du RDV");
                    throw new Exception();
                }
                else
                    r.Duree = comboBoxDureeHeure.SelectionBoxItem.ToString() + ":" + comboBoxDureeMinute.SelectionBoxItem.ToString();

                int d = Convert.ToInt32(comboBoxDureeMinute.SelectionBoxItem) + Convert.ToInt32(comboBoxMinuteRDV.SelectionBoxItem);
                switch (d)
                {
                    case 90:
                        r.HeureFin = Convert.ToString(Convert.ToInt32(comboBoxDureeHeure.SelectionBoxItem) + Convert.ToInt32(comboBoxHeureRDV.SelectionBoxItem) + 1) + ":30";
                        break;
                    case 75:
                        r.HeureFin = Convert.ToString(Convert.ToInt32(comboBoxDureeHeure.SelectionBoxItem) + Convert.ToInt32(comboBoxHeureRDV.SelectionBoxItem) + 1) + ":15";
                        break;
                    case 60:
                        r.HeureFin = Convert.ToString(Convert.ToInt32(comboBoxDureeHeure.SelectionBoxItem) + Convert.ToInt32(comboBoxHeureRDV.SelectionBoxItem) + 1) + ":00";
                        break;
                    default:
                        string m = Convert.ToString(Convert.ToInt32(comboBoxDureeMinute.SelectionBoxItem) + Convert.ToInt32(comboBoxMinuteRDV.SelectionBoxItem));
                        r.HeureFin = Convert.ToString(Convert.ToInt32(comboBoxDureeHeure.SelectionBoxItem) + Convert.ToInt32(comboBoxHeureRDV.SelectionBoxItem)) + ":" + m;
                        break;
                }

                r.Description = descriptionRDV.Text.Replace("'", "''");
                if (this.type == "ajouter" && RDVS.RDVIsFree(r))
                {
                    if (NomPatientRDV.IsReadOnly)
                        r.IdPatient = this.id_patient;
                    else
                    {
                        System.Windows.MessageBox.Show("Veuillez choisir un patient");
                        throw new Exception();
                    }
                    RDVS.persistRDV(r);
                    RemplirRDVGrid.Visibility = Visibility.Hidden;
                    this.PageWindow_Loaded(sender, e);
                    ListeRDV.Visibility = Visibility.Visible;
                }

                else if (this.type == "modifier" && RDVS.RDVIsFree(r, this.id) )
                {
                    if (NomPatientRDV.IsReadOnly)
                        r.IdPatient = this.id_patient;
                    else
                    {
                        System.Windows.MessageBox.Show("Veuillez choisir un patient");
                        throw new Exception();
                    }
                    r.Id = this.id;
                    RDVS.ModifyRDV(r);
                    RemplirRDVGrid.Visibility = Visibility.Hidden;
                    this.PageWindow_Loaded(sender, e);
                    ListeRDV.Visibility = Visibility.Visible;
                }
                Int32 rdv = RDVS.NombreTotalPatient();
                TotalPatiente.Content = Convert.ToString(rdv);
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Veuillez vérifier les champs du RDV");
            }
        }

        private void ModifierRDV_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dtr = (DataRowView)rdvDataGrid.SelectedItem;
                this.hideAllGrids();
                this.initialiserRDVChamp();
                this.type = "modifier";
                RDVS r = RDVS.getRDV(dtr["id"].ToString());
                this.id = r.Id;
                this.id_patient = r.IdPatient;
                if (r.TypeRDV.Trim(' ') == "CPN1")
                    cpn1.IsChecked = true;
                else if (r.TypeRDV.Trim(' ') == "CPN2")
                    cpn2.IsChecked = true;
                else if (r.TypeRDV.Trim(' ') == "CPN3")
                    cpn3.IsChecked = true;
                else if (r.TypeRDV.Trim(' ') == "CPN4")
                    cpn4.IsChecked = true;
                else if (r.TypeRDV.Trim(' ') == "Autre")
                    autreType.IsChecked = true;
                else if (r.TypeRDV.Trim(' ') == "Echographie")
                    echographie.IsChecked = true;
                else if (r.TypeRDV.Trim(' ') == "CPoN1")
                    cpon1.IsChecked = true;
                else if (r.TypeRDV.Trim(' ') == "CPoN2")
                    cpon2.IsChecked = true;

                if (r.Statut.Trim(' ') == "En attente")
                    enAttente.IsChecked = true;
                else if (r.Statut.Trim(' ') == "Effectué")
                    effectue.IsChecked = true;

                NomPatientRDV.Text = r.NomPatient;
                PrenomPatientRDV.Text = r.PrenomPatient;
                dateRDV.Text = r.Date.ToString();

                string[] heure = r.Heure.Trim(' ').Split(':');
                comboBoxHeureRDV.Text = heure[0];
                comboBoxMinuteRDV.Text = heure[1];

                string[] duree = r.Duree.Trim(' ').Split(':');
                comboBoxDureeHeure.Text = duree[0];
                comboBoxDureeMinute.Text = duree[1];

                descriptionRDV.Text = r.Description;
                RDVGrid.Visibility = Visibility.Visible;
                RemplirRDVGrid.Visibility = Visibility.Visible;
                ListeRDV.Visibility = Visibility.Hidden;
            }
            catch (Exception) { }

        }

        private void VoirDossierAPartirDeRDV(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dtr = (DataRowView)rdvDataGrid.SelectedItem;
                Dossier d = new Dossier(Projet.Patient.getPatientCin(dtr["id_patient"].ToString()),this);
                d.Show();
            }
            catch (Exception) { }
        }

        private void deleteRDV_Click(object sender,RoutedEventArgs e)
        {
            try
            {
                DataRowView dtr = (DataRowView)rdvDataGrid.SelectedItem;
                string message, caption;
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                DataRowView row = (DataRowView)((System.Windows.Controls.Button)e.Source).DataContext;
                message = "Êtes-vous sûre de vouloir supprimer ce RDV ?";
                caption = "Suppression de RDV";
                result = System.Windows.Forms.MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    RDVS.deleteRDV(dtr["id"].ToString());
                    Int32 rdv = RDVS.NombreTotalPatient();
                    TotalPatiente.Content = Convert.ToString(rdv);
                    this.PageWindow_Loaded(sender, e);
                }
            }
            catch (Exception) { }
        }

        private void chercherRDV_click(object sender, RoutedEventArgs e)
        {
            string selectedItem = "";
                switch (comboBoxRDV.SelectionBoxItem.ToString())
                {
                    case "Type de RDV":
                        selectedItem = "type_rdv";
                        break;
                    case "Nom de la patiente":
                        selectedItem = "nom_patient";
                        break;
                    case "Prénom de la patiente":
                        selectedItem = "prenom_patient";
                        break;
                    case "Statut du RDV":
                        selectedItem = "statut";
                        break;
                }
                try
                {
                    if (DatePickerRDV.SelectedDate.ToString().Equals(""))
                        rdvDataGrid.ItemsSource = RDVS.chercherRDV(selectedItem, RDVSearchText.Text.Replace("'", "''")).DefaultView;
                    else
                        rdvDataGrid.ItemsSource = RDVS.chercherRDVAvecDate(selectedItem, RDVSearchText.Text.Replace("'", "''"), (DateTime)DatePickerRDV.SelectedDate).DefaultView;

                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }

        //********************************************************************Acceuil
        private void AfficherRDVDuJour_Click(object sender, MouseButtonEventArgs e)
        {
            rdvDataGrid.ItemsSource = RDVS.AfficherRDVAujourdhui().DefaultView;
            this.hideAllGrids();
            this.initialiserRDVChamp();
            RemplirRDVGrid.Visibility = Visibility.Hidden;
            ChoisirPatientPourRDVGrid.Visibility = Visibility.Hidden;
            ListeRDV.Visibility = Visibility.Visible;
            RDVGrid.Visibility = Visibility.Visible;
        }

        private void AfficherRDVDuJourAttente_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                rdvDataGrid.ItemsSource = RDVS.AfficherRDVAujourdhuiAvecStatut("En attente").DefaultView;
                this.hideAllGrids();
                this.initialiserRDVChamp();
                RemplirRDVGrid.Visibility = Visibility.Hidden;
                ChoisirPatientPourRDVGrid.Visibility = Visibility.Hidden;
                ListeRDV.Visibility = Visibility.Visible;
                RDVGrid.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Aucune consultation n'est en attente");
            }
        }

        private void AfficherRDVDuJourEffectue_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                rdvDataGrid.ItemsSource = RDVS.AfficherRDVAujourdhuiAvecStatut("Effectué").DefaultView;
                this.hideAllGrids();
                this.initialiserRDVChamp();
                RemplirRDVGrid.Visibility = Visibility.Hidden;
                ChoisirPatientPourRDVGrid.Visibility = Visibility.Hidden;
                ListeRDV.Visibility = Visibility.Visible;
                RDVGrid.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Aucune consultation n'a été efféctuée");
            }
        }

        private void AfficherRDVDuJourCPN_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                rdvDataGrid.ItemsSource = RDVS.AfficherRDVAujourdhuiAvecType("cpn").DefaultView;
                this.hideAllGrids();
                this.initialiserRDVChamp();
                RemplirRDVGrid.Visibility = Visibility.Hidden;
                ChoisirPatientPourRDVGrid.Visibility = Visibility.Hidden;
                ListeRDV.Visibility = Visibility.Visible;
                RDVGrid.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Aucune CPN n'est prévue pour aujourd'hui");
            }
        }

        private void AfficherRDVDuJourCPoN_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                rdvDataGrid.ItemsSource = RDVS.AfficherRDVAujourdhuiAvecType("cpon").DefaultView;
                this.hideAllGrids();
                this.initialiserRDVChamp();
                RemplirRDVGrid.Visibility = Visibility.Hidden;
                ChoisirPatientPourRDVGrid.Visibility = Visibility.Hidden;
                ListeRDV.Visibility = Visibility.Visible;
                RDVGrid.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Aucune CPoN n'est prévue pour aujourd'hui");
            }
        }

        private void AfficherRDVDuJourEcho_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                rdvDataGrid.ItemsSource = RDVS.AfficherRDVAujourdhuiAvecType("echo").DefaultView;
                this.hideAllGrids();
                this.initialiserRDVChamp();
                RemplirRDVGrid.Visibility = Visibility.Hidden;
                ChoisirPatientPourRDVGrid.Visibility = Visibility.Hidden;
                ListeRDV.Visibility = Visibility.Visible;
                RDVGrid.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Aucune échographie n'est prévue pour aujourd'hui");
            }
        }

        //***********************************************************************Rapport CPN

        private void VoirRapportCPN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dtr = (DataRowView)rapportCPNDataGrid.SelectedItem;
                DateTime d = (DateTime)dtr["date_creation"];
                System.Diagnostics.Process.Start("C:/Users/HP/Desktop/RapportCPN/" + d.Day.ToString() + "-" + d.Month.ToString() + "-" + d.Year.ToString() + ".pdf");
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Il y a une erreur lors de l'ouverture du Rapport CPN ! Vérifier que le rapport existe");
            }
        }

        private void DeleteRapportCPN_Click(object sender,RoutedEventArgs e)
        {
            DataRowView dtr = (DataRowView)rapportCPNDataGrid.SelectedItem;
            RapportCPN.deleteRapport(dtr["id"].ToString());
            this.PageWindow_Loaded(sender,e);
        }

        private void chercherRapportCPN_click(object sender, RoutedEventArgs e)
        {
            string selectedItem = "";
            switch (comboBoxRapportCPN.SelectionBoxItem.ToString())
            {
                case "Mois":
                    selectedItem = "Mois";
                    break;
            }
            try
            {
                if (DatePickerRapportCPN.SelectedDate.ToString().Equals(""))
                    rapportCPNDataGrid.ItemsSource = RapportCPN.chercherRapportCPN(selectedItem, RapportCPNSearchText.Text.Replace("'", "''")).DefaultView;
                else
                    rapportCPNDataGrid.ItemsSource = RapportCPN.chercherRapportCPNAvecDate(selectedItem, RapportCPNSearchText.Text.Replace("'", "''"), (DateTime)DatePickerRapportCPN.SelectedDate).DefaultView;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        //***********************************************************************Rapport CPoN

        private void VoirRapportCPoN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dtr = (DataRowView)rapportCPoNDataGrid.SelectedItem;
                DateTime d = (DateTime)dtr["date_creation"];
                System.Diagnostics.Process.Start("C:/Users/HP/Desktop/RapportCPoN/" + d.Day.ToString() + "-" + d.Month.ToString() + "-" + d.Year.ToString() + ".pdf");
            }catch(Exception ex)
            {
                System.Windows.MessageBox.Show("Il y a une erreur lors de l'ouverture du Rapport CPoN ! Vérifier que le rapport existe");
            }
        }

        private void DeleteRapportCPoN_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dtr = (DataRowView)rapportCPoNDataGrid.SelectedItem;
            RapportCPoN.deleteRapport(dtr["id"].ToString());
            this.PageWindow_Loaded(sender, e);
        }

        private void chercherRapportCPoN_click(object sender, RoutedEventArgs e)
        {
            string selectedItem = "";
            switch (comboBoxRapportCPoN.SelectionBoxItem.ToString())
            {
                case "Mois":
                    selectedItem = "Mois";
                    break;
            }
            try
            {
                if (DatePickerRapportCPoN.SelectedDate.ToString().Equals(""))
                    rapportCPoNDataGrid.ItemsSource = RapportCPoN.chercherRapportCPoN(selectedItem, RapportCPoNSearchText.Text.Replace("'", "''")).DefaultView;
                else
                    rapportCPoNDataGrid.ItemsSource = RapportCPoN.chercherRapportCPoNAvecDate(selectedItem, RapportCPoNSearchText.Text.Replace("'", "''"), (DateTime)DatePickerRapportCPoN.SelectedDate).DefaultView;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        //***********************************************************************Rapport Journalier

        private void VoirRapportJournalier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dtr = (DataRowView)rapportJournalierDataGrid.SelectedItem;
                DateTime d = (DateTime)dtr["date_creation"];
                System.Diagnostics.Process.Start("C:/Users/HP/Desktop/RapportJournalier/" + d.Day.ToString() + "-" + d.Month.ToString() + "-" + d.Year.ToString() + ".pdf");
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Il y a une erreur lors de l'ouverture du Rapport Journalier ! Vérifier que le rapport existe");
            }
        }

        private void DeleteRapportJournalier_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dtr = (DataRowView)rapportJournalierDataGrid.SelectedItem;
            RapportJournalier.deleteRapport(dtr["id"].ToString());
            this.PageWindow_Loaded(sender, e);
        }

        private void chercherRapportJournalier_click(object sender, RoutedEventArgs e)
        {
            string selectedItem = "";
            switch (comboBoxRapportJournalier.SelectionBoxItem.ToString())
            {
                case "Mois":
                    selectedItem = "mois";
                    break;
                case "Jour":
                    selectedItem = "jour";
                    break;
            }
            try
            {
                if (DatePickerRapportJournalier.SelectedDate.ToString().Equals(""))
                    rapportJournalierDataGrid.ItemsSource = RapportJournalier.chercherRapportJournalier(selectedItem, RapportJournalierSearchText.Text.Replace("'", "''")).DefaultView;
                else
                    rapportJournalierDataGrid.ItemsSource = RapportJournalier.chercherRapportJournalierAvecDate(selectedItem, RapportJournalierSearchText.Text.Replace("'", "''"), (DateTime)DatePickerRapportJournalier.SelectedDate).DefaultView;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void CreerRapport_Click(object sender, RoutedEventArgs e)
        {
            RapportJournalier.CreerRapport();
            this.PageWindow_Loaded(sender,e);
        }

    }
}
