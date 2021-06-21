using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Returner : MonoBehaviour
{
    private PoolingManager objectPool;
    public static bool isPromove = false;

    private void Start()
    {
        objectPool = FindObjectOfType<PoolingManager>();
        Invoke("SetActiveFalseitSelf", 166);
    }
    private void OnDisable()
    {
        if (objectPool != null)
            objectPool.ReturnProjectile(gameObject);
    }
    void SetActiveFalseitSelf()
    {
        gameObject.SetActive(false);
    }
}
