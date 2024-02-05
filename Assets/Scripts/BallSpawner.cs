using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{

    [SerializeField] GameObject ball;

    private float increment;

    public static BallSpawner instance {get; private set;}


    public void AssignIncrement(float increment){
        this.increment = increment;
    }

    void Awake(){
        instance = this;
    }


    void OnDestroy(){
        CancelInvoke();
    }

    public void SpawnBall(float betAmount){


            GameObject ballSpawned = Instantiate(ball, transform);
            ballSpawned.transform.localPosition = Vector3.zero;
            ballSpawned.GetComponent<BallScript>().Setup(increment, betAmount);
        
        
    }
    
}
