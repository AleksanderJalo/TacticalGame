using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float slingForce;
    [SerializeField] private GameObject pointer;
    private MeshRenderer pointerMeshCollider;
    [SerializeField] private GameObject pointer2;
    private MeshRenderer pointer2MeshCollider;
    Vector3 startingPos;
    [SerializeField] private LayerMask backWallLayer;
    Vector3 endingPos;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        pointerMeshCollider = pointer.GetComponent<MeshRenderer>();
        pointerMeshCollider.enabled = false;
       
    }
    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, backWallLayer);
            Vector3 pointerPosition = raycastHit.point;
            pointer.transform.position = new Vector3(pointerPosition.x, pointerPosition.y, transform.position.z);
            pointerMeshCollider.enabled = true;
            startingPos = Input.mousePosition;
        }
        if(Input.GetMouseButtonUp(0)){
            endingPos = Input.mousePosition;
            Vector3 direction = (startingPos - endingPos).normalized;
            Vector3 opositeDirection = (endingPos - startingPos).normalized;
            pointer.transform.right = opositeDirection;
            rb.AddForce(direction * slingForce, ForceMode.Impulse);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, backWallLayer);
            Vector3 pointerPosition = raycastHit.point;
            pointer2.transform.position = new Vector3(pointerPosition.x, pointerPosition.y, transform.position.z);
            pointer2MeshCollider.enabled = true;
            
            
        }

        
    }
    
}
