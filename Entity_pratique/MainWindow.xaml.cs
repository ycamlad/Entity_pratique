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

namespace Entity_pratique
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GalerieEntities myBdd = new GalerieEntities();
        public MainWindow()
        {
            InitializeComponent();
            cboArtiste.DataContext = myBdd.Artistes.ToList();
            cboArtiste.DisplayMemberPath = "Prenom";
            lstOeuvre.DisplayMemberPath = "Titre";
            dtg.DisplayMemberPath = "Titre";

        }

        private void cboArtiste_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Artiste sArtiste = cboArtiste.SelectedItem as Artiste;
           lstOeuvre.DataContext = sArtiste.Oeuvres.ToList();
            dtg.DataContext = sArtiste.Oeuvres.ToList();
 }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            
            Oeuvre o = new Oeuvre();
            o.IdOeuvre = "12359";
            o.Titre = "manga";
            o.Valeur = 530;
            o.IdArtiste = "22222";
            o.Etat = "expose";

            myBdd.Oeuvres.Add(o);
            try
            {
                myBdd.SaveChanges();
                MessageBox.Show("succes!");
                Artiste myArtist = cboArtiste.SelectedItem as Artiste;
                lstOeuvre.DataContext = myArtist.Oeuvres.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
                  
            }
        }

        private void btnVendre_Click(object sender, RoutedEventArgs e)
        {
            Oeuvre myOeuvre = lstOeuvre.SelectedItem as Oeuvre;
            myOeuvre.Valeur -=100 ;

            try
            {
                myBdd.SaveChanges();
                MessageBox.Show("vendue!");
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            Oeuvre myOeuvre = lstOeuvre.SelectedItem as Oeuvre;
            myBdd.Oeuvres.Remove(myOeuvre);
            try
            {
                myBdd.SaveChanges();
                MessageBox.Show("supprimer!");
                Artiste myArtist = cboArtiste.SelectedItem as Artiste;
                lstOeuvre.DataContext = myArtist.Oeuvres.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            Artiste myArtist = cboArtiste.SelectedItem as Artiste;
            if (chk.IsChecked == true) {
                lstOeuvre.DataContext = (from p in myArtist.Oeuvres
                                        where p.Etat.Trim() == "expose" 
                                        select p).ToList();

            }
            else
            {
                lstOeuvre.DataContext = myArtist.Oeuvres.ToList();

            }


            // < ListView x: Name = "lstArgent" HorizontalAlignment = "Left" Height = "217" Margin = "660,59,0,0" VerticalAlignment = "Top" Width = "157"  IsSynchronizedWithCurrentItem = "True" ItemsSource = "{Binding}" DisplayMemberPath = "Montant" >


            //cboCompte.DataContext = mybdd.Compte_Bancaire.ToList();

            //dtgAffiche.DataContext = (from c in mybdd.Compte_Bancaire
            //                          where c.CODE_Client == "1234"
            //                          select c).ToList();

            //private void cboCompte_SelectionChanged(object sender, SelectionChangedEventArgs e)
            //{
            //    Compte_Bancaire compte_ = cboCompte.SelectedItem as Compte_Bancaire;
            //    lstArgent.DataContext = compte_.Numero_de_Compte.ToList();

            //}
        }
    }
}
