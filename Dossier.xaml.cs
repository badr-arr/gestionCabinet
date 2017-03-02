using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
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
    /// Logique d'interaction pour Dossier.xaml
    /// </summary>
    public partial class Dossier : Window
    {
        string id;
        string idDuDossier;
        Projet.Page page;
        Patient patient;
        string type;
        public Dossier(string cin,Projet.Page p)
        {
            this.patient = Projet.Patient.getPatient(cin);
            this.idDuDossier = DossierPatient.getIdDossier(this.patient.Id);
            this.page = p;
            InitializeComponent();
            Titre.Content = Titre.Content+" "+patient.Nom.Trim(' ')+" "+patient.Prenom.Trim(' ');
            NumDossierLabel.Content += patient.NumDossier;
            this.HideAllGrids();
            acceuilGrid.Visibility = Visibility.Visible;
        }

        private void HideAllGrids()
        {
            acceuilGrid.Visibility = Visibility.Hidden;
            RemplirCPNGrid.Visibility = Visibility.Hidden;
            ListeCPN.Visibility = Visibility.Hidden;
            cpnGrid.Visibility = Visibility.Hidden;
            cponGrid.Visibility = Visibility.Hidden;
            echographieGrid.Visibility = Visibility.Hidden;
            antecedentGrid.Visibility = Visibility.Hidden;
            RDVGrid.Visibility = Visibility.Hidden;
            RemplirPatientGrid.Visibility = Visibility.Hidden;
        }

        private void InformationPatient_click(object sender, RoutedEventArgs e)
        {
            this.HideAllGrids();
            this.RemplirChampPatientPourModification();
            RemplirPatientGrid.Visibility = Visibility.Visible;
        }

        private void Accueil_Click(object sender, RoutedEventArgs e)
        {
            this.HideAllGrids();
            acceuilGrid.Visibility = Visibility.Visible;
        }

        private void CPN_Click(object sender, RoutedEventArgs e)
        {
            this.HideAllGrids();
            cpnDataGrid.ItemsSource = CPN.AfficherCPNDuPatient(this.idDuDossier).DefaultView;
            ListeCPN.Visibility = Visibility.Visible;
            cpnGrid.Visibility = Visibility.Visible;
        }

        private void CPON_Click(object sender, RoutedEventArgs e)
        {
            this.HideAllGrids();
            cponDataGrid.ItemsSource = CPoN.AfficherCPoNDuPatient(this.idDuDossier).DefaultView;
            ListeCPoN.Visibility = Visibility.Visible;
            RemplirCPoNGrid.Visibility = Visibility.Hidden;
            cponGrid.Visibility = Visibility.Visible;
        }

        private void Echo_Click(object sender , RoutedEventArgs e)
        {
            this.HideAllGrids();
            echographieDataGrid.ItemsSource = Echographie.AfficherEchographieDuPatient(this.idDuDossier).DefaultView;
            RemplirEcho.Visibility = Visibility.Hidden;
            ListeEcho.Visibility = Visibility.Visible;
            echographieGrid.Visibility = Visibility.Visible;
        }

        private void Antecedent_Click(object sender, RoutedEventArgs e)
        {
            this.HideAllGrids();
            antecedentDataGrid.ItemsSource = Projet.Antecedent.AfficherAntecedentDuPatient(this.idDuDossier).DefaultView;
            ListeAntecedent.Visibility = Visibility.Visible;
            RemplirAntecedent.Visibility = Visibility.Hidden;
            antecedentGrid.Visibility = Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Projet.BDProjetDataSet bDProjetDataSet = ((Projet.BDProjetDataSet)(this.FindResource("bDProjetDataSet")));
                // Chargez les données dans la table cpn. Vous pouvez modifier ce code si nécessaire.
                Projet.BDProjetDataSetTableAdapters.cpnTableAdapter bDProjetDataSetcpnTableAdapter = new Projet.BDProjetDataSetTableAdapters.cpnTableAdapter();
                bDProjetDataSetcpnTableAdapter.Fill(bDProjetDataSet.cpn);
                System.Windows.Data.CollectionViewSource cpnViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("cpnViewSource")));
                cpnViewSource.View.MoveCurrentToFirst();
                // Chargez les données dans la table cpon. Vous pouvez modifier ce code si nécessaire.
                Projet.BDProjetDataSetTableAdapters.cponTableAdapter bDProjetDataSetcponTableAdapter = new Projet.BDProjetDataSetTableAdapters.cponTableAdapter();
                bDProjetDataSetcponTableAdapter.Fill(bDProjetDataSet.cpon);
                System.Windows.Data.CollectionViewSource cponViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("cponViewSource")));
                cponViewSource.View.MoveCurrentToFirst();
                // Chargez les données dans la table echographie. Vous pouvez modifier ce code si nécessaire.
                Projet.BDProjetDataSetTableAdapters.echographieTableAdapter bDProjetDataSetechographieTableAdapter = new Projet.BDProjetDataSetTableAdapters.echographieTableAdapter();
                bDProjetDataSetechographieTableAdapter.Fill(bDProjetDataSet.echographie);
                System.Windows.Data.CollectionViewSource echographieViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("echographieViewSource")));
                echographieViewSource.View.MoveCurrentToFirst();
                // Chargez les données dans la table antecedent. Vous pouvez modifier ce code si nécessaire.
                Projet.BDProjetDataSetTableAdapters.antecedentTableAdapter bDProjetDataSetantecedentTableAdapter = new Projet.BDProjetDataSetTableAdapters.antecedentTableAdapter();
                bDProjetDataSetantecedentTableAdapter.Fill(bDProjetDataSet.antecedent);
                System.Windows.Data.CollectionViewSource antecedentViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("antecedentViewSource")));
                antecedentViewSource.View.MoveCurrentToFirst();
                // Chargez les données dans la table rdv. Vous pouvez modifier ce code si nécessaire.
                Projet.BDProjetDataSetTableAdapters.rdvTableAdapter bDProjetDataSetrdvTableAdapter = new Projet.BDProjetDataSetTableAdapters.rdvTableAdapter();
                bDProjetDataSetrdvTableAdapter.Fill(bDProjetDataSet.rdv);
                System.Windows.Data.CollectionViewSource rdvViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("rdvViewSource")));
                rdvViewSource.View.MoveCurrentToFirst();
            }
            catch (Exception)
            {

            }
        }

        private void Agenda_Click(object sender, RoutedEventArgs e)
        {
            this.HideAllGrids();
            this.initialiserRDVChamp();
            RemplirRDVGrid.Visibility = Visibility.Hidden;
            rdvDataGrid.ItemsSource = RDVS.AfficherRDVDuPatient(this.patient.Id).DefaultView;
            ListeRDV.Visibility = Visibility.Visible;
            RDVGrid.Visibility = Visibility.Visible;
        }

        private void Deconnexion(object sender,RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
            this.page.Close();
        }

        //**********************************************************************CPN Grid
        private void AnnulerAjoutCPN_Click(object sender, RoutedEventArgs e)
        {
            this.HideAllGrids();
            RemplirCPNGrid.Visibility = Visibility.Hidden;
            ListeCPN.Visibility = Visibility.Visible;
            cpnGrid.Visibility = Visibility.Visible;
        }

        private void AjouterCPN_Click(object sender, RoutedEventArgs e)
        {
            this.HideAllGrids();
            this.initialiserChampCPN();
            this.type = "ajouter";
            RemplirCPNGrid.Visibility = Visibility.Visible;
            cpnGrid.Visibility = Visibility.Visible;
        }

        private void EnregistrerCPN_Click(object sender, RoutedEventArgs e)
        {
            CPN c = new CPN();
            try
            {
                c.IdDossier = this.idDuDossier;

                if (!((bool)cpn1.IsChecked) && !((bool)cpn2.IsChecked) && !((bool)cpn3.IsChecked) && !((bool)cpn4.IsChecked) && !((bool)autreCpn.IsChecked))
                {
                    System.Windows.MessageBox.Show("Veuillez choisir un type de CPN");                    
                    throw new Exception();
                }
                else if (((bool)cpn1.IsChecked) && !((bool)cpn2.IsChecked) && !((bool)cpn3.IsChecked) && !((bool)cpn4.IsChecked) && !((bool)autreCpn.IsChecked))
                    c.Type = "CPN1";
                else if (((bool)cpn2.IsChecked) && !((bool)cpn1.IsChecked) && !((bool)cpn3.IsChecked) && !((bool)cpn4.IsChecked) && !((bool)autreCpn.IsChecked))
                    c.Type = "CPN2";
                else if (((bool)cpn3.IsChecked) && !((bool)cpn2.IsChecked) && !((bool)cpn1.IsChecked) && !((bool)cpn4.IsChecked) && !((bool)autreCpn.IsChecked))
                    c.Type = "CPN3";
                else if (((bool)cpn4.IsChecked) && !((bool)cpn2.IsChecked) && !((bool)cpn3.IsChecked) && !((bool)cpn1.IsChecked) && !((bool)autreCpn.IsChecked))
                    c.Type = "CPN4";
                else if (((bool)autreCpn.IsChecked) && !((bool)cpn2.IsChecked) && !((bool)cpn3.IsChecked) && !((bool)cpn4.IsChecked) && !((bool)cpn1.IsChecked))
                    c.Type = "Autre";
                else
                {
                    System.Windows.MessageBox.Show("Plusieurs champs sont séléctionner dans le type de CPN");
                    throw new Exception();
                }


                if (c.Type == "CPN3" && !(bool)mois8.IsChecked && !(bool)mois9.IsChecked)
                {
                    System.Windows.MessageBox.Show("Veuillez cocher le mois de la patiente");
                    throw new Exception();
                }
                else if (c.Type == "CPN3" && (bool)mois8.IsChecked && (bool)mois9.IsChecked)
                {
                    System.Windows.MessageBox.Show("Les deux cases du 3ème trimestre sont cochées");
                    throw new Exception();
                }
                else if (c.Type == "CPN3" && (bool)mois8.IsChecked && !(bool)mois9.IsChecked)
                    c.MoisPatiente = "8";
                else if (c.Type == "CPN3" && !(bool)mois8.IsChecked && (bool)mois9.IsChecked)
                    c.MoisPatiente = "9";
                else if (c.Type != "CPN3" && ((bool)mois8.IsChecked || (bool)mois9.IsChecked))
                {
                    System.Windows.MessageBox.Show("Vous ne pouvez pas cocher une case en cas de trimestre différent du 3ème");
                    throw new Exception();
                }


                if ((bool)bilanEffectue.IsChecked)
                    c.BilanAvant = "Oui";

                if ((bool)bilanEffectue.IsChecked)
                    c.DescriptionBilanAvant = bilanEffectueChamp.Text.Replace("'","''");
                else if (!bilanEffectueChamp.Text.Trim(' ').Equals("") && !(bool)bilanEffectue.IsChecked)
                {
                    System.Windows.MessageBox.Show("Veuillez cocher la case bilan pour ajouter les bilans éffectués");
                    throw new Exception();
                }


                if ((bool)ai.IsChecked && !(bool)ni.IsChecked)
                    c.Inscription = "AI";
                else if ((bool)ni.IsChecked && !(bool)ai.IsChecked)
                    c.Inscription = "NI";
                else
                {
                    System.Windows.MessageBox.Show("Veuillez remplir le type d'inscription");
                    throw new Exception();
                }


                if ((bool)medicalise.IsChecked)
                    c.Medicalise = "Oui";

                if ((bool)medicalise.IsChecked)
                    c.CauseMedicalise = causeMedicalise.Text.Replace("'", "''");
                else if (!causeMedicalise.Text.Trim(' ').Equals("") && !(bool)medicalise.IsChecked)
                {
                    System.Windows.MessageBox.Show("Veuillez cocher la case Médicalisée pour ajouter les causes");
                    throw new Exception();
                }


                if ((bool)ouiRisque.IsChecked) 
                    c.Risque = "Oui";


                if ((bool)metrorragie.IsChecked && (bool)ouiRisque.IsChecked)
                    c.Metrorragie = "Oui";
                else if ((bool) metrorragie.IsChecked && !(bool)ouiRisque.IsChecked)
                {
                    System.Windows.MessageBox.Show("Veuillez cocher la case du risque avant d'ajouter les types");
                    throw new Exception();
                }

                if ((bool)hta.IsChecked && (bool)ouiRisque.IsChecked)
                    c.HTA = "Oui";
                else if ((bool)hta.IsChecked && !(bool)ouiRisque.IsChecked)
                {
                    System.Windows.MessageBox.Show("Veuillez cocher la case du risque avant d'ajouter les types");
                    throw new Exception();
                }

                if ((bool)anemie.IsChecked && (bool)ouiRisque.IsChecked)
                    c.Anemie = "Oui";
                else if ((bool)anemie.IsChecked && !(bool)ouiRisque.IsChecked)
                {
                    System.Windows.MessageBox.Show("Veuillez cocher la case du risque avant d'ajouter les types");
                    throw new Exception();
                }

                if ((bool)diabete.IsChecked && (bool)ouiRisque.IsChecked)
                    c.Diabete = "Oui";
                else if ((bool)diabete.IsChecked && !(bool)ouiRisque.IsChecked)
                {
                    System.Windows.MessageBox.Show("Veuillez cocher la case du risque avant d'ajouter les types");
                    throw new Exception();
                }

                if ((bool)cardiopathie.IsChecked && (bool)ouiRisque.IsChecked)
                    c.Cardiopathie = "Oui";
                else if ((bool)cardiopathie.IsChecked && !(bool)ouiRisque.IsChecked)
                {
                    System.Windows.MessageBox.Show("Veuillez cocher la case du risque avant d'ajouter les types");
                    throw new Exception();
                }

                if ((bool)infection.IsChecked && (bool)ouiRisque.IsChecked)
                    c.Infection = "Oui";
                else if ((bool)infection.IsChecked && !(bool)ouiRisque.IsChecked)
                {
                    System.Windows.MessageBox.Show("Veuillez cocher la case du risque avant d'ajouter les types");
                    throw new Exception();
                }

                if ((bool)autreRisque.IsChecked && (bool)ouiRisque.IsChecked)
                    c.Autre = "Oui";
                else if ((bool)autreRisque.IsChecked && !(bool)ouiRisque.IsChecked)
                {
                    System.Windows.MessageBox.Show("Veuillez cocher la case du risque avant d'ajouter les types");
                    throw new Exception();
                }


                if ((bool)autreRisque.IsChecked)
                    c.DescriptionAutre = autreRisqueChamp.Text.Replace("'", "''");
                else if (!autreRisqueChamp.Text.Trim(' ').Equals("") && !(bool)autreRisque.IsChecked) 
                {
                    System.Windows.MessageBox.Show("Veuillez cocher la case autre avant de remplir le champ autres risques");
                    throw new Exception();
                }

                if ((bool)pec.IsChecked && !(bool)referee.IsChecked)
                    c.GestionGAR = "PEC";
                else if (!(bool)pec.IsChecked && (bool)referee.IsChecked)
                    c.GestionGAR = "Référée";
                else if ((bool)pec.IsChecked && (bool)referee.IsChecked)
                {
                    System.Windows.MessageBox.Show("PEC et référée sont tous les deux cochés");
                    throw new Exception();
                }


                if ((bool)pec.IsChecked || (bool)referee.IsChecked)
                    c.DescriptionGAR = gestionGAR.Text.Replace("'", "''");
                else if (!(bool)pec.IsChecked && !(bool)referee.IsChecked && !gestionGAR.Text.Trim(' ').Equals(""))
                {
                    System.Windows.MessageBox.Show("Veuillez choisir le type de gestion de GAR avant d'ajouter la description");
                    throw new Exception();
                }

                if ((bool)ferRecu.IsChecked)
                    c.Fer = "Oui";

                if ((bool)vitamineDRecu.IsChecked)
                    c.VitamineD = "Oui";

                c.DateCPN = (DateTime)dateConsultation.SelectedDate;
                c.Bilan = bilanGeneral.Text.Replace("'", "''");

                if (this.type == "ajouter")
                    CPN.persistCPN(c);
                else if (this.type == "modifier")
                {
                    c.Id = this.id;
                    CPN.ModifyCPN(c);
                }
                RemplirCPNGrid.Visibility = Visibility.Hidden;
                ListeCPN.Visibility = Visibility.Visible;
                cpnDataGrid.ItemsSource = CPN.AfficherCPNDuPatient(this.idDuDossier).DefaultView;
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Veuillez vérifier tous les champs");
            }
        }

        private void initialiserChampCPN()
        {
            mois8.IsChecked = false;
            mois9.IsChecked = false;
            CPNSearchText.Text = "";
            DatePickerCPN.Text = "";
            cpn1.IsChecked = false;
            cpn2.IsChecked = false;
            cpn3.IsChecked = false;
            cpn4.IsChecked = false;
            autreCpn.IsChecked = false;
            bilanEffectue.IsChecked = false;
            bilanEffectueChamp.Text = "";
            ai.IsChecked = false;
            ni.IsChecked = false;
            medicalise.IsChecked = false;
            causeMedicalise.Text = "";
            ouiRisque.IsChecked = false;
            metrorragie.IsChecked = false;
            hta.IsChecked = false;
            cardiopathie.IsChecked = false;
            infection.IsChecked = false;
            diabete.IsChecked = false;
            anemie.IsChecked = false;
            autreRisque.IsChecked = false;
            autreRisqueChamp.Text = "";
            pec.IsChecked = false;
            referee.IsChecked = false;
            gestionGAR.Text = "";
            ferRecu.IsChecked = false;
            vitamineDRecu.IsChecked = false;
            dateConsultation.Text = "";
            bilanGeneral.Text = "";
        }

        private void ModifierCPN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.initialiserChampCPN();
                this.type = "modifier";
                DataRowView dtr = (DataRowView)cpnDataGrid.SelectedItem;
                this.id = dtr["id"].ToString();
                CPN p = CPN.getCPN(this.id);
                if (p.Type.Trim(' ') == "CPN1")
                    cpn1.IsChecked = true;
                else if (p.Type.Trim(' ') == "CPN2")
                    cpn2.IsChecked = true;
                else if (p.Type.Trim(' ') == "CPN3")
                    cpn3.IsChecked = true;
                else if (p.Type.Trim(' ') == "CPN4")
                    cpn4.IsChecked = true;
                else if (p.Type.Trim(' ') == "Autre")
                    autreCpn.IsChecked = true;

                if (p.MoisPatiente.Trim(' ') == "8")
                    mois8.IsChecked = true;
                else if (p.MoisPatiente.Trim(' ') == "9")
                    mois9.IsChecked = true;

                if (p.BilanAvant.Trim(' ') == "Oui")
                    bilanEffectue.IsChecked = true;

                bilanEffectueChamp.Text = p.DescriptionBilanAvant;

                if (p.Inscription.Trim(' ') == "AI")
                    ai.IsChecked = true;
                else if (p.Inscription.Trim(' ') == "NI")
                    ni.IsChecked = true;
                if (p.Medicalise.Trim(' ') == "Oui")
                    medicalise.IsChecked = true;
                causeMedicalise.Text = p.CauseMedicalise;
                if (p.Risque.Trim(' ') == "Oui")
                    ouiRisque.IsChecked = true;
                if(p.Metrorragie.Trim(' ') == "Oui")
                    metrorragie.IsChecked = true;
                if(p.HTA.Trim(' ') == "Oui")
                    hta.IsChecked = true;
                if (p.Cardiopathie.Trim(' ') == "Oui")
                    cardiopathie.IsChecked = true;
                if (p.Infection.Trim(' ') == "Oui")
                    infection.IsChecked = true;
                if (p.Diabete.Trim(' ') == "Oui")
                    diabete.IsChecked = true;
                if (p.Anemie.Trim(' ') == "Oui")
                    anemie.IsChecked = true;
                if (p.Autre.Trim(' ') == "Oui")
                    autreRisque.IsChecked = true;
                autreRisqueChamp.Text = p.DescriptionAutre;
                if (p.GestionGAR.Trim(' ') == "PEC")
                    pec.IsChecked = true;
                else if (p.GestionGAR.Trim(' ') == "Référée")
                    referee.IsChecked = true;
                gestionGAR.Text = p.DescriptionGAR;
                if (p.Fer.Trim(' ') == "Oui")
                    ferRecu.IsChecked = true;
                if (p.VitamineD.Trim(' ') == "Oui")
                    vitamineDRecu.IsChecked = true;
                dateConsultation.Text = p.DateCPN.ToString();
                bilanGeneral.Text = p.Bilan;
                ListeCPN.Visibility = Visibility.Hidden;
                RemplirCPNGrid.Visibility = Visibility.Visible;
                cpnGrid.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

        }

        private void SupprimerCPN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dtr = (DataRowView)cpnDataGrid.SelectedItem;
                string message, caption;
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                DataRowView row = (DataRowView)((System.Windows.Controls.Button)e.Source).DataContext;
                message = "Êtes-vous sûre de vouloir supprimer la CPN séléctionnée ?";
                caption = "Suppression de CPN";
                result = System.Windows.Forms.MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    CPN.deleteCPN(dtr["id"].ToString());
                    cpnDataGrid.ItemsSource = CPN.AfficherCPNDuPatient(this.idDuDossier).DefaultView;
                }
            }
            catch (Exception) { }
        }

        private void chercherCPN_click(object sender, RoutedEventArgs e)
        {
            string selectedItem = "";
            switch (comboBoxCPN.SelectionBoxItem.ToString())
            {
                case "Type de CPN":
                    selectedItem = "type";
                    break;
            }
            try
            {
                if (DatePickerCPN.SelectedDate.ToString().Equals(""))
                    cpnDataGrid.ItemsSource = CPN.chercherCPN(selectedItem, CPNSearchText.Text.Replace("'", "''"), this.idDuDossier).DefaultView;
                else
                    cpnDataGrid.ItemsSource = CPN.chercherCPNAvecDate(selectedItem, CPNSearchText.Text.Replace("'", "''"), this.idDuDossier ,(DateTime)DatePickerCPN.SelectedDate).DefaultView;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }


        }

        //*************************************************************CPoN
        private void AnnulerAjoutCPoN_Click(object sender, RoutedEventArgs e)
        {
            this.HideAllGrids();
            RemplirCPoNGrid.Visibility = Visibility.Hidden;
            ListeCPoN.Visibility = Visibility.Visible;
            cponGrid.Visibility = Visibility.Visible;
        }

        private void AjouterCPoN_Click(object sender, RoutedEventArgs e)
        {
            this.HideAllGrids();
            this.type = "ajouter";
            this.initialiserChampCPoN();
            ListeCPoN.Visibility = Visibility.Hidden;
            RemplirCPoNGrid.Visibility = Visibility.Visible;
            cponGrid.Visibility = Visibility.Visible;
            
        }

        private void EnregistrerCPoN_Click(object sender, RoutedEventArgs e)
        {
            CPoN c = new CPoN();
            try
            {
                if (!(bool)cpon1.IsChecked && !(bool)cpon2.IsChecked && !(bool)autreCpon.IsChecked)
                {
                    System.Windows.MessageBox.Show("Veuillez cocher le type de la CPoN");
                    throw new Exception();
                }
                else if ((bool)cpon1.IsChecked && !(bool)cpon2.IsChecked && !(bool)autreCpon.IsChecked)
                    c.NomConsultation = "CPoN1";
                else if ((bool)cpon2.IsChecked && !(bool)cpon1.IsChecked && !(bool)autreCpon.IsChecked)
                    c.NomConsultation = "CPoN2";
                else if ((bool)autreCpon.IsChecked && !(bool)cpon2.IsChecked && !(bool)cpon1.IsChecked)
                    c.NomConsultation = "Autre";
                else
                {
                    System.Windows.MessageBox.Show("Plus d'un champ est coché dans le type de CPoN");
                    throw new Exception();
                }

                if ((bool)ouiDeces.IsChecked)
                    c.Deces = "Oui";
                c.DateEffectiveAccouchement = (DateTime) dateEffectiveAccouchement.SelectedDate;
                c.LieuAccouchement = LieuAccouchement.Text.Replace("'","''");

                if ((bool)surveille.IsChecked && !(bool)nonSurveille.IsChecked)
                    c.TypeLieuAccouchement = "Surveillé";
                else if (!(bool)surveille.IsChecked && (bool)nonSurveille.IsChecked)
                    c.TypeLieuAccouchement = "Non surveillé";
                else if ((bool)surveille.IsChecked && (bool)nonSurveille.IsChecked)
                {
                    System.Windows.MessageBox.Show("Plus d'un champ est coché dans le type de lieu d'accouchement");
                    throw new Exception();
                }

                if (!(bool)tardive.IsChecked && !(bool)precoce.IsChecked && !(bool)autreType.IsChecked)
                {
                    System.Windows.MessageBox.Show("Veuillez choisir un type de consultation");
                    throw new Exception();
                }
                else if ((bool)tardive.IsChecked && !(bool)precoce.IsChecked && !(bool)autreType.IsChecked)
                    c.TypeConsultation = "Tardive";
                else if (!(bool)tardive.IsChecked && (bool)precoce.IsChecked && !(bool)autreType.IsChecked)
                    c.TypeConsultation = "Précoce";
                else if (!(bool)tardive.IsChecked && !(bool)precoce.IsChecked && (bool)autreType.IsChecked)
                    c.TypeConsultation = "Autre";
                else
                {
                    System.Windows.MessageBox.Show("Plus d'un champ est coché dans le type de consultation");
                    throw new Exception();
                }

                if ((bool)autreType.IsChecked)
                    c.DescriptionAutre = descriptionTypeCpon.Text.Replace("'", "''");
                else if(!(bool)autreType.IsChecked && !descriptionTypeCpon.Text.Trim(' ').Equals(""))
                {
                    System.Windows.MessageBox.Show("Pour définir le type autre veuillez cocher la case autre dans le type de consultation");
                    throw new Exception();
                }

                if ((bool)ouiComplication.IsChecked)
                    c.Complication = "Oui";

                
                
                if ((bool)hemorragie.IsChecked && (bool)ouiComplication.IsChecked)
                        c.Hemorragie = "Oui";
                else if ((bool)hemorragie.IsChecked && !(bool)ouiComplication.IsChecked)
                {
                    System.Windows.MessageBox.Show("Pour définir le type de complication veuillez cocher la case ");
                    throw new Exception();
                }

                if ((bool)infectionPuerperale.IsChecked && (bool)ouiComplication.IsChecked)
                        c.Infection = "Oui";
                else if ((bool)infectionPuerperale.IsChecked && !(bool)ouiComplication.IsChecked)
                {
                    System.Windows.MessageBox.Show("Pour définir le type de complication veuillez cocher la case ");
                    throw new Exception();
                }

                if ((bool)eclampsie.IsChecked && (bool)ouiComplication.IsChecked)
                        c.Eclampsie = "Oui";
                else if ((bool)eclampsie.IsChecked && !(bool)ouiComplication.IsChecked)
                {
                    System.Windows.MessageBox.Show("Pour définir le type de complication veuillez cocher la case ");
                    throw new Exception();
                }

                if ((bool)phlebite.IsChecked && (bool)ouiComplication.IsChecked)
                    c.Phlebite = "Oui";
                else if ((bool)phlebite.IsChecked && !(bool)ouiComplication.IsChecked)
                {
                    System.Windows.MessageBox.Show("Pour définir le type de complication veuillez cocher la case ");
                    throw new Exception();
                }

                if ((bool)complicationMammaire.IsChecked && (bool)ouiComplication.IsChecked)
                        c.Mammaire = "Oui";
                else if ((bool)complicationMammaire.IsChecked && !(bool)ouiComplication.IsChecked)
                {
                    System.Windows.MessageBox.Show("Pour définir le type de complication veuillez cocher la case ");
                    throw new Exception();
                }

                if ((bool)anemieComplication.IsChecked && (bool)ouiComplication.IsChecked)
                        c.Anemie = "Oui";
                else if ((bool)anemieComplication.IsChecked && !(bool)ouiComplication.IsChecked )
                {
                    System.Windows.MessageBox.Show("Pour définir le type de complication veuillez cocher la case ");
                    throw new Exception();
                }
                if ((bool)autreComplication.IsChecked && (bool)ouiComplication.IsChecked)
                    c.Autre = "Oui";
                else if ((bool)autreComplication.IsChecked && !(bool)ouiComplication.IsChecked)
                {
                    System.Windows.MessageBox.Show("Pour définir le type de complication veuillez cocher la case ");
                    throw new Exception();
                }


                if ((bool)autreComplication.IsChecked)
                    c.DescriptionAutre = autreComplicationChamp.Text.Replace("'", "''");
                else if (!(bool)autreComplication.IsChecked && !autreComplicationChamp.Text.Trim(' ').Equals(""))
                {
                    System.Windows.MessageBox.Show("Pour définir les autres risques veuillez cocher la case autre dans le type de consultation");
                    throw new Exception();
                }


                if ((bool)pecCpon.IsChecked && !(bool)refereeCpon.IsChecked)
                    c.GestionComplication = "PEC";
                else if (!(bool)pecCpon.IsChecked && (bool)refereeCpon.IsChecked)
                    c.GestionComplication = "Référée";
                else if ((bool)pecCpon.IsChecked && (bool)refereeCpon.IsChecked)
                {
                    System.Windows.MessageBox.Show("Plus d'une case est cochée dans la gestion de complication");
                    throw new Exception();
                }

                if (((bool)pecCpon.IsChecked || (bool)refereeCpon.IsChecked))
                    c.DescriptionGestionComplication = gestionComplication.Text.Replace("'", "''");
                else if (!(bool)pecCpon.IsChecked && !(bool)refereeCpon.IsChecked && !gestionComplication.Text.Trim(' ').Equals(""))
                {
                    System.Windows.MessageBox.Show("Pour décrire la gestion de complication veuillez cocher une case de la gestion des cas compliqués");
                    throw new Exception();
                }

                if ((bool)ferRecuCpon.IsChecked)
                    c.Fer = "Oui";

                c.DateConsultation = (DateTime)dateCpon.SelectedDate;
                c.BilanGeneral = bilanGeneralCpon.Text.Replace("'", "''");
                c.IdDossier = this.idDuDossier;
                if (this.type == "ajouter")
                {
                    CPoN.persistCPoN(c);
                    cponDataGrid.ItemsSource = CPoN.AfficherCPoNDuPatient(this.idDuDossier).DefaultView;
                    ListeCPoN.Visibility = Visibility.Visible;
                    RemplirCPoNGrid.Visibility = Visibility.Hidden;
                }
                else if (this.type == "modifier")
                {
                    c.Id = this.id;
                    c.IdDossier = this.idDuDossier; 
                    CPoN.ModifyCPoN(c);
                    cponDataGrid.ItemsSource = CPoN.AfficherCPoNDuPatient(this.idDuDossier).DefaultView;
                    ListeCPoN.Visibility = Visibility.Visible;
                    RemplirCPoNGrid.Visibility = Visibility.Hidden;
                    
                }


            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Veuillez vérifier tous les champs");
            }
        }

        private void initialiserChampCPoN()
        {
            cpon1.IsChecked = false;
            cpon2.IsChecked = false;
            autreCpon.IsChecked = false;
            ouiDeces.IsChecked = false;
            surveille.IsChecked = false;
            nonSurveille.IsChecked = false;
            tardive.IsChecked = false;
            precoce.IsChecked = false;
            autreType.IsChecked = false;
            ouiComplication.IsChecked = false;
            hemorragie.IsChecked = false;
            infectionPuerperale.IsChecked = false;
            eclampsie.IsChecked = false;
            phlebite.IsChecked = false;
            complicationMammaire.IsChecked = false;
            anemieComplication.IsChecked = false;
            autreComplication.IsChecked = false;
            pecCpon.IsChecked = false;
            refereeCpon.IsChecked = false;
            ferRecuCpon.IsChecked = false;
            dateEffectiveAccouchement.Text = "";
            LieuAccouchement.Text = "";
            descriptionTypeCpon.Text = "";
            autreComplicationChamp.Text = "";
            gestionComplication.Text = "";
            dateCpon.Text = "";
            bilanGeneralCpon.Text = "";
            CPoNSearchText.Text = "";
            DatePickerCPoN.Text = "";
        }

        private void ModifierCPoN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.initialiserChampCPoN();
                this.type = "modifier";
                DataRowView dtr = (DataRowView)cponDataGrid.SelectedItem;
                this.id = dtr["id"].ToString();
                CPoN p = CPoN.getCPoN(this.id);

                if (p.NomConsultation.Trim(' ') == "CPoN1")
                    cpon1.IsChecked = true;
                else if (p.NomConsultation.Trim(' ') == "CPoN2")
                    cpon2.IsChecked = true;
                else if (p.NomConsultation.Trim(' ') == "Autre")
                    autreCpon.IsChecked = true;

                if (p.Deces.Trim(' ') == "Oui")
                    ouiDeces.IsChecked = true;

                dateEffectiveAccouchement.Text = p.DateEffectiveAccouchement.ToString();

                LieuAccouchement.Text = p.LieuAccouchement;

                if (p.TypeLieuAccouchement.Trim(' ') == "Surveillé")
                    surveille.IsChecked = true;
                else if (p.TypeLieuAccouchement.Trim(' ') == "Non surveillé")
                    nonSurveille.IsChecked = true;

                if (p.TypeConsultation.Trim(' ') == "Tardive")
                    tardive.IsChecked = true;
                else if (p.TypeConsultation.Trim(' ') == "Précoce")
                    precoce.IsChecked = true;
                else if (p.TypeConsultation.Trim(' ') == "Autre")
                    autreType.IsChecked = true;

                descriptionTypeCpon.Text = p.DescriptionAutre;

                if (p.Complication.Trim(' ') == "Oui")
                    ouiComplication.IsChecked = true;
                if (p.Hemorragie.Trim(' ') == "Oui")
                    hemorragie.IsChecked = true;
                if (p.Phlebite.Trim(' ') == "Oui")
                    phlebite.IsChecked = true;
                if (p.Eclampsie.Trim(' ') == "Oui")
                    eclampsie.IsChecked = true;
                if (p.Infection.Trim(' ') == "Oui")
                    infectionPuerperale.IsChecked = true;
                if (p.Mammaire.Trim(' ') == "Oui")
                    complicationMammaire.IsChecked = true;
                if (p.Anemie.Trim(' ') == "Oui")
                    anemieComplication.IsChecked = true;
                if (p.Autre.Trim(' ') == "Oui")
                    autreComplication.IsChecked = true;

                autreComplicationChamp.Text = p.DescriptionAutre;

                if (p.GestionComplication.Trim(' ') == "PEC")
                    pecCpon.IsChecked = true;
                else if (p.GestionComplication.Trim(' ') == "Référée")
                    refereeCpon.IsChecked=true;

                gestionComplication.Text = p.DescriptionGestionComplication;

                if (p.Fer.Trim(' ') == "Oui")
                    ferRecuCpon.IsChecked = true;

                dateCpon.Text = p.DateConsultation.ToString();

                bilanGeneralCpon.Text = p.BilanGeneral;

                RemplirCPoNGrid.Visibility = Visibility.Visible;
                ListeCPoN.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

        }

        private void SupprimerCPoN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dtr = (DataRowView)cponDataGrid.SelectedItem;
                string message, caption;
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                DataRowView row = (DataRowView)((System.Windows.Controls.Button)e.Source).DataContext;
                message = "Êtes-vous sûre de vouloir supprimer la CPoN séléctionnée ?";
                caption = "Suppression de CPoN";
                result = System.Windows.Forms.MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    CPoN.deleteCPoN(dtr["id"].ToString());
                    cponDataGrid.ItemsSource = CPoN.AfficherCPoNDuPatient(this.idDuDossier).DefaultView;
                }
            }
            catch (Exception) { }
        }

        private void chercherCPoN_click(object sender, RoutedEventArgs e)
        {
            string selectedItem = "";
            switch (comboBoxCPoN.SelectionBoxItem.ToString())
            {
                case "CPoN":
                    selectedItem = "nom_consulation";
                    break;
                case "Type de consultation":
                    selectedItem = "type_consultation";
                    break;
            }
            try
            {
                if (DatePickerCPoN.SelectedDate.ToString().Equals(""))
                    cponDataGrid.ItemsSource = CPoN.chercherCPoN(selectedItem, CPoNSearchText.Text.Replace("'", "''"), this.idDuDossier).DefaultView;
                else
                    cponDataGrid.ItemsSource = CPoN.chercherCPoNAvecDate(selectedItem, CPoNSearchText.Text.Replace("'", "''"), this.idDuDossier, (DateTime)DatePickerCPoN.SelectedDate).DefaultView;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }


        }

        private void CalculerNombreJour_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime StartDate = (DateTime)dateEffectiveAccouchement.SelectedDate;
                double a = (DateTime.Today - StartDate).TotalDays;
                System.Windows.MessageBox.Show(a.ToString());
            }catch(Exception)
            {
                System.Windows.MessageBox.Show("Veuillez saisir le date d'accouchement");
            }
        }

        //*************************************************************Echographie
        private void AjouterEcho_Click(object sender, RoutedEventArgs e)
        {
            this.type = "ajouter";
            this.initialiserChampEcho();
            RemplirEcho.Visibility = Visibility.Visible;
            ListeEcho.Visibility = Visibility.Hidden;
            echographieGrid.Visibility = Visibility.Visible;
        }

        private void initialiserChampEcho()
        {
            dateEcho.Text = "";
            DatePickerEcho.Text = "";
            bilanEcho.Text = "";
        }

        private void EnregistrerEcho_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Echographie echo = new Echographie();
                echo.IdDossier = this.idDuDossier;
                echo.DateEchographie = (DateTime) dateEcho.SelectedDate;
                echo.DescriptionEchographie = bilanEcho.Text.Replace("'", "''");
                if (this.type == "ajouter")
                {
                    Echographie.persistEchographie(echo);
                    echographieDataGrid.ItemsSource = Echographie.AfficherEchographieDuPatient(this.idDuDossier).DefaultView;
                    ListeEcho.Visibility = Visibility.Visible;
                    RemplirEcho.Visibility = Visibility.Hidden;
                }
                else if (this.type == "modifier")
                {
                    echo.Id = this.id;
                    Echographie.ModifyEchographie(echo);
                    echographieDataGrid.ItemsSource = Echographie.AfficherEchographieDuPatient(this.idDuDossier).DefaultView;
                    ListeEcho.Visibility = Visibility.Visible;
                    RemplirEcho.Visibility = Visibility.Hidden;
                }
            }
            catch
            {

            }
        }

        private void AnnulerEcho_Click(object sender, RoutedEventArgs e)
        {
            ListeEcho.Visibility = Visibility.Visible;
            RemplirEcho.Visibility = Visibility.Hidden;

        }

        private void ModifierEcho_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.initialiserChampEcho();
                this.type = "modifier";
                DataRowView dtr = (DataRowView)echographieDataGrid.SelectedItem;
                this.id = dtr["id"].ToString();
                Echographie p = Echographie.getEcho(this.id);
                dateEcho.Text = p.DateEchographie.ToString();
                bilanEcho.Text = p.DescriptionEchographie;
                ListeEcho.Visibility = Visibility.Hidden;
                RemplirEcho.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {}
        }

        private void SupprimerEcho_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dtr = (DataRowView)echographieDataGrid.SelectedItem;
                string message, caption;
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                DataRowView row = (DataRowView)((System.Windows.Controls.Button)e.Source).DataContext;
                message = "Êtes-vous sûre de vouloir supprimer l'échographie séléctionnée ?";
                caption = "Suppression d'échographie";
                result = System.Windows.Forms.MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    Echographie.deleteEchographie(dtr["id"].ToString());
                    echographieDataGrid.ItemsSource = Echographie.AfficherEchographieDuPatient(this.idDuDossier).DefaultView;
                }
            }
            catch (Exception) { }
        }

        private void chercherEcho_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                echographieDataGrid.ItemsSource = Echographie.chercherEchographieAvecDate(this.idDuDossier, (DateTime)DatePickerEcho.SelectedDate).DefaultView;
            }
            catch(Exception)
            {
                echographieDataGrid.ItemsSource = Echographie.AfficherEchographieDuPatient(this.idDuDossier).DefaultView;
            }
            
        }

        //**************************************************************Antecedent
        private void AjouterAntecedent_Click(object sender, RoutedEventArgs e)
        {
            this.type = "ajouter";
            this.initialiserAntecedentChamp();
            RemplirAntecedent.Visibility = Visibility.Visible;
            ListeAntecedent.Visibility = Visibility.Hidden;
            antecedentGrid.Visibility = Visibility.Visible;
        }

        private void initialiserAntecedentChamp()
        {
            g.Text = "0";
            p.Text = "0";
            surveilleAnt.IsChecked = false;
            nonSurveilleAnt.IsChecked = false;
            medicaux.IsChecked = false;
            chirurgicaux.IsChecked = false;
            obstetricaux.IsChecked = false;
            CauseNonSurveille.Text = "";
            noteAntecedent.Text = "";
            AntecedentSearchText.Text = "";

        }

        private void EnregistrerAntecedent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Antecedent ant = new Projet.Antecedent();
                ant.IdDossier = this.idDuDossier;
                if ((bool)surveilleAnt.IsChecked && !(bool)nonSurveilleAnt.IsChecked)
                    ant.TypeAccouchement = "Surveillé";
                else if (!(bool)surveilleAnt.IsChecked && (bool)nonSurveilleAnt.IsChecked)
                    ant.TypeAccouchement = "Non surveillé";
                else if ((bool)surveilleAnt.IsChecked && (bool)nonSurveilleAnt.IsChecked)
                {
                    System.Windows.MessageBox.Show("Les deux cases sont cochées dans le type d'accouchement");
                    throw new Exception();
                }
                

                if ((bool)nonSurveilleAnt.IsChecked)
                    ant.Cause = CauseNonSurveille.Text.Replace("'", "''");
                else if (!(bool)nonSurveilleAnt.IsChecked && !CauseNonSurveille.Text.Trim(' ').Equals(""))
                {
                    System.Windows.MessageBox.Show("Pour ajouter les causes, veuillez cocher la case Non surveillé");
                    throw new Exception();
                }


                if ((bool)medicaux.IsChecked)
                    ant.Medicaux = "Oui";
                if ((bool)chirurgicaux.IsChecked)
                    ant.Chirurgicaux = "Oui";
                if ((bool)obstetricaux.IsChecked)
                    ant.Obstetricaux= "Oui";

                ant.DescriptionAntecedent = noteAntecedent.Text.Replace("'", "''");

                try
                {
                    double a = Convert.ToDouble(p.Text.Replace("'", "''"));
                    double b = Convert.ToDouble(g.Text.Replace("'", "''"));
                    ant.G = g.Text.Replace("'", "''");
                    ant.P = p.Text.Replace("'", "''");
                }
                catch
                {
                    System.Windows.MessageBox.Show("L'un des champs p ou g n'est un nombre");
                    throw new Exception();
                }
            
                if (this.type == "ajouter")
                {
                    Projet.Antecedent.persistAntecedent(ant);
                    antecedentDataGrid.ItemsSource = Projet.Antecedent.AfficherAntecedentDuPatient(this.idDuDossier).DefaultView;
                    ListeAntecedent.Visibility = Visibility.Visible;
                    RemplirAntecedent.Visibility = Visibility.Hidden;
                }
                else if (this.type == "modifier")
                {
                    ant.Id = this.id;
                    Projet.Antecedent.ModifyAntecedent(ant);
                    antecedentDataGrid.ItemsSource = Projet.Antecedent.AfficherAntecedentDuPatient(this.idDuDossier).DefaultView;
                    ListeAntecedent.Visibility = Visibility.Visible;
                    RemplirAntecedent.Visibility = Visibility.Hidden;
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Veuillez vérifier les champs");
            }
        }

        private void AnnulerAntecedent_Click(object sender, RoutedEventArgs e)
        {
            ListeAntecedent.Visibility = Visibility.Visible;
            RemplirAntecedent.Visibility = Visibility.Hidden;

        }

        private void ModifierAntecedent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.initialiserAntecedentChamp();
                this.type = "modifier";
                DataRowView dtr = (DataRowView)antecedentDataGrid.SelectedItem;
                this.id = dtr["id"].ToString();
                Antecedent ant = Projet.Antecedent.getAntecedent(this.id);

                g.Text = ant.G;
                p.Text = ant.P;
                if (ant.TypeAccouchement.Trim(' ') == "Surveillé")
                    surveilleAnt.IsChecked = true;
                else if (ant.TypeAccouchement.Trim(' ') == "Non surveillé")
                    nonSurveilleAnt.IsChecked = true;

                CauseNonSurveille.Text = ant.Cause;

                if (ant.Medicaux.Trim(' ') == "Oui")
                    medicaux.IsChecked = true;
                if (ant.Chirurgicaux.Trim(' ') == "Oui")
                    chirurgicaux.IsChecked = true;
                if (ant.Obstetricaux.Trim(' ') == "Oui")
                    obstetricaux.IsChecked = true;

                noteAntecedent.Text = ant.DescriptionAntecedent;

                ListeAntecedent.Visibility = Visibility.Hidden;
                RemplirAntecedent.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            { System.Windows.MessageBox.Show(ex.Message); }
        }

        private void SupprimerAntecedent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dtr = (DataRowView)antecedentDataGrid.SelectedItem;
                string message, caption;
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                DataRowView row = (DataRowView)((System.Windows.Controls.Button)e.Source).DataContext;
                message = "Êtes-vous sûre de vouloir supprimer l'antécédent séléctionné ?";
                caption = "Suppression d'antécédent";
                result = System.Windows.Forms.MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    Projet.Antecedent.deleteAntecedent(dtr["id"].ToString());
                    antecedentDataGrid.ItemsSource = Projet.Antecedent.AfficherAntecedentDuPatient(this.idDuDossier).DefaultView;
                }
            }
            catch (Exception) { }
        }

        private void chercherAntecedent_Click(object sender, RoutedEventArgs e)
        {
            string selectedItem = "";
            switch (comboBoxAntecedent.SelectionBoxItem.ToString())
            {
                case "Nombre de G":
                    selectedItem = "g";
                    break;
                case "Nombre de P":
                    selectedItem = "p";
                    break;
                case "Type d'accouchement":
                    selectedItem = "type_accouchement";
                    break;
            }
            try
            {
                antecedentDataGrid.ItemsSource = Projet.Antecedent.chercherAntecedent(this.idDuDossier,selectedItem ,AntecedentSearchText.Text.Replace("'","''")).DefaultView;
            }
            catch (Exception)
            {
            }

        }

        //*************************************************RDV
        private void AjouterRDV_Click(object sender, RoutedEventArgs e)
        {
            this.HideAllGrids();
            this.initialiserRDVChamp();
            this.type = "ajouter";
            RemplirRDVGrid.Visibility = Visibility.Visible;
            ListeRDV.Visibility = Visibility.Hidden;
            RDVGrid.Visibility = Visibility.Visible;
        }

        private void AnnulerRDV_Click(object sender, RoutedEventArgs e)
        {
            RDVGrid.Visibility = Visibility.Visible;
            ListeRDV.Visibility = Visibility.Visible;
            RemplirRDVGrid.Visibility = Visibility.Hidden;
        }

        private void initialiserRDVChamp()
        {
            cpn1RDV.IsChecked = false;
            cpn2RDV.IsChecked = false;
            cpn3RDV.IsChecked = false;
            cpn4RDV.IsChecked = false;
            autreCPNRDV.IsChecked = false;
            echographieRDV.IsChecked = false;
            cpon1RDV.IsChecked = false;
            cpon2RDV.IsChecked = false;
            enAttente.IsChecked = false;
            effectue.IsChecked = false;
            NomPatientRDV.Text = this.patient.Nom;
            PrenomPatientRDV.Text = this.patient.Prenom;
            dateRDV.Text = "";
            comboBoxHeureRDV.Text = "";
            comboBoxMinuteRDV.Text = "00";
            comboBoxDureeHeure.Text = "00";
            comboBoxDureeMinute.Text = "00";
            descriptionRDV.Text = "";
            RDVSearchText.Text = "";
            DatePickerRDV.Text = "";
            NomPatientRDV.IsReadOnly = true;
            PrenomPatientRDV.IsReadOnly = true;
        }

        private void EnregistrerRDV_Click(object sender, RoutedEventArgs e)
        {
            RDVS r = new RDVS();
            r.NomPatient = this.patient.Nom;
            r.PrenomPatient = this.patient.Prenom;
            try
            {
                if (!((bool)cpn1RDV.IsChecked) && !((bool)cpn2RDV.IsChecked) && !((bool)cpn3RDV.IsChecked) && !((bool)cpn4RDV.IsChecked) && !((bool)autreCPNRDV.IsChecked) && !((bool)cpon1RDV.IsChecked) && !((bool)cpon2RDV.IsChecked) && !((bool)echographieRDV.IsChecked))
                {
                    System.Windows.MessageBox.Show("Veuillez choisir un type de CPN");
                    throw new Exception();
                }
                else if (((bool)cpn1RDV.IsChecked) && !((bool)cpn2RDV.IsChecked) && !((bool)cpn3RDV.IsChecked) && !((bool)cpn4RDV.IsChecked) && !((bool)autreCPNRDV.IsChecked) && !((bool)cpon1RDV.IsChecked) && !((bool)cpon2RDV.IsChecked) && !((bool)echographieRDV.IsChecked))
                    r.TypeRDV = "CPN1";
                else if (((bool)cpn2RDV.IsChecked) && !((bool)cpn1RDV.IsChecked) && !((bool)cpn3RDV.IsChecked) && !((bool)cpn4RDV.IsChecked) && !((bool)autreCPNRDV.IsChecked) && !((bool)cpon1RDV.IsChecked) && !((bool)cpon2RDV.IsChecked) && !((bool)echographieRDV.IsChecked))
                    r.TypeRDV = "CPN2";
                else if (((bool)cpn3RDV.IsChecked) && !((bool)cpn2RDV.IsChecked) && !((bool)cpn1RDV.IsChecked) && !((bool)cpn4RDV.IsChecked) && !((bool)autreCPNRDV.IsChecked) && !((bool)cpon1RDV.IsChecked) && !((bool)cpon2RDV.IsChecked) && !((bool)echographieRDV.IsChecked))
                    r.TypeRDV = "CPN3";
                else if (((bool)cpn4RDV.IsChecked) && !((bool)cpn2RDV.IsChecked) && !((bool)cpn3RDV.IsChecked) && !((bool)cpn1RDV.IsChecked) && !((bool)autreCPNRDV.IsChecked) && !((bool)cpon1RDV.IsChecked) && !((bool)cpon2RDV.IsChecked) && !((bool)echographieRDV.IsChecked))
                    r.TypeRDV = "CPN4";
                else if (((bool)autreCPNRDV.IsChecked) && !((bool)cpn2RDV.IsChecked) && !((bool)cpn3RDV.IsChecked) && !((bool)cpn4RDV.IsChecked) && !((bool)cpn1RDV.IsChecked) && !((bool)cpon1RDV.IsChecked) && !((bool)cpon2RDV.IsChecked) && !((bool)echographieRDV.IsChecked))
                    r.TypeRDV = "Autre";
                else if (((bool)cpon1RDV.IsChecked) && !((bool)cpon2RDV.IsChecked) && !((bool)cpn2RDV.IsChecked) && !((bool)cpn3RDV.IsChecked) && !((bool)cpn4RDV.IsChecked) && !((bool)cpn1RDV.IsChecked) && !((bool)autreCPNRDV.IsChecked) && !((bool)echographieRDV.IsChecked))
                    r.TypeRDV = "CPoN1";
                else if (((bool)cpon2RDV.IsChecked) && !((bool)cpon1RDV.IsChecked) && !((bool)cpn2RDV.IsChecked) && !((bool)cpn3RDV.IsChecked) && !((bool)cpn4RDV.IsChecked) && !((bool)cpn1RDV.IsChecked) && !((bool)echographieRDV.IsChecked) && !((bool)autreCPNRDV.IsChecked))
                    r.TypeRDV = "CPoN2";
                else if (((bool)echographieRDV.IsChecked) && !((bool)cpn2RDV.IsChecked) && !((bool)cpn3RDV.IsChecked) && !((bool)cpn4RDV.IsChecked) && !((bool)cpn1RDV.IsChecked) && !((bool)cpon1RDV.IsChecked) && !((bool)cpon2RDV.IsChecked) && !((bool)autreCPNRDV.IsChecked))
                    r.TypeRDV = "Echographie";
                else
                {
                    System.Windows.MessageBox.Show("Plusieurs champs sont séléctionner dans le type de CPN");
                    throw new Exception();
                }

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
                r.IdPatient = this.patient.Id;
                if (this.type == "ajouter" && RDVS.RDVIsFree(r))
                {
                    
                    RDVS.persistRDV(r);
                    rdvDataGrid.ItemsSource = RDVS.AfficherRDVDuPatient(this.patient.Id).DefaultView;
                    RemplirRDVGrid.Visibility = Visibility.Hidden;
                    ListeRDV.Visibility = Visibility.Visible;
                    this.page.PageWindow_Loaded(sender,e);
                }

                else if (this.type == "modifier" && RDVS.RDVIsFree(r, this.id))
                {
                    r.Id = this.id;
                    RDVS.ModifyRDV(r);
                    rdvDataGrid.ItemsSource = RDVS.AfficherRDVDuPatient(this.patient.Id).DefaultView;
                    RemplirRDVGrid.Visibility = Visibility.Hidden;
                    ListeRDV.Visibility = Visibility.Visible;
                    this.page.PageWindow_Loaded(sender, e);
                }
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
                this.initialiserRDVChamp();
                this.type = "modifier";
                RDVS r = RDVS.getRDV(dtr["id"].ToString());
                this.id = r.Id;
                if (r.TypeRDV.Trim(' ') == "CPN1")
                    cpn1RDV.IsChecked = true;
                else if (r.TypeRDV.Trim(' ') == "CPN2")
                    cpn2RDV.IsChecked = true;
                else if (r.TypeRDV.Trim(' ') == "CPN3")
                    cpn3RDV.IsChecked = true;
                else if (r.TypeRDV.Trim(' ') == "CPN4")
                    cpn4RDV.IsChecked = true;
                else if (r.TypeRDV.Trim(' ') == "Autre")
                    autreCPNRDV.IsChecked = true;
                else if (r.TypeRDV.Trim(' ') == "Echographie")
                    echographieRDV.IsChecked = true;
                else if (r.TypeRDV.Trim(' ') == "CPoN1")
                    cpon1RDV.IsChecked = true;
                else if (r.TypeRDV.Trim(' ') == "CPoN2")
                    cpon2RDV.IsChecked = true;

                if (r.Statut.Trim(' ') == "En attente")
                    enAttente.IsChecked = true;
                else if (r.Statut.Trim(' ') == "Effectué")
                    effectue.IsChecked = true;

                NomPatientRDV.Text = this.patient.Nom;
                PrenomPatientRDV.Text = this.patient.Prenom;
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

        private void deleteRDV_Click(object sender, RoutedEventArgs e)
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
                    rdvDataGrid.ItemsSource = RDVS.AfficherRDVDuPatient(this.patient.Id).DefaultView;
                    this.page.PageWindow_Loaded(sender, e);
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
                case "Statut du RDV":
                    selectedItem = "statut";
                    break;
            }
            try
            {
                if (DatePickerRDV.SelectedDate.ToString().Equals(""))
                    rdvDataGrid.ItemsSource = RDVS.chercherRDV(this.patient.Id, selectedItem, RDVSearchText.Text.Replace("'", "''")).DefaultView;
                else
                    rdvDataGrid.ItemsSource = RDVS.chercherRDVAvecDate(this.patient.Id,selectedItem, RDVSearchText.Text.Replace("'", "''"), (DateTime)DatePickerRDV.SelectedDate).DefaultView;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        //**************************************************Information patient

        private void RemplirChampPatientPourModification()
        {

            nomPatient.Text = this.patient.Nom;
            prenomPatient.Text = this.patient.Prenom;
            numDossier.Text = this.patient.NumDossier;
            cinPatient.Text = this.patient.Cin;
            tel.Text = this.patient.Tel;
            adresse.Text = this.patient.Adresse;
            DernierePeriode.Text = this.patient.DDR.ToString();
            dateNaissance.Text = this.patient.DateNaissance.ToString();
            prenomMari.Text = this.patient.NomMari;
            nomMari.Text = this.patient.PrenomMari;
            groupage.Text = this.patient.Groupage;
            dpa.Text = this.patient.DPA.ToString();
            assurance.Text = this.patient.Assurance;
            description.Text = this.patient.Description;
        }

        private void EnregistrerPatient_Click(object sender, RoutedEventArgs e)
        {
            Patient pp = new Projet.Patient();
            try
            {
                pp.Nom = nomPatient.Text.Replace("'", "''");
                pp.Prenom = prenomPatient.Text.Replace("'", "''");
                pp.NumDossier = numDossier.Text.Replace("'", "''");
                pp.Cin = cinPatient.Text.Replace("'", "''");
                pp.Adresse = adresse.Text.Replace("'", "''");
                pp.DateNaissance = (DateTime)dateNaissance.SelectedDate;
                pp.PrenomMari = prenomMari.Text.Replace("'", "''");
                pp.NomMari = nomMari.Text.Replace("'", "''");
                pp.DDR = (DateTime)DernierePeriode.SelectedDate;
                pp.Groupage = groupage.Text.Replace("'", "''");

                pp.DPA = (DateTime)dpa.SelectedDate;
                pp.DateAjoute = DateTime.Today;
                pp.Assurance = assurance.Text.Replace("'", "''");
                pp.Description = description.Text.Replace("'", "''");

                if (this.page.TelIsValid(tel.Text))
                    pp.Tel = tel.Text.Replace("'", "''");
                else
                    throw new Exception();
                pp.Id = this.patient.Id;
                Projet.Patient.ModifyPatient(pp);
                this.patient = Projet.Patient.getPatient(pp.Cin) ;
                this.page.PageWindow_Loaded(sender, e);
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Veuillez remplir tous les champs du patient");
            }
            
        }


    }
}
