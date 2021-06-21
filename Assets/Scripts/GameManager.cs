using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject Projectile;
    [SerializeField] GameObject TargetPoint;
    [SerializeField] Functions _functions;
    [SerializeField] Camera _targetCamera;
    [SerializeField] PoolingManager _pooling;
    [SerializeField] Slider _progressbar;
    public LayerMask _layer;

    bool isMotioning;
    Vector3 StartPos;
    Vector3 distance;
    int _click = 0;
    void Start()
    {
        TargetPoint.SetActive(false);
        _targetCamera.enabled = false;
    }

    void Update()
    {
        if (isMotioning)
        {
            // _functions.speedForGravity.GetComponent<Slider>().interactable = false;
            _functions.SlidersInteractableFalse();
        }
        else
        {
            _functions.SlidersInteractableTrue();
        }
        Shooting();
    }
    void Shooting()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, _layer))
        {
            transform.rotation = Quaternion.LookRotation(_functions.GetTargetDistance() - transform.position);
           
        }
        if (Input.GetMouseButtonDown(0) && _functions.LayerCheckToigoreUI(ray, out hitInfo, _layer))
        {
            StartPos = transform.position;
            distance = hitInfo.point - StartPos;
            distance.y = 0;
            ClickCounter();
        }
    }
    void ClickCounter()
    {
        _click++;
        if (_click == 1)
        {
            if (_click>=1)
            {
                _click = 1;
            }
            FirstClick(); //Positioning
        }
        if (_click == 2 && !isMotioning)
        {
            SecondClick(); //Throwing
        }
    }
    void FirstClick()
    {        
       Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, _layer))
        {
            transform.position = hitInfo.point+new Vector3(0,.5f,0);
        }       
    }
    void SecondClick()
    {
        isMotioning = true;
        TargetPoint.transform.position = _functions.GetTargetDistance() + new Vector3(0, .1f, 0);
        TargetPoint.SetActive(true);
        _targetCamera.enabled = true;
        _targetCamera.transform.position = TargetPoint.transform.position + new Vector3(0, 3.3f, -10);
        StartCoroutine(ProjectileMotion());       
    }
    IEnumerator ProjectileMotion()
    {
        float HorizontalV = distance.z / _functions.Gettime();
        Vector3 Horizontalx = distance / _functions.Gettime();
        float elapse_time = 0;
        Projectile = _pooling.GetProjectile();
        if (Projectile != null)
        {
            Projectile.transform.position = StartPos-new Vector3(0,.2f,0);
            while (elapse_time < _functions.Gettime())
            {
                Projectile.transform.Translate((Horizontalx.x) * Time.deltaTime, (_functions.GetVerticalVelocity() - (_functions.speedForGravity.value * elapse_time)) * Time.deltaTime, HorizontalV * Time.deltaTime);
                elapse_time += Time.deltaTime;
                _progressbar.value += 1 / _functions.Gettime()*Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(.2f);
            _targetCamera.enabled = false;
            isMotioning = false;            
        }
        _click = 0;
        _progressbar.value = 0;
    }
}


