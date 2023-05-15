using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

namespace HurryUp 
{
    public class TaxiController : MonoBehaviour
    {
        public bool isUseD = false;
        public bool isUseA = false;
        public bool isUseW = false;
        public bool isUseS = false;


        public float carSpeed = 200f;

        private bool isPressA = false;
        private bool isPressD = false;
        private bool isPressW = false;
        private bool isPressS = false;


        public bool isMoveingTaxi = false;

        private void Update()
        {
            if (!isMoveingTaxi)
            {
                return;
            }

            if (isUseA)
            {

                if (isPressA)
                {

                    transform.position -= Vector3.forward * Time .deltaTime * carSpeed;

                    transform.rotation = Quaternion.Euler(0, -180f, 0);
                }
            }

            if (isUseD)
            {

                if (isPressD)
                {

                    transform.position += Vector3.forward * Time.deltaTime * carSpeed;


                    transform.rotation = Quaternion.Euler(0,0,0);
                }
            }

            if (isUseW)
            {

                if (isPressW)
                {

                    transform.position += Vector3.left * Time.deltaTime * carSpeed;

                    transform.rotation = Quaternion.Euler(0, -90f, 0);
                }
            }

            if (isUseS)
            {

                if (isPressS)
                {

                    transform.position -= Vector3.left * Time.deltaTime * carSpeed;

                    transform.rotation = Quaternion.Euler(0, 90, 0);
                }
            }
        }


        /// <summary>
        /// Í£Ö¹ÒÆ¶¯
        /// </summary>
        public void StopAction() 
        {
            isMoveingTaxi = false;


            isPressA = false;
            isPressD = false;
            isPressS = false;
            isPressW = false;

        }



        public void UpButtonPressDown() 
        {

            if (isMoveingTaxi)
            {
                return;
            }

            if (!isUseW)
            {
                return;
            }

            isMoveingTaxi = true;
            isPressW = true;
        }

        public void DownButtonPressDown()
        {
            if (isMoveingTaxi)
            {
                return;
            }

            if (!isUseS)
            {
                return;
            }

            isMoveingTaxi = true;
            isPressS = true;
        }
        public void LeftButtonPressDown()
        {
            if (isMoveingTaxi)
            {
                return;
            }

            if (!isUseA)
            {
                return;
            }

            isMoveingTaxi = true;
            isPressA = true;
        }
        public void RightButtonPressDown()
        {
            if (isMoveingTaxi)
            {
                return;
            }

            if (!isUseD)
            {
                return;
            }

            isPressD = true;
            isMoveingTaxi = true;
        }


    }


}
