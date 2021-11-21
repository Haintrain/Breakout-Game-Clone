using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GenericManager : NetworkBehaviour
{
    //I should've just used some globals

    private static GenericManager instance = null;

    private Vector2 screenSize;

    [SyncVar]
    private int n_score;

    private GenericManager()
    {
        n_score = 0;
    }

    public static GenericManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake() 
    { 
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

 

    public Vector2 GetScreenSize()
    {
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

        return screenSize;
    }

    public int score
    {
        get { return this.n_score; }
        set { this.n_score = value; }
    }
}
