


class Couple
{
    private string name;
    private float valeur;

    public Couple(string n, float v)
    {
        name = n;
        valeur = v;
    }

    public string Name { get { return name; } set { name = value; } }
    public float Valeur { get { return valeur; } set { valeur = value; } }
}