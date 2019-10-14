using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public GameObject selectionne;

    public Vector3 positionCible;
    public Vector3 positionDepart;
    public List<Noeud> chemin;

    // Start is called before the first frame update
    void Start()
    {
        selectionne = null;
    }

    // Update is called once per frame
    void Update()
    {
        List<Noeud> chemin;
        if (Input.GetButtonUp("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
              //  if (hit.transform.gameObject.tag != "ground" && hit.transform.gameObject.tag != "grid")
              //  {
                    selectionne = hit.transform.gameObject;
                    positionCible = hit.point;
              //  }

                Noeud depart = new Noeud(true, Mathf.FloorToInt(5), Mathf.FloorToInt(2));
                Noeud arrive = new Noeud(true, Mathf.FloorToInt(positionCible.x), Mathf.FloorToInt(positionCible.z));
                //Debug.Log("Depart : " + depart.x + "," + depart.y + "   arrive :" + arrive.x + "," + arrive.y);

                chemin = Astar.FindPath(GameManage.DonnerInstance.Carte, depart, arrive, GameManage.DonnerInstance.Diagonale);
            }
        }

    }

    public GameObject GetSelection()
    {
        return selectionne;
    }
}
