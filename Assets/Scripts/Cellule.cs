using System.Collections.Generic;


public class Cellule
{
    List<ElementGeneric> elements;
    private int hauteur = 0;
    private Noeud noeud;


    public Cellule(int x, int y, bool walkable)
    {
        hauteur = 0;
        noeud = new Noeud(walkable, x, y);

    }

    public string Element()
    {
        string str = "";
        foreach (ElementGeneric elt in elements)
        {
            str += elt.DonnerTypeElement.ToString() + ":" + elt.DonnerIdElement.ToString();
        }

        return str;
    }

    public bool EstOccupe { get { return noeud.EstOccupe; } set { noeud.EstOccupe = value; } }
    public int Hauteur { get { return hauteur; } set { hauteur = value; } }
    public Noeud Node{ get{ return noeud;} set { noeud = value;}}
    public int X { get { return noeud.x; } set { noeud.x = value; } }
    public int Y { get { return noeud.y; } set { noeud.y = value; } }
    public Noeud Parent { get { return noeud.parent; } set { noeud.parent = value; } }

    public void AjouterElement(ElementGeneric elt)
    {
        if (elements == null)
            elements = new List<ElementGeneric>();

        elements.Add(elt);
        EstOccupe = true;
    }

    public void SupprimerElement(ElementGeneric elt)
    {
        if (elements == null)
            return;

        elements.Remove(elt);

        if(elements.Count <= 0)
            EstOccupe = false;
    }
}
