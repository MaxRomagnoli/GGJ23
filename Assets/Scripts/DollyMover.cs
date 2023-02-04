using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DollyMover : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private CinemachineTrackedDolly _cinemachineTrackedDolly;
    private float _actualSpeed;

    // Start is called before the first frame update
    void Start()
    {
        CinemachineVirtualCamera _camera = this.GetComponent<CinemachineVirtualCamera>();
        _cinemachineTrackedDolly = _camera.GetCinemachineComponent<CinemachineTrackedDolly>();
        float _pathLenght = _cinemachineTrackedDolly.m_Path.MaxPos;
        _actualSpeed = speed / _pathLenght;
    }

    // Update is called once per frame
    void Update()
    {
        _cinemachineTrackedDolly.m_PathPosition += (_actualSpeed * Time.deltaTime);
    }
}
