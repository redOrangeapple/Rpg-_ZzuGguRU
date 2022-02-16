using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    private BoxCollider2D bound;
    public string BoundName;

    private Camera_manager theCamera;
    // Start is called before the first frame update
    void Start()
    {
        bound = GetComponent<BoxCollider2D>();
        theCamera = FindObjectOfType<Camera_manager>();
       // theCamera.SetBound(bound);
    }

    // Update is called once per frame
    public void SetBound()
    {
        if(theCamera !=null)
        {
            theCamera.SetBound(bound);

        }

    }
}
