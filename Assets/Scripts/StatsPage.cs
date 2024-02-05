using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsPage : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI money;
    [SerializeField] private TextMeshProUGUI multi;

    // Start is called before the first frame update
    void OnEnable()
    {
        Debug.Log("caleed");

        money.text = PlayerPrefs.GetFloat("coinRecord", 100f).ToString("F3").Replace(",",".");
        multi.text = PlayerPrefs.GetFloat("recordMulti", 0f).ToString("F2").Replace(",",".") + "x";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
