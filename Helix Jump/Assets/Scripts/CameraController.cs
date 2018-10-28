using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController controller;
    private bool move = false;

    private float destinationHeight = 0f;
    private float currentHeight = 0f;

    private Vector3 startPosition;

    void Start()
    {
        controller = this;
        DontDestroyOnLoad(gameObject);
        startPosition = transform.localPosition;
        GameController.OnLevelFinished += ResetCamera;
    }

    private void ResetCamera()
    {
        move = false;
        destinationHeight = 0f;
        currentHeight = 0f;
        transform.localPosition = startPosition;
    }

    void Update()
    {
        if (move)
        {
            if (currentHeight- destinationHeight<=0.02f)
                move = false;

            currentHeight = Mathf.Lerp(currentHeight, destinationHeight, 0.01f);
        }
    }

    private void FixedUpdate()
    {
        if (move)
        {
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
        }
    }

    public void MoveCamera(Transform destination)
    {
        destinationHeight = destination.position.y;
        currentHeight = transform.position.y;
        move = true;
    }
}
