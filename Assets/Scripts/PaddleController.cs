using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PaddleController : NetworkBehaviour
{
    public float speed;

    public GameObject ball;

    [SyncVar]
    private bool newBall = true;

    private GameObject spawnedBall;

    private Vector3 cameraPos;
    private Vector2 screenSize;

    private float objectWidth;

    void Start()
    {
        Camera cam = Camera.main;

        cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;

        objectWidth = transform.localScale.x / 2f;

        if (isLocalPlayer)
        {
            Debug.Log("Spawned");
            CmdSpawnBall();
        }
    }

    [Command]
    void CmdSpawnBall()
    {
        //Spawns the ball and tries to link ball and paddle to each other. Something seems to be going wrong on the client side
        spawnedBall = Instantiate(ball);
        spawnedBall.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.125f, this.transform.position.z);
        spawnedBall.GetComponent<BallController>().SetPaddle(this.gameObject);

        NetworkServer.Spawn(spawnedBall);
    }

    public void SetNewBall()
    {
        newBall = true;
    }

    void Update()
    {
        if (!isLocalPlayer) { return; }

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.Translate(direction * Time.deltaTime * speed);
        Vector3 paddlePos = transform.position;
        
        paddlePos.x = Mathf.Clamp(paddlePos.x, cameraPos.x - screenSize.x + objectWidth, cameraPos.x + screenSize.x - objectWidth);
        transform.position = paddlePos;

        FireBall();
    }

    [Command]
    void FireBall()
    {
        if (newBall == true)
        {
            //Keeps the ball situated above the corresponding paddle till fired
            spawnedBall.transform.position = new Vector3(this.transform.position.x, spawnedBall.transform.position.y, spawnedBall.transform.position.z);

            if (Input.GetMouseButtonDown(0) && isLocalPlayer)
            {
                //Fires in a random 90 degree cone after mouse click
                Vector3 directionA = Quaternion.AngleAxis(Random.Range(-45.0f, 45.0f), Vector3.forward) * transform.up;
                spawnedBall.gameObject.GetComponent<Rigidbody>().velocity = directionA * speed;

                newBall = false;
            }
        }
    }
}
