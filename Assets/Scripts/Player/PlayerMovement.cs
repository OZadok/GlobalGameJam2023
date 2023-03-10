using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    private Transform _cameraTransform;
    [SerializeField] private Animator _animator;

    private float _forwardAmount;
    private float _turnAmount;

    // [SerializeField] private LayerMask _notAllowedInsideLayerMask;
    // [SerializeField] private Collider _playerCollider;

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
        // Debug.Log($"signAngle: {signAngle}");
        if (Mathf.Abs(signAngle) < 5f)
        {
            _turnAmount = 0;
            transform.Rotate(Vector3.up, signAngle);
        }
        else
        {
            _turnAmount = Mathf.Sign(signAngle) * 0.5f;
        }
        // _turnAmount = signAngle / 120f;//signAngle*signAngle / (90f*90f) * Mathf.Sign(signAngle) * 0.5f;//Mathf.Abs(signAngle) <= 0.1f? 0 : Mathf.Sign(signAngle) * 0.5f); //Vector3.Project(move, transform.right).magnitude;//Mathf.Atan2(move.x, move.z);
        _forwardAmount = Mathf.Abs(signAngle) <= 90 ? Mathf.Abs(normMove.z) : 0;//Vector3.Project(move, transform.forward).magnitude;

        
        _animator.SetFloat("Forward", _forwardAmount, 0.1f, Time.deltaTime);
        _animator.SetFloat("Turn", _turnAmount,0.05f, Time.deltaTime);
        // _animator.SetFloat("Turn", _turnAmount);
        
        if (!IsPositionLegal(transform.position + transform.forward * _forwardAmount))
        {
            _animator.SetFloat("Forward", 0);
        }

        KeepInLegalSpace();
        KeepOutOfVine();
    }

    private bool KeepInLegalSpace()
    {
        if (IsPositionLegal(transform.position))
        {
            return false;
        }

        transform.position = GameManager.Instance.GardenBedCollider.ClosestPointOnBounds(transform.position);
        return true;
    }

    private bool IsPositionLegal(Vector3 position)
    {
        return GameManager.Instance.GardenBedCollider.bounds.Contains(position);
    }

    private void KeepOutOfVine()
    {
        if (OurPhysicsSystem.Instance.CheckCollisionWithVine(transform.position, OurPhysicsSystem.PlayerRadius, out var vineTransform))
        {
            var vinePosition = vineTransform.position.WithY(0);
            var direction = (-vinePosition + transform.position.WithY(0)).normalized;
            var distance = OurPhysicsSystem.PlayerRadius + OurPhysicsSystem.VineRadius;
            var newPosition = vinePosition + direction * distance;
            transform.position = newPosition;
        }
    }

    // private bool asdf()
    // {
    //     // Physics.
    //     // var ray = new Ray(transform.position, transform.forward);
    //     // if (_playerCollider.Raycast(ray, out var hitInfo, 0.1f));
    //     // _notAllowedInsideLayerMask
    // }
}
