using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaticBall : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float redTimerLength = 0.5f;
    
    private float timer;
    private bool isColored;
    private bool timerOn;

    private AudioSource audioSource;

    private void Start(){
        audioSource = GetComponent<AudioSource>();
    }






    public void StartBop(){
        animator.ResetTrigger("bop");
        animator.SetTrigger("bop");

        if(!isColored){

            audioSource.pitch = UnityEngine.Random.Range(.6f,1.1f);
            audioSource.Play();

            spriteRenderer.color = Color.red;
            timer = 0f;
            timerOn = true;
            isColored = true;     
        }
    
    }

    void Update(){
        if(timerOn){
            timer+=Time.deltaTime;
            if(timer > redTimerLength){
                spriteRenderer.color = Color.white;
                timerOn = false;
                isColored = false; 
            }
        }
    }

}
