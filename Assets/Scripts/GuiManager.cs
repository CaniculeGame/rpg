using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager : MonoBehaviour
{
    public GameObject mjGui;
    public GameObject jGui;
    public GameObject coGui;
    public GameObject createGui;

    public Camera[] cameras;


    public void Start()
    {
        mjGui.gameObject.SetActive(false);
        jGui.gameObject.SetActive(false);
        coGui.gameObject.SetActive(true);
    }

    public void StartJoueur()
    {
        mjGui.gameObject.SetActive(false);
        jGui.gameObject.SetActive(true);
        coGui.gameObject.SetActive(false);

        GameManage.DonnerInstance.Mode = GameManage.MODE.MODE_NORMAL;
        GameManage.DonnerInstance.Role = GameManage.ROLE.ROLE_JOUEUR;
    }

    public void StartMj()
    {
        mjGui.gameObject.SetActive(true);
        jGui.gameObject.SetActive(false);
        coGui.gameObject.SetActive(false);

        GameManage.DonnerInstance.Mode = GameManage.MODE.MODE_NORMAL;
        GameManage.DonnerInstance.Role = GameManage.ROLE.ROLE_MJ;

        mjGui.transform.GetChild(1).gameObject.SetActive(true);
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
        coGui.gameObject.SetActive(true);

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
}
