using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherTest : MonoBehaviour
{
    private Weather_Manager theWeather;
    public bool rain;
    // Start is called before the first frame update
    void Start()
    {
        theWeather = FindObjectOfType<Weather_Manager>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
            if(rain)
            theWeather.Rain();
            else
            theWeather.RainStop();
    }

}
