using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HurryUp 
{
    public class AppRoad : MonoBehaviour
    {
        [SerializeField] TMP_Text weatherSituation;

        [SerializeField] Image content;

        [SerializeField] Sprite changtONG;
        [SerializeField] Sprite huanXing;
        [SerializeField] Sprite yongDu;

        private void OnEnable()
        {
            weatherSituation.text = $"今天从家到公司的路况：{GameManager.instance.todayRoadSituation}";


            switch (GameManager.instance.todayRoadSituation)
            {
                case RoadSituationType.拥堵:

                    content.sprite = yongDu;
                    break;
                case RoadSituationType.缓行:

                    content.sprite = huanXing;
                    break;
                case RoadSituationType.畅通:

                    content.sprite = changtONG;
                    break;
                default:
                    break;
            }

        }
    }
}

