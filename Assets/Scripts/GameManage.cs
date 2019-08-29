using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage
{

    public enum MODE : int
    {
        MODE_AUCUN  = 0,
        MODE_NORMAL = 1,
        MODE_CONSTRUCTION = 2,
        MODE_MAX = 3
    }

    public enum ROLE : int
    {
        ROLE_AUCUN = 0,
        ROLE_JOUEUR = 1,
        ROLE_MJ = 2,
        ROLE_MAX = 3
    }

    public enum CAMERA_TYPE : int
    {
        CAMERA_TYPE_AUCUN = 0,
        CAMERA_TYPE_ORTHO = 1,
        CAMERA_TYPE_ISO = 2,
        CAMERA_TYPE_MAX = 3
    }

    public enum ACTION_TYPE : int
    {
        ACTION_TYPE_AUCUN = 0,
        ACTION_TYPE_CHANGEMENT_CARTE = 1,
        ACTION_TYPE_SAUVEGARDER_CARTE = 2
    }



    /* notre unique instance */
    private static GameManage instance = null;
    private MODE mode = MODE.MODE_AUCUN;
    private ROLE roleJoueur = ROLE.ROLE_AUCUN;
    private CAMERA_TYPE cameraType = CAMERA_TYPE.CAMERA_TYPE_AUCUN;
    private ACTION_TYPE actionType = ACTION_TYPE.ACTION_TYPE_AUCUN;
    private Carte carte = null;



    /* constructeur */
    private GameManage()
    {
        /* creer valeurs à partager */
        Initialiser();
    }


    /* recuperation des valeurs partagees*/
    public static GameManage DonnerInstance
    {
        get
        {
            if (instance == null)
                instance = new GameManage();

            return instance;
        }
    }

    private void Initialiser()
    {

    }

    public ROLE Role
    {
        get { return roleJoueur; }
        set { roleJoueur = value; }
    }

    public MODE Mode
    {
        get { return mode; }
        set { mode = value; }
    }

    public CAMERA_TYPE CameraType
    {
        get { return cameraType; }
        set { cameraType = value; }
    }

    public ACTION_TYPE Action
    {
        get { return actionType; }
        set { actionType = value; }
    }

    public Carte Carte
    {
        get { return carte; }
        set { carte = value; }
    }

    public void ChargerCarte(string path)
    {
        carte = new Carte(path);
        actionType = ACTION_TYPE.ACTION_TYPE_CHANGEMENT_CARTE;
    }

    public void SavegarderCarte(string path)
    {
        carte.SavegarderFichierCarte(path);
    }

    public void NouvelleCarte(Carte crte)
    {

        carte = null;
        carte = crte;
    }


}
