using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rotation_WireStripper : MonoBehaviour
{
    [SerializeField] private InputAction pressed, axis;
    [SerializeField] private int speed = 1;

    private Transform cam;

    private Vector2 rotation;
    private bool rotateAllowed;
    
    private void Awake() {
        cam = Camera.main.transform;
        pressed.Enable();
        axis.Enable();
        pressed.performed += _ => { StartCoroutine(Rotate()); };
        pressed.canceled += _ => { rotateAllowed = false; }; 
        axis.performed += context => { rotation = context.ReadValue<Vector2>(); };
    }

    //for rotation
    private IEnumerator Rotate()
    {
        rotateAllowed = true;
        while (rotateAllowed) 
        {
            rotation *= speed;
            //transform.Rotate(Vector3.up, rotation.x, Space.World); //rotate in x-axis
            transform.Rotate(-cam.right, rotation.y, Space.World); //rotate in y-axis

            yield return null;
        }
    }

}
