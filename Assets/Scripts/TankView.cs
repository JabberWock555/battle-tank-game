using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankView : MonoBehaviour
{
    private TankController tankController;
    private Camera cam;
    private float movement;
    private float rotation;

    [SerializeField] private Rigidbody body;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        tankController.CameraSetup(cam);
        //cam.transform.SetParent(transform);
        //cam.transform.position = new Vector3(-10f, 21f, -10f);
    }

    // Update is called once per frame
    void Update()
    {
        InputControls();

        if(movement != 0)
        {
            tankController.Move(movement);
        }
        if(rotation != 0)
        {
            tankController.Rotate(rotation);
        }   
    }

    private void LateUpdate()
    {
        tankController.CameraMovement();
    }

    private void InputControls()
    {
        movement = Input.GetAxis("Vertical1");
        rotation = Input.GetAxis("Horizontal1");
    }


    public void SetTankController(TankController _tankController)
    {
        tankController = _tankController;
    }

    public Rigidbody GetRigidBody()
    {
        return body;
    }

    public Camera GetCamera()
    {
        return cam;
    }
}
