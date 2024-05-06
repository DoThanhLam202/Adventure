using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillBar;
    public TextMeshProUGUI valueText;

    public void UpdateBar(int currentExp, int maxExp1LV)
    {
        fillBar.fillAmount = (float)currentExp / (float)maxExp1LV;
        valueText.text = currentExp + " / " + maxExp1LV;
    }
}
