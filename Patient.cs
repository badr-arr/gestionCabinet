using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projet
{
    class Patient
    {
        string id;
        string prenom;
        string nom;
        string numDossier;
        string cin;
        string tel;
        DateTime dateNaissance;
        string adresse;
        string prenomMari;
        string nomMari;
        DateTime ddr;
        string groupage;
        DateTime dpa;
        DateTime dateAjout;
        string description;
        string assurance;

        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string Nom
        {
            get
            {
                return nom;
            }
            set
            {
                nom = value;
            }
        }
        public string Prenom
        {
            get
            {
                return prenom;
            }
            set
            {
                prenom = value;
            }
        }
        public string NumDossier
        {
            get
            {
                return numDossier;
            }
            set
            {
                numDossier = value;
            }
        }
        public string Cin
        {
            get
            {
                return cin;
            }
            set
            {
                cin = value;
            }
        }
        public string Tel
        {
            get
            {
                return tel;
            }
            set
            {
                tel = value;
            }
        }
        public DateTime DateNaissance
        {
            get
            {
                return dateNaissance;
            }
            set
            {
                dateNaissance = value;
            }
        }
        public string Adresse
        {
            get
            {
                return adresse;
            }
            set
            {
                adresse = value;
            }
        }
        public string PrenomMari
        {
            get
            {
                return prenomMari;
            }
            set
            {
                prenomMari = value;
            }
        }
        public string NomMari
        {
            get
            {
                return nomMari;
            }
            set
            {
                nomMari = value;
            }
        }
        public DateTime DDR
        {
            get
            {
                return ddr;
            }
            set
            {
                ddr = value;
            }
        }
        public string Groupage
        {
            get
            {
                return groupage;
            }
            set
            {
                groupage = value;
            }
        }
        public DateTime DPA
        {
            get
            {
                return dpa;
            }
            set
            {
                dpa = value;
            }
        }
        public DateTime DateAjoute
        {
            get
            {
                return dateAjout;
            }
            set
            {
                dateAjout = value;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
        public string Assurance
        {
            get
            {
                return assurance;
            }
            set
            {
                assurance = value;
            }
        }

        public static void persistPatient(Patient p)
        {
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            DateTime d = DateTime.Today;
            string tab = "Table_patient(nom,prenom,num_dossier,cin,tel,date_naissance,adresse,prenom_mari,nom_mari,ddr,groupage,dpa,date_ajout,bilan,assurance)";
            string v = "values('" + p.Nom + "','" + p.Prenom + "','" + p.NumDossier + "','" + p.Cin + "','" + p.Tel + "','" + p.DateNaissance.Date + "','" + p.Adresse + "','" + p.PrenomMari + "','" + p.NomMari + "','" + p.DDR.Date + "','" + p.Groupage + "','" + p.DPA + "','" + p.DateAjoute.Date + "','" + p.Description + "','" + p.Assurance + "')";
            string q = "insert into " + tab + " " + v;
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();
                Patient pa = Patient.getPatient(p.Cin);
                tab = "dossier_patient(id_patient,nom,prenom,date_creation,date_modification,num_dossier)";
                v = "values(" + Convert.ToInt32(pa.Id) + ",'" + pa.Nom + "','" + pa.Prenom + "','" + d.Date + "','" + d.Date + "','" + pa.NumDossier + "')";
                q = "insert into " + tab + " " + v;
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();
                MessageBox.Show("Ajout du patient avec succès");
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static bool verifierUniciteCin(string cin)
        {
            if (cin.Equals("") || cin.Trim(' ').Equals(""))
            {
                MessageBox.Show("CIN obligatoire");
                return false;
            }
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "select * from Table_patient where cin='" + cin + "'";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    MessageBox.Show("CIN déja existant !!");
                    return false;
                }
                dataReader.Close();
                command.Dispose();
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        public static bool verifierUniciteNumDossier(string n)
        {
            if (n.Equals("") || n.Trim(' ').Equals(""))
            {
                MessageBox.Show("N° de dossier obligatoire !!");
                return false;
            }
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "select * from Table_patient where num_dossier='" + n + "'";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    MessageBox.Show("N° de dossier déja existant !!");
                    return false;
                }
                dataReader.Close();
                command.Dispose();
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        public static Patient getPatient(string n)
        {
            Patient p = new Patient();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "Select * from Table_patient where cin='" + n + "'";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    if (dataReader.HasRows)
                    {
                        p.Id = dataReader["id"].ToString();
                        p.Nom = dataReader["nom"].ToString();
                        p.Prenom = dataReader["prenom"].ToString();
                        p.NumDossier = dataReader["num_dossier"].ToString();                        
                        p.Cin = dataReader["cin"].ToString();
                        p.Tel = dataReader["tel"].ToString();
                        p.DateNaissance = Convert.ToDateTime(dataReader["date_naissance"].ToString());
                        p.Adresse = dataReader["adresse"].ToString();
                        p.PrenomMari = dataReader["prenom_mari"].ToString();
                        p.NomMari = dataReader["nom_mari"].ToString();
                        p.DDR = Convert.ToDateTime(dataReader["ddr"].ToString());
                        p.Groupage = dataReader["groupage"].ToString();
                        p.DPA = Convert.ToDateTime(dataReader["dpa"].ToString());
                        p.DateAjoute = Convert.ToDateTime(dataReader["date_ajout"].ToString());
                        p.Description = dataReader["bilan"].ToString();
                        p.Assurance = dataReader["assurance"].ToString();
                        return p;
                    }
                }
                dataReader.Close();
                command.Dispose();
                c.Close();
            }
            catch (Exception)
            {

            }
            return null;
        }

        public static string getPatientCin(string id)
        {
            string cin;
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "Select * from Table_patient where id=" + Convert.ToInt32(id);
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    if (dataReader.HasRows)
                    {
                        cin = dataReader["cin"].ToString();
                        return cin;
                    }
                }
                dataReader.Close();
                command.Dispose();
                c.Close();
            }
            catch (Exception)
            {

            }
            return null;
        }

        public static void ModifyPatient(Patient p)
        {
            DateTime da = DateTime.Today;
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            DateTime d = p.DateNaissance;
            string q = "update Table_patient set nom='" + p.Nom + "',prenom='" + p.Prenom + "',num_dossier='" + p.NumDossier + "',cin='" + p.Cin + "',tel='" + p.Tel + "',date_naissance='" + p.DateNaissance.Date + "',adresse='" + p.Adresse + "',prenom_mari='" + p.PrenomMari + "',nom_mari='" + p.NomMari + "',ddr='" + p.DDR.Date + "',groupage='" + p.Groupage + "',dpa='" + p.DPA.Date + "',date_ajout='" + p.DateAjoute.Date + "',bilan='" + p.Description + "',assurance='" + p.Assurance + "' where id=" + Convert.ToInt32(p.Id);
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();
                q = "update dossier_patient set num_dossier='" + p.NumDossier + "',nom='" + p.Nom + "',prenom='" + p.Prenom + "',date_modification='" + da.Date + "' where id_patient=" + Convert.ToInt32(p.Id);
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();
                q = "update rdv set nom_patient='" + p.Nom + "',prenom_patient='" + p.Prenom + "' where id_patient=" + Convert.ToInt32(p.Id);
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();
                c.Close();
                MessageBox.Show("Modification avec succès");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void deletePatient(Patient p)
        {
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "delete from Table_patient where id=" + Convert.ToInt32(p.Id);
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();
                q = "delete from RDV where id_patient=" + Convert.ToInt32(p.Id);
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();
                c.Close();
                MessageBox.Show("Patient supprimé");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static DataTable chercherPatient(string critere, string mot)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from Table_patient where " + critere + " like '%" + mot + "%'";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt);
                }
                return dt;
            }
            catch (Exception)
            {

            }
            return null;
        }

        public static DataTable chercherPatientAvecDate(string critere, string mot,DateTime d)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from Table_patient where " + critere + " like '%" + mot + "%' and date_naissance='"+d.Date+"'";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt);
                }
                return dt;
            }
            catch (Exception)
            {

            }
            return null;
        }


    }
}
