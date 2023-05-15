using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace HurryUp 
{
    public class AppMoney : MonoBehaviour
    {
        [SerializeField] TMP_Text yuEText;

        [SerializeField] TransactionsItemController transactionItem;
        [SerializeField] Transform group;
        [SerializeField] List<GameObject> spawnInfo = new List<GameObject>();

        private void OnEnable()
        {

            //--------测试
            //GameManager.instance.AddMoney(new IncomeInfo(-100, IncomeType.迟到));
            //GameManager.instance.AddMoney(new IncomeInfo(3, IncomeType.奖励));
            //GameManager.instance.AddMoney(new IncomeInfo(300, IncomeType.工资));
            //GameManager.instance.AddMoney(new IncomeInfo(-100, IncomeType.迟到));
            //GameManager.instance.AddMoney(new IncomeInfo(-100, IncomeType.迟到));
            //GameManager.instance.AddMoney(new IncomeInfo(-100, IncomeType.迟到));
            //------------
            InitInfo();
        }

        private void OnDisable()
        {
            if (spawnInfo.Count != 0)
            {
                foreach (var item in spawnInfo)
                {
                    Destroy(item);
                }
                spawnInfo.Clear();
            }
        }

        private void InitInfo() 
        {
            yuEText.text = $"<color=#83FAFE>$</color>{GameManager.instance.currentMoneyCount}";

            if (GameManager.instance.incomeInfoQueue.Count != 0)
            {

                var infoQueue = new Queue<IncomeInfo>(GameManager.instance.incomeInfoQueue.Reverse());

                foreach (var item in infoQueue)
                {

                    var info = Instantiate(transactionItem,group);
                    info.gameObject.SetActive(true);
                    info.UpdateItemInfo(item);
                    spawnInfo.Add(info.gameObject);
                }
            }
        }
    }

}
