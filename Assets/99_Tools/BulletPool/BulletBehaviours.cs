
using System;
using UnityEngine;

public class BulletBehaviours : MonoBehaviour
{
    private Timer m_disableTimer;
    [SerializeField] private float disableTime;

    private float _Damage;

    private Rigidbody _Rb;

    private void Awake()
    {
        m_disableTimer = new Timer(disableTime, ()=>BulletPoolerManagement.Instance.ResetBullet(this));
        _Rb = GetComponent<Rigidbody>();
    }

    public void SetDamage(float value) => _Damage = value;

    private void OnEnable()
    {
        m_disableTimer.ResetPlay();
    }

    private void OnDisable()
    {
        m_disableTimer.Pause();
        _Rb.velocity = Vector3.zero;
    }

    private void OnDestroy()
    {
        m_disableTimer.Pause();
    }

    private void OnCollisionEnter(Collision collision)
    {
        BulletPoolerManagement.Instance.ResetBullet(this);

        if (collision.collider.gameObject.TryGetComponent<TokenBehaviours>(out TokenBehaviours shootingObject))
        {
            shootingObject.GetDamage(1);
        }
    }
}
