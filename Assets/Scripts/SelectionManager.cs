using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public GameObject selectionne;

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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag != "ground" && hit.transform.gameObject.tag != "grid")
                    selectionne = hit.transform.gameObject;
                else
                    selectionne = null;


                if ( selectionne != null && doubleClick.DoubleClic())
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
