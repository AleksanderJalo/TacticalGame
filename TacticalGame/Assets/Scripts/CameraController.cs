using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private const float MIN_FOLLOW_Y_OFFSET = 2;
    private const float MAX_FOLLOW_Y_OFFSET = 12;
    private Vector3 targetFollowOffset;
    private CinemachineTransposer cinemachineTransposer;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float zoomSpeed = 1f;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;


    private void Start() {
        cinemachineTransposer  = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }
    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();

    }


    private void HandleMovement(){

        Vector3 inputMoveDirection = new Vector3(0, 0, 0);

        if(Input.GetKey(KeyCode.W)){
            inputMoveDirection.z += 1;
        }

         if(Input.GetKey(KeyCode.S)){
            inputMoveDirection.z -= 1;
        }

         if(Input.GetKey(KeyCode.A)){
            inputMoveDirection.x -= 1;
        }

         if(Input.GetKey(KeyCode.D)){
            inputMoveDirection.x += 1;
        }

        Vector3 moveVector = transform.forward * inputMoveDirection.z + transform.right * inputMoveDirection.x;
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    private void HandleRotation(){

        Vector3 rotationVector = new Vector3(0, 0, 0);

        if(Input.GetKey(KeyCode.Q)){
            rotationVector.y = 1;
        }

        if(Input.GetKey(KeyCode.E)){
            rotationVector.y = -1;
        }

        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }

    private void HandleZoom(){

        float zoomAmount = 1f;

        if(Input.mouseScrollDelta.y >= 0){
            targetFollowOffset.y -= zoomAmount;
    
        }
        if(Input.mouseScrollDelta.y <= 0){
            targetFollowOffset.y += zoomAmount;
        }

        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * zoomSpeed);
    }
        
    
}
