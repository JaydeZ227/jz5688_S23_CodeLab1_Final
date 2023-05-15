using HurryUp;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerPlayerFire : MonoBehaviour
{
    public MMSquashAndStretch open;
    bool isTrigger = false;
    public Vector3 dir;
    public float fireSpeed = 10;
   // public DeformationToucher touch;
   public  bool isFire = false;
   public  void SetFire() 
    {
        if (isTrigger)
        {
            return;
        }
        if (isFire )
        {
            return;
        }
        Debug.Log("���÷���");
        isFire = true;
        open.enabled = true;
        float x = FindObjectOfType<PlayerBike_XiaoYuan>().transform.position.z - transform.position.z;
        dir = new Vector3(0, 1f, -x).normalized;
        // touch.TriggerThis(collision.transform.position) ;
        BikeGameManager.instance.TriggerTimeStop();
        if (GetComponent<Car>())
        {
            GetComponent<Car>().isMove = false;

        }
        if (GetComponent<TrafficLightCar>())
        {
            GetComponent<TrafficLightCar>().isMove = false;
            
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.transform.tag);
        if (isTrigger)
        {
            return;
        }

        if (collision.transform.tag == "Player" && !collision.GetComponent<Collider>().isTrigger)
        {
            isTrigger = true;
            GameManager.instance.RemoveMoney(removeScore);
            Debug.Log("��Ǯ");
            if (GetComponent<Car>())
            {

                GetComponent<Car>().DeleteThis();
            }
            if (GetComponent<TrafficLightCar>())
            {

                GetComponent<TrafficLightCar>().DeleteThis();
            }

        }
    }
    public int removeScore = 40;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.tag);
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFire)
        {
          //  Debug.Log("����");
            transform.position += dir* fireSpeed*Time.deltaTime;
        }
    }
}
