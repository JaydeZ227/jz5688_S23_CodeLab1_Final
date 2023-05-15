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
                case IncomeType.����:
                    icon.sprite = gongZiSpr;
                    break;
                case IncomeType.�ٵ�:
                    icon.sprite = chiDaoSpr;
                    break;
                case IncomeType.����:
                    icon.sprite = xingRenSpr;
                    break;
                case IncomeType.����:
                    icon.sprite = qiCheSpr;
                    break;
                case IncomeType.����:
                    icon.sprite = jiangLiSpr;
                    break;
                case IncomeType.����:
                    icon.sprite = fangZuSpr;
                    break;
                case IncomeType.����:
                    icon.sprite = diTieSpr;
                    break;
            }

        }

    }
}

