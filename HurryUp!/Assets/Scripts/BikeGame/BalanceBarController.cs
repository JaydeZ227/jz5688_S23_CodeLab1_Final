using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HurryUp
{
    public class BalanceBarController : MonoBehaviour
    {
        [SerializeField] Image controllerArea;
        [SerializeField] Image balanceMover;
        [SerializeField] RectTransform parent;

        public float currentControllerAreaWidth = 200f;

        private float parentWidth;
        private float minMoveX;
        private float maxMoveX;

        public bool canInput = false;

        public float controlAreaSpeed = 2f;

        private void Start()
        {
            InitBar();
        }

        public void InitBar()
        {
            parentWidth = parent.rect.width;

            controllerArea.rectTransform.sizeDelta = new Vector2(currentControllerAreaWidth, controllerArea.rectTransform.sizeDelta.y);

            minMoveX = controllerArea.rectTransform.sizeDelta.x / 2;

            maxMoveX = parentWidth - minMoveX;

            canInput = true;
        }


        public void Update()
        {
            if (!canInput)
            {
                return;
            }

            ControlMover();

        }

        private void ControlMover()
        {

            var inputX = Input.GetAxis("Horizontal");

            var pos = controllerArea.rectTransform.anchoredPosition;

            var newPos = pos.x + inputX * Time.deltaTime * controlAreaSpeed;
            //var newPos = pos.x - inputX * Time.deltaTime * controlAreaSpeed;
            if (newPos < minMoveX)
            {
                newPos = minMoveX;
            }
            else if (newPos > maxMoveX)
            {
                newPos = maxMoveX;
            }

            controllerArea.rectTransform.anchoredPosition = new Vector2(newPos, controllerArea.rectTransform.anchoredPosition.y);
        }

        public void MoveToNext(float duration, MoveToNext moveType, Action onFinish = null)
        {

            Vector2 randomAimPos = Vector2.zero;

            switch (moveType)
            {
                case HurryUp.MoveToNext.Left:
                    randomAimPos = new Vector2( UnityEngine.Random.Range(0, balanceMover.rectTransform.anchoredPosition.x),0f);
                    break;
                case HurryUp.MoveToNext.Right:
                    randomAimPos = new Vector2(UnityEngine.Random.Range( balanceMover.rectTransform.anchoredPosition.x,parentWidth), 0f);
                    break;
            }

            StartCoroutine(MoveToAimPostion(randomAimPos, duration,onFinish));
        }

        IEnumerator MoveToAimPostion(Vector2 aimPos,float duration , Action onFinish = null) 
        {
            float timer = 0;
            Vector2 startPos = balanceMover.rectTransform.anchoredPosition;

            while (timer < duration)
            {
                timer += Time.deltaTime;

                balanceMover.rectTransform.anchoredPosition = Vector2.Lerp(startPos, aimPos, timer / duration);

                yield return null;
            }

            onFinish?.Invoke();
        }

        /// <summary>
        /// 判断当前Mover是否在控制区域内
        /// </summary>
        /// <returns></returns>
        public bool CheckIsInController()
        {
            var checkMinX = controllerArea.rectTransform.anchoredPosition.x - currentControllerAreaWidth / 2f;
            var checkMaxX = controllerArea.rectTransform.anchoredPosition.x + currentControllerAreaWidth / 2f;

            return balanceMover.rectTransform.anchoredPosition.x >= checkMinX && 
                balanceMover.rectTransform.anchoredPosition.x <= checkMaxX;
        }
        public int GetDir() 
        {
            var checkMinX = controllerArea.rectTransform.anchoredPosition.x - currentControllerAreaWidth / 2f;
            var checkMaxX = controllerArea.rectTransform.anchoredPosition.x + currentControllerAreaWidth / 2f;
            if (balanceMover.rectTransform.anchoredPosition.x < checkMinX)
            {
                return -1;
            }
            if (balanceMover.rectTransform.anchoredPosition.x > checkMaxX)
            {
                return 1;
            }
            return 0;
        }
    }

    public enum MoveToNext
    {
        Left,
        Right
    }
}

