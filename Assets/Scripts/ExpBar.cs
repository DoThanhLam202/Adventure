using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public Image fillBar;
    public TextMeshProUGUI valueText;
    public TextMeshProUGUI LVText;

    public void UpdateBar(int currentExp, int maxExp1LV, int lv)
    {
        fillBar.fillAmount = (float)currentExp / (float)maxExp1LV;
        valueText.text = currentExp + " / " + maxExp1LV;
        LVText.text = "LV." + lv;
    }
}
