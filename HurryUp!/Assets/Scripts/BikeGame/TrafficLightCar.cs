using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HurryUp 
{
    public class TrafficLightCar : MonoBehaviour
    {

        public Transform startPoint;

        public Transform endPoint;

        public float moveDuration = 2f;

        private bool isBeginToMove = false;

        float timer = 0f;

        private void Start()
        {
            if (startPoint != null)
            {
                transform.position = startPoint.position;
            }
        }

        public void BeginMove()
        {
            isBeginToMove = true;

            if (startPoint != null)
            {
                transform.position = startPoint.position;
            }

            timer = 0f;
        }


        public void ResetCar()
        {
            transform.position = startPoint.position;
            isMove = true;
            transform.GetComponent<triggerPlayerFire>().open.enabled = false;
            GetComponent<triggerPlayerFire>().isFire = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Åö×²Íæ¼Ò");
               // GameManager.instance.AddMoney(new IncomeInfo(-10f, IncomeType.Æû³µ));

                //gameObject.SetActive(false);
            }
        }
        public void DeleteThis() 
        {
            gameObject.SetActive(false);
            
        }
        public bool isMove = false;

        private void Update()
        {
            if (!isMove)
            {
                return;
            }
            if (isBeginToMove)
            {
           
                if (timer > moveDuration)
                {
                    isBeginToMove = false;
                    return;
                }

                timer += Time.deltaTime;

                transform.position = Vector3.Lerp(startPoint.position, endPoint.position, timer / moveDuration);
            }

        }
    }


}
