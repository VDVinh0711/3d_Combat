using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
   [SerializeField] private float _moveSpeed;
   [SerializeField] private Rigidbody _rigidbody;
   [SerializeField] private PlayerInput _playerInput;
   [SerializeField] private Transform _cam;
   private InputAction _movement;
   private InputAction _Jump;
   private float turnSmoothVelocity;

   private void Awake()
   {
      _movement = _playerInput.actions.FindAction("Movement");
      _Jump = _playerInput.actions.FindAction("Jump");
   }

   private void Update()
   {
      Move();
   }

   private void Move()
   {
      Vector2 dir = _movement.ReadValue<Vector2>();
      var direction = new Vector3(dir.x, 0, dir.y).normalized;
      if (dir.magnitude < 0.1f) return;
      float tangetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg  + _cam.eulerAngles.y;
      float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, tangetAngle, ref turnSmoothVelocity, 0.1f);
      transform.rotation = Quaternion.Euler(0f,angle,0f);
      Vector3 moveDir = Quaternion.Euler(0f, tangetAngle, 0f) * Vector3.forward;
      transform.Translate( moveDir*_moveSpeed);
      
   }
}
