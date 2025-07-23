using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject _target;
    private Vector3 _pos;
    private Vector3 _cameraPos;
    private const int CAMERA_POS_Z = -10;
    public float _cameraDistanceToPlayer;

    void Start()
    {
        _pos = Camera.main.gameObject.transform.position;
        _cameraPos = _target.transform.position;
    }

    void Update()
    {
        _cameraPos.y = _target.transform.position.y + _cameraDistanceToPlayer;
        _cameraPos.z = CAMERA_POS_Z;
        Camera.main.gameObject.transform.position = _cameraPos;
    }
}
