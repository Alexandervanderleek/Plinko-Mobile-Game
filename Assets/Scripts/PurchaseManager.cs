using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseManager : MonoBehaviour
{

    [SerializeField] private BallPlacementManager ballPlacementManager;
    [SerializeField] private UIManager uIManager;

    private bool nineUnlocked = true;
    private bool tenUnlocked = true;
    private bool elevenUnlocked = true;

    [SerializeField] private GameObject nineLock;
    [SerializeField] private GameObject tenLock;
    [SerializeField] private GameObject elevenLock;

    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI confirmText;




    // Start is called before the first frame update
    void Awake()
    {
        if(PlayerPrefs.GetString("nine","f") == "f"){
            nineUnlocked = false;
            nineLock.SetActive(true);
        }
        if(PlayerPrefs.GetString("ten","f") == "f"){
            tenUnlocked = false;
            tenLock.SetActive(true);

        }
        if(PlayerPrefs.GetString("eleven","f") == "f"){
            elevenUnlocked = false;
            elevenLock.SetActive(true);
        }
    }


    public void ChangeRows(int rows){
        switch(rows){
            case 9:{
                if(nineUnlocked){
                    ballPlacementManager.GeneratePyramind(rows);
                    uIManager.SetActiveRowHighlights(1);
                }else{
                    uIManager.ShowConfirmation();

                    confirmText.text = "Purchse Row 9 \n for 75 credits";


                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(()=>{
                         if(CoinManager.instance.getCoins() >= 75){
                            CoinManager.instance.UseCoins(75f);
                            PlayerPrefs.SetString("nine","t");
                            SoundManager.instance.CoinsEarned();


                            ballPlacementManager.GeneratePyramind(rows);
                            uIManager.SetActiveRowHighlights(1);
                            uIManager.HideConfirmation();
                            nineLock.SetActive(false);
                            nineUnlocked = true;
                         }else{
                            SoundManager.instance.ErrorPlay();
                        }

                    });
                }
                break;


            }
            case 10:{
                if(tenUnlocked){
                    ballPlacementManager.GeneratePyramind(rows);
                    uIManager.SetActiveRowHighlights(2);
                }else{
                    uIManager.ShowConfirmation();

                    confirmText.text = "Purchse Row 10 \n for 150 credits";

                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(()=>{


                        if(CoinManager.instance.getCoins() >= 150){
                            CoinManager.instance.UseCoins(150f);
                            PlayerPrefs.SetString("ten","t");
                            SoundManager.instance.CoinsEarned();


                            ballPlacementManager.GeneratePyramind(rows);
                            uIManager.SetActiveRowHighlights(2);
                            uIManager.HideConfirmation();
                            tenLock.SetActive(false);
                            tenUnlocked = true;
                        }else{
                            SoundManager.instance.ErrorPlay();
                        }
                        

                    });
                }
                break;
            }
            case 11:{
                if(elevenUnlocked){
                    ballPlacementManager.GeneratePyramind(rows);
                    uIManager.SetActiveRowHighlights(3);
                }else{
                    uIManager.ShowConfirmation();

                    confirmText.text = "Purchse Row 11 \n for 250 credits";

                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(()=>{
                    
                    if(CoinManager.instance.getCoins() >= 250){
                        CoinManager.instance.UseCoins(250f);
                        PlayerPrefs.SetString("eleven","t");
                        SoundManager.instance.CoinsEarned();
                        
                        ballPlacementManager.GeneratePyramind(rows);
                        uIManager.SetActiveRowHighlights(3);
                        uIManager.HideConfirmation();
                        elevenLock.SetActive(false);
                        elevenUnlocked = true;
                    }else{
                            SoundManager.instance.ErrorPlay();
                        }

                    });
                }
                break;
            }
        }
       
    }
}
