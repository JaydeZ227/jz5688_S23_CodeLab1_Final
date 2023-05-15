using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSpaceSetFire : MonoBehaviour
{
    List<triggerPlayerFire> playerFireList=new List<triggerPlayerFire>();
    private void OnTriggerEnter(Collider collision)
    {
       // Debug.Log(collision.tag);
        if (collision.tag=="Car")
        {
            triggerPlayerFire  triggerFire= collision.GetComponent<triggerPlayerFire>();
            if (!playerFireList.Contains(triggerFire))
            {
                playerFireList.Add(triggerFire);
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Car")
        {
            triggerPlayerFire triggerFire = collision.GetComponent<triggerPlayerFire>();
            if (playerFireList.Contains(triggerFire))
            {
                playerFireList.Remove(triggerFire);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      //  Debug.Log("xwadwadsada"+playerFireList.Count);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerFireList.Count>0)
            {
                for (int i = 0; i < playerFireList.Count; i++)
                {
                    if (!playerFireList[i].isFire)
                    {
                        playerFireList[i].SetFire();
                    }
                }
            }
        }
    }
}
