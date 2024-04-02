using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cam;
    private Rigidbody Rigidbody;
    private float MoveSpeed = 10f;
    private float JumpPower = 5;
    private float turnSmoothVelocity;
    private float turnSmoothTime = 0.01f;

    public Vector2 MoveComposite { set { m_moveComposite = value; } }
    private Vector2 m_moveComposite = Vector2.zero;
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void Jump()
    {
        Rigidbody.velocity = Vector3.up * JumpPower;
    }
    private void Update()
    {
        Vector3 moveDirection = new Vector3(m_moveComposite.x, 0, m_moveComposite.y);
        if (moveDirection.magnitude >= 1)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg+cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            //Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0)* Vector3.forward;
            //moveDir = moveDir.normalized;
            //CharacterController.Move(moveDir.normalized * MoveSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward* MoveSpeed * Time.deltaTime);
        }
    }
}
