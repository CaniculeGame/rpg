using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public GameObject selectionne;



    // Start is called before the first frame update
    void Start()
    {
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
                if(hit.transform.gameObject.tag != "ground" && hit.transform.gameObject.tag != "grid")
                    selectionne = hit.transform.gameObject;
            }
        }

    }

    public GameObject GetSelection()
    {
        return selectionne;
    }
}
