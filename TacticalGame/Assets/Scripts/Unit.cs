using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private float stoppingDistance = 0.01f;
    private Vector3 targetPosition;
    private void Update(){
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        float moveSpeed = 4f;
        transform.position += moveDirection * Time.deltaTime * moveSpeed;
        if(Vector3.Distance(transform.position, targetPosition) < stoppingDistance ){
            transform.position = targetPosition;
        }

        if(Input.GetKeyDown(KeyCode.T)){
            Move(new Vector3(4,0,4));
        }
    }
    private void Move(Vector3 targetPosition){
        this.targetPosition = targetPosition;
    }
}
