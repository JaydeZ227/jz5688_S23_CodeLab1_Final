using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HurryUp 
{
    public class Walker : MonoBehaviour
    {
        public Transform startPoint;

        public Transform endPoint;

        public float moveDuration = 2f;

        private bool isBeginToMove = false;

        float timer = 0f;

        [SerializeField] Animator animator;
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

            animator.Play("Walk");
        }


        public void ResetWalker() 
        {
            animator.Play("Idle");
            transform.position = startPoint.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {

                GameManager.instance.AddMoney(new IncomeInfo(-5f, IncomeType.行人));

                gameObject.SetActive(false);


                BikeGameManager.instance.zhuangCount++;
                if (BikeGameManager.instance.zhuangCount >= 2)
                {
                    GameManager.instance.yesterdayBikeZhuangLeTwice = true;
                }
            }
        }

        private void Update()
        {
            if (isBeginToMove)
            {
                if (timer > moveDuration)
                {
                    isBeginToMove = false;
                    animator.Play("Idle");
                    return;
                }

                timer += Time.deltaTime;

                transform.position = Vector3.Lerp(startPoint.position,endPoint.position,timer /moveDuration);
            }
            
        }


    }

}
