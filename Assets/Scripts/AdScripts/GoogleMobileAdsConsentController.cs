using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Ump.Api;
using UnityEngine;
using UnityEngine.UI;

namespace GoogleMobileAds{

    public class GoogleMobileAdsConsentController : MonoBehaviour
    {

        private Text _errorText;

        private GameObject _errorPopup;

        private Button _privacyButton;

        public bool CanRequestAds => ConsentInformation.CanRequestAds();


        // Start is called before the first frame update
        void Start()
        {
            if(_privacyButton != null){
                _privacyButton.interactable = false;
            }

            if(_errorPopup != null){
                _errorPopup.SetActive(false);
            }
        }

        public void GatherConsent(Action<String> onComplete){
            Debug.Log("Gatering consent");

            var requestParam = new ConsentRequestParameters
            {
                TagForUnderAgeOfConsent = false,
                ConsentDebugSettings = new ConsentDebugSettings
                {
                    DebugGeography = DebugGeography.Disabled,
                    TestDeviceHashedIds = GoogleMobileAdsController.TestDeviceIds
                }
            };

            onComplete = (onComplete == null) ? UpdateErrorPopup : onComplete + UpdateErrorPopup;

           ConsentInformation.Update(requestParam, (FormError updateError) => {
                
                UpdatePrivacyButton();

                if(updateError != null){
                    Debug.Log("error things about to happen");
                    onComplete(updateError.Message);
                    return;
                }

                if(CanRequestAds){
                    Debug.Log("can request");
                    onComplete(null);
                    return;
                }

                ConsentForm.LoadAndShowConsentFormIfRequired((FormError showError) => {
                    UpdatePrivacyButton();
                    if(showError != null){
                        if(onComplete != null){
                            onComplete(showError.Message);
                        }
                    }
                    else if(onComplete != null){
                        onComplete(null);
                    }
                });
           });

        }

        public void ShowPrivacyOptionsForm(Action<string> onComplete){
            Debug.Log("Showing options privacy form.");

            onComplete = (onComplete == null) ? UpdateErrorPopup : onComplete + UpdateErrorPopup;

            ConsentForm.ShowPrivacyOptionsForm((FormError showError) => {
                
                UpdatePrivacyButton();

                if(showError != null){
                    onComplete?.Invoke(showError.Message);
                }

                else
                {
                    onComplete?.Invoke(null);
                }

            });

        }

        public void ResetConsentInformation(){
            ConsentInformation.Reset();
            UpdatePrivacyButton();
        }

        void UpdatePrivacyButton(){
            if(_privacyButton != null){
                _privacyButton.interactable = ConsentInformation.PrivacyOptionsRequirementStatus == PrivacyOptionsRequirementStatus.Required;
            }
        }
    
        void UpdateErrorPopup(string message){
            if(string.IsNullOrEmpty(message)){
                return;
            }

            if(_errorText != null){
                Debug.Log("error msg updated");
                _errorText.text = message;
            }

            if(_errorPopup != null){
                Debug.Log("Error popup updated");
                _errorPopup.SetActive(true);
            }
        } 
    }

}
