using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepCameraTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CameraController.controller.MoveCamera(transform);
            GameController.controller.UpdateProgress();
            DestroyChildren();
        }
    }

    private void DestroyChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i).GetChild(0);
            child.GetComponent<MeshCollider>().enabled = false;
            child.GetComponent<Animator>().SetInteger("random", Random.Range(0, 3));
            child.GetComponent<Animator>().SetTrigger("destroy");
        }
    }
}
