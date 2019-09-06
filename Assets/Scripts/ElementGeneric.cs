using UnityEngine;

public class ElementGeneric : MonoBehaviour
{
    public enum TYPE_ELEMENT : uint
    {
        TYPE_ELEMENT_AUCUN = 0,
        TYPE_ELEMENT_PERSONNAGE = 1,
        TYPE_ELEMENT_PNJ = 2,
        TYPE_ELEMENT_OBJET = 3,
        TYPE_ELEMENT_MAX,
    }

    public TYPE_ELEMENT typeElement;
    public uint idElment;

    public int height;
    public int widh;
    public int hauteur;

    public uint DonnerIdElement { get { return idElment; } }
    public TYPE_ELEMENT DonnerTypeElement { get { return typeElement; }}
    public void AjouterElement(TYPE_ELEMENT element) { typeElement = element; }


    public ElementGeneric()
    {
        typeElement = TYPE_ELEMENT.TYPE_ELEMENT_AUCUN;
        idElment = 0;

        height = 1;
        widh = 1;
        hauteur = 1;
    }


    public ElementGeneric(TYPE_ELEMENT elt, uint id, int h = 1, int w = 1, int l = 1)
    {
        typeElement = elt;
        idElment = id;

        height = h;
        widh = w;
        hauteur = l;
    }

}
