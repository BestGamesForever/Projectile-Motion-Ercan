using UnityEngine;
using UnityEngine.UI;

public class Functions : MonoBehaviour
{
    public Slider speedForGravity;
    [SerializeField] Slider hmaksimum;
    Vector3 worldPosition;
    private void Start()
    {
        speedForGravity.value = 10;
        hmaksimum.value = 10;
    }
    public float GetVerticalVelocity()
    {
        //hmax= Voy^2/2g => Voy=√2gHmax =>
        return  Mathf.Sqrt(2 * speedForGravity.value * hmaksimum.value);
    }

    public Vector3 GetTargetDistance()
    {
        Plane plane = new Plane(Vector3.up, 0);
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);//Target Position When clicked with mouse;          
        }
        return worldPosition;
    }
    public float Gettime()
    {
        //T=2*Voy/g =>
        return (2*GetVerticalVelocity())/speedForGravity.value;       
    }
    public float GetHorizontalVelocity()
    {
        Vector3 dis = GetTargetDistance() - transform.position;
        dis.y = 0;
        dis.x = 0;
        return dis.z /Gettime();
    }
    public bool LayerCheckToigoreUI(Ray ray, out RaycastHit hitInfo,LayerMask layer)
    {
        return Physics.Raycast(ray, out hitInfo,layer);
    }

    public void SlidersInteractableFalse()
    {
        speedForGravity.interactable = false;
        hmaksimum.interactable = false;
    }
    public void SlidersInteractableTrue()
    {
        speedForGravity.interactable = true;
        hmaksimum.interactable = true;
    }

}
