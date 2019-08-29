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
    private Cellule[,] carte;
    private uint maxX;
    private uint maxY;
    private string name;
    private string path;

    public Carte(string pth, string nom , uint xMax, uint yMax)
    {
        maxX = xMax;
        maxY = yMax;

        name = nom;
        path = pth;

        carte = new Cellule[maxX, maxY];
        GameManage.DonnerInstance.Action = GameManage.ACTION_TYPE.ACTION_TYPE_CHANGEMENT_CARTE;
    }

    public Carte(string pth)
    {
        if (pth == null)
            return;

        LireFichierCarte(pth);
    }


    private void LireFichierCarte(string pth)
    {

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
                carte[xcel, ycel] = new Cellule();
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

    public uint Xmax { get { return maxX; } set { maxX = value; } }
    public uint Ymax { get { return maxY; } set { maxY = value; } }
    public string Name { get { return name; } set { name = value; } }
    public string Path { get { return path; } set { path = value; } }

    public Cellule DonnerCellule(int x, int y)
    {

        if (x < 0 || y < 0)
            return null;

        if (x >= maxX || y >= maxY)
            return null;

        return carte[x, y];
    }

}
