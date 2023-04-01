
using UnityEngine;

public class BulletBehaviours : MonoBehaviour
{
    private Timer m_disableTimer;
    [SerializeField] private float disableTime;

    private void Awake()
    {
        m_disableTimer = new Timer(disableTime, ()=>BulletPoolerManagement.Instance.ResetBullet(this));
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf)
            transform.Translate(0,0,1);
    }

    private void OnEnable()
    {
        m_disableTimer.ResetPlay();
    }

    private void OnDestroy()
    {
        m_disableTimer.Pause();
    }
}
