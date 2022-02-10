using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject pokedex;
    float rotationY;
    Quaternion originalRotation;
    Animator poke_anim;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.localRotation;
        
        //Check aspect ratio for starting animation
        poke_anim = pokedex.GetComponent<Animator>();
        float aspect = Camera.main.aspect;
        if(aspect >= 1.0){
            poke_anim.SetBool("open", true);
        }else
        {
            poke_anim.SetBool("open", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        rotationY += Input.GetAxis("Mouse Y") * 1F;
        rotationY = ClampAngle (rotationY, -5F, 5F);
        Quaternion yQuaternion = Quaternion.AngleAxis (-rotationY, Vector3.right);
        transform.localRotation = originalRotation * yQuaternion;
    }

    public static float ClampAngle (float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp (angle, min, max);
    }
}
