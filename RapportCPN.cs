using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projet
{
    class RapportCPN
    {
        string mois;
        public string Mois
        {
            get
            {
                return mois;
            }
            set
            {
                mois = value;
            }
        }
        DateTime dateCreation;
        public DateTime DateCreation
        {
            get
            {
                return dateCreation;
            }
            set
            {
                dateCreation = value;
            }
        }
        string nbreCPN1;
        public string NbreCPN1
        {
            get
            {
                return nbreCPN1;
            }
            set
            {
                nbreCPN1 = value;
            }
        }
        string nbreCPN2;
        public string NbreCPN2
        {
            get
            {
                return nbreCPN2;
            }
            set
            {
                nbreCPN2 = value;
            }
        }
        string nbreCpn2Ni;
        public string NbreCpn2Ni
        {
            get
            {
                return nbreCpn2Ni;
            }
            set
            {
                nbreCpn2Ni = value;
            }
        }
        string nbreCpn2Ai;
        public string NbreCpn2Ai
        {
            get
            {
                return nbreCpn2Ai;
            }
            set
            {
                nbreCpn2Ai = value;
            }
        }
        string nbreCPN3_8;
        public string NbreCPN3_8
        {
            get
            {
                return nbreCPN3_8;
            }
            set
            {
                nbreCPN3_8 = value;
            }
        }
        string nbreCPN3_8NI;
        public string NbreCPN3_8NI
        {
            get
            {
                return nbreCPN3_8NI;
            }
            set
            {
                nbreCPN3_8NI = value;
            }
        }
        string nbreCPN3_8AI;
        public string NbreCPN3_8AI
        {
            get
            {
                return nbreCPN3_8AI;
            }
            set
            {
                nbreCPN3_8AI = value;
            }
        }
        string nbreCPN3_9;
        public string NbreCPN3_9
        {
            get
            {
                return nbreCPN3_9;
            }
            set
            {
                nbreCPN3_9 = value;
            }
        }
        string nbreCPN3_9NI;
        public string NbreCPN3_9NI
        {
            get
            {
                return nbreCPN3_9NI;
            }
            set
            {
                nbreCPN3_9NI = value;
            }
        }
        string nbreCPN3_9AI;
        public string NbreCPN3_9AI
        {
            get
            {
                return nbreCPN3_9AI;
            }
            set
            {
                nbreCPN3_9AI = value;
            }
        }
        string nbreAutreCPN;
        public string NbreAutreCPN
        {
            get
            {
                return nbreAutreCPN;
            }
            set
            {
                nbreAutreCPN = value;
            }
        }
        string nbreAutreCPNNI;
        public string NbreAutreCPNNI
        {
            get
            {
                return nbreAutreCPNNI;
            }
            set
            {
                nbreAutreCPNNI = value;
            }
        }
        string nbreAutreCPNAI;
        public string NbreAutreCPNAI
        {
            get
            {
                return nbreAutreCPNAI;
            }
            set
            {
                nbreAutreCPNAI = value;
            }
        }
        string nbreCPNMedicalise;
        public string NbreCPNMedicalise
        {
            get
            {
                return nbreCPNMedicalise;
            }
            set
            {
                nbreCPNMedicalise = value;
            }
        }
        string nbreMetrorragie;
        public string NbreMetrorragie
        {
            get
            {
                return nbreMetrorragie;
            }
            set
            {
                nbreMetrorragie = value;
            }
        }
        string nbreHTA;
        public string NbreHTA
        {
            get
            {
                return nbreHTA;
            }
            set
            {
                nbreHTA = value;
            }
        }
        string nbreAnemie;
        public string NbreAnemie
        {
            get
            {
                return nbreAnemie;
            }
            set
            {
                nbreAnemie = value;
            }
        }
        string nbreDiabete;
        public string NbreDiabete
        {
            get
            {
                return nbreDiabete;
            }
            set
            {
                nbreDiabete = value;
            }
        }
        string nbreCardiopathie;
        public string NbreCardiopathie
        {
            get
            {
                return nbreCardiopathie;
            }
            set
            {
                nbreCardiopathie = value;
            }
        }
        string nbreInfection;
        public string NbreInfection
        {
            get
            {
                return nbreInfection;
            }
            set
            {
                nbreInfection = value;
            }
        }
        string nbreAutreRisque;
        public string NbreAutreRisque
        {
            get
            {
                return nbreAutreRisque;
            }
            set
            {
                nbreAutreRisque = value;
            }
        }
        string nbreGarPec;
        public string NbreGarPec
        {
            get
            {
                return nbreGarPec;
            }
            set
            {
                nbreGarPec = value;
            }
        }
        string nbreGarReferee;
        public string NbreGarReferee
        {
            get
            {
                return nbreGarReferee;
            }
            set
            {
                nbreGarReferee = value;
            }
        }
        string nbreFer;
        public string NbreFer
        {
            get
            {
                return nbreFer;
            }
            set
            {
                nbreFer = value;
            }
        }
        string nbreVitamineD;
        public string NbreVitamineD
        {
            get
            {
                return nbreVitamineD;
            }
            set
            {
                nbreVitamineD = value;
            }
        }
                

        public static Int32 NombreCPN(string cpn,DateTime avant, DateTime apres)
        {
            Int32 nbre = 0;
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "select count(*) from cpn where type='"+cpn+"' and date_cpn between '" + avant.Date + "' and '" + apres.Date + "'";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                nbre = (Int32)command.ExecuteScalar();
                command.Dispose();
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return nbre;
        }

        public static Int32 NombreCPNInscription(string cpn, DateTime avant, DateTime apres,string inscription)
        {
            Int32 nbre = 0;
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "select count(*) from cpn where type='" + cpn + "' and date_cpn between '" + avant.Date + "' and '" + apres.Date + "' and inscription='"+inscription+"'";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                nbre = (Int32)command.ExecuteScalar();
                
                command.Dispose();
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return nbre;
        }

        public static Int32 NombreCPN3(string mois, DateTime avant, DateTime apres)
        {
            Int32 nbre = 0;
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "select count(*) from cpn where type='CPN3' and date_cpn between '" + avant.Date + "' and '" + apres.Date + "' and mois_trim='" + mois + "'";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                nbre = (Int32)command.ExecuteScalar();
                command.Dispose();
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return nbre;
        }

        public static Int32 NombreCPN3Inscription(string mois, DateTime avant, DateTime apres,string inscription)
        {
            Int32 nbre = 0;
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "select count(*) from cpn where type='CPN3' and date_cpn between '" + avant.Date + "' and '" + apres.Date + "' and mois_trim='" + mois + "' and inscription='"+inscription+"'";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                nbre = (Int32)command.ExecuteScalar();
                
                command.Dispose();
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return nbre;
        }

        public static Int32 NombreCPNMedicalise(DateTime avant, DateTime apres)
        {
            Int32 nbre = 0;
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "select count(*) from cpn where medicalise='Oui' and date_cpn between '" + avant.Date + "' and '" + apres.Date + "'";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                nbre = (Int32)command.ExecuteScalar();
                command.Dispose();
                c.Close();
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message);
            }
            return nbre;
        }

        public static Int32 NombreCPNRisque(string risque, DateTime avant, DateTime apres)
        {
            Int32 nbre = 0;
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "select count(*) from cpn where "+risque+"='Oui' and date_cpn between '" + avant.Date + "' and '" + apres.Date + "'";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                nbre = (Int32)command.ExecuteScalar();
                command.Dispose();
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return nbre;
        }

        public static Int32 NombreCpnGar(string gar, DateTime avant, DateTime apres)
        {
            Int32 nbre = 0;
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "select count(*) from cpn where gestionGAR='" + gar + "' and date_cpn between '" + avant.Date + "' and '" + apres.Date + "'";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                nbre = (Int32)command.ExecuteScalar();
                command.Dispose();
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return nbre;
        }

        public static Int32 NombreSupplement(string supp, DateTime avant, DateTime apres)
        {
            Int32 nbre = 0;
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "select count(*) from cpn where " + supp + "='Oui' and date_cpn between '" + avant.Date + "' and '" + apres.Date + "'";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                nbre = (Int32)command.ExecuteScalar();
                command.Dispose();
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return nbre;
        }

        public static bool Exist(string mois, DateTime d)
        {
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            Int32 compteur = 0;
            string q = "select * from rapportCPN where Mois='" + mois + "' and date_creation = '" + d.Date + "'";
            try
            {

                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    if (dataReader.HasRows)
                    {
                        return true;
                    }
                }
                dataReader.Close();
                command.Dispose();
                c.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        public static void persistRapport(RapportCPN r)
        {

            SqlConnection c = Connexion.connect();
            SqlCommand command;
            DateTime d = DateTime.Today;
            string tab = "rapportCPN(Mois,date_creation,cpn1,cpn2,cpn3_8,cpn3_9,autre_cpn,cpn_medicalise,metrorragie,hta,diabete,cardiopathie,infection,autre_risque,gar_pec,gar_referee,fer,vitamine_d,anemie,cpn2_ni,cpn2_ai,cpn3_8_ni,cpn3_8_ai,cpn3_9_ni,cpn3_9_ai,autre_cpn_ni,autre_cpn_ai)";
            string v = "values('" + r.Mois + "','" + r.DateCreation.Date + "'," + Convert.ToInt32(r.NbreCPN1) + "," + Convert.ToInt32(r.NbreCPN2) + "," + Convert.ToInt32(r.NbreCPN3_8) + "," + Convert.ToInt32(r.NbreCPN3_9) + "," + Convert.ToInt32(r.NbreAutreCPN) + "," + Convert.ToInt32(r.NbreCPNMedicalise) + "," + Convert.ToInt32(r.NbreMetrorragie) + "," + Convert.ToInt32(r.NbreHTA) + "," + Convert.ToInt32(r.NbreDiabete) + "," + Convert.ToInt32(r.NbreCardiopathie) + "," + Convert.ToInt32(r.NbreInfection) + "," + Convert.ToInt32(r.NbreAutreRisque) + "," + Convert.ToInt32(r.NbreGarPec) + "," + Convert.ToInt32(r.NbreGarReferee) + "," + Convert.ToInt32(r.NbreFer) + "," + Convert.ToInt32(r.NbreVitamineD) + "," + Convert.ToInt32(r.NbreAnemie) + "," + Convert.ToInt32(r.NbreCpn2Ni) + "," + Convert.ToInt32(r.NbreCpn2Ai) + "," + Convert.ToInt32(r.NbreCPN3_8NI) + "," + Convert.ToInt32(r.NbreCPN3_8AI) + "," + Convert.ToInt32(r.NbreCPN3_9NI) + "," + Convert.ToInt32(r.NbreCPN3_9AI) + "," + Convert.ToInt32(r.NbreAutreCPNNI) + "," + Convert.ToInt32(r.NbreAutreCPNAI) + ")";
            string q = "insert into " + tab + " " + v;
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                command.ExecuteNonQuery();
                command.Dispose();
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void deleteRapport(string id)
        {

            SqlConnection c = Connexion.connect();
            SqlCommand command;
            DateTime d = DateTime.Today;
            string q = "delete from rapportCPN where id="+Convert.ToInt32(id);
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                command.ExecuteNonQuery();
                command.Dispose();
                MessageBox.Show("Rapport supprimé");
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static DataTable chercherRapportCPN(string critere, string mot)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from rapportCPN where " + critere + " like '%" + mot + "%'";
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

        public static DataTable chercherRapportCPNAvecDate(string critere, string mot, DateTime t)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from rapportCPN where " + critere + " like '%" + mot + "%' and date_creation='" + t.Date + "'";
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

        public static void CreerRapport()
        {
            try
            {
                RapportCPN r = new RapportCPN();
                System.IO.Directory.CreateDirectory("C:/Users/HP/Desktop/RapportCPN");
                var yr = DateTime.Today.Year;
                var mth = DateTime.Today.Month;
                DateTime firstDay = new DateTime(yr, mth, 1).AddMonths(-1);
                DateTime lastDay = new DateTime(yr, mth, 1).AddDays(-1);
                r.DateCreation = DateTime.Today;
                r.Mois = DateTime.Now.AddMonths(-1).ToString("MMMM", CultureInfo.CreateSpecificCulture("fr"));
                r.NbreCPN1 = RapportCPN.NombreCPN("CPN1",firstDay,lastDay).ToString();
                r.NbreCPN2 = RapportCPN.NombreCPN("CPN2", firstDay.Date, lastDay.Date).ToString();
                r.NbreCpn2Ni = RapportCPN.NombreCPNInscription("CPN2", firstDay.Date, lastDay.Date,"NI").ToString();
                r.NbreCpn2Ai = RapportCPN.NombreCPNInscription("CPN2", firstDay.Date, lastDay.Date, "AI").ToString();
                r.NbreCPN3_8 = RapportCPN.NombreCPN3("8", firstDay.Date, lastDay.Date).ToString();
                r.NbreCPN3_8NI = RapportCPN.NombreCPN3Inscription("8", firstDay.Date, lastDay.Date,"NI").ToString();
                r.NbreCPN3_8AI = RapportCPN.NombreCPN3Inscription("8", firstDay.Date, lastDay.Date, "AI").ToString();
                r.NbreCPN3_9 = RapportCPN.NombreCPN3("9", firstDay.Date, lastDay.Date).ToString();
                r.NbreCPN3_9NI = RapportCPN.NombreCPN3Inscription("9", firstDay.Date, lastDay.Date, "NI").ToString();
                r.NbreCPN3_9AI = RapportCPN.NombreCPN3Inscription("9", firstDay.Date, lastDay.Date, "AI").ToString();
                r.NbreAutreCPN = RapportCPN.NombreCPN("Autre", firstDay.Date, lastDay.Date).ToString();
                r.NbreAutreCPNNI = RapportCPN.NombreCPNInscription("Autre", firstDay.Date, lastDay.Date, "NI").ToString();
                r.NbreAutreCPNAI = RapportCPN.NombreCPNInscription("Autre", firstDay.Date, lastDay.Date, "AI").ToString();
                r.NbreCPNMedicalise = RapportCPN.NombreCPNMedicalise(firstDay.Date, lastDay.Date).ToString();
                r.NbreMetrorragie = RapportCPN.NombreCPNRisque("metrorragie",firstDay.Date, lastDay.Date).ToString();
                r.NbreHTA = RapportCPN.NombreCPNRisque("hta", firstDay.Date, lastDay.Date).ToString();
                r.NbreAnemie = RapportCPN.NombreCPNRisque("anemie", firstDay.Date, lastDay.Date).ToString();
                r.NbreDiabete = RapportCPN.NombreCPNRisque("diabete", firstDay.Date, lastDay.Date).ToString();
                r.NbreCardiopathie = RapportCPN.NombreCPNRisque("cardiopathie", firstDay.Date, lastDay.Date).ToString();
                r.NbreInfection = RapportCPN.NombreCPNRisque("infection", firstDay.Date, lastDay.Date).ToString();
                r.NbreAutreRisque = RapportCPN.NombreCPNRisque("autres", firstDay.Date, lastDay.Date).ToString();
                r.NbreGarPec = RapportCPN.NombreCpnGar("PEC", firstDay.Date, lastDay.Date).ToString();
                r.NbreGarReferee = RapportCPN.NombreCpnGar("Référée", firstDay.Date, lastDay.Date).ToString();
                r.NbreFer = RapportCPN.NombreSupplement("fer", firstDay.Date, lastDay.Date).ToString();
                r.NbreVitamineD = RapportCPN.NombreSupplement("vitamine_d", firstDay.Date, lastDay.Date).ToString();
                if (!RapportCPN.Exist(r.Mois, r.DateCreation))
                {
                    Document doc = new Document(PageSize.A4);
                    PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("C:/Users/HP/Desktop/RapportCPN/" + r.DateCreation.Day.ToString() + "-" + r.DateCreation.Month.ToString() + "-" + r.DateCreation.Year.ToString() + ".pdf", FileMode.Create));
                    doc.Open();
                    Paragraph titre = new Paragraph("\nCONSULTATION PRENATALE\n\n");
                    titre.Alignment = Element.ALIGN_CENTER;
                    Paragraph creation = new Paragraph("Rapport du mois de : " + r.Mois.ToUpper() + "\nDate de création" + r.DateCreation.Date.ToString());
                    creation.Alignment = Element.ALIGN_RIGHT;
                    Paragraph mois = new Paragraph("Date début :" + firstDay.Date + "\nDate fin :" + lastDay.Date);
                    mois.Alignment = Element.ALIGN_LEFT;
                    doc.Add(creation);
                    doc.Add(mois);
                    doc.Add(titre);
                    PdfPTable table = new PdfPTable(4);
                    PdfPCell cell = new PdfPCell(new Phrase("Performances\n"));
                    cell.Colspan = 3;
                    table.AddCell(cell);
                    table.AddCell("Urbain");

                    cell = new PdfPCell(new Phrase("CPN du 1er trimestre\n(<= 15 SA)\n"));
                    cell.Colspan = 2;
                    table.AddCell(cell);
                    table.AddCell("NI\n");
                    table.AddCell(r.NbreCPN1);

                    cell = new PdfPCell(new Phrase("CPN du 2ème trimestre (24 à 28 SA)\n"));
                    cell.Rowspan = 2;
                    cell.Colspan = 2;
                    table.AddCell(cell);
                    table.AddCell("NI\n");
                    table.AddCell(r.NbreCpn2Ni);
                    table.AddCell("AI\n");
                    table.AddCell(r.NbreCpn2Ai);

                    cell = new PdfPCell(new Phrase("CPN du 3ème\ntrimestre\n"));
                    cell.Rowspan = 4;
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase("8ème mois\n(32 à 34 SA)\n"));
                    cell.Rowspan = 2;
                    table.AddCell(cell);
                    table.AddCell("NI\n");
                    table.AddCell(r.NbreCPN3_8NI);
                    table.AddCell("AI\n");
                    table.AddCell(r.NbreCPN3_8AI);
                    cell = new PdfPCell(new Phrase("9ème mois\n(>36 SA)\n"));
                    cell.Rowspan = 2;
                    table.AddCell(cell);
                    table.AddCell("NI\n");
                    table.AddCell(r.NbreCPN3_9NI);
                    table.AddCell("AI\n");
                    table.AddCell(r.NbreCPN3_9AI);

                    cell = new PdfPCell(new Phrase("Autres consultations prénatales\n"));
                    cell.Rowspan = 2;
                    cell.Colspan = 2;
                    table.AddCell(cell);
                    table.AddCell("NI\n");
                    table.AddCell(r.NbreAutreCPNNI);
                    table.AddCell("AI\n");
                    table.AddCell(r.NbreAutreCPNAI);

                    cell = new PdfPCell(new Phrase("Nombre de CPN médicalisées\n"));
                    cell.Colspan = 3;
                    table.AddCell(cell);
                    table.AddCell(r.NbreCPNMedicalise);

                    cell = new PdfPCell(new Phrase("Grossesses à risque\n"));
                    cell.Rowspan = 9;
                    cell.Rotation = 90;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase("Nombre de risques\ndépistées\n"));
                    cell.Rowspan = 7;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_CENTER; ;
                    table.AddCell(cell);
                    table.AddCell("Métrorragie\n");
                    table.AddCell(r.NbreMetrorragie);
                    table.AddCell("HTA\n");
                    table.AddCell(r.NbreHTA);
                    table.AddCell("Anémie\n");
                    table.AddCell(r.NbreAnemie);
                    table.AddCell("Diabète\n");
                    table.AddCell(r.NbreDiabete);
                    table.AddCell("Cardiopathie\n");
                    table.AddCell(r.NbreCardiopathie);
                    table.AddCell("Infection\n");
                    table.AddCell(r.NbreInfection);
                    table.AddCell("Autres\n");
                    table.AddCell(r.NbreAutreRisque);
                    cell = new PdfPCell(new Phrase("Nbre GAR Prises en charge\n"));
                    cell.Colspan = 2;
                    table.AddCell(cell);
                    table.AddCell(r.NbreGarPec);
                    cell = new PdfPCell(new Phrase("Nbre GAR Référées\n"));
                    cell.Colspan = 2;
                    table.AddCell(cell);
                    table.AddCell(r.NbreGarReferee);

                    cell = new PdfPCell(new Phrase("Suplémentation\n"));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_CENTER; 
                    cell.Rowspan = 2;
                    cell.Rotation = 90;
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase("Nombre de femmes ayant reçu le\nfer\n"));
                    cell.Colspan = 2;
                    table.AddCell(cell);
                    table.AddCell(r.NbreFer);
                    cell = new PdfPCell(new Phrase("Nombre de femmes ayant reçu la\nvitamine D\n"));
                    cell.Colspan = 2;
                    table.AddCell(cell);
                    table.AddCell(r.NbreVitamineD);

                    doc.Add(table);
                    doc.Close();
                    RapportCPN.persistRapport(r);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
    }
}
