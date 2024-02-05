using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;


namespace GoogleMobileAds{
    public class RewardedAdController : MonoBehaviour{
    // Start is called before the first frame update
    
    //public GameObject AdLoadedStatus;

    #if UNITY_ANDROID
        private const string _adUnitId = "ca-app-pub-4487344159300856/2586165075";
    #elif UNITY_IPHONE
        private const string _adUnitId = "";
    #else
        private const string _adUnitId = "unused";
    #endif

    [SerializeField]
    private GameObject rewardedPanel;

    private RewardedAd _rewardedAd;

    public void LoadAd(){
        if(_rewardedAd != null){
            DestroyAd();
        }

        Debug.Log("Loading rewarded ad request");

        var AdRequest = new AdRequest();

        RewardedAd.Load(_adUnitId, AdRequest, (RewardedAd ad, LoadAdError error) => {
            if(error != null){
                Debug.LogError("Rewarded ad failed to load an ad with error " + error);
                return;
            }

            if(ad == null){
                Debug.LogError("Unexpected error: Reward load event fired with null ad and null error");
                return;
            }

            Debug.Log("Rewarded ad loaded with response" + ad.GetResponseInfo());
            _rewardedAd = ad;

            RegisterEventHandlers(ad);

            //AdLoadedStatus?.SetActive(true);

        });
    }

    public void ShowAd(){
        if(_rewardedAd != null && _rewardedAd.CanShowAd()){
            Debug.Log("Showing rewarded ad");
            _rewardedAd.Show((Reward reward) => {
                Debug.Log(String.Format("Rewarded ad granted a reward: {0} : {1}", reward.Amount, reward.Type));
                CoinManager.instance.GainedCoins((float)100f);   
            });
        }else{
            Debug.LogError("Rewarded ad is not ready yet");
        }

        //AdLoadedStatus?.SetActive(false);
    }

    public void DestroyAd(){
        if(_rewardedAd != null){
            Debug.Log("Destroying a ad");
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        //AdLoadedStatus?.SetActive(false);
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));

             
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when the ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
            rewardedPanel.SetActive(false);
            LoadAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content with error : "
                + error);
        };
    }

}


}

