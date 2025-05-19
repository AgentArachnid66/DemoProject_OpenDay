using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Target;
    public float FollowSpeed = 5f;
    public float OrbitSpeed = 5f;
    public float InterpSpeed = 5f;
    public float OrbitRadius = 5f;
    public float HeightOffset;

    [SerializeField]
    private Vector3 TargetPosition;
    [SerializeField]
    private float orientation;

    [SerializeField]
    private Vector3 Delta;
    [SerializeField]
    private Vector3 PreviousMousePosition = Vector3.zero;

    private Camera _Camera;

    private void LateUpdate()
    {
        Delta = (Input.mousePosition - PreviousMousePosition);
        TargetPosition = Vector3.zero;

        orientation += Delta.x * OrbitSpeed;


        HeightOffset += (Input.mouseScrollDelta.y * FollowSpeed);
        HeightOffset = Mathf.Clamp(HeightOffset, 1, 10);
        OrbitRadius += (Input.mouseScrollDelta.y * FollowSpeed);
        OrbitRadius = Mathf.Clamp(OrbitRadius, 2, 8);

        TargetPosition.y = HeightOffset;
        TargetPosition.z = OrbitRadius;
        TargetPosition = InterpTo(_Camera.transform.localPosition, TargetPosition, InterpSpeed);

        _Camera.transform.position = transform.TransformPoint(TargetPosition);
        _Camera.transform.LookAt(Target.transform);

        transform.rotation = Quaternion.Euler(new Vector3(0, orientation, 0));



        

        PreviousMousePosition = Input.mousePosition;

    }


    private void Awake()
    {
        _Camera = GetComponentInChildren<Camera>();
    }

    private Vector3 InterpTo(Vector3 Current, Vector3 Target, float Speed)
    {
        return new Vector3(
            Mathf.MoveTowards(Current.x, Target.x, Speed * Time.deltaTime),
            Mathf.MoveTowards(Current.y, Target.y, Speed * Time.deltaTime),
            Mathf.MoveTowards(Current.z, Target.z, Speed * Time.deltaTime));
    }
}
