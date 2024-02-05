using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject adPanel;
    [SerializeField] private GameObject confirmPurchase;
    [SerializeField] private GameObject statsPanel;

    [SerializeField] private GameObject[] rowHighlights;

    [SerializeField] private GameObject[] riskHighlights;


    // Start is called before the first frame update
    void Start()
    {
        settingsPanel.SetActive(false);
        confirmPurchase.SetActive(false);
        adPanel.SetActive(false);
        statsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowStats(){
        SoundManager.instance.PlayDefault();
        statsPanel.SetActive(true);
    }

    public void HideStats(){
        statsPanel.SetActive(false);
    }


    public void ShowSettings(){
        SoundManager.instance.PlayDefault();
        settingsPanel.SetActive(true);
    }

    public void HideSettings(){
        settingsPanel.SetActive(false);
    }

    public void ShowAdPanel(){
        SoundManager.instance.PlayDefault();
        adPanel.SetActive(true);
    }

    public void HidePanel(){
        adPanel.SetActive(false);
    }

    public void ShowConfirmation(){
        SoundManager.instance.PlayDefault();
        confirmPurchase.SetActive(true);
    }

    public void HideConfirmation(){
        confirmPurchase.SetActive(false);
    }


    public void SetActiveRowHighlights(int active){
        SoundManager.instance.PlayDefault();

        for(int i = 0; i < rowHighlights.Length;i++){
            if(i==active){
                rowHighlights[i].gameObject.SetActive(true);
            }else{
                rowHighlights[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetActiveRiskHighlights(int active){
        SoundManager.instance.PlayDefault();

        for(int i = 0; i < riskHighlights.Length;i++){
            if(i==active){
                riskHighlights[i].gameObject.SetActive(true);
            }else{
                riskHighlights[i].gameObject.SetActive(false);
            }
        }
    }


}
