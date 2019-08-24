using System;

namespace UnityTest
{

    public class Singleton 
    {
        /* notre unique instance */
        private static Singleton instance = null;


        /*valeurs à garder */
       private  int numeroNiveau = -1;




        /* constructeur */
        private Singleton()
        {
            /* creer valeurs à partager */
            Initialiser();
        }


        /* recuperation des valeurs partagees*/
        public static Singleton DonnerInstance
        {
            get
            {
                if (instance == null)
                    instance = new Singleton();

                return instance;
            }
        }


        private void Initialiser()
        {
            numeroNiveau = 0;
        }

        public void RetourMainMenu()
        {

        }


        /* Fonctions d'acces aux donnees */
        public int DonnerNumeroDuNiveau { get { return numeroNiveau; } set { numeroNiveau = value; } }


    }
}