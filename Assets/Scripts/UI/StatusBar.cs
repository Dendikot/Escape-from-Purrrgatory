using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    private Image bar;
    private float stat;

    // Start is called before the first frame update
    void Awake()
    {
        bar = this.GetComponent<Image>();
        stat = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = stat;
    }

    public void StatChange(float charStats, float maxValue) {
        stat = charStats / maxValue;
    }
}
