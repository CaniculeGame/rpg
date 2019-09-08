using System.Collections.Generic;


public class Cellule
{
    private bool estOccupe = false;

    List<ElementGeneric> elements;
    private int hauteur = 0;

    public Cellule()
    {
        hauteur = 0;
    }

    public string Element()
    {
        string str= "";
        foreach (ElementGeneric elt in elements)
        {
            str += elt.DonnerTypeElement.ToString()+":"+elt.DonnerIdElement.ToString();
        }

        return str;
    }

    public bool EstOccupe { get { return estOccupe; } set { estOccupe = value; } }
    public int Hauteur { get { return hauteur; } set { hauteur = value; } }

    public void AjouterElement(ElementGeneric elt)
    {
        if (elements == null)
            elements = new List<ElementGeneric>();

        elements.Add(elt);
        estOccupe = true;
    }

    public void SupprimerElement(ElementGeneric elt)
    {
        if (elements == null)
            return;

        elements.Remove(elt);

        if(elements.Count <= 0)
            estOccupe = false;
    }
}
