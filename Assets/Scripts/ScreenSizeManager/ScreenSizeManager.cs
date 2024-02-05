using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizeManager : MonoBehaviour
{
    public static ScreenSizeManager instance {private set; get;}

    void Awake(){
        instance = this;
    }


    public float GetScreenHeight(){
        return Camera.main.orthographicSize * 2f;
    }

    public float GetScreenWidth(){
        return GetScreenHeight() * Screen.width / Screen.height;
    }

}
