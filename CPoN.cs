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
    class CPoN
    {
        string id;
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
        string idDossier;
        public string IdDossier
        {
            get
            {
                return idDossier;
            }
            set
            {
                idDossier = value;
            }
        }
        string nomConsultation;
        public string NomConsultation
        {
            get
            {
                return nomConsultation;
            }
            set
            {
                nomConsultation = value;
            }
        }
        DateTime dateEffectiveAccouchement;
        public DateTime DateEffectiveAccouchement
        {
            get
            {
                return dateEffectiveAccouchement;
            }
            set
            {
                dateEffectiveAccouchement = value;
            }
        }
        string deces;
        public string Deces
        {
            get
            {
                return deces;
            }
            set
            {
                deces = value;
            }
        }
        string typeConsultation;
        public string TypeConsultation
        {
            get
            {
                return typeConsultation;
            }
            set
            {
                typeConsultation = value;
            }
        }
        string descriptionTypeConsultation;
        public string DescriptionTypeConsultation
        {
            get
            {
                return descriptionTypeConsultation;
            }
            set
            {
                descriptionTypeConsultation = value;
            }
        }
        string complication;
        public string Complication
        {
            get
            {
                return complication;
            }
            set
            {
                complication = value;
            }
        }
        string hemorragie;
        public string Hemorragie
        {
            get
            {
                return hemorragie;
            }
            set
            {
                hemorragie = value;
            }
        }
        string infection;
        public string Infection
        {
            get
            {
                return infection;
            }
            set
            {
                infection = value;
            }
        }
        string eclampsie;
        public string Eclampsie
        {
            get
            {
                return eclampsie;
            }
            set
            {
                eclampsie = value;
            }
        }
        string phlebite;
        public string Phlebite
        {
            get
            {
                return phlebite;
            }
            set
            {
                phlebite = value;
            }
        }
        string mammaire;
        public string Mammaire
        {
            get
            {
                return mammaire;
            }
            set
            {
                mammaire = value;
            }
        }
        string anemie;
        public string Anemie
        {
            get
            {
                return anemie;
            }
            set
            {
                anemie = value;
            }
        }
        string autre;
        public string Autre
        {
            get
            {
                return autre;
            }
            set
            {
                autre = value;
            }
        }
        string descriptionAutre;
        public string DescriptionAutre
        {
            get
            {
                return descriptionAutre;
            }
            set
            {
                descriptionAutre = value;
            }
        }
        string lieuAccouchement;
        public string LieuAccouchement
        {
            get
            {
                return lieuAccouchement;
            }
            set
            {
                lieuAccouchement = value;
            }
        }
        string typeLieuAccouchement;
        public string TypeLieuAccouchement
        {
            get
            {
                return typeLieuAccouchement;
            }
            set
            {
                typeLieuAccouchement = value;
            }
        }
        string fer;
        public string Fer
        {
            get
            {
                return fer;
            }
            set
            {
               fer = value;
            }
        }
        DateTime dateConsulation;
        public DateTime DateConsultation
        {
            get
            {
                return dateConsulation;
            }
            set
            {
                dateConsulation = value;
            }
        }
        string bilanGeneral;
        public string BilanGeneral
        {
            get
            {
                return bilanGeneral;
            }
            set
            {
                bilanGeneral = value;
            }
        }
        string gestionComplication;
        public string GestionComplication
        {
            get
            {
                return gestionComplication;
            }
            set
            {
                gestionComplication = value;
            }
        }
        string descriptionGestionComplication;
        public string DescriptionGestionComplication
        {
            get
            {
                return descriptionGestionComplication;
            }
            set
            {
                descriptionGestionComplication = value;
            }
        }

        public static void persistCPoN(CPoN p)
        {
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string tab = "cpon(id_dossier,date_reel_accouchement,deces,type_consultation,complication,hemorragie,infection,eclampsie,phlebite,mammaire,anemie,autre,type_lieu_accouchement,fer,date_consultation,bilan_g,description_autre,description_referee,description_lieu,description_type_autre,gestion_complication,nom_consulation)";
            string v = "values(" + Convert.ToInt32(p.IdDossier) + ",'" + p.DateEffectiveAccouchement.Date + "','" + p.Deces + "','" + p.typeConsultation + "','" + p.Complication + "','" + p.Hemorragie + "','" + p.Infection + "','" + p.Eclampsie + "','" + p.Phlebite + "','" + p.Mammaire + "','" + p.Anemie + "','" + p.Autre + "','" + p.TypeLieuAccouchement + "','" + p.Fer + "','" + p.DateConsultation.Date + "','" + p.BilanGeneral + "','" + p.DescriptionAutre + "','" + p.DescriptionGestionComplication + "','" + p.LieuAccouchement + "','" + p.DescriptionTypeConsultation + "','" + p.GestionComplication + "','"+p.NomConsultation+"')";
            string q = "insert into " + tab + " " + v;
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();
                MessageBox.Show("Ajout de CPoN avec succès");
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static DataTable AfficherCPoNDuPatient(string idDossier)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from cpon where id_dossier= " + Convert.ToInt32(idDossier) + "";
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

        public static CPoN getCPoN(string id)
        {
            CPoN p = new CPoN();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "Select * from cpon where id=" + Convert.ToInt32(id) + "";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    if (dataReader.HasRows)
                    {
                        p.DateEffectiveAccouchement = (DateTime) dataReader["date_reel_accouchement"];
                        p.Deces = dataReader["deces"].ToString();
                        p.TypeConsultation = dataReader["type_consultation"].ToString();
                        p.Complication = dataReader["complication"].ToString();
                        p.Hemorragie= dataReader["hemorragie"].ToString();
                        p.Infection = dataReader["infection"].ToString();
                        p.Eclampsie = dataReader["eclampsie"].ToString();
                        p.Phlebite = dataReader["phlebite"].ToString();
                        p.Mammaire = dataReader["mammaire"].ToString();
                        p.Anemie = dataReader["anemie"].ToString();
                        p.Autre = dataReader["autre"].ToString();
                        p.TypeLieuAccouchement = dataReader["type_lieu_accouchement"].ToString();
                        p.Fer = dataReader["fer"].ToString();
                        p.DateConsultation = (DateTime)dataReader["date_consultation"];
                        p.BilanGeneral = dataReader["bilan_g"].ToString();
                        p.DescriptionAutre = dataReader["description_autre"].ToString();
                        p.DescriptionGestionComplication= dataReader["description_referee"].ToString();
                        p.LieuAccouchement = dataReader["description_lieu"].ToString();
                        p.DescriptionTypeConsultation = dataReader["description_type_autre"].ToString();
                        p.GestionComplication = dataReader["gestion_complication"].ToString();
                        p.NomConsultation = dataReader["nom_consulation"].ToString();
                    }
                }
                dataReader.Close();
                command.Dispose();
                c.Close();
            }
            catch (Exception)
            {

            }
            return p;
        }

        public static void ModifyCPoN(CPoN p)
        {
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "update cpon set date_reel_accouchement='"+p.DateEffectiveAccouchement.Date+"',deces='"+p.Deces+"',type_consultation='"+p.TypeConsultation+"',complication='"+p.Complication+"',hemorragie='"+p.Hemorragie+"',infection='"+p.Infection+"',eclampsie='"+p.Eclampsie+"',phlebite='"+p.Phlebite+"',mammaire='"+p.Mammaire+"',anemie='"+p.Anemie+"',autre='"+p.Autre+"',type_lieu_accouchement='"+p.TypeLieuAccouchement+"',fer='"+p.Fer+"',date_consultation='"+p.DateConsultation.Date+"',bilan_g='"+p.BilanGeneral+"',description_autre='"+p.DescriptionAutre+"',description_referee='"+p.DescriptionGestionComplication+"',description_lieu='"+p.LieuAccouchement+"',description_type_autre='"+p.DescriptionTypeConsultation+"',gestion_complication='"+p.GestionComplication+"',nom_consulation='"+p.NomConsultation+"' where id=" + Convert.ToInt32(p.Id);
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();
                MessageBox.Show("Modification avec succès");
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void deleteCPoN(string p)
        {
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "delete from cpon where id=" + Convert.ToInt32(p);
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();
                c.Close();
                MessageBox.Show("CPoN supprimé");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static DataTable chercherCPoN(string critere, string mot, string idDossier)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from cpon where " + critere + " like '%" + mot + "%' and id_dossier=" + Convert.ToInt32(idDossier);
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

        public static DataTable chercherCPoNAvecDate(string critere, string mot, string idDossier, DateTime d)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from cpon where " + critere + " like '%" + mot + "%' and date_consultation='" + d.Date + "'  and id_dossier=" + Convert.ToInt32(idDossier);
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
