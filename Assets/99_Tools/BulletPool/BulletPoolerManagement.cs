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

    public BulletBehaviours GetBullet(Vector3 position, float damage)
    {
        for (int i = 0; i < _Bullets.Count; i++)
        {
            if (!_Bullets[i].gameObject.activeInHierarchy)
            {
                _Bullets[i].gameObject.transform.position = position;
                _Bullets[i].gameObject.SetActive(true);
                _Bullets[i].SetDamage(damage);
                return _Bullets[i];
            }
        }
        
        GameObject obj = Instantiate(BulletsPrefab);
        obj.SetActive(true);
        obj.transform.position = position;
        BulletBehaviours bullets = obj.GetComponent<BulletBehaviours>();
        _Bullets.Add(bullets);
        
        return bullets;
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
