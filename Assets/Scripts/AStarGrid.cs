using System;
using System.Collections.Generic;


public class AStarGrid
{

    private static int Compare2Noeud(Cellule n1, Cellule n2)
    {
        /*   if (n1.Node.heuristique < n2.Node.heuristique)
               return 1;
           else if (n1.Node.heuristique == n2.Node.heuristique)
               return 0;
           else
               return -1;*/
        return -1;
    }


    private static Stack<Cellule> ReconstituerChemin(Cellule n)
    {
        return null;
    }


    private static bool EstDansCarte(int x, int y, int xM, int yM)
    {
        if (x < 0 || y < 0)
            return false;

        if (x > xM || y > yM)
            return false;

        return true;
    }

    private static List<Cellule> ObtenirVoisin( Carte carte, Cellule n, bool avecDiagolane)
    {
        List<Cellule> newList = new List<Cellule>();

        int xDepart = n.Node.x;
        int yDepart = n.Node.y;

        int xMax = (int)carte.Xmax;
        int yMax = (int)carte.Ymax;


        for(int x = xDepart - 1; x <= xDepart +1; x++)
        {
            for(int y = yDepart -1; y <= yDepart +1; y++)
            {
                if (!EstDansCarte(x, y, xMax, yMax))
                    continue;

                if (x == xDepart && y == yDepart)
                    continue;

                if(!avecDiagolane)
                    if(x == xDepart - 1 && y == yDepart + 1 ||
                       x == xDepart + 1 && y == yDepart + 1 ||
                       x == xDepart - 1 && y == yDepart - 1 ||
                       x == xDepart + 1 && y == yDepart - 1)
                        continue;

                if (carte.DonnerCellule(x, y) != null && carte.DonnerCellule(x, y).EstOccupe)
                    continue;

                newList.Add(carte.DonnerCellule(x, y));
            }
        }

        return newList;
    }

    private static int Distance(Cellule n, Cellule arrive, bool avecDiagonale)
    {
        if(avecDiagonale)
            return (int)Math.Sqrt( Math.Pow(arrive.Node.x - n.Node.x,2) + Math.Pow(arrive.Node.y - n.Node.y,2));
        else
            return  Math.Abs(arrive.Node.x - n.Node.x) + Math.Abs(arrive.Node.y - n.Node.y);
    }

    public static Stack<Cellule> Chemin(Carte carte, Cellule depart, Cellule arrive, bool avecDiagolane)
    {
        Stack<Cellule> closeList   = new Stack<Cellule>();
        Stack<Cellule> openList    = new Stack<Cellule>();
        Stack<Cellule> cheminFinal = new Stack<Cellule>();

        openList.Push(depart);
        while( openList.Count > 0 && openList.Count < 1000)
        {
            Cellule noeudCourrant = openList.Pop();
            if (noeudCourrant.Node.x == arrive.Node.x && noeudCourrant.Node.y == arrive.Node.y)
                break;

            closeList.Push(noeudCourrant);
            List<Cellule> voisin = ObtenirVoisin(carte, noeudCourrant, avecDiagolane);
            for(int i = 0; i < voisin.Count ; i++)
            {
                Cellule noeud = voisin[i];
                if (closeList.Contains(noeud))
                    continue;
/*
                Cellule.Noeud nd = noeud.Node;
                nd.cout = noeudCourrant.Node.cout + 1; 
                nd.heuristique = voisin[i].Node.cout + Distance(voisin[i], arrive,avecDiagolane); 
                noeud.Node = nd;
*/

                if(openList.Contains(noeud))
                {
                  

                }


                if (closeList.Count > 1000)
                    return null;
            }  
        }

        // pas de chemin trouvé
        if (openList.Count != 0)
            return cheminFinal;

        //chemin trouvé


        return null;
    }
}
