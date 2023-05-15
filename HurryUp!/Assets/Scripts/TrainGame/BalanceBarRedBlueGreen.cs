using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HurryUp 
{
    public class BalanceBarRedBlueGreen : MonoBehaviour
    {
        [SerializeField] Image whiteBar;

        [SerializeField] float speed = 200f;

        [SerializeField] float addValue = 100f;

        public bool isInBule = false;

        public bool isInRed = false;

        public void InitBar() 
        {
            whiteBar.rectTransform.anchoredPosition = new Vector3(350f, 0f);
        }


        private void Update()
        {
            var posX = whiteBar.rectTransform.anchoredPosition.x;

            posX -= Time.deltaTime * speed;

            if (TrainGameManager.instance.currentTrainType == TrainMoveType.É²³µ)
            {

                if (Input.GetKeyDown(KeyCode.W))
                {
                    posX += addValue;
                }
            }
            else 
            {

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    posX += addValue;
                }
            }

            isInBule = false;
            isInRed = false;

            if (posX < 75f)
            {
                posX = 75f;
            }

            if (posX > 625f)
            {
                posX = 625f;
            }

            if (whiteBar.rectTransform.anchoredPosition.x < 200f)
            {

                if (TrainGameManager.instance.currentTrainType == TrainMoveType.É²³µ)
                {
                    TrainGameManager.instance.ShaCheBlue();
                }
                else 
                {
                    TrainGameManager.instance.TingZhiBlue();
                }

                isInBule = true;
            }

            if (whiteBar.rectTransform.anchoredPosition.x > 500f)
            {
                if (TrainGameManager.instance.currentTrainType == TrainMoveType.É²³µ)
                {
                    TrainGameManager.instance.ShaCheRed();
                }
                else
                {
                    TrainGameManager.instance.TingZhiRed();
                }

                isInRed = true;
            }


            whiteBar.rectTransform.anchoredPosition = new Vector3(posX, 0f);
        }
    }



}
