using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeColliders : MonoBehaviour
{
    public float zPosition;
    public PhysicMaterial bouncy;

    private Vector3 cameraPos;
    private Vector2 screenSize;

    float colDepth = 4;

    private Transform topCollider;
    private Transform bottomCollider;
    private Transform leftCollider;
    private Transform rightCollider;

    void Start()
    {  
        AddColliders();
    }

    void AddColliders()
    {
        Camera cam = Camera.main;

        topCollider = new GameObject().transform;
        bottomCollider = new GameObject().transform;
        rightCollider = new GameObject().transform;
        leftCollider = new GameObject().transform;

        topCollider.name = "TopCollider";
        bottomCollider.name = "BottomCollider";
        rightCollider.name = "RightCollider";
        leftCollider.name = "LeftCollider";

        topCollider.gameObject.AddComponent<BoxCollider>();
        bottomCollider.gameObject.AddComponent<BoxCollider>();
        rightCollider.gameObject.AddComponent<BoxCollider>();
        leftCollider.gameObject.AddComponent<BoxCollider>();

        topCollider.GetComponent<Collider>().material = bouncy;
        bottomCollider.GetComponent<Collider>().material = bouncy;
        leftCollider.GetComponent<Collider>().material = bouncy;
        rightCollider.GetComponent<Collider>().material = bouncy;

        cameraPos = Camera.main.transform.position;
        screenSize = GenericManager.Instance.GetScreenSize();

        rightCollider.localScale = new Vector3(colDepth, screenSize.y * 2, colDepth);
        rightCollider.position = new Vector3(cameraPos.x + screenSize.x + (rightCollider.localScale.x * 0.5f), cameraPos.y, zPosition);
        leftCollider.localScale = new Vector3(colDepth, screenSize.y * 2, colDepth);
        leftCollider.position = new Vector3(cameraPos.x - screenSize.x - (leftCollider.localScale.x * 0.5f), cameraPos.y, zPosition);
        topCollider.localScale = new Vector3(screenSize.x * 2, colDepth, colDepth);
        topCollider.position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y + (topCollider.localScale.y * 0.5f), zPosition);
        bottomCollider.localScale = new Vector3(screenSize.x * 2, colDepth, colDepth);
        bottomCollider.position = new Vector3(cameraPos.x, cameraPos.y - screenSize.y - (bottomCollider.localScale.y * 0.5f), zPosition);

        rightCollider.tag = "Wall";
        leftCollider.tag = "Wall";
        topCollider.tag = "Wall";

        bottomCollider.tag = "Death";
    }
}