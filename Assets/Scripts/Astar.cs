using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Astar : MonoBehaviour
{
	private static List<Noeud> m_openList;
	private static List<Noeud> m_closeList;
	
	public static List<Noeud> FindPath( Carte param_graphe, Noeud param_start, Noeud param_end, bool avecDiagonale )
	{
        Debug.Log("Depart : "+ param_start.x +"," + param_start.y + "   arrive :" + param_end.x +"," + param_end.y);


        // on crée les listes fermées et les listes ouvertes
        m_openList  = new List<Noeud>();
		m_closeList = new List<Noeud>();
		
		// on crée la variable qui va accueillir le chemin final
		List<Noeud> finalPath= new List<Noeud>();


		//  traitement
		//ajout noeud de départ à liste ouverte
		m_openList.Add(param_start);
		Noeud courant = null;
		//tant que la list est pas nul faire
		while(m_openList.Count > 0)
		{

			//Récupération du node avec le plus petit F contenu dans la liste ouverte. On le nommera CURRENT.
			courant = getNoeudcourant();

			//Stopper la boucle si arrivé
			if( courant.x == param_end.x && courant.y == param_end.y) 
			{
//				Debug.Log("arriver");
				break;
			}

			//Basculler courrant de liste ouverte vers liste fermé
			m_closeList.Add(courant);
			m_openList.Remove(courant);

			//pour chacun des 8 ou 4 noeuds adjacents au noeud courant faire:
			List<Noeud> voisin = ObtenirVoisin(param_graphe, courant, avecDiagonale);
//			Debug.Log("voisin= "+voisin.Count);
			foreach(Noeud elem in voisin)
			{
				// si elem est un obstacle 
				if(elem.EstOccupe == true) 
				{
					Debug.Log("obstacle");
					continue;
				}
                // si elem  est dans la liste fermé on passe au suivant
                if (m_closeList.Contains(elem) == true)
                {
                    //Debug.Log("est dans liste");
                    continue;
                }

				//si elem n'est pas dans la liste ouverte : l'ajouter et faire les mises à jour adequates
				if(m_openList.Contains(elem)== false)
				{
					elem.g=elem.parent.g + param_graphe.SizeCase;
                    elem.h= Distance(elem, param_end, avecDiagonale) * param_graphe.SizeCase;
                    elem.ActuPoid();
					elem.parent = courant;
					m_openList.Add(elem);
					//Debug.Log("ajout");
				}
				else //si deja dans la liste on met a jour 
				{
					int gAncien  = elem.g;
					int gNouveau = elem.parent.g + param_graphe.SizeCase;

					if(gNouveau<gAncien)
					{
						elem.parent = courant;
						elem.g = gNouveau;
						elem.h = Distance(elem,param_end,avecDiagonale)*param_graphe.SizeCase;
						elem.ActuPoid();
						//Debug.Log("maj");
					}
				}
			}
		}

		//aucun chemin trouver
		if( m_openList.Count == 0 )
			return null;
		
		//on construit le chemin
		Noeud DernierNoeud = courant;
        while (DernierNoeud.x != param_start.x || DernierNoeud.y != param_start.y)
		{
				finalPath.Add(DernierNoeud);
				DernierNoeud = DernierNoeud.parent;
		}

		finalPath.Reverse();
		return finalPath;
	}



    private static int Distance(Noeud depart, Noeud arrive, bool avecDiagonale)
    {
        if (avecDiagonale)
            return (int)Math.Sqrt(Math.Pow(arrive.x - depart.x, 2) + Math.Pow(arrive.y - depart.y, 2));
        else
            return Math.Abs(arrive.x - depart.x) + Math.Abs(arrive.y - depart.y);
    }

    private static bool EstDansCarte(int x, int y, int xM, int yM)
    {
        if (x < 0 || y < 0)
            return false;

        if (x >= xM || y >= yM)
            return false;

        return true;
    }

    private static List<Noeud> ObtenirVoisin(Carte carte, Noeud n, bool avecDiagolane)
    {
        List<Noeud> voisins = new List<Noeud>();

        int xDepart = n.x;
        int yDepart = n.y;

        int xMax = (int)carte.Xmax;
        int yMax = (int)carte.Ymax;


        for (int x = xDepart - 1; x <= xDepart + 1; x++)
        {
            for (int y = yDepart - 1; y <= yDepart + 1; y++)
            {
                if (!EstDansCarte(x, y, xMax, yMax))
                    continue;

                if (x == xDepart && y == yDepart)
                    continue;

                if (!avecDiagolane)
                    if (x == xDepart - 1 && y == yDepart + 1 ||
                       x == xDepart + 1 && y == yDepart + 1 ||
                       x == xDepart - 1 && y == yDepart - 1 ||
                       x == xDepart + 1 && y == yDepart - 1)
                        continue;

                Noeud node = carte.DonnerCellule(x, y).Node;
                voisins.Add(node);
            }
        }

        return voisins;
    }

    //récupération du noeud avec le plus petit poid F contenu dans la liste m_open
    private static Noeud getNoeudcourant()
	{
		Noeud NoeudRef;
		Noeud NoeudTemp;
		int fmin=0;

		NoeudRef  = m_openList[0];
		fmin= NoeudRef.poid;

		for(int i=0; i< m_openList.Count; i++)
		{
			NoeudTemp = m_openList[i];
			if(fmin >= NoeudTemp.poid)
			{
				NoeudRef = NoeudTemp;
				fmin=NoeudRef.poid;
			}
		}

		return NoeudRef;
	}

}
