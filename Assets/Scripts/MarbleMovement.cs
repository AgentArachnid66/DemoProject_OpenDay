using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleMovement : MonoBehaviour
{
    private Rigidbody Rigidbody;
    private Vector3 cachedTargetForce = new Vector3();
    [SerializeField]
    private Vector3 cachedCameraForward;
    [SerializeField]
    private Vector3 cachedCameraRight;
    public LayerMask GroundedMask;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        cachedTargetForce = Vector3.zero;
        cachedCameraForward = transform.position - Camera.main.transform.position;
        cachedCameraRight = Vector3.Cross(cachedCameraForward, Camera.main.transform.up) * -1;

        cachedCameraForward = cachedCameraForward.normalized;
        cachedCameraRight = cachedCameraRight.normalized;

        if (Input.GetKey(KeyCode.W))
        {
            cachedTargetForce += cachedCameraForward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            cachedTargetForce += cachedCameraRight * -1;
        }
        if(Input.GetKey(KeyCode.S))
        {
            cachedTargetForce += cachedCameraForward * -1;
        }
        if( Input.GetKey(KeyCode.D))
        {
            cachedTargetForce += cachedCameraRight;
        }
        cachedTargetForce = cachedTargetForce.normalized;

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            cachedTargetForce += Vector3.up * 200f;
        }

        if(Rigidbody != null)
        {
            Rigidbody.AddForce(cachedTargetForce);
        }
    }

    private bool IsGrounded()
    {
        float GroundedDistance = 2f;
        if (Rigidbody.velocity.y == 0)
        {
            return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, GroundedDistance, GroundedMask);
        }
        return false;
    }
}
