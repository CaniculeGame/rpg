using System.IO;

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

    public Carte(uint xMax, uint yMax)
    {
        maxX = xMax;
        maxY = yMax;

        carte = new Cellule[maxX, maxY];
    }

    public Carte(string path)
    {
        if (path == null)
            return;

        LireFichierCarte(path);
    }

    private void LireFichierCarte(string path)
    {

        string[] lines;

        if (path == null)
            return;

#if UNITY_ANDROID

#else
        lines = File.ReadAllLines(path);
#endif

        int nbLines = lines.Length;
        if (nbLines < 2)
            return;

        maxX = uint.Parse(lines[0]);
        maxY = uint.Parse(lines[1]);


        if (maxX < 0 || maxY < 0)
            return;


        carte = new Cellule[maxX, maxY];
        //parseur
        for (int i = 2; i < nbLines; i++)
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


    public void SavegarderFichierCarte(string path)
    {

    }


    public uint Xmax { get { return maxX; } set { maxX = value; } }
    public uint Ymax { get { return maxY; } set { maxY = value; } }


    public Cellule DonnerCellule(int x, int y)
    {

        if (x < 0 || y < 0)
            return null;

        if (x >= maxX || y >= maxY)
            return null;

        return carte[x, y];
    }

}
