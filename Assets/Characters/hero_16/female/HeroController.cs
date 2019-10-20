using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeroController : MonoBehaviour
{
    private Animator animator;
    public int vie = 100;
    public int pm = 5;
    public int pmMax = 5;
    public int pa = 10;
    public int paMax = 5;

    public DoubleClick doubleCLick;

    public float vitesse = 4f;

    public bool deplacementEnCours = false;
    public Vector3 positionCible;
    public Vector3 positionDepart;
    public List<Noeud> chemin;
    private Noeud noeudActuel;

    // Use this for initialization
    void Start()
    {
        doubleCLick = new DoubleClick();
        animator = this.GetComponent<Animator>();
        vie = 100;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
#if !UNITY_EDITOR
        if (GameManage.DonnerInstance.Role != GameManage.ROLE.ROLE_JOUEUR)
            return;
#endif
        if (deplacementEnCours)
        {
            Deplacement();
            return;
        }

        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        bool shift = Input.GetKey(KeyCode.LeftShift);

        animator.SetInteger("vie", vie);
        if (vie <= 0)
            return;


        if (vertical != 0 || horizontal != 0)
        {
            Vector3 pos = this.transform.position;
            if (shift)
            {
                animator.SetInteger("state", 4);
                this.transform.Translate((-1 * vertical * 2 * Time.deltaTime), 0, (-1 * horizontal * 2 * Time.deltaTime));
            }
            else
            {
                this.transform.Translate(-1 * vertical * Time.deltaTime, 0, (-1 * horizontal * Time.deltaTime));
                animator.SetInteger("state", 1);
            }


        }
        else if (Input.GetButtonUp("Fire1"))
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

    }


    private void StopDeplacement()
    {
        animator.SetInteger("state", 0);
        deplacementEnCours = false;
        pm = pmMax;
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
        if (pm <= 0)
            StopDeplacement();

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
        pm = pm - (int)(Vector3.Distance(positionDepart,transform.position)/10);


        depart = new Noeud(true, Mathf.FloorToInt(this.transform.position.x), Mathf.FloorToInt(this.transform.position.z));
        // si arrivé sur case d'arrive, on supprime la case depart
        if (EstMilieuxCase(this.transform.position, depart, arrive))
        {
            if (chemin.Count > 1)
                chemin.Remove(chemin[0]);
            else
                StopDeplacement();
        }
    }
}
