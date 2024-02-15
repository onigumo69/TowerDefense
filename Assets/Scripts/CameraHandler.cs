using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _cinemachineVirtualCamera;

    float _orthographicSize;
    float _targetOrthographicSize;

    void Start()
    {
        _orthographicSize = _cinemachineVirtualCamera.m_Lens.OrthographicSize;
        _targetOrthographicSize = _cinemachineVirtualCamera.m_Lens.OrthographicSize;
    }

    void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        float edgeScrollingSize = 30f;
        if(Input.mousePosition.x > Screen.width - edgeScrollingSize)
        {
            x = 1f;
        }
        if(Input.mousePosition.x < edgeScrollingSize)
        {
            x = -1f;
        }
        if(Input.mousePosition.y > Screen.height - edgeScrollingSize)
        {
            y = 1f;
        }
        if(Input.mousePosition.y < edgeScrollingSize)
        {
            y = -1f;
        }

        Vector3 moveDir = new Vector3(x, y).normalized;
        float moveSpeed = 30f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    void HandleZoom()
    {
        float zoomAmount = 2f;
        _targetOrthographicSize -= Input.mouseScrollDelta.y * zoomAmount;
        float minOrthographicSize = 10f;
        float maxOrthographicSize = 30f;
        _targetOrthographicSize = Mathf.Clamp(_targetOrthographicSize, minOrthographicSize, maxOrthographicSize);

        float zoomSpeed = 5f;
        _orthographicSize = Mathf.Lerp(_orthographicSize, _targetOrthographicSize, zoomSpeed * Time.deltaTime);

        _cinemachineVirtualCamera.m_Lens.OrthographicSize = _orthographicSize;
    }
}
