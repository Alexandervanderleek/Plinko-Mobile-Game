using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RecentEarningsManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI[] textElements;

    [SerializeField] private Color red;
    [SerializeField] private Color green;
    [SerializeField] private Color grey;

    // Start is called before the first frame update
    void Start()
    {
        CollectionBasket.EarnedCoins += CollecitonCallback;

        foreach(TextMeshProUGUI text  in textElements){
            text.text = "";
        }
    }


    private void CollecitonCallback(object sender, CollectionBasket.OnBasketHit e){
        AddNumber(e.winnings, e.factor);
    }


    private void AddNumber(float number,  float factor){

        Debug.Log(factor);
        

        if(factor > PlayerPrefs.GetFloat("recordMulti",0f)){
            PlayerPrefs.SetFloat("recordMulti",factor);
        }
        
        for(int i = textElements.Length-1;i>0;i--){
            
            textElements[i].color = textElements[i-1].color;
            textElements[i].text = textElements[i-1].text;
        }

        switch(factor){
            case 1:{
                textElements[0].color =  Color.gray; 
                break;
            }
            case >1:{
                textElements[0].color =  green; 
                break;
            }
            case <1:{
                textElements[0].color =  red; 
                break;
            }
        }
        
        
        

        textElements[0].text = "+" + number.ToString("F3").Replace(",",".");

    }
}
