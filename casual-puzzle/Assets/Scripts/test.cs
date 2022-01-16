using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
     public MeshRenderer enderer;
    public Material green;
    public Material blue;

    private float timer;
    private float timer2;
    // Start is called before the first frame update
    void Start()
    {
        timer = .5f;
        timer2 = .5f; 
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if( timer <= 0) {
            timer2 -= Time.deltaTime;
            if(timer2 > 0) {
                enderer.sharedMaterial = blue;
            } else {
                enderer.sharedMaterial = green;
                timer = .5f;
                timer2 = .5f;
            }
        }
    }

    
}
