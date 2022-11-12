using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit : MonoBehaviour
{
    private float stoppingDistance = 0.01f;
    private Vector3 targetPosition;
    private float moveSpeed = 4f;
    private float rotateSpeed = 10f;
    [SerializeField]private Animator unitAniamtor;
    private void Awake(){
        targetPosition = transform.position;
    }
    private void Update(){
        
        
       
        if(Vector3.Distance(transform.position, targetPosition) < stoppingDistance ){
            transform.position = targetPosition;
            unitAniamtor.SetBool("isWalking", false);
        }

        if(transform.position != targetPosition){
            unitAniamtor.SetBool("isWalking", true);
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
             transform.position += moveDirection * Time.deltaTime * moveSpeed;
        }

       
    }
    public void Move(Vector3 targetPosition){
        this.targetPosition = targetPosition;
    }
}
