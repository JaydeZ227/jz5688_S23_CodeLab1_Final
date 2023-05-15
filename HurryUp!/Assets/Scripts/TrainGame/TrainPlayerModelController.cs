using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HurryUp 
{
    public class TrainPlayerModelController : MonoBehaviour
    {
        [SerializeField] Transform model;

        [SerializeField] float swingSpeed = 20f;

        [SerializeField] Transform startPoint;
        [SerializeField] Transform endPoint;

        [SerializeField] Rigidbody myRigidbody;

        [SerializeField] float enterForce = 30f;
        // Update is called once per frame
        void Update()
        {
            switch (TrainGameManager.instance.currentTrainType)
            {
                case TrainMoveType.行驶中:
                    XingSHIZhongLogic();
                    break;
                case TrainMoveType.刹车:
                    ShaCheLogic();
                    break;
                case TrainMoveType.停止后:
                    break;
                case TrainMoveType.进入车厢:
                    EnterMetroLogic();
                    break;
                case TrainMoveType.挤出车厢:
                    ExitMetroLogic();
                    break;
                default:
                    break;
            }
        }


        public void ExitMetroLogic() 
        {
            if (Vector3.Distance(transform.position, endPoint.transform.position) <= 0.05f)
            {
                //Debug.Log("到达");

                EndEnterMetro();

                return;
            }

            if (TrainGameManager.instance.tingZhiBar.isInBule || TrainGameManager.instance.tingZhiBar.isInRed)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {

                //Debug.Log("急急急就");

                var dir = ( startPoint.transform.position - endPoint.transform.position).normalized;

                myRigidbody.AddForce(dir * enterForce);
            }
            else
            {
                myRigidbody.velocity = Vector3.zero;
            }

        }


        public void EnterMetroLogic() 
        {

            if (Vector3.Distance(transform.position, endPoint.transform.position) <= 0.05f)
            {
                Debug.Log("到达");

                EndEnterMetro();

                return;
            }

            if (TrainGameManager.instance.tingZhiBar.isInBule || TrainGameManager.instance.tingZhiBar.isInRed)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {

                Debug.Log("急急急就");

                var dir = (endPoint.transform.position - startPoint.transform.position).normalized;

                myRigidbody.AddForce(dir * enterForce);
            }
            else 
            {
                myRigidbody.velocity = Vector3.zero;
            }

            //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;

            //if (Physics.Raycast(ray, out hit, 1000f)) 
            //{
            //    Debug.Log("右点");

            //    if (Input.GetKeyDown(KeyCode.Space))
            //    {

            //        Debug.Log("急急急就");

            //        var dir = (hit.point - startPoint.transform.position).normalized;

            //        myRigidbody.AddForce(dir * enterForce);

            //        transform.forward = new Vector3(dir.x,0,dir.y) ;
            //    }
            //}
        }


        public void XingSHIZhongLogic() 
        {
            if (TrainGameManager.instance.xingShiZhongBar.CheckIsInController())
            {

                if (model.localEulerAngles.z != 0 )
                {

                    float aimRotatZ = 0;

                    if (model.localEulerAngles.z >= 320f)
                    {
                        aimRotatZ = Mathf.Lerp(model.localEulerAngles.z, 360f, Time.deltaTime * swingSpeed);
                    }
                    else if (model.localEulerAngles.z <= 45f)
                    {
                        aimRotatZ = Mathf.Lerp(model.localEulerAngles.z, 0f, Time.deltaTime * swingSpeed);
                    }

                    model.transform.localRotation = Quaternion.Euler(0, 0, aimRotatZ);
                }

            }
            else 
            {
                if (TrainGameManager.instance.currentMove == MoveToNext.Left)
                {
                    var rotation = model.localEulerAngles;

                    float aimZ = rotation.z + Time.deltaTime * swingSpeed;

                    model.localRotation = Quaternion.Euler(rotation.x,rotation.z,aimZ);
                }
                else 
                {
                    var rotation = model.rotation.eulerAngles;

                    float aimZ = rotation.z - Time.deltaTime * swingSpeed;

                    model.localRotation = Quaternion.Euler(rotation.x, rotation.z, aimZ);
                }
            }
        }


        public void ShaCheLogic() 
        {
            if (TrainGameManager.instance.shaCheBar.isInBule)
            {
                Debug.Log("开始Bule");

                var rotation = model.localEulerAngles;

                float aimZ = rotation.z;

                float aimx = rotation.x + Time.deltaTime * swingSpeed;

                model.localRotation = Quaternion.Euler(aimx, rotation.y, aimZ);
            }
            else if (TrainGameManager.instance.shaCheBar.isInRed)
            {
                Debug.Log("开始Red");

                var rotation = model.localEulerAngles;

                float aimZ = rotation.z;

                float aimx = rotation.x - Time.deltaTime * swingSpeed;

                model.localRotation = Quaternion.Euler(aimx, rotation.y, aimZ);
            }
            else 
            {
                float aimRotatZ = 0;

                if (model.localEulerAngles.z >= 320f)
                {
                    aimRotatZ = Mathf.Lerp(model.localEulerAngles.z, 360f, Time.deltaTime * swingSpeed * 0.5f);
                }
                else if (model.localEulerAngles.z <= 45f)
                {
                    aimRotatZ = Mathf.Lerp(model.localEulerAngles.z, 0f, Time.deltaTime * swingSpeed * 0.5f);
                }

                float aimRotatX = 0;

                if (model.localEulerAngles.x >= 320f)
                {
                    aimRotatX = Mathf.Lerp(model.localEulerAngles.x, 360f, Time.deltaTime * swingSpeed);
                }
                else if (model.localEulerAngles.x <= 45f)
                {
                    aimRotatX = Mathf.Lerp(model.localEulerAngles.x, 0f, Time.deltaTime * swingSpeed);
                }

                float aimRotatY = 0;

                if (model.localEulerAngles.y >= 320f)
                {
                    aimRotatY = Mathf.Lerp(model.localEulerAngles.y, 360f, Time.deltaTime * swingSpeed);
                }
                else if (model.localEulerAngles.y <= 45f)
                {
                    aimRotatY = Mathf.Lerp(model.localEulerAngles.y, 0f, Time.deltaTime * swingSpeed);
                }

                model.transform.localRotation = Quaternion.Euler(aimRotatX, aimRotatY, aimRotatZ);

            }
       
        }
        public void StartEnterMetro()
        {
            transform.position = startPoint.transform.position;

            transform.forward = startPoint.transform.forward;

            TrainGameManager.instance.currentTrainType = TrainMoveType.进入车厢;
        }

        public void FinishEnterMetro() 
        {
            EndEnterMetro();
        }

        public void EndEnterMetro()
        {
            transform.position = endPoint.transform.position;

            transform.forward = endPoint.transform.forward;

        }


        public void BeginExitMetro() 
        {
            EndEnterMetro();
        }

        public void EndExitMetro() 
        {
            
        }

    }


}
