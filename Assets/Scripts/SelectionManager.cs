using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public GameObject selectionne;
    public GameObject guiManager;

    public Vector3 positionCible;
    public Vector3 positionDepart;
    public List<Noeud> chemin;

    private DoubleClick doubleClick;

    // Start is called before the first frame update
    void Start()
    {
        doubleClick = new DoubleClick();
        selectionne = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            Transform hit = GuiManager.SelectObjet(Input.mousePosition);
            if (hit != null)
            {
                if (hit.gameObject.tag != "ground" && hit.gameObject.tag != "grid")
                    selectionne = hit.gameObject;
                else
                    selectionne = null;

                if (guiManager == null)
                    return;

                if (selectionne != null && GameManage.DonnerInstance.Mode != GameManage.MODE.MODE_CONSTRUCTION)
                    guiManager.GetComponent<GuiManager>().AfficherUiObjet(hit.gameObject);
                else
                    guiManager.GetComponent<GuiManager>().CacherUiObjet();


                Debug.Log(Mathf.FloorToInt(hit.transform.position.x) + "  " + Mathf.FloorToInt(hit.transform.position.z));

                if (selectionne != null && doubleClick.DoubleClic())
                {

                }

            }
        }

    }

    public GameObject GetSelection()
    {
        return selectionne;
    }
}
