using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BrickSpawner : NetworkBehaviour
{
    public int blocksWidth;
    public float zPosition;
    public float layers;

    private float[] sizes;
    private float total;

    private float modifier;

    private Vector3 cameraPos;
    private Vector2 screenSize;

    public GameObject prefab;


    [SerializeField]
    private float[] finalSizes;

    void Start()
    {
        sizes = new float[blocksWidth];
        finalSizes = new float[blocksWidth];

        SpawnBricks();
    }

    [ServerCallback]
    private void SpawnBricks()
    {
        Camera cam = Camera.main;

        cameraPos = Camera.main.transform.position;

        screenSize = GenericManager.Instance.GetScreenSize();

        screenSize.x *= 2f;
        screenSize.x -= screenSize.x * 0.1f;

        float currentCoord = 0f;
        float currentHeight = 0.2f;

        for (int h = 0; h < layers; h++)
        {
            currentHeight -= 1f / layers;

            Color newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

            for (int i = 0; i < sizes.Length; i++)
            {
                sizes[i] = Random.Range(5f, 15f);
                total += sizes[i];
            }

            modifier = 100f / total;

            for (int i = 0; i < sizes.Length; i++)
            {
                finalSizes[i] = sizes[i] * modifier;
            }

            for (int i = 0; i < finalSizes.Length; i++)
            {
                GameObject brick = Instantiate(prefab);
                brick.transform.parent = gameObject.transform;

                brick.transform.localScale = new Vector3(finalSizes[i] * screenSize.x / 100, 1f, 1f);
                brick.transform.position = new Vector3(cameraPos.x - screenSize.x * 0.5f + currentCoord + (brick.transform.localScale.x * 0.5f), cameraPos.y - (screenSize.y * currentHeight), zPosition);

                brick.gameObject.GetComponent<Brick>().SetColor(newColor);

                currentCoord += finalSizes[i] * screenSize.x / 100;
                brick.transform.localScale -= new Vector3(0.1f, 0.1f, 0);

                NetworkServer.Spawn(brick);
            }

            total = 0f;
            modifier = 0f;
            currentCoord = 0f;
        }
    }
}
