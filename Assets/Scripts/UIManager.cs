using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class UIManager : NetworkBehaviour
{
    public Text scoreText;

    public void Update()
    {
        //Probably a better way than this
        setscore();
    }

    public void setscore()
    {
        scoreText.text = "Score: " + GenericManager.Instance.score.ToString();
    }
}