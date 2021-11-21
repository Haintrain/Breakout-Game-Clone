using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BallController : NetworkBehaviour
{
    private bool newBall = true;

    public Vector3 velocity;

    [SerializeField]
    [SyncVar]
    private GameObject paddle;

    public float speed;

    void Start()
    {
    }

    
    public void SetPaddle(GameObject paddle)
    {
        this.paddle = paddle;
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = new Vector3(paddle.transform.position.x, paddle.transform.position.y + 0.25f, transform.position.z);
    }


    void Update()
    {
        velocity = transform.GetComponent<Rigidbody>().velocity;
    }


    private void ResetBall()
    {
        //Moves ball back into firing position
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = new Vector3(paddle.transform.position.x, paddle.transform.position.y + 0.25f, transform.position.z);

        paddle.GetComponent<PaddleController>().SetNewBall();
    }

    [ServerCallback]
    void OnCollisionEnter(Collision collision)
    {
        //Serverside collision stuff
        if(collision.gameObject.tag == "Brick")
        {
            GenericManager.Instance.score += 100;
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag == "Death")
        {
            ResetBall();
        }
    }
}
