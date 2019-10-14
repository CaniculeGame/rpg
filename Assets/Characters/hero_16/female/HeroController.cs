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

    float vitesse = 4f;

    public bool deplacementEnCours = false;
    public Vector3 positionCible;
    public Vector3 positionDepart;
    public List<Noeud> chemin;

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
        if (GameManage.DonnerInstance.Role != GameManage.ROLE.ROLE_JOUEUR)
            return;

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
                        positionCible = hit.point - transform.position;

                    Noeud depart = new Noeud(true, Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.z));
                    Noeud arrive = new Noeud(true, Mathf.FloorToInt(positionCible.z), Mathf.FloorToInt(positionCible.z));

                    if (arrive.x < 0)
                        arrive.x = 0;
                    if (arrive.y < 0)
                        arrive.y = 0;

                    Carte carte = GameManage.DonnerInstance.Carte;
                    if (carte != null)
                        chemin = Astar.FindPath(carte, depart, arrive, GameManage.DonnerInstance.Diagonale);
                }

                deplacementEnCours = true;
                transform.LookAt(positionCible);
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


    public void  Deplacement()
    {
        if (!deplacementEnCours)
        {
            deplacementEnCours = true;
            Noeud depart = new Noeud(true,Mathf.FloorToInt(positionCible.x), Mathf.FloorToInt(positionCible.y));
            Noeud arrive = new Noeud(true,Mathf.FloorToInt(positionCible.x), Mathf.FloorToInt(positionCible.y));
            chemin = Astar.FindPath(GameManage.DonnerInstance.Carte, depart, arrive, GameManage.DonnerInstance.Diagonale);
        }

        animator.SetInteger("state", 1);
        if (positionCible == transform.position || pm <= 0)
        {
            deplacementEnCours = false;
            animator.SetInteger("state", 0);
            pm = pmMax;
            return;
        }

        // calcul vecteur
        transform.Translate(Vector3.forward * vitesse * Time.deltaTime);
        pm = pm - (int)(Vector3.Distance(positionDepart,transform.position)/10);

    }
}
