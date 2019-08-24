using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    public new Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        if(collider != null)
            Physics.IgnoreCollision(this.GetComponent<Collider>(), collider);
    }
}
