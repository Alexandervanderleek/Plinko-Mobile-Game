using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private float coins;

    public static CoinManager instance;

    [SerializeField] private TextMeshProUGUI coinText;

    


    void Awake(){
        if(instance == null){
            instance = this;
            instance.LoadData();
        }else{
            Destroy(gameObject);
        }
    }

    void SaveData(){
        
        coinText.text = coins.ToString("F3").Replace(",",".");

        PlayerPrefs.SetFloat("coins", coins);
    }

    public void UseCoins(float amnt){
        coins -= amnt;
        SaveData();
    }

    public void GainedCoins(float amnt){
        

        coins += amnt;
        
        if(coins >= PlayerPrefs.GetFloat("coinRecord",0)){
            PlayerPrefs.SetFloat("coinRecord",coins);
        }
        
        SaveData();
    }

    public float getCoins(){
        return coins;
    }

    void LoadData(){
        coins = PlayerPrefs.GetFloat("coins",100);
        coinText.text = coins.ToString("F3").Replace(",",".");
    }

    
}
