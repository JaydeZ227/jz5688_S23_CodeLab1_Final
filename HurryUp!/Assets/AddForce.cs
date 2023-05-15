using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Vector3 force;
    private void OnMouseDown()
    {
        GetComponent<Rigidbody>().AddForce(force);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
