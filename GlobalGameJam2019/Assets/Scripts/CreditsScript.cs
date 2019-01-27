using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScript : MonoBehaviour
{
    Vector3 startPos;

    private void Start(){
        startPos = transform.position;
    }

    void Update(){
        transform.Translate(Vector3.up);
        Debug.Log(transform.position.y);
    }

    public void refresh(){
        transform.position = startPos;
    }
}
