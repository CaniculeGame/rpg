﻿using System;
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
    public TerrainLayer[] terrainLayers;
    public Texture2D[] terrainTextures;

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
            AfficherGrid(true);

        if (GameManage.DonnerInstance.Action == GameManage.ACTION_TYPE.ACTION_TYPE_CHANGEMENT_CARTE)
        {
            aChanger = true;
#if UNITY_EDITOR
            GameManage gm = GameManage.DonnerInstance;
#endif
            ViderWorld();
            SupprimerTerrain();
            NouveauTerrain();
            AfficherGrid(true);
            GameManage.DonnerInstance.Action = GameManage.ACTION_TYPE.ACTION_TYPE_AUCUN;

        }

        if (aChanger)
        {
            GenerateGrid();
            aChanger = false;
        }


        if (GameManage.DonnerInstance.Mode == GameManage.MODE.MODE_CONSTRUCTION)
        {
            AfficherGrid(true);

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
                    Transform obj = GuiManager.SelectObjet(position);
                    if (obj != null && obj.tag == "grid")
                    {
                        Vector3 newPos = obj.position;
                        GameObject go = InstanciateObjet((TYPE_OBJET)objSelection, newPos);

                        int x = Mathf.FloorToInt(newPos.x);
                        int y = Mathf.FloorToInt(newPos.z);


                        ElementGeneric elt = go.GetComponent<ElementGeneric>();
                        carte.DonnerCellule(x, y).AjouterElement(elt);
                    }
                }
            }
            else if (Input.GetMouseButton(1))
            {
                Vector3 position = Input.mousePosition;
                Transform obj = GuiManager.SelectObjet(position);
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
                {
                    GameManage.DonnerInstance.Carte.DonnerCellule((int)obj.position.x, (int)obj.position.z).EstOccupe = false;

                    /* decondanation des cellules occupées par le mesh - la condanation se fait par collider*/
                    int h = elt.height;
                    int w = elt.widh;

                    int i = 0;
                    int j = 0;
                    for (i = 0; i < w; i++)
                    {
                        for (j = 0; j < h; j++)
                        {
                            if(GameManage.DonnerInstance.Carte.DonnerCellule((int)obj.position.x + i - (w / 2), (int)obj.position.z + j - (h / 2)) != null)
                                GameManage.DonnerInstance.Carte.DonnerCellule((int)obj.position.x + i - (w / 2), (int)obj.position.z + j - (h / 2)).EstOccupe = false;
                        }
                    }


                    decorsPool[id].SupprimerObject(obj.gameObject);
                }
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


        /* choix texture principale */
        terrainData.terrainLayers = terrainLayers;
        float[,,] map = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainLayers.Length];
        for (int y = 0; y < terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < terrainData.alphamapWidth; x++)
            {
                for(int i = 0; i < terrainLayers.Length; i++)
                {
                    if(i == (int)carte.Terrain-1)
                        map[x, y, i] = 1;
                    else
                        map[x, y, i] = 0;
                }
            }
        }
        terrainData.SetAlphamaps(0,0, map);

    }

    public void AfficherGrid(bool afficher)
    {
        if (afficher == false && grilleEstAfficher)
            ShowGrid(false);
        else if(afficher == true && !grilleEstAfficher)
            ShowGrid(true);
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
                    GameObject go = grid.CreerObject(new Vector3(origineX + i * _h, 0.1f, origineY + j * _w), Quaternion.identity);
                 /*   if(go != null)
                    {
                        if (GameManage.DonnerInstance.Carte.DonnerCellule(i, j).EstOccupe)
                            go.GetComponent<Renderer>().material = materialPlane[1];
                        else
                            go.GetComponent<Renderer>().material = materialPlane[0];
                    }*/
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





    public void SelectionnerObjet(int idObj)
    {
        objSelection = idObj;
    }


    public void DeselectionnerObjet()
    {
        objSelection = -1;
    }



    public GameObject InstanciateObjet(TYPE_OBJET typeObj,  Vector3 position)
    {
        if (decorsPool == null)
            return null;

        if ((int)typeObj >= decorsPool.Length)
            return null;


        GameObject go = decorsPool[(int)typeObj].CreerObject(position, Quaternion.identity);

        /* pansement */
        if(typeObj != TYPE_OBJET.TYPE_OBJET_2)
            go.transform.Rotate(new Vector3(270, 0, 0));

        return go;
    }

}
