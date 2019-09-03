using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWorld : MonoBehaviour
{
    public GameObject genericGround;
    public GameObject plane;

    public int x = 10;
    public int y = 10;
    public int h = 1;
    public int w = 1;
    public int l = 1;

    public bool montrerQuadrillage = true;
    public bool aChanger = false;
    private bool grilleEstAfficher = false;

    private PoolObjects solGenerique;
    private PoolObjects grid;

    public void ViderWorld()
    {
        if(solGenerique != null )
            solGenerique.SupprimerTousLesObjets("ground");

        if (grid != null)
            grid.SupprimerTousLesObjets("grid");
    }

    private void GenerateGrid()
    {
        if (genericGround == null)
            return;

        if (GameManage.DonnerInstance.Carte == null)
            return;

        x = (int)GameManage.DonnerInstance.Carte.Xmax;
        y = (int)GameManage.DonnerInstance.Carte.Ymax;

        int origineX = -((x) / 2);
        int origineY = -((y) / 2);

        if (solGenerique == null)
        {
            solGenerique = new PoolObjects();
            solGenerique.SetGameObject = genericGround;
            solGenerique.SetParentGameObject = this.transform;
        }


        Carte carte = GameManage.DonnerInstance.Carte;
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                if (carte.DonnerCellule(i, j) != null &&
                    carte.DonnerCellule(i, j).EstOccupe)
                {
                    int ht = carte.DonnerCellule(i, j).Hauteur;
                    for(int a = 0; a <= ht; a++) 
                        solGenerique.CreerObject(new Vector3(origineX + i * h, a, origineY + j * w), Quaternion.identity);
                }
            }

        }

    }

    public GameObject Objet { set { aChanger = true; genericGround = value; } get { return genericGround; } }
    public int X { set { aChanger = true; x = value; } get { return x; } }
    public int Y { set { aChanger = true; y = value; } get { return y; } }
    public int H { set { aChanger = true; h = value; } get { return h; } }
    public int W { set { aChanger = true; w = value; } get { return w; } }
    public int L { set { aChanger = true; l = value; } get { return l; } }

    // Start is called before the first frame update
    void Start()
    { 
        GenerateGrid();

        if (montrerQuadrillage)
            ShowGrid(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManage.DonnerInstance.Role == GameManage.ROLE.ROLE_AUCUN)
            return;

        if (GameManage.DonnerInstance.Role == GameManage.ROLE.ROLE_JOUEUR)
            AfficherGrid(false);

        if (GameManage.DonnerInstance.Action == GameManage.ACTION_TYPE.ACTION_TYPE_CHANGEMENT_CARTE)
        { aChanger = true; ViderWorld(); GameManage.DonnerInstance.Action = GameManage.ACTION_TYPE.ACTION_TYPE_AUCUN; }

        if (aChanger)
        {
            GenerateGrid();
            aChanger = false;
        }


        if (GameManage.DonnerInstance.Mode == GameManage.MODE.MODE_CONSTRUCTION )
        {
            Carte carte = GameManage.DonnerInstance.Carte;
            if (Input.GetMouseButtonUp(0) || Input.touchCount > 0)
            {
                Vector3 position;

#if UNITY_ANDROID
            position = Input.touches[0].position;
#else
                position = Input.mousePosition;
#endif
                int i = 0;
                int j = 0;
                Transform obj = SelectObjet(position);
                if (obj != null && obj.tag == "grid")
                {
                    Vector3 newPos = obj.position;
                    newPos.y = 0;
                    solGenerique.CreerObject(newPos, Quaternion.identity);
                    grid.SupprimerObject(obj.gameObject);
                }
                else if (obj != null && obj.tag == "ground")
                {
                    Vector3 newPos = obj.position;
                    i = (int)Mathf.Abs(newPos.x);
                    j = (int)Mathf.Abs(newPos.z);
                   
                    
                    newPos.y = carte.DonnerCellule(i,j).Hauteur;
                    carte.DonnerCellule(i,j).Hauteur++;
                    solGenerique.CreerObject(newPos, Quaternion.identity);
                }

            }
            else if (Input.GetMouseButton(1))
            {
                Vector3 position = Input.mousePosition;
                Transform obj = SelectObjet(position);
                if (obj != null && obj.tag == "ground")
                {
                    Vector3 newPos = obj.position;
                    newPos.y = -0.5f;
                    grid.CreerObject(newPos, Quaternion.identity);

                    // A faire: si en dessous il y a autre chose qu'un cube
                    solGenerique.SupprimerObject(obj.gameObject);
                }
            }
        }

    }

    public void AfficherGrid(bool afficher)
    {
        ShowGrid(afficher);
    }

    public void SwitchGrid()
    {
        if (grilleEstAfficher)
            ShowGrid(false);
        else
            ShowGrid(true);
    }

    private void ShowGrid(bool afficher)
    {
        int mult = 2;

        if (grid == null)
        {
            grid = new PoolObjects();
            grid.SetGameObject = plane;
            grid.SetParentGameObject = this.transform;
        }

        grilleEstAfficher = afficher;

        if (afficher)
        {
            int origineX = -((x * mult) / 2);
            int origineY = -((y * mult) / 2);

            for (int i = 0; i < x * mult; i++)
            {
                for (int j = 0; j < y * mult; j++)
                {
                        grid.CreerObject(new Vector3(origineX + i * h, -w/2.0f, origineY + j * w), Quaternion.identity);
                }
            }
        }
        else
        {
            for(int i = 0; i <  this.transform.childCount; i++)
            {
                if (this.transform.GetChild(i).tag == "grid")
                    grid.SupprimerObject(this.transform.GetChild(i).gameObject);
            }
        }
    }



    public Transform SelectObjet(Vector3 position)
    {
        Transform obj = null;

        // Did we hit the surface?
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
            obj = hit.transform;


        return obj;
    }



    public void CreerBlock()
    {
        if (solGenerique == null)
            return;

        solGenerique.CreerObject(Vector3.zero, Quaternion.identity);

    }

    public void SupprimerBlock()
    {
        if (solGenerique == null)
            return;
    }

}
