using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCOM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = transform.GetChild(0).transform.localPosition * transform.localScale.z;
    }
}
