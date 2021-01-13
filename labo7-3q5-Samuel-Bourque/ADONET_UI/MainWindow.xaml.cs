using ADONET_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace ADONET_UI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AfficherCoursSession(object sender, RoutedEventArgs e)
        {
            PCours.getInstance().listerCoursSession(5);
            tbMessages.AppendText("Operation terminee.\n");
        }

        private void DeterminerNbCoursInfo(object sender, RoutedEventArgs e)
        {
            int nb = PCours.getInstance().denombrerCoursInfo();
            tbMessages.AppendText($"Nb cours d'info: {nb}\n");
        }

        private void AjouterCoursTrump(object sender, RoutedEventArgs e)
        {
            try
            {
                PCours.getInstance().insererNouveauCours("330-666-RA", "Le monde selon Trump", 3, 3, 3, 6);
                tbMessages.AppendText("Cours sur Trump ajoute.\n");
            } catch(PCoursException)
            {
                tbMessages.AppendText("Ce cours existe deja.\n");
            }
        }

        private void DoublerTheorieSession5(object sender, RoutedEventArgs e)
        {
            PCours.getInstance().doublerTheorieSession5();
            tbMessages.AppendText("Les cours de 5e session ont ete doubles.\n");
        }

        private void SupprimerCoursPhilo(object sender, RoutedEventArgs e)
        {
            int nbCoursSupprimes = 0;
            nbCoursSupprimes = PCours.getInstance().supprimerCoursPhilo();
            tbMessages.AppendText("Plus de cours de philo.\n");
            tbMessages.AppendText("Nombre de cours supprimés : " + nbCoursSupprimes.ToString());
        }

        private void AfficherCoursSessionDA(object sender, RoutedEventArgs e)
        {
            PCours.getInstance().listerCoursSessionDataAdapter(5);
            tbMessages.AppendText("Operation terminee.\n");
        }
    }
}
