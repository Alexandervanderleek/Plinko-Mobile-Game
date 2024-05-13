using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Ump.Api;
using System;
using GoogleMobileAds.Sample;

namespace GoogleMobileAds{

    public class GoogleMobileAdsController : MonoBehaviour
    {

        private static bool? _isInitialized;

        [SerializeField]
        private GoogleMobileAdsConsentController _consentController;

        [SerializeField]
        private InterstitialAdController _interstetialController;
        [SerializeField]
        private RewardedAdController _rewardedAdController;
        [SerializeField]
        private AppOpenAdController _appOpenController;

        
        

        internal static List<string> TestDeviceIds = new List<string>()
        {
            AdRequest.TestDeviceSimulator,
            #if UNITY_IPHONE
                ""
            #elif UNITY_ANDROID
                ""
                
            #endif
        };

        // Start is called before the first frame update
        void Start()
        {

            MobileAds.SetiOSAppPauseOnBackground(true);

            MobileAds.RaiseAdEventsOnUnityMainThread = true;

            MobileAds.SetRequestConfiguration( 
                new RequestConfiguration{});

            if(_consentController.CanRequestAds){
                InitializeGoogleMobileAds();
            }

            InitializingGoogleMobileAdsConsent();

            
    
        }

        private void InitializingGoogleMobileAdsConsent(){
            Debug.Log("Google mobile ads gathering consent");

            _consentController.GatherConsent((string error)=>{
                if(error != null){
                    Debug.LogError("Failed to gather consent" + error);
                }else{
                    Debug.Log("Google mobile ads consent updated: " + ConsentInformation.ConsentStatus);
                }

                if(_consentController.CanRequestAds){
                    InitializeGoogleMobileAds();
                }
            });
        }


        private void InitializeGoogleMobileAds(){
            if(_isInitialized.HasValue){
                return;
            }

            _isInitialized = false;

            Debug.Log("Google Ads are initilizing");

            MobileAds.Initialize((InitializationStatus initStatus) => {
                if(initStatus == null){
                    Debug.LogError("Google ads init failed");
                    _isInitialized = false;
                    return;
                }

                var adapterStatusMap = initStatus.getAdapterStatusMap();
                if(adapterStatusMap != null){
                    foreach(var item in adapterStatusMap){
                        Debug.Log(string.Format("Adapter {0} is {1}", item.Key, item.Value.InitializationState));

                    }
                }
                Debug.Log("Google mobile ads initilization complete");
                _isInitialized = true;

                //InterstitialAdController.load

                _rewardedAdController.LoadAd();

                _interstetialController.LoadAd();

                _appOpenController.LoadAd();

            });
        }


        public void OpenAdInspector(){
            Debug.Log("Opening Ad Inspector");
            MobileAds.OpenAdInspector((AdInspectorError error) => {
                if(error != null){
                    Debug.Log("Ad Inspector failed to open with error " + error);
                    return;
                }

                Debug.Log("Ad inspector opened succesfully");
            });
        }

        public void OpenPrivacyOptions(){
                _consentController.ShowPrivacyOptionsForm((string error) => {
                    if(error != null){
                        Debug.LogError("Failed to show consent privacy for with error " + error);
                    }else{
                        Debug.Log("Privacy form opened succesfully");
                    }
                });
        }
        
    }
}
