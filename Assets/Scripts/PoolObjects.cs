using System.Collections.Generic;
using UnityEngine;


/*********************************************
 * 
 * - a utiliser pour chaque groupe d'objet qui n'est pas unique
 * - mettre le script dans un gameobject vide
 * 
 * ******************************************/

public class PoolObjects
{
    private GameObject objet;
    private Transform parent;
    private Stack<GameObject> stackObj;


    // appeler cette fonction quand l'obj n'est plus utilisé
    public void SupprimerObject(GameObject obj)
    {
        if (obj == null ||stackObj == null)
            return;

        //desactive obj
        obj.SetActive(false);
        // mettre obj dans piscine
        stackObj.Push(obj);
    }


    // appeler cette fonction pour creer un objet
    public GameObject CreerObject(Vector3 position, Quaternion rotation)
    {
        GameObject obj;

        if (stackObj == null)
            stackObj = new Stack<GameObject>();

        // si pile est vide, alors on instancie un objet
        if (stackObj.Count <= 0)
        {
            obj = GameObject.Instantiate(objet, position, rotation);
            obj.transform.parent = parent;
            return obj;
        }
        else
        {
            obj = stackObj.Pop();
            if (obj != null)
            {
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
                return obj;
            }
        }

        return null;
    }


    public GameObject SetGameObject
    {
        set { objet = value; }
    }

    public Transform SetParentGameObject
    {
        set { parent = value; }
    }
}
