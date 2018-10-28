using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float rotationValue = 0f;
    private float destinationRotation = 0f;
    private float startValue = 0f;
    private float startRotation = 0f;
    private bool inputGiven = false;
    private Quaternion movementValue;

    private Vector3 startingRotation;
    private bool playing = false;

    private void Start()
    {
        startingRotation = transform.rotation.eulerAngles;
        GameController.OnLevelFinished += Reset;
        GameController.OnLevelStarted += Starting;
    }


    void Update ()
    {
        if (Input.GetMouseButtonDown(0) && !inputGiven)
        {
            inputGiven = true;
            startValue = Input.mousePosition.x;
            startRotation = transform.eulerAngles.y;
            movementValue = transform.rotation;
        }

        if (inputGiven)
        {
            rotationValue = (Input.mousePosition.x - startValue) / 360f;
            destinationRotation = startRotation + 180 * rotationValue;
            movementValue = Quaternion.Lerp(movementValue, Quaternion.Euler(new Vector3(0, destinationRotation, 0)), 0.05f);
        }

        if (Input.GetMouseButtonUp(0) && inputGiven)
        {
            inputGiven = false;
        }
    }

    private void FixedUpdate()
    {
        if(playing)
            transform.rotation = movementValue;
    }

    private void Starting()
    {
        playing = true;
    }

    private void Reset()
    {
        playing = false;
        rotationValue = 0f;
        destinationRotation = 0f;
        startValue = 0f;
        startRotation = 0f;
        inputGiven = false;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
}
