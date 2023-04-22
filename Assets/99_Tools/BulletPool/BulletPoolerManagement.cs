using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolerManagement : Singleton<BulletPoolerManagement>
{
    [SerializeField] private GameObject BulletsPrefab;
    [SerializeField] private int _MaxPoolSize;
    [SerializeField] private List<BulletBehaviours> _Bullets = new List<BulletBehaviours>();

    void Start()
    {
        for (int i = 0; i < _MaxPoolSize; i++)
        {
            GameObject obj = Instantiate(BulletsPrefab);
            obj.SetActive(false);
            BulletBehaviours bullets = obj.GetComponent<BulletBehaviours>();
            _Bullets.Add(bullets);
        }
    }

    private void Update()
    {
        if (Input.anyKey)
            GetBullet();
    }

    public BulletBehaviours GetBullet()
    {
        for (int i = 0; i < _Bullets.Count; i++)
        {
            if (!_Bullets[i].gameObject.activeInHierarchy)
            {
                _Bullets[i].gameObject.SetActive(true);
                return _Bullets[i];
            }
        }
        
        GameObject obj = Instantiate(BulletsPrefab);
        obj.SetActive(true);
        BulletBehaviours bullets = obj.GetComponent<BulletBehaviours>();
        _Bullets.Add(bullets);
        
        return null;
    }

    public void ResetBullet(BulletBehaviours bullet)
    {

        bullet.gameObject.SetActive(false);
        if (_Bullets.Count > _MaxPoolSize)
        {
            Destroy(bullet.gameObject,0.25f);
            _Bullets.Remove(bullet);
        }
    }
    
    public void ResetAllBullet()
    {
        foreach (var bullet in _Bullets)
        {
            bullet.gameObject.SetActive(false);
        }
    }
}
