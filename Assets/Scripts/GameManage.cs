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



    /* notre unique instance */
    private static GameManage instance = null;
    private MODE mode = MODE.MODE_AUCUN;
    private ROLE roleJoueur = ROLE.ROLE_AUCUN;
    private CAMERA_TYPE cameraType = CAMERA_TYPE.CAMERA_TYPE_AUCUN;

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

}
