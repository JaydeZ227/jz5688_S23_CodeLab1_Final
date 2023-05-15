using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HurryUp 
{
    public class TrafficLightController : MonoBehaviour
    {

        [SerializeField] List<Image> trafficLightImageList =new List<Image>();

        [SerializeField] List<Walker> walker = new List<Walker>();

        public float delayTime = 0;

        public float redLightDuration = 4f;

        public float greenLightDuration = 4f;

        TrafficLightType currentTrafficLight = TrafficLightType.Red;

        private List<Car> waitCar = new List<Car>();

        [SerializeField] List<TrafficLightCar> horizontalCars = new List<TrafficLightCar>();



        private void Start()
        {
            ChangeTrafficLightImage( TrafficLightType.Red);

            StartCoroutine(WaitTimeToChangeTrafficLight());
        }

        IEnumerator WaitTimeToChangeTrafficLight() 
        {
            ChangeTrafficLightImage(TrafficLightType.Red);
            yield return new WaitForSeconds(delayTime);

            while (true)
            {
                ChangeTrafficLightImage(TrafficLightType.Red);
                yield return new WaitForSeconds(redLightDuration);

                ChangeTrafficLightImage(TrafficLightType.Green);
                yield return new WaitForSeconds(greenLightDuration);
            }

        }


        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Player")&&!other.isTrigger)
            {
                //ChangeTrafficLightImage(TrafficLightType.Green);

                if (currentTrafficLight != TrafficLightType.Green)
                {
                    GameManager.instance.AddMoney(new IncomeInfo(-10f, IncomeType.––»À));
                }
            }


            if (other.GetComponent<Car>())
            {
                if (currentTrafficLight != TrafficLightType.Green)
                {
                    other.GetComponent<Car>().isMove = false;

                    waitCar.Add(other.GetComponent<Car>());
                }
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && !other.isTrigger)
            {
                BikeGameManager.instance.PassTrafficLight();
            }
        }

        public void ChangeTrafficLightImage(TrafficLightType type) 
        {
            switch (type)
            {
                case TrafficLightType.Red:
                    foreach (var item in trafficLightImageList)
                    {
                        item.color = Color.red;
                    }


                    foreach (var item in walker)
                    {
                        item.ResetWalker();
                    }

                    currentTrafficLight = TrafficLightType.Red;




                    if (horizontalCars.Count != 0)
                    {
                        foreach (var item in horizontalCars)
                        {
                            item.BeginMove();
                        }
                    }


                    break;
                case TrafficLightType.Yellow:

                    foreach (var item in trafficLightImageList)
                    {
                        item.color = Color.yellow;
                    }

                    currentTrafficLight = TrafficLightType.Yellow;
                    break;
                case TrafficLightType.Green:

                    foreach (var item in trafficLightImageList)
                    {
                        item.color = Color.green;
                    }

                    foreach (var item in walker)
                    {
                        item.BeginMove();
                    }

                    currentTrafficLight = TrafficLightType.Green;

                    if (waitCar.Count != 0)
                    {
                        foreach (var item in waitCar)
                        {
                            item.isMove = true;
                        }

                        waitCar.Clear();
                    }


                    if (horizontalCars.Count != 0)
                    {
                        foreach (var item in horizontalCars)
                        {
                            item.ResetCar();
                        }
                    }

                    break;
                default:
                    break;
            }
        }

    }

    public enum TrafficLightType
    {
        Red,
        Yellow,
        Green
    }
}
