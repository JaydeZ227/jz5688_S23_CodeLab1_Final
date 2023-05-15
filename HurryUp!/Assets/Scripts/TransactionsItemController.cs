using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HurryUp 
{
    public class TransactionsItemController : MonoBehaviour
    {

        [SerializeField] Image icon;
        [SerializeField] TMP_Text content;

        [SerializeField] Sprite gongZiSpr;
        [SerializeField] Sprite chiDaoSpr;
        [SerializeField] Sprite xingRenSpr;
        [SerializeField] Sprite qiCheSpr;
        [SerializeField] Sprite jiangLiSpr;
        [SerializeField] Sprite fangZuSpr;
        [SerializeField] Sprite diTieSpr;

        [SerializeField] Color addMonenyColor;
        [SerializeField] Color remveMonenyColor;

        public void UpdateItemInfo(IncomeInfo income) 
        {

            if (income.value < 0)
            {
                content.color = remveMonenyColor;
                content.text = $"-${income.value * -1}";
            }
            else 
            {
                content.color = addMonenyColor;
                content.text = $"+${income.value}";    
            }

            switch (income.incomeType)
            {
                case IncomeType.工资:
                    icon.sprite = gongZiSpr;
                    break;
                case IncomeType.迟到:
                    icon.sprite = chiDaoSpr;
                    break;
                case IncomeType.行人:
                    icon.sprite = xingRenSpr;
                    break;
                case IncomeType.汽车:
                    icon.sprite = qiCheSpr;
                    break;
                case IncomeType.奖励:
                    icon.sprite = jiangLiSpr;
                    break;
                case IncomeType.房租:
                    icon.sprite = fangZuSpr;
                    break;
                case IncomeType.地铁:
                    icon.sprite = diTieSpr;
                    break;
            }

        }

    }
}

