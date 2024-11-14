using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private float _movementSpeed = 1f;
    private Rigidbody _rb;
    private float _horizontalInput;
    private float _verticalInput;
    public Transform orientation;
    private Vector3 _movedirection;
    public bool isStarting = false;

    public static Action IsOnFinish;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isStarting)
        {
            MyInput();
            MovePlayer();
        }
     
    }


    public void StartGameSequence()
    {
        Debug.Log("Start Game PM ");
        isStarting = true;
    }

    public void PauseGame()
    {
        Debug.Log("Paused Game PM ");
        isStarting = false;
    }
    public void FinishGameSequence()
    {
        
        Debug.Log("Finish Game");
        IsOnFinish?.Invoke();
        isStarting = false;
     
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            FinishGameSequence();
        }
    }


    private void MyInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }


    private void MovePlayer()
    {
        _movedirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;

        _rb.AddForce(_movedirection.normalized * _movementSpeed * 10 * Time.deltaTime, ForceMode.Force);
    }


}
