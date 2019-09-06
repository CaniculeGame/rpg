using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWorld : MonoBehaviour
{
    public enum TYPE_OBJET : int
    {
        TYPE_OBJET_1 = 0,
        TYPE_OBJET_2 = 1,
        TYPE_OBJET_3 = 2,
        TYPE_OBJET_4 = 3,
        TYPE_OBJET_MAX
    }


    public GameObject plane;
    public GameObject[] decors;

    public int _x = 10;
    public int _y = 10;
    public int _h = 1;
    public int _w = 1;
    public int _l = 1;

    public bool montrerQuadrillage = true;
    public bool aChanger = false;
    private bool grilleEstAfficher = false;
    private int objSelection = -1;

    private PoolObjects grid;
    private PoolObjects[] decorsPool;

    public void ViderWorld()
    {
        if (grid != null)
            grid.SupprimerTousLesObjets("grid");
    }

    private void GenerateGrid()
    {

        if (GameManage.DonnerInstance.Carte == null)
            return;

        _x = (int)GameManage.DonnerInstance.Carte.Xmax;
        _y = (int)GameManage.DonnerInstance.Carte.Ymax;

        int origineX = -((_x) / 2);
        int origineY = -((_y) / 2);


        Carte carte = GameManage.DonnerInstance.Carte;
        for (int i = 0; i < _x; i++)
        {
            for (int j = 0; j < _y; j++)
            {
                if (carte.DonnerCellule(i, j) != null &&
                    carte.DonnerCellule(i, j).EstOccupe)
                {
                    int ht = carte.DonnerCellule(i, j).Hauteur;
                }
            }

        }

    }


    public int X { set { aChanger = true; _x = value; } get { return _x; } }
    public int Y { set { aChanger = true; _y = value; } get { return _y; } }
    public int H { set { aChanger = true; _h = value; } get { return _h; } }
    public int W { set { aChanger = true; _w = value; } get { return _w; } }
    public int L { set { aChanger = true; _l = value; } get { return _l; } }

    // Start is called before the first frame update
    void Start()
    { 
        GenerateGrid();

        if (montrerQuadrillage)
            ShowGrid(true);

        if (decors == null)
            return;

        if (decorsPool == null)
            decorsPool = new PoolObjects[decors.Length];


        for(int i = 0; i < decorsPool.Length; i++)
        {
            if (decorsPool[i] == null && decors != null && decors.Length >= i)
            {
                decorsPool[i] = new PoolObjects();
                decorsPool[i].SetGameObject = decors[i];
                decorsPool[i].SetParentGameObject = this.transform;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManage.DonnerInstance.Role == GameManage.ROLE.ROLE_AUCUN)
            return;

        if (GameManage.DonnerInstance.Role == GameManage.ROLE.ROLE_JOUEUR)
            AfficherGrid(false);

        if (GameManage.DonnerInstance.Action == GameManage.ACTION_TYPE.ACTION_TYPE_CHANGEMENT_CARTE)
        {
            aChanger = true;
            ViderWorld();
            SupprimerTerrain();
            NouveauTerrain();
            GameManage.DonnerInstance.Action = GameManage.ACTION_TYPE.ACTION_TYPE_AUCUN;
        }

        if (aChanger)
        {
            GenerateGrid();
            aChanger = false;
        }


        if (GameManage.DonnerInstance.Mode == GameManage.MODE.MODE_CONSTRUCTION)
        {
            if (!grilleEstAfficher)
                ShowGrid(true);

            Carte carte = GameManage.DonnerInstance.Carte;
            if (Input.GetMouseButtonUp(0) || Input.touchCount > 0)
            {
                Vector3 position;

#if UNITY_ANDROID
            position = Input.touches[0].position;
#else
                position = Input.mousePosition;
#endif

                if (objSelection != -1)
                {
                    Transform obj = SelectObjet(position);
                    if (obj != null && obj.tag == "grid")
                    {
                        Vector3 newPos = obj.position;
                        InstanciateObjet((TYPE_OBJET)objSelection, newPos);

                        int x = Mathf.FloorToInt(newPos.x);
                        int y = Mathf.FloorToInt(newPos.z);
                        carte.DonnerCellule(x, y).AjouterElement(new ElementGeneric(ElementGeneric.TYPE_ELEMENT.TYPE_ELEMENT_OBJET, (uint)objSelection));
                    }
                }

            }
            else if (Input.GetMouseButton(1))
            {
                Vector3 position = Input.mousePosition;
                Transform obj = SelectObjet(position);
                if (obj != null && obj.tag == "decor")
                {
                    DetruireObjet(obj);
                }
            }
        }

    }

    private void DetruireObjet(Transform obj)
    {
        if (obj == null)
            return;

        ElementGeneric elt = obj.GetComponent<ElementGeneric>();
        if (elt == null)
            return;

        int id = (int)elt.DonnerIdElement;
        ElementGeneric.TYPE_ELEMENT type = ElementGeneric.TYPE_ELEMENT.TYPE_ELEMENT_OBJET;

        switch (type)
        {
            case ElementGeneric.TYPE_ELEMENT.TYPE_ELEMENT_OBJET:
                if (id >= 0 && id < decorsPool.Length)
                    decorsPool[id].SupprimerObject(obj.gameObject);
            break;

            default:
                break;
        }
    }

    private void SupprimerTerrain()
    {
        GameObject.Destroy(GameObject.Find("Terrain"));
    }

    private void NouveauTerrain()
    {
        Carte carte = GameManage.DonnerInstance.Carte;
        if (carte == null)
            return;

        Vector3 position = new Vector3(0, 0, 0);
        TerrainData terrainData = new TerrainData();
        terrainData.size = new Vector3(carte.Xmax, 100, carte.Ymax);
        GameObject terrain = Terrain.CreateTerrainGameObject(terrainData);
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
        int mult = 1;

        if (grid == null)
        {
            grid = new PoolObjects();
            grid.SetGameObject = plane;
            grid.SetParentGameObject = this.transform;
        }

        grilleEstAfficher = afficher;

        if (afficher)
        {
            float origineX = 0.5f;
            float origineY = 0.5f;

            for (int i = 0; i < _x * mult; i++)
            {
                for (int j = 0; j < _y * mult; j++)
                {
                        grid.CreerObject(new Vector3(origineX + i * _h, 0.1f, origineY + j * _w), Quaternion.identity);
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

        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 200))
            obj = hit.transform;


        return obj;
    }


    public void SelectionnerObjet(int idObj)
    {
        objSelection = idObj;
    }


    public void DeselectionnerObjet()
    {
        objSelection = -1;
    }



    public void InstanciateObjet(TYPE_OBJET typeObj,  Vector3 position)
    {
        if (decorsPool == null)
            return;

        if ((int)typeObj >= decorsPool.Length)
            return;

        decorsPool[(int)typeObj].CreerObject(position, Quaternion.identity);
    }

}
