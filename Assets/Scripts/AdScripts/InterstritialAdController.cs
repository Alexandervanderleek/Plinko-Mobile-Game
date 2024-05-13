using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

namespace GoogleMobileAds{



    public class InterstitialAdController : MonoBehaviour
    {
        //public GameObject AdLoadedStatus;

        // These ad units are configured to always serve test ads.
        #if UNITY_ANDROID
                private const string _adUnitId = "";
        #elif UNITY_IPHONE
                private const string _adUnitId = "";
        #else
                private const string _adUnitId = "unused";
        #endif

        private InterstitialAd _interstitialAd;


        public void LoadAd(){
            if(_interstitialAd != null ){
                DestroyAd();
            }

            Debug.Log("Loading interstetial ad");

            var adRequest = new AdRequest();
        
            InterstitialAd.Load(_adUnitId, adRequest, (InterstitialAd ad, LoadAdError error) => {
                if(error != null){
                    Debug.LogError("Interstetial ad failed to load an ad with error: " + error);
                    return;
                }

                if(ad == null){
                    Debug.LogError("Unexpected error: Interstetial load event fired with null ad and null error");
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : " + ad.GetResponseInfo());

                _interstitialAd = ad;

                RegisterEventHandlers(ad);

               // AdLoadedStatus?.SetActive(true);
            });
        }

        public void ShowAd(){
            if(_interstitialAd != null && _interstitialAd.CanShowAd()){
                Debug.Log("Showing interstetial");
                _interstitialAd.Show();   
            }else{
                Debug.LogError("Interstitial ad not ready yet");
                LoadAd();
            }

            //AdLoadedStatus?.SetActive(false);
        }

        public void LogResponseInfo(){
            if(_interstitialAd != null){
                var responseInfo = _interstitialAd.GetResponseInfo();
                UnityEngine.Debug.Log(responseInfo);
            }
        }

        public void DestroyAd(){
            if(_interstitialAd != null){
                Debug.Log("Destroying the ad");
                _interstitialAd.Destroy();
                _interstitialAd = null;
            }

            //AdLoadedStatus?.SetActive(false);
                
        }

     

        private void RegisterEventHandlers(InterstitialAd ad)
        {
            // Raised when the ad is estimated to have earned money.
            ad.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            // Raised when an impression is recorded for an ad.
            ad.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Interstitial ad recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            ad.OnAdClicked += () =>
            {
                Debug.Log("Interstitial ad was clicked.");
            };
            // Raised when an ad opened full screen content.
            ad.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Interstitial ad full screen content opened.");
            };
            // Raised when the ad closed full screen content.
            ad.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Interstitial ad full screen content closed.");
                LoadAd();
            };
            // Raised when the ad failed to open full screen content.
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Interstitial ad failed to open full screen content with error : "
                    + error);
            };
        }


        


    }

}
