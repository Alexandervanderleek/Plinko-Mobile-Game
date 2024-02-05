using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CollectionBasket : MonoBehaviour
{

    private float multiplier;

    [SerializeField] private TextMeshProUGUI multiplierText;

    public static event EventHandler<OnBasketHit> EarnedCoins;

    public class OnBasketHit: EventArgs{
        public float winnings;
        public float factor;
    }

    private AudioSource audioSource;
    private Animator animator;

    [SerializeField] private SpriteRenderer main;
    [SerializeField] private SpriteRenderer shadow;


    void Start(){
        audioSource = GetComponent<AudioSource>();
        animator  = GetComponent<Animator>();
    }

    

    public void Setup(float multiplier, Color color, Color shadowColor){

        this.multiplier = multiplier;

        multiplierText.text = multiplier.ToString("F1").Replace(",",".");
        if(color != Color.green){
            main.color = color;
            shadow.color = shadowColor;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision2D){


        switch(multiplier){
            case <= 1:{
                audioSource.pitch = 0.8f;
                break;
            }
            case < 4:{
                audioSource.pitch = 1.0f;
                break;
            }
            default:{
                audioSource.pitch = 1.2f;
                break;
            }
        }
        audioSource.Play();
        
        if(animator.GetCurrentAnimatorStateInfo(0).length < animator.GetCurrentAnimatorStateInfo(0).normalizedTime){
            animator.SetTrigger("trigger");
        }
        

        float amount = collision2D.gameObject.GetComponent<BallScript>().GetBetValue() * multiplier;

        CoinManager.instance.GainedCoins(collision2D.gameObject.GetComponent<BallScript>().GetBetValue() * multiplier);

        EarnedCoins?.Invoke(this, new OnBasketHit{winnings =collision2D.gameObject.GetComponent<BallScript>().GetBetValue() * multiplier , factor = multiplier });

        Destroy(collision2D.gameObject);
    }
}
