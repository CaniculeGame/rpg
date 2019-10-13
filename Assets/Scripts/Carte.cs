using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

/* format fichier Text
x
y
xn;yn:hauteur:typeElement:idElement
*/


public class Carte 
{
    public enum  TYPE_TERRAIN : int
    {
        TYPE_TERRAIN_AUCUN = 0,
        TYPE_TERRAIN_PLAINE = 1,
        TYPE_TERRAIN_DESERT = 2,
        TYPE_TERRAIN_TROPICAL = 3,
        TYPE_TERRAIN_POLAIRE = 4,
        TYPE_TERRAIN_MAX
    }


    private Cellule[,] carte;
    private uint maxX;
    private uint maxY;
    private string name;
    private string path;
    private int sizeCase;
    private TYPE_TERRAIN typeTerrain;

    public Carte(string pth, string nom , uint xMax, uint yMax, int sizeTill = 1, TYPE_TERRAIN type = TYPE_TERRAIN.TYPE_TERRAIN_PLAINE)
    {
        maxX = xMax;
        maxY = yMax;

        name = nom;
        path = pth;

        typeTerrain = type;

        sizeCase = sizeTill;

        carte = new Cellule[maxX, maxY];
        for (int x = 0; x < maxX; x++)
            for (int y = 0; y < yMax; y++)
                DonnerCellule(x, y);
    }

    public Carte(string pth)
    {

        if (pth == null)
            return;

        /*LireFichierCarte(pth);*/
    }


    private void LireFichierCarte(string pth)
    {
        return; /* A faire*/

       string[] lines;

        if (pth == null)
            return;

        path = pth;

#if UNITY_ANDROID

#else
        lines = File.ReadAllLines(path);
#endif

        int nbLines = lines.Length;
        if (nbLines < 3)
            return;

        maxX = uint.Parse(lines[0]);
        maxY = uint.Parse(lines[1]);
        name = lines[2];

        if (maxX < 0 || maxY < 0)
            return;


        carte = new Cellule[maxX, maxY];
        //parseur
        for (int i = 3; i < nbLines; i++)
        {
            string[] str = lines[i].Split(':');
            if (str.Length == 5)
            {
                uint xcel = uint.Parse(str[0]);
                uint ycel = uint.Parse(str[1]);
                int ht = int.Parse(str[2]);
                int typeElt = int.Parse(str[3]);
                uint id = uint.Parse(str[4]);

                ElementGeneric elt = new ElementGeneric((ElementGeneric.TYPE_ELEMENT)typeElt, id);
               /* carte[xcel, ycel] = new Cellule();*/
                carte[xcel, ycel].AjouterElement(elt);
                carte[xcel, ycel].Hauteur = ht;
            }
        }

    }


    public void SavegarderFichierCarte(string pth)
    {
        if (carte == null)
            return;

        if (name == null && pth == null && path == null)
            path = Application.dataPath + "/Resources/Cartes/Carte" + DateTime.Now.ToString();
        else if (pth != null)
            path = pth;
        else
        {
            path =  name;
            File.Create(path);
        }

        string strEntete = Xmax.ToString() + "\n" + Ymax.ToString() + "\n";
        List<string> lines = new List<string>();

        for (int i = 0; i < Xmax; i++)
        {
            for (int j = 0; j < Ymax; j++)
            {
                if(carte[i, j]!= null && carte[i,j].EstOccupe)
                {
                    string str = i.ToString()+":"+j.ToString()+":"+ carte[i, j].Hauteur.ToString()+":"+carte[i,j].Element()+"\n";
                    lines.Add(str);
                }
            }
        }

        File.AppendAllLines(path, lines.ToArray());
    }

    public int  SizeCase { get { return sizeCase; } set { sizeCase = value; } }
    public uint Xmax { get { return maxX; } set { maxX = value; } }
    public uint Ymax { get { return maxY; } set { maxY = value; } }
    public string Name { get { return name; } set { name = value; } }
    public string Path { get { return path; } set { path = value; } }
    public TYPE_TERRAIN Terrain { get { return typeTerrain; } set { typeTerrain = value; } }

    public Cellule DonnerCellule(int x, int y)
    {

        if (x < 0 || y < 0)
            return null;

        if (x >= maxX || y >= maxY)
            return null;

        if(carte[x,y] == null)
        {
            carte[x, y] = new Cellule(x,y,false);
        }

        return carte[x, y];
    }

}
