using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePosition : MonoBehaviour
{
    [SerializeField] GameObject BasicObject;
    [SerializeField] GameObject MoveObject;

    private Transform BasicTransform;
    private Transform MoveTransform;
    private Vector3 BasicPos;
    private Vector3 MovePos;
    private Quaternion BasicRotate;
    private Quaternion MoveRotate;
    void Update()
    {
        BasicTransform = BasicObject.transform;
        MoveTransform = MoveObject.transform;

        BasicPos = BasicTransform.position;
        MovePos = MoveTransform.position;
        BasicRotate = BasicTransform.rotation;
        MoveRotate = MoveTransform.rotation;

        MovePos = BasicPos;
        MovePos.z = BasicPos.z + 1;

        MoveRotate = BasicRotate;

        MoveTransform.position = MovePos;
        MoveTransform.rotation = MoveRotate;
    }
}
