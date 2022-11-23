using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform bulletHitVfxPrefab;
    [SerializeField] private TrailRenderer trailRenderer;
    private Vector3 targetPosition;
    public void Setup(Vector3 targetPosition){
        this.targetPosition = targetPosition;
    }

    private void Update(){
        Vector3 moveDir = (targetPosition - transform.position).normalized;
        float distanceBeforeMoving = Vector3.Distance(transform.position, targetPosition);
        float bulletSpeed = 200;

        transform.position += moveDir * bulletSpeed * Time.deltaTime;
        float distanceAfterMoving = Vector3.Distance(transform.position, targetPosition);
        
        if(distanceBeforeMoving < distanceAfterMoving){
            transform.position = targetPosition;
            Instantiate(bulletHitVfxPrefab, targetPosition, Quaternion.identity);
            trailRenderer.transform.parent = null;
            Destroy(gameObject);
        }
    }
}
