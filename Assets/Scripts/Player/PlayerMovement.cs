using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    private Transform _cameraTransform;
    [SerializeField] private Animator _animator;

    private float _forwardAmount;
    private float _turnAmount;

    private void Awake()
    {
        Reset();
    }

    private void Reset()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
    }

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
    }

    private Vector2 _movement = Vector2.zero;
    private void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
        
       
        
        
        // Debug.Log($"forwardAmount: {forwardAmount}");
        
        // var direction = new Vector3(movement.x, 0, movement.y);
        // direction = _cameraTransform.TransformDirection(direction);
        // direction.y = 0;
        // direction = direction.normalized * _speed * Time.deltaTime;
        // characterController.Move(direction);
        //
        // transform.
    }

    private void FixedUpdate()
    {
        var camForward =  Vector3.Scale(_cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        var move = _movement.y * camForward + Quaternion.Euler(0, 90, 0) * camForward * _movement.x;


        var normMove = Quaternion.FromToRotation(transform.forward, Vector3.forward) * move;
        var signAngle = Vector3.SignedAngle(Vector3.forward, normMove, Vector3.up); 
        _turnAmount = Mathf.Abs(signAngle) <= 0.1f? 0 : Mathf.Sign(signAngle); //Vector3.Project(move, transform.right).magnitude;//Mathf.Atan2(move.x, move.z);
        _forwardAmount = Mathf.Abs(signAngle) <= 90 ? Mathf.Abs(normMove.z) * _speed : 0;//Vector3.Project(move, transform.forward).magnitude;

        
        _animator.SetFloat("Forward", _forwardAmount, 0.1f, Time.deltaTime);
        _animator.SetFloat("Turn", _turnAmount, 0.1f, Time.deltaTime);
    }
}
