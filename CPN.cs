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
    class CPN
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
        string type;
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
        string bilanAvant;
        public string BilanAvant
        {
            get
            {
                return bilanAvant;
            }
            set
            {
                bilanAvant = value;
            }
        }
        string descriptionBilanAvant;
        public string DescriptionBilanAvant
        {
            get
            {
                return descriptionBilanAvant;
            }
            set
            {
                descriptionBilanAvant = value;
            }
        }
        string inscription;
        public string Inscription
        {
            get
            {
                return inscription;
            }
            set
            {
                inscription = value;
            }
        }
        string medicalise;
        public string Medicalise
        {
            get
            {
                return medicalise;
            }
            set
            {
                medicalise = value;
            }
        }
        string causeMedicalise;
        public string CauseMedicalise
        {
            get
            {
                return causeMedicalise;
            }
            set
            {
                causeMedicalise = value;
            }
        }
        string risque;
        public string Risque
        {
            get
            {
                return risque;
            }
            set
            {
                risque = value;
            }
        }
        string metrorragie;
        public string Metrorragie
        {
            get
            {
                return metrorragie;
            }
            set
            {
                metrorragie= value;
            }
        }
        string hta;
        public string HTA
        {
            get
            {
                return hta;
            }
            set
            {
                hta= value;
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
        string diabete;
        public string Diabete
        {
            get
            {
                return diabete;
            }
            set
            {
                diabete = value;
            }
        }
        string cardiopathie;
        public string Cardiopathie
        {
            get
            {
                return cardiopathie;
            }
            set
            {
                cardiopathie = value;
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
        string gestionGAR;
        public string GestionGAR
        {
            get
            {
                return gestionGAR;
            }
            set
            {
                gestionGAR = value;
            }
        }
        string descriptionGAR;
        public string DescriptionGAR
        {
            get
            {
                return descriptionGAR;
            }
            set
            {
                descriptionGAR = value;
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
        string vitamineD;
        public string VitamineD
        {
            get
            {
                return vitamineD;
            }
            set
            {
                vitamineD = value;
            }
        }
        string bilan;
        public string Bilan
        {
            get
            {
                return bilan;
            }
            set
            {
                bilan = value;
            }
        }
        DateTime dateCPN;
        public DateTime DateCPN
        {
            get
            {
                return dateCPN;
            }
            set
            {
                dateCPN = value;
            }
        }
        string moisPatiente;
        public string MoisPatiente
        {
            get
            {
                return moisPatiente;
            }
            set
            {
                moisPatiente = value;
            }
        }


        public static void persistCPN(CPN p)
        {
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string tab = "cpn(id_dossier,type,bilan,inscription,medicalise,risque,metrorragie,hta,anemie,diabete,cardiopathie,infection,autres,description_autre,description_referee,fer,vitamine_d,bilan_g,date_cpn,description_medical,bilan_description,gestionGAR,mois_trim)";
            string v = "values(" + Convert.ToInt32(p.IdDossier) + ",'" + p.Type + "','" + p.BilanAvant + "','" + p.Inscription + "','" + p.Medicalise + "','" + p.Risque + "','" + p.Metrorragie + "','" + p.HTA + "','" + p.Anemie + "','" + p.Diabete + "','" + p.Cardiopathie + "','" + p.Infection + "','" + p.Autre + "','" + p.DescriptionAutre + "','" + p.DescriptionGAR + "','" + p.Fer + "','" + p.VitamineD + "','" + p.Bilan + "','" + p.DateCPN.Date + "','" + p.CauseMedicalise + "','" + p.DescriptionBilanAvant + "','" + p.GestionGAR + "','"+p.MoisPatiente+"')";
            string q = "insert into " + tab + " " + v;
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();
                MessageBox.Show("Ajout de CPN avec succès");
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static DataTable AfficherCPNDuPatient(string idDossier)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from cpn where id_dossier= " + Convert.ToInt32(idDossier) + "";
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

        public static CPN getCPN(string id)
        {
            CPN p = new CPN();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "Select * from cpn where id=" + Convert.ToInt32(id) + "";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    if (dataReader.HasRows)
                    {
                        p.Type = dataReader["type"].ToString();
                        p.BilanAvant = dataReader["bilan"].ToString();
                        p.Inscription = dataReader["inscription"].ToString();
                        p.Medicalise = dataReader["medicalise"].ToString();
                        p.Risque = dataReader["risque"].ToString();
                        p.Metrorragie = dataReader["metrorragie"].ToString();
                        p.HTA = dataReader["hta"].ToString();
                        p.Anemie = dataReader["anemie"].ToString();
                        p.Diabete = dataReader["diabete"].ToString();
                        p.Cardiopathie = dataReader["cardiopathie"].ToString();
                        p.Infection = dataReader["infection"].ToString();
                        p.Autre = dataReader["autres"].ToString();
                        p.DescriptionAutre = dataReader["description_autre"].ToString();
                        p.DescriptionGAR = dataReader["description_referee"].ToString();
                        p.Fer = dataReader["fer"].ToString();
                        p.VitamineD = dataReader["vitamine_d"].ToString();
                        p.Bilan = dataReader["bilan_g"].ToString();
                        p.DateCPN = (DateTime)dataReader["date_cpn"];
                        p.CauseMedicalise = dataReader["description_medical"].ToString();
                        p.DescriptionBilanAvant = dataReader["bilan_description"].ToString();
                        p.GestionGAR = dataReader["gestionGAR"].ToString();
                        p.MoisPatiente = dataReader["mois_trim"].ToString();
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

        public static void ModifyCPN(CPN p)
        {
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "update cpn set type='"+p.Type+"',bilan='"+p.BilanAvant+"',inscription='"+p.Inscription+"',medicalise='"+p.Medicalise+"',risque='"+p.Risque+"',metrorragie='"+p.Metrorragie+"',hta='"+p.HTA+"',anemie='"+p.Anemie+"',diabete='"+p.Diabete+"',cardiopathie='"+p.Cardiopathie+"',infection='"+p.Infection+"',autres='"+p.Autre+"',description_autre='"+p.DescriptionAutre+"',description_referee='"+p.DescriptionGAR+"',fer='"+p.Fer+"',vitamine_d='"+p.VitamineD+"',bilan_g='"+p.Bilan+"',date_cpn='"+p.DateCPN.Date+"',description_medical='"+p.CauseMedicalise+"',bilan_description='"+p.DescriptionBilanAvant+"',gestionGAR='"+p.GestionGAR+ "',mois_trim='" + p.MoisPatiente + "' where id=" + Convert.ToInt32(p.Id);
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

        public static void deleteCPN(string p)
        {
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "delete from cpn where id=" + Convert.ToInt32(p);
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();
                c.Close();
                MessageBox.Show("CPN supprimé");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static DataTable chercherCPN(string critere, string mot,string idDossier)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from cpn where " + critere + " like '%" + mot + "%' and id_dossier="+Convert.ToInt32(idDossier);
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

        public static DataTable chercherCPNAvecDate(string critere, string mot,string idDossier , DateTime d)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from cpn where " + critere + " like '%" + mot + "%' and date_cpn='" + d.Date + "'  and id_dossier=" + Convert.ToInt32(idDossier);
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
