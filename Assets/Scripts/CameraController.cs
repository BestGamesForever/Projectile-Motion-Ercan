using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] float Speed;
    Vector3 TempPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float Xaxis=Input.GetAxis("Horizontal")*Speed*Time.deltaTime;
        float Zaxis=Input.GetAxis("Vertical") * Speed * Time.deltaTime; ;
        transform.Translate(Xaxis, 0, Zaxis);

        if (Input.GetMouseButtonDown(2))
        {
             TempPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Debug.Log("Hello");
        }
        if (Input.GetMouseButton(2))
        {
            Vector3 direction = TempPos - Camera.main.ScreenToViewportPoint(Input.mousePosition);
            //transform.position = Target.position+new Vector3(0,5,-10);
            transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
            transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180,Space.World);
           
           
            TempPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
    }
}
