using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance { get; private set;}

    [SerializeField] private AudioSource defaultClick;
    [SerializeField] private AudioSource errorClick;
    [SerializeField] private AudioSource coinsClick;

    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 45;
        instance  = this;
    }

    public void PlayDefault(){
        defaultClick.Play();
    }

    public void ErrorPlay(){
        errorClick.Play();
    }

    public void CoinsEarned(){
        coinsClick.Play();
    }
    
}
