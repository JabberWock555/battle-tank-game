using UnityEngine;

public class TankController
{
    private TankModel tankModel;
    private TankView tankView;
    private Camera cam;
    private Rigidbody body;
    private Transform camTarget;

    public TankController(TankView _tankView, TankModel _tankModel)
    {
        tankView = GameObject.Instantiate<TankView>(_tankView);
        tankModel = _tankModel;
        body = tankView.GetRigidBody();

        tankView.SetTankController(this);
        tankModel.SetTankController(this);
        //cam = tankView.GetCamera();
        
    }

    public void CameraSetup(Camera _cam)
    {
        cam = _cam;
        cam.transform.rotation = Quaternion.Euler(tankModel.camOffsetRotation);
        camTarget = tankView.transform;
    }

    public void CameraMovement()
    {
        Vector3 camPos = camTarget.position + tankModel.camOffsetPosition;
        cam.transform.position = Vector3.Lerp(cam.transform.position, camPos, tankModel.camSpeed);
        cam.transform.LookAt(camTarget.position);
    }


    public void Move(float input)
    {
        body.velocity = tankView.transform.forward * input * tankModel.movementSpeed ;
    }

    public void Rotate(float input)
    {
        Vector3 rotation = new Vector3(0f, input * tankModel.rotationSpeed, 0f);
        Quaternion deltaRotation = Quaternion.Euler(rotation * Time.deltaTime);
        body.MoveRotation(body.rotation * deltaRotation);
    }
}
