using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallPlacementManager : MonoBehaviour
{

    [SerializeField] private GameObject plinkoBallPrefab;
    [SerializeField] private GameObject placedBalls;

    [SerializeField] private GameObject basket;
    [SerializeField] private GameObject baskets;

    [SerializeField] private GameObject ballSpawner;

    [SerializeField] private Color red;
    [SerializeField] private Color redShadow;

    [SerializeField] private Color orange;
    [SerializeField] private Color orangeShadow;

    [SerializeField] private Color yellow;
    [SerializeField] private Color yellowShadow;


    private float increment = 0f;

    private int rows = 8;
    private int risk = 1;
    private float gap;
    private float startBaskets;
    private float startPosY;


    public void Start(){
        GeneratePyramind(8);
    }


    public void GeneratePyramind(int rows){

        this.rows = rows;

        foreach(Transform child in placedBalls.transform){
            Destroy(child.gameObject);
        }

        foreach(Transform child in baskets.transform){
            Destroy(child.gameObject);
        }


        startPosY =  -0.4f * (ScreenSizeManager.instance.GetScreenHeight()/2);
        float startPosX = -ScreenSizeManager.instance.GetScreenWidth()/2 + plinkoBallPrefab.transform.lossyScale.x/2;

        increment = 0.03f + 0.005f * (10 - rows);
        float guardSize = 0.375f;

        float[] multipliers = new float[rows+2];

        switch(rows){
            case 8:{
                guardSize = 0.375f;
                switch(risk){
                    case 0:{
                        multipliers = new float[] {5.6f,2.1f,1.1f,1f,0.5f,1f,1.1f,2.1f,5.6f};
                        break;
                    }
                    case 1:{
                        multipliers = new float[] {13f,3f,1.3f,0.7f,0.4f,0.7f,1.3f,3f,13f};
                        break;
                    }
                    case 2:{
                        multipliers = new float[] {29f,4f,1.5f,0.3f,0.2f,0.3f,1.5f,4f,29f};
                        break;
                    }
                }
                break;
            }
            case 9:{
                guardSize = 0.425f;
                switch(risk){
                    case 0:{
                        multipliers = new float[] {5.6f,2f,1.6f,1f,0.7f,0.7f,1f,1.6f,2f,5.6f};
                        break;
                    }
                    case 1:{
                        multipliers = new float[] {18f,4f,1.7f,0.9f,0.5f,0.5f,0.9f,1.7f,4f,18f};
                        break;
                    }
                    case 2:{
                        multipliers = new float[] {43f,7f,2f,0.6f,0.2f,0.2f,0.6f,2f,7f,43f};
                        break;
                    }
                }
                break;
            }
            case 10:{
                guardSize = 0.5f;
                switch(risk){
                    case 0:{
                        multipliers = new float[] {8.9f,3f,1.4f,1.1f,1f,0.5f,1f,1.1f,1.4f,3f,8.9f};
                        break;
                    }
                    case 1:{
                        multipliers = new float[] {22f,5f,2f,1.4f,0.6f,0.4f,0.6f,1.4f,2f,5f,22f};
                        break;
                    }
                    case 2:{
                        multipliers = new float[] {76f,10f,3f,0.9f,0.3f,0.2f,0.3f,0.9f,3f,10f,76f};
                        break;
                    }
                }
                break;
            }

            case 11:{
                guardSize = 0.625f;
                switch(risk){
                    case 0:{
                        multipliers = new float[] {8.4f,3f,1.9f,1.3f,1f,0.7f,0.7f,1f,1.3f,1.9f,3f,8.4f};
                        break;
                    }
                    case 1:{
                        multipliers = new float[] {24f,6f,3f,1.8f,0.7f,0.5f,0.5f,0.7f,1.8f,3f,6f,24f};
                        break;
                    }
                    case 2:{
                        multipliers = new float[] {120f,14.8f,5f,1.4f,0.4f,0.2f,0.2f,0.4f,1.4f,5f,14.8f,120f};
                        break;
                    }
                }
                break;
            }
        }

        gap = (ScreenSizeManager.instance.GetScreenWidth() - plinkoBallPrefab.transform.lossyScale.x)/(rows+1);

        startBaskets = startPosX + gap/2;
        
        for(int i = 0; i < rows; i ++){
            for(int x = 0; x < rows + 2 - i; x++){
            
                GameObject plinkoBall = Instantiate(plinkoBallPrefab, placedBalls.transform );
                plinkoBall.transform.localScale = new Vector2(ScreenSizeManager.instance.GetScreenWidth() * increment, ScreenSizeManager.instance.GetScreenWidth() * increment);
                plinkoBall.transform.position = new Vector2(startPosX + (gap * x) + (i * gap/2), startPosY + gap * i );
                plinkoBall.name = "row"+i;

                plinkoBall.transform.GetChild(0).transform.localScale = new Vector2( 0.1f , 1f);
                plinkoBall.transform.GetChild(0).transform.localPosition = new Vector2(guardSize , -guardSize );

                plinkoBall.transform.GetChild(1).transform.localScale = new Vector2( 0.1f , 1f);
                plinkoBall.transform.GetChild(1).transform.localPosition = new Vector2(-guardSize , -guardSize );

                if(i==0 && x<rows+1){

                    GameObject basketSpawned = Instantiate(basket, baskets.transform);
                    basketSpawned.name = "basket "+ x;

                    if(x==0 || x == rows){
                        basketSpawned.GetComponent<CollectionBasket>().Setup(multipliers[x],red, redShadow);
                    }else{

                        if(x==1 || x==2 || x==rows-1 || x==rows-2){
                            basketSpawned.GetComponent<CollectionBasket>().Setup(multipliers[x],orange, orangeShadow);
                        }else{
                            basketSpawned.GetComponent<CollectionBasket>().Setup(multipliers[x],yellow, yellowShadow);
                        }

                        

                    }


                    basketSpawned.transform.position = new Vector2(startBaskets + x*gap, startPosY - gap );
                    basketSpawned.transform.localScale = new Vector2(gap * 0.9f, gap*0.9f);

                }

                if(i==rows-1 && x==1){
                    GameObject spawner = Instantiate(ballSpawner, placedBalls.transform);
                    spawner.GetComponent<BallSpawner>().AssignIncrement(increment);
                    spawner.transform.position = new Vector2(startPosX + (gap * x) + (i * gap/2), startPosY + gap * (i+2) );
                }

            }
        }  
    }

    public void UpdateBaskets(int risk){

        this.risk = risk;

        float[] multipliers = new float[rows+2];


        foreach(Transform child in baskets.transform){
            Destroy(child.gameObject);
        }

        switch(rows){
            case 8:{
                switch(risk){
                    case 0:{
                        multipliers = new float[] {5.6f,2.1f,1.1f,1f,0.5f,1f,1.1f,2.1f,5.6f};
                        break;
                    }
                    case 1:{
                        multipliers = new float[] {13f,3f,1.3f,0.7f,0.4f,0.7f,1.3f,3f,13f};
                        break;
                    }
                    case 2:{
                        multipliers = new float[] {29f,4f,1.5f,0.3f,0.2f,0.3f,1.5f,4f,29f};
                        break;
                    }
                }
                break;
            }
            case 9:{
                switch(risk){
                    case 0:{
                        multipliers = new float[] {5.6f,2f,1.6f,1f,0.7f,0.7f,1f,1.6f,2f,5.6f};
                        break;
                    }
                    case 1:{
                        multipliers = new float[] {18f,4f,1.7f,0.9f,0.5f,0.5f,0.9f,1.7f,4f,18f};
                        break;
                    }
                    case 2:{
                        multipliers = new float[] {43f,7f,2f,0.6f,0.2f,0.2f,0.6f,2f,7f,43f};
                        break;
                    }
                }
                break;
            }
            case 10:{
                switch(risk){
                    case 0:{
                        multipliers = new float[] {8.9f,3f,1.4f,1.1f,1f,0.5f,1f,1.1f,1.4f,3f,8.9f};
                        break;
                    }
                    case 1:{
                        multipliers = new float[] {22f,5f,2f,1.4f,0.6f,0.4f,0.6f,1.4f,2f,5f,22f};
                        break;
                    }
                    case 2:{
                        multipliers = new float[] {76f,10f,3f,0.9f,0.3f,0.2f,0.3f,0.9f,3f,10f,76f};
                        break;
                    }
                }
                break;
            }
        }

        for(int x = 0; x < rows + 1; x++){
            GameObject basketSpawned = Instantiate(basket, baskets.transform);

            if(x==0 || x == rows){
                        basketSpawned.GetComponent<CollectionBasket>().Setup(multipliers[x],red,redShadow);
                    }else{

                        if(x==1 || x==2 || x==rows-1 || x==rows-2){
                            basketSpawned.GetComponent<CollectionBasket>().Setup(multipliers[x],orange,orangeShadow);
                        }else{
                            basketSpawned.GetComponent<CollectionBasket>().Setup(multipliers[x],yellow,yellowShadow);
                        }                        
            }

            basketSpawned.transform.position = new Vector2(startBaskets + x*gap, startPosY - gap );
            basketSpawned.transform.localScale = new Vector2(gap * 0.9f, gap*0.9f);
        }

    }



    public float GetIncrement(){
        return increment;
    }

    
}
