using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;
    [SerializeField]
    private Queue<GameObject> projectilePool = new Queue<GameObject>();
    [SerializeField]
    int poolStartSize;
    int counter;
    private void Start()
    {
        for (int i = 0; i < poolStartSize; i++)
        {
            GameObject projectile = Instantiate(_projectilePrefab);
            projectilePool.Enqueue(projectile);
            projectile.SetActive(false);
            counter = projectilePool.Count; 
        }
    }
    public GameObject GetProjectile()
    {
        if (counter == 24)
        {
            return null;
        }

        if (projectilePool.Count != 0)
        {
            GameObject _projectile = projectilePool.Dequeue();
            _projectile.SetActive(true);           
            return _projectile;
        }
        else
        {           
            for (int i = 0; i < 1; i++)
            {
                counter++;
                Debug.Log("counter" + counter);
                GameObject projectile = Instantiate(_projectilePrefab);
                projectilePool.Enqueue(projectile);
                projectile.SetActive(false);
            }
             GameObject _projectile = projectilePool.Dequeue();
             _projectile.SetActive(true);          
            return _projectile;        
        }      
    }
    public void ReturnProjectile(GameObject projectile)
    {
        projectilePool.Enqueue(projectile);
        projectile.SetActive(false);
    }
}
