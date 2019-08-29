public class ElementGeneric
{
    public enum TYPE_ELEMENT : uint
    {
        TYPE_ELEMENT_AUCUN = 0,
        TYPE_ELEMENT_PERSONNAGE = 1,
        TYPE_ELEMENT_PNJ = 2,
        TYPE_ELEMENT_OBJET = 3,
        TYPE_ELEMENT_MAX = 4,
    }


    private TYPE_ELEMENT typeElement;
    private uint idElment;


    public uint DonnerIdElement { get { return idElment; } }
    public TYPE_ELEMENT DonnerTypeElement { get { return typeElement; }}
    public void AjouterElement(TYPE_ELEMENT element) { typeElement = element; }


    public ElementGeneric()
    {
        typeElement = TYPE_ELEMENT.TYPE_ELEMENT_AUCUN;
        idElment = 0;
    }


    public ElementGeneric(TYPE_ELEMENT elt, uint id)
    {
        typeElement = elt;
        idElment = id;
    }

}
