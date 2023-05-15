using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HurryUp 
{
    public class TrainAi : MonoBehaviour
    {
        [SerializeField] List<Transform> aimPoint = new List<Transform>();

        [SerializeField] float moveSpeed = 50f;

        CharacterController myCharacterController;

        [SerializeField]private bool isMove = false;

        [SerializeField] Animator animator;

        private int currentMovePointOffset = 0;

        Rigidbody myRigidbody;

        private void Start()
        {
            myCharacterController = GetComponent<CharacterController>();

            myRigidbody = GetComponent<Rigidbody>();

            //BeginMove();
        }

        public void BeginMove() 
        {
            isMove = true;

            currentMovePointOffset = 0;

            animator.Play("Run");
        }

        public void StopMove() 
        {
            isMove = false;
            animator.Play("Idle");
        }


        private void Update()
        {
            //if (!isMove || aimPoint.Count == 0)
            //{
            //    return;
            //}


            //if (Vector3.Distance(transform.position, aimPoint[currentMovePointOffset].position) > 0.05f)
            //{
            //    var dir = (aimPoint[currentMovePointOffset].position - transform.position).normalized;

            //    myCharacterController.Move(dir * moveSpeed * Time.deltaTime);

            //    transform.forward = dir;
            //}
            //else 
            //{
            //    currentMovePointOffset++;
            //    if (currentMovePointOffset >= aimPoint.Count)
            //    {
            //        currentMovePointOffset = aimPoint.Count - 1;

            //        //ÒÆ¶¯½áÊø
            //        StopMove();
                    
            //    }
            //}

        }

        private Vector3 startPos;

        public void MoveToAimPosition(Transform point)
        {
            startPos = transform.position;

            StartCoroutine(AysncMoveToAim(point));
        }

        public void ResetPosition() 
        {
            StopAllCoroutines();
            transform.position = startPos;
        }

        IEnumerator AysncMoveToAim(Transform point) 
        {
            while (Vector3.Distance(transform.position , point.transform.position) > 0.05f)
            {

                var dir = (point.transform.position - transform.position).normalized;

                myRigidbody.AddForce(dir * moveSpeed);

                yield return new WaitForSeconds(0.5f);
            }

        }



        public void SwingToLeft() 
        {
            animator.transform.localRotation = Quaternion.Euler(0,0,25f);
        }


        public void SwingToRight() 
        {

            animator.transform.localRotation = Quaternion.Euler(0, 0, -25f);
        }


        public void ResetRotation() 
        {
            
            animator.transform.localRotation = Quaternion.Euler(0, 0, 0f);
        }

        public void SwingToForword() 
        {
            animator.transform.localRotation = Quaternion.Euler(25f, 0, 0f);
        }

        public void SwingToBack() 
        {
            animator.transform.localRotation = Quaternion.Euler(-25f, 0, 0f);
        }

    }
}
