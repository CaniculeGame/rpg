﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    public GameObject mjGui;
    public GameObject jGui;
    public GameObject mainGui;
    public GameObject connectionGui;
    public GameObject choixPersoGui;
    public GameObject creationMJGui;
    public GameObject creationJoueurGui;
    public GameObject newCarteGui;

    public Camera[] cameras;


    public void Start()
    {
        mjGui.gameObject.SetActive(false);
        jGui.gameObject.SetActive(false);
        mainGui.gameObject.SetActive(true);
        newCarteGui.gameObject.SetActive(false);

        choixPersoGui.gameObject.SetActive(false);
        creationMJGui.gameObject.SetActive(false);
        creationJoueurGui.gameObject.SetActive(false);
        connectionGui.gameObject.SetActive(true);
    }

    public void StartJoueur()
    {
        mjGui.gameObject.SetActive(false);
        jGui.gameObject.SetActive(true);
        mainGui.gameObject.SetActive(false);

        choixPersoGui.gameObject.SetActive(false);
        creationMJGui.gameObject.SetActive(false);
        creationJoueurGui.gameObject.SetActive(false);
        connectionGui.gameObject.SetActive(false);

        GameManage.DonnerInstance.Mode = GameManage.MODE.MODE_NORMAL;
        GameManage.DonnerInstance.Role = GameManage.ROLE.ROLE_JOUEUR;
    }

    public void StartMj()
    {
        mjGui.gameObject.SetActive(true);
        jGui.gameObject.SetActive(false);
        mainGui.gameObject.SetActive(false);
        newCarteGui.gameObject.SetActive(false);

        choixPersoGui.gameObject.SetActive(false);
        creationMJGui.gameObject.SetActive(false);
        creationJoueurGui.gameObject.SetActive(false);
        connectionGui.gameObject.SetActive(false);

        GameManage.DonnerInstance.Mode = GameManage.MODE.MODE_NORMAL;
        GameManage.DonnerInstance.Role = GameManage.ROLE.ROLE_MJ;

        mjGui.transform.GetChild(1).gameObject.SetActive(true);


        string path;

#if UNITY_ANDROID
        path =;
#else
        path = Application.dataPath + "/Resources/Cartes/Carte1.txt";
#endif

        GameManage.DonnerInstance.ChargerCarte(path);
    }

    public void SaveCarte()
    {
        GameManage.DonnerInstance.SavegarderCarte(null);
    }

    public void ConstructionMode()
    {
        if (GameManage.DonnerInstance.Role != GameManage.ROLE.ROLE_MJ)
            return;

        if (GameManage.DonnerInstance.Mode == GameManage.MODE.MODE_CONSTRUCTION)
        {
            for(int i =0; i < mjGui.transform.childCount;i++)
                mjGui.transform.GetChild(i).gameObject.SetActive(false);

            mjGui.transform.GetChild(1).gameObject.SetActive(true);
            GameManage.DonnerInstance.Mode = GameManage.MODE.MODE_NORMAL;
        }
        else
        {
            for (int i = 0; i < mjGui.transform.childCount; i++)
                mjGui.transform.GetChild(i).gameObject.SetActive(false);

            mjGui.transform.GetChild(0).gameObject.SetActive(true);
            mjGui.transform.GetChild(1).gameObject.SetActive(true);
            GameManage.DonnerInstance.Mode = GameManage.MODE.MODE_CONSTRUCTION;
        }
    }

    public void QuitterApp()
    {
        Application.Quit();
    }

    public void SwitchCamera()
    {

        if (cameras[0].isActiveAndEnabled)
        {
            cameras[0].gameObject.SetActive(false);
            cameras[1].gameObject.SetActive(true);

            GameManage.DonnerInstance.CameraType = GameManage.CAMERA_TYPE.CAMERA_TYPE_ORTHO;
        }
        else
        {
            cameras[0].gameObject.SetActive(true);
            cameras[1].gameObject.SetActive(false);

            GameManage.DonnerInstance.CameraType = GameManage.CAMERA_TYPE.CAMERA_TYPE_ISO;
        }
    }


    public void MainMenu()
    {
        mjGui.gameObject.SetActive(false);
        jGui.gameObject.SetActive(false);
        mainGui.gameObject.SetActive(true);
        newCarteGui.gameObject.SetActive(false);

        choixPersoGui.gameObject.SetActive(false);
        creationMJGui.gameObject.SetActive(false);
        creationJoueurGui.gameObject.SetActive(false);
        connectionGui.gameObject.SetActive(true);

        GameManage.DonnerInstance.Mode = GameManage.MODE.MODE_AUCUN;
        GameManage.DonnerInstance.Role = GameManage.ROLE.ROLE_AUCUN;
    }


    public void AfficherFichePerso()
    {
        jGui.transform.GetChild(1).gameObject.SetActive(true);
        jGui.transform.GetChild(0).gameObject.SetActive(false);


    }

    public void CacherFichePerso()
    {
        jGui.transform.GetChild(1).gameObject.SetActive(false);
        jGui.transform.GetChild(0).gameObject.SetActive(true);
    }


    public void AfficherCreerCarte()
    {
        mjGui.gameObject.SetActive(false);
        jGui.gameObject.SetActive(false);
        mainGui.gameObject.SetActive(false);
        newCarteGui.gameObject.SetActive(true);
    }

    public void CreerCarte()
    {
        mjGui.gameObject.SetActive(true);
        jGui.gameObject.SetActive(false);
        mainGui.gameObject.SetActive(false);
        newCarteGui.gameObject.SetActive(false);

        uint x = uint.Parse(newCarteGui.transform.GetChild(3).GetChild(2).GetComponent<Text>().text);
        uint y = uint.Parse(newCarteGui.transform.GetChild(4).GetChild(2).GetComponent<Text>().text);
        string nom = newCarteGui.transform.GetChild(5).GetChild(2).GetComponent<Text>().text;

        Carte newCarte = new Carte(null,nom,x, y);
        GameManage.DonnerInstance.NouvelleCarte(newCarte);

    }

    public void CacherCreerCarte()
    {
        mjGui.gameObject.SetActive(true);
        jGui.gameObject.SetActive(false);
        mainGui.gameObject.SetActive(false);
        newCarteGui.gameObject.SetActive(false);
    }


    public void AfficherConnection()
    {
        mjGui.gameObject.SetActive(false);
        jGui.gameObject.SetActive(false);
        mainGui.gameObject.SetActive(true);
        newCarteGui.gameObject.SetActive(false);

        choixPersoGui.gameObject.SetActive(false);
        creationMJGui.gameObject.SetActive(false);
        creationJoueurGui.gameObject.SetActive(false);
        connectionGui.gameObject.SetActive(true);
    }

    public void CacherConnection()
    {

    }


    public void CacherCreationMj()
    {

    }

    public void AfficherCreationMj()
    {
        mjGui.gameObject.SetActive(false);
        jGui.gameObject.SetActive(false);
        mainGui.gameObject.SetActive(true);
        newCarteGui.gameObject.SetActive(false);

        choixPersoGui.gameObject.SetActive(false);
        creationMJGui.gameObject.SetActive(true);
        creationJoueurGui.gameObject.SetActive(false);
        connectionGui.gameObject.SetActive(false);
    }

    public void CacherCreationJoueur()
    {

    }

    public void AfficherCreationJoueur()
    {
        mjGui.gameObject.SetActive(false);
        jGui.gameObject.SetActive(false);
        mainGui.gameObject.SetActive(true);
        newCarteGui.gameObject.SetActive(false);

        choixPersoGui.gameObject.SetActive(false);
        creationMJGui.gameObject.SetActive(false);
        creationJoueurGui.gameObject.SetActive(true);
        connectionGui.gameObject.SetActive(false);
    }


    public void CacherChoixPersonnage()
    {

    }

    public void AfficherChoixPersonnage()
    {
        mjGui.gameObject.SetActive(false);
        jGui.gameObject.SetActive(false);
        mainGui.gameObject.SetActive(true);
        newCarteGui.gameObject.SetActive(false);

        choixPersoGui.gameObject.SetActive(true);
        creationMJGui.gameObject.SetActive(false);
        creationJoueurGui.gameObject.SetActive(false);
        connectionGui.gameObject.SetActive(false);
    }


    public void Connection()
    {

        AfficherChoixPersonnage();
    }

    public void Deconnection()
    {

        AfficherConnection();
    }





}
