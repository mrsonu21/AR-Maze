using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRedIfPlayerTouch : MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _meshRenderer.material.color = Color.red;
        }
    }

}
