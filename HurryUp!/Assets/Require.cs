using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Require : MonoBehaviour
{
    public bool isReset = false;
    private void OnValidate()
    {
        if (isReset)
        {
            return;
        }
        isReset = true;
        //transform.GetChild(0).SetParent(transform.GetChild(1));
        transform.GetComponent<triggerPlayerFire>().open = transform.GetChild(0).GetComponent<MMSquashAndStretch>();
        //transform.GetChild(1).position = transform.GetChild(0).transform.position;
        //   transform.GetChild(0).GetChild(0).SetParent(transform.GetChild(1).transform);
        //transform.GetChild(1).rotation = transform.GetChild(0).transform.rotation;
        //transform.GetChild(1).localScale = transform.GetChild(0).transform.localScale;
        //transform.GetComponent<triggerPlayerFire>().open=transform.GetChild(0).GetChild(0).GetComponent<MMSquashAndStretch>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
