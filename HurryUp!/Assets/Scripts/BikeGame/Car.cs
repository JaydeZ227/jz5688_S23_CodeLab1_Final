using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HurryUp 
{
    public class Car : MonoBehaviour
    {
        public Transform aimPoint;

        public float duration = 10f;

        public float disToPlayer = 20f;

        public bool isMove = false;

        private Vector3 startPosition;

        private float timer = 0f;
        private void Start()
        {
            startPosition = transform.position;
        }

        public void DeleteThis()
        {
            Destroy(gameObject);
        }
        private void Update()
        {
            if (!isMove)
            {
                return;
            }

            if (Vector3.Distance(BikeGameManager.instance.player.transform.position,transform.position) <= disToPlayer)
            {
                timer += Time.deltaTime;

                if (timer > duration)
                {
                    isMove = false;

                    return;
                }

                transform.position = Vector3.Lerp(startPosition, aimPoint.position, timer / duration);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //GameManager.instance.AddMoney(new IncomeInfo(-10f, IncomeType.Æû³µ));

                //Destroy(gameObject);
            }
        }

    }


}
