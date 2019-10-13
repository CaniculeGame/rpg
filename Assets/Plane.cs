using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public Material[] materialPlane;


    private void LateUpdate()
    {
         if (GameManage.DonnerInstance.Carte.DonnerCellule((int)transform.position.x, (int)transform.position.y) != null)
            if ( GameManage.DonnerInstance.Carte.DonnerCellule((int)transform.position.x, (int)transform.position.y).EstOccupe == false)
                GetComponent<Renderer>().material = materialPlane[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "decor")
        {
            this.GetComponent<Renderer>().material = materialPlane[1];

            if (GameManage.DonnerInstance.Carte.DonnerCellule((int)this.transform.position.x, (int)this.transform.position.y) != null)
                GameManage.DonnerInstance.Carte.DonnerCellule((int)this.transform.position.x, (int)this.transform.position.y).EstOccupe = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "decor")
        {
            this.GetComponent<Renderer>().material = materialPlane[0];
            if (GameManage.DonnerInstance.Carte.DonnerCellule((int)this.transform.position.x, (int)this.transform.position.y) != null)
                GameManage.DonnerInstance.Carte.DonnerCellule((int)this.transform.position.x, (int)this.transform.position.y).EstOccupe = false;
        }
    }
}

/*

                        if(GameManage.DonnerInstance.Carte.DonnerCellule(x, y) != null)
                            if (GameManage.DonnerInstance.Carte.DonnerCellule(x, y).EstOccupe)
                                obj.GetComponent<Renderer>().material = materialPlane[1];
                            else
                                obj.GetComponent<Renderer>().material = materialPlane[0];

                        int h = elt.height;
int w = elt.widh;

int i = 0;
int j = 0;
                        for (i = 0; i<w; i++)
                        {
                            for (j = 0; j<h; j++)
                            {
                                carte.DonnerCellule(x+i-(h/2), y+j-(w/2)).EstOccupe = true;
                            }
                        }
                        if (i != 0)
                        {
                            ShowGrid(false);
ShowGrid(true);
                        }
*/