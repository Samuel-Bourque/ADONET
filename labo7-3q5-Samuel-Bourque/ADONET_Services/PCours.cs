using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ADONET_Services
{
    public class PCours
    {
        private static PCours _pCours = null;
        private string chaineConnexion = "Data Source = info88.cegepthetford.ca; Initial Catalog = C3Q5sambour; Persist Security Info=True;User ID = sambour3q5; Password=Ferland123";

        private PCours()
        {
            // Constructeur
        }

        public static PCours getInstance()
        {
            if (_pCours == null)
                _pCours = new PCours();
            return _pCours;
        }

        public void listerCoursSession(int noSession)
        {
            using (SqlConnection connexion = new SqlConnection(chaineConnexion))
            {

                SqlCommand cmdSelectCoursSession = new SqlCommand("Select NOCOURS, TITRE from COURSINFO Where noSession = @noSession", connexion);
                cmdSelectCoursSession.Parameters.Add("@noSession", SqlDbType.Int);
                cmdSelectCoursSession.Parameters["@noSession"].Value = noSession;
                connexion.Open();
                SqlDataReader reader = cmdSelectCoursSession.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Debug.WriteLine(String.Format("{0}, {1}", reader[0], reader[1]));
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
        }

        public int denombrerCoursInfo()
        {
            int nbCoursInfo = 0;
            using (SqlConnection connexion = new SqlConnection(chaineConnexion))
            {
                SqlCommand cmdNbCoursInfo = new SqlCommand("SELECT COUNT(*) as nbCours FROM COURSINFO Where NOCOURS like '420%' ", connexion);
                connexion.Open();
                nbCoursInfo = (int)cmdNbCoursInfo.ExecuteScalar();
                connexion.Close();
            }
            return nbCoursInfo;
        }

        public void insererNouveauCours(string noCours, string titre, int theo, int labo, int perso, int noSession)
        {
            try
            {
                using (SqlConnection connexion = new SqlConnection(chaineConnexion))
                {
                    SqlDataAdapter ajoutCours = new SqlDataAdapter("SELECT * FROM COURSINFO", connexion);
                    SqlCommandBuilder builder = new SqlCommandBuilder(ajoutCours);
                    DataTable dtCours = new DataTable();
                    ajoutCours.Fill(dtCours);
                    DataRow nouvelleRangee = dtCours.NewRow();
                    nouvelleRangee["NOCOURS"] = noCours;
                    nouvelleRangee["TITRE"] = titre;
                    nouvelleRangee["PONDTHEO"] = theo;
                    nouvelleRangee["PONDLAB"] = labo;
                    nouvelleRangee["PONDTP"] = perso;
                    nouvelleRangee["NOSESSION"] = noSession;
                    dtCours.Rows.Add(nouvelleRangee);
                    ajoutCours.Update(dtCours);
                    Debug.WriteLine("Ajout du nouveau cours");
                }
            }
            catch (Exception e)
            {
                throw new PCoursException(e.Message);
            }
        }

        public void doublerTheorieSession5()
        {
            using (SqlConnection connexion = new SqlConnection(chaineConnexion))
            {
                SqlCommand cmdDoublerTheo = new SqlCommand("Update COURSINFO Set PONDTHEO = PONDTHEO*2 Where noSession = 5", connexion);
                connexion.Open();
                cmdDoublerTheo.ExecuteNonQuery();
                connexion.Close();
            }
        }
        public int supprimerCoursPhilo()
        {
            using (SqlConnection connexion = new SqlConnection(chaineConnexion))
            {
                SqlCommand cmdSupprimerCoursPhilo = new SqlCommand("DELETE FROM COURSINFO Where NOCOURS like '340%'", connexion);
                connexion.Open();
                int nbCoursSupprimes = (int)cmdSupprimerCoursPhilo.ExecuteNonQuery();
                connexion.Close();
                return nbCoursSupprimes;
            }

        }

        public void listerCoursSessionDataAdapter(int noSession)
        {
            using (SqlConnection connexion = new SqlConnection(chaineConnexion))
            {
                SqlDataAdapter daCours = new SqlDataAdapter("SELECT NOCOURS, TITRE from COURSINFO Where noSession =" + noSession, connexion);
                DataSet dsCours = new DataSet();
                daCours.Fill(dsCours, "COURSINFO");
                foreach (DataRow tupleCours in dsCours.Tables["COURSINFO"].Rows)
                {
                    Debug.WriteLine(String.Format("noCours: {0}", tupleCours["NOCOURS"]), String.Format("titre: {0}", tupleCours["TITRE"]));

                }
            }
        }
    }
}