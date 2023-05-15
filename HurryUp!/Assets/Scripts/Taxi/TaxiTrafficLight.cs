using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HurryUp 
{
    public class TaxiTrafficLight : MonoBehaviour
    {
        public bool useW;
        public bool useA;
        public bool useS;
        public bool useD;


        public bool isChangeLight = false;

        public TrafficLightType currentType;

        public List<Image> lightImgs = new List<Image>();

        public float changeLightInterval = 5f;
        private void Start()
        {
            if (isChangeLight)
            {
                StartCoroutine(WaitTimeToChange());
            }
        }

        IEnumerator WaitTimeToChange() 
        {

            while (true)
            {
                ChangeLight( TrafficLightType.Green);

                yield return new WaitForSeconds(changeLightInterval);

                ChangeLight(TrafficLightType.Red);

                yield return new WaitForSeconds(changeLightInterval);
            }
        
        }

        public void ChangeLight(TrafficLightType lightType) 
        {

            switch (lightType)
            {
                case TrafficLightType.Red:

                    foreach (var item in lightImgs)
                    {
                        item.color = Color.red;
                    }

                    currentType = TrafficLightType.Red;
                    break;
                case TrafficLightType.Yellow:
                    foreach (var item in lightImgs)
                    {
                        item.color = Color.yellow;
                    }

                    currentType = TrafficLightType.Yellow;
                    break;
                case TrafficLightType.Green:
                    foreach (var item in lightImgs)
                    {
                        item.color = Color.green;
                    }

                    currentType = TrafficLightType.Green;
                    break;
                default:
                    break;
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {

                if (currentType == TrafficLightType.Red)
                {
                    other.transform.position = new Vector3(transform.position.x, other.transform.position.y, transform.position.z);

                    Debug.Log("ÉèÖÃ³ö×â³µ");
                    var taxi = other.GetComponent<TaxiController>();

                    taxi.StopAction();

                    taxi.isUseA = useA;
                    taxi.isUseS = useS;
                    taxi.isUseD = useD;
                    taxi.isUseW = useW;
                }
            }
        }

    }
}
