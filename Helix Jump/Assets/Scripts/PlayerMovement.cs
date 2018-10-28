using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rigid;
    [SerializeField] private float destinationHeight = 250f;
    //private float currentHeight;
    private bool down = true;
    private bool usingGravity = false;

    private Vector3 startPosition;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        startPosition = transform.localPosition;

        GameController.OnLevelStarted += TriggerGravity;
        GameController.OnLevelFinished += Reset;
    }

    private void TriggerGravity()
    {
        usingGravity = !usingGravity;
        rigid.useGravity = usingGravity;
        if (!usingGravity)
        {
            rigid.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rigid.isKinematic = !usingGravity;
        }
        else
        {
            rigid.isKinematic = !usingGravity;
            rigid.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

    }

    private void Reset()
    {
        TriggerGravity();
        transform.localPosition = startPosition;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag.Equals("Finish"))
        {
            GameController.controller.FinishLevel();
        }

        if (down)
        {
            rigid.AddForce(0, destinationHeight, 0);
            down = false;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (!down)
        {
            down = true;
        }

    }
}
