using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
    /* gui */
    public GameObject vieGui;
    public GameObject paGui;
    public GameObject pmGui;

    /* animation */
    private Animator animator;
    public float vitesse = 4f;

    /* Personnage */
    private int vieActu = 0;
    public int vieMax   = 100;
    private int pmActu  = 0;
    public int pmMax    = 5;
    private int paActu  = 0;
    public int paMax    = 5;

    /* gestion action */
    public DoubleClick doubleCLick;

    /* gestion deplacement */
    public bool deplacementEnCours = false;
    public Vector3 positionCible;
    public Vector3 positionDepart;
    public List<Noeud> chemin;

    // Use this for initialization
    void Start()
    {
        doubleCLick = new DoubleClick();

        animator = this.GetComponent<Animator>();

        CreerPersonnage(pmMax, paMax, vieMax);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManage.DonnerInstance.Role == GameManage.ROLE.ROLE_AUCUN)
            return;

        if (deplacementEnCours)
        {
            Deplacement();
            MajGUI();
            return;
        }

        animator.SetInteger("vie", vieActu);
        if (vieActu <= 0)
            return;

        if (Input.GetButtonUp("Fire1"))
        {
            Transform obj;
#if UNITY_EDITOR

            obj = GuiManager.SelectObjet(Input.mousePosition);
#elif UNITY_ANDROID
        if(Input.touchCount >0)
            return;

        obj = GuiManager.SelectObjet(Input.touches[0].position;
#endif

            if (doubleCLick.DoubleClic())
            {
                /* deplacement */
                if (!deplacementEnCours)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                        positionCible = hit.point;

                    Noeud depart = new Noeud(true, Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.z));
                    Noeud arrive = new Noeud(true, Mathf.FloorToInt(positionCible.x), Mathf.FloorToInt(positionCible.z));

                    if (arrive.x < 0)
                        arrive.x = 0;
                    if (arrive.y < 0)
                        arrive.y = 0;

                    Carte carte = GameManage.DonnerInstance.Carte;
                    if (carte != null)
                        chemin = Astar.FindPath(carte, depart, arrive, GameManage.DonnerInstance.Diagonale);
                }

                deplacementEnCours = true;
                positionDepart = transform.position;
            }
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            animator.SetInteger("state", 2);
        }
        else
        {
            animator.SetInteger("state", 0);
        }

        MajGUI();

    }

    private void MajGUI()
    {
        /* maj pm, pa, vie*/
        if (pmGui != null)
        {
            pmGui.transform.GetChild(1).GetComponent<Slider>().maxValue = pmMax;
            pmGui.transform.GetChild(1).GetComponent<Slider>().value = pmActu;
            pmGui.transform.GetChild(2).GetComponent<Text>().text = pmActu + "/" + pmMax;
        }

        if (paGui != null)
        {
            paGui.transform.GetChild(1).GetComponent<Slider>().maxValue = paMax;
            paGui.transform.GetChild(1).GetComponent<Slider>().value = paActu;
            paGui.transform.GetChild(2).GetComponent<Text>().text = paActu + "/" + paMax;
        }

        if (vieGui != null)
        {
            vieGui.transform.GetChild(1).GetComponent<Slider>().maxValue = vieMax;
            vieGui.transform.GetChild(1).GetComponent<Slider>().value = vieActu;
            vieGui.transform.GetChild(2).GetComponent<Text>().text = vieActu + "/" + vieMax;
        }
    }


    public void CreerPersonnage(int pm, int pa, int vie)
    {
        paMax = pa;
        pmMax = pm;
        vieMax = vie;

        pmActu = pmMax;
        paActu = paMax;
        vieActu = vieMax;

    }

    public int VieActu { get { return vieActu; } set { if (value >= 0) vieActu = value; } }
    public int PmActu { get { return pmActu; } set { if (value >= 0) pmActu = value; } }
    public int PaActu { get { return paActu; } set { if (value >= 0) paActu = value; } }
    public int VieMax { get { return vieMax; } set { if (value >= 0) vieMax = value; } }
    public int PmMax { get { return pmMax; } set { if (value >= 0) pmMax = value; } }
    public int PaMax { get { return paMax; } set { if (value >= 0) paMax = value; } }
    public float Vitesse { get { return vitesse; } set { if (value >= 0) vitesse = value; } }


    private void StopDeplacement()
    {
        animator.SetInteger("state", 0);
        deplacementEnCours = false;
        pmActu = pmMax;
        paActu = paMax;
        return;
    }

    // si milieux et mm case
    private bool EstMilieuxCase(Vector3 pos, Noeud caseDepart, Noeud caseArrivee, float size = 0.5f, float margeErreur = 0.2f)
    {
        if (size <= 0)
            size = 0.5f;

        // meme case
        if(caseDepart.x == caseArrivee.x && caseDepart.y == caseArrivee.y)
        {
            //calcul bbox
            float x, y, h, w = 0;
            x = caseArrivee.x + size - margeErreur;
            y = caseArrivee.y + size - margeErreur;
            w = caseArrivee.x + size + margeErreur;
            h = caseArrivee.y + size + margeErreur;

            //milieux de la case avec marge erreur
            if ((pos.x >= x && pos.x < w) 
                && (pos.z >= y && pos.z < h))
                return true;
        }

        return false;
    }

    public void  Deplacement()
    {
        // on ne peut plus se deplacer
        if (pmActu <= 0 && paActu <=0)
            StopDeplacement();

        if (chemin == null)
            return;

        if (chemin.Count <= 1)
        {
            StopDeplacement();
            return;
        }

        //case actuelle
        Noeud depart = chemin[0];
        int size = GameManage.DonnerInstance.Carte.SizeCase;


#if UNITY_EDITOR
        string str = "";
        foreach (Noeud n in chemin)
        {
            str += " ; (" + n.x + "," + n.y + ")";
        }
        Debug.Log(str);
#endif

        // recupere la case visé:
        Noeud arrive = chemin[1];
        //continue animation
        animator.SetInteger("state", 1);

        // calcul vecteur : on vise le milieux de la case

        float x = (float)arrive.x + (float)size /2.0f;
        float z = (float)arrive.y + (float)size /2.0f;
        Vector3 deplacement = new Vector3(x - this.transform.position.x, 0, z - this.transform.position.z);

        //met a jour deplacement
        deplacement.Normalize();
        transform.Translate(deplacement.normalized * vitesse * Time.deltaTime);


        depart = new Noeud(true, Mathf.FloorToInt(this.transform.position.x), Mathf.FloorToInt(this.transform.position.z));
        // si arrivé sur case d'arrive, on supprime la case depart
        if (EstMilieuxCase(this.transform.position, depart, arrive))
        {
            
            if (PmActu <= 0)
                paActu -= size;
            else
                pmActu -= size;

            if (chemin.Count > 1)
                chemin.Remove(chemin[0]);
            else
                StopDeplacement();
        }
    }
}
