using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Brick : NetworkBehaviour
{
    [SyncVar]
    public Color myColor;

    public void SetColor(Color color)
    {
        myColor = color;
        transform.gameObject.GetComponent<Renderer>().material.color = myColor;
    }

    //I'm not sure how late I need to call this for it to render on the client properly
    public void Update()
    {
        transform.gameObject.GetComponent<Renderer>().material.color = myColor;
    }
}
