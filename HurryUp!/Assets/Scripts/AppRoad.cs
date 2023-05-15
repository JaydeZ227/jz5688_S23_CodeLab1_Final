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
            weatherSituation.text = $"����Ӽҵ���˾��·����{GameManager.instance.todayRoadSituation}";


            switch (GameManager.instance.todayRoadSituation)
            {
                case RoadSituationType.ӵ��:

                    content.sprite = yongDu;
                    break;
                case RoadSituationType.����:

                    content.sprite = huanXing;
                    break;
                case RoadSituationType.��ͨ:

                    content.sprite = changtONG;
                    break;
                default:
                    break;
            }

        }
    }
}

