using UnityEngine;

public class CameraMotion : MonoBehaviour
{    
    float swing_Angle;
    float elevate_angle;
 
    void Start()
    {
        swing_Angle = 0;
        elevate_angle = 0;
    }

    // Update is called once per frame
    void Update()
    {
            transform.localRotation = Quaternion.identity;
            // Mouse Y capture swing angle (right left), Mouse X capture elevate angle (up down)
            swing_Angle += Input.GetAxis("Mouse X");
            elevate_angle -= Input.GetAxis("Mouse Y");

            // Camera rotation constraints
            if (elevate_angle >= 90) {
                elevate_angle = 90;
            } else if (elevate_angle <= -90){
                elevate_angle = -90;
            }
            transform.Rotate(elevate_angle, swing_Angle, 0);
        
    }
}
