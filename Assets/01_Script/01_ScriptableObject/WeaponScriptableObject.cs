using System;
using UnityEngine;
using Utils.Tools;

[CreateAssetMenu(fileName = "Gun", menuName = "New Scriptable Object/New Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    public enum ShootingParam
    {
        NONE,
        SEMIAUTO,
        AUTO,
        BURST
    }
    
    public enum ShootingStyle
    {
        NONE,
        HITMARKER,
        INSTANCE
    }
    
    
    [Header("Visual")]
    [SerializeField] private GameObject _GunVisual;
    [SerializeField] private ParticleSystem _MuzzleFlashEffect;

    [Header("Bullets")]
    [SerializeField] private int _CurrentAmountOfBulletsInMagazin;
    [SerializeField] private int _CurrentAmountOfBulletsReserve;
    [SerializeField] private int _MaxAmountOfBulletsPerMagazin;
    [SerializeField] private int _MaxAmountOfBullets;
    [SerializeField] private GameObject _BulletsPrefabs;

    [Header("Parameter")]
    [SerializeField] private ShootingParam _ShootingParameter;
    [SerializeField] private ShootingStyle _ShootingStyle;
    [SerializeField] private float _BulletsDistance;
    [SerializeField] private float _BulletsDamage;
    [SerializeField] private float _FireRate = 0.5f;
    [SerializeField] private float _ReloadTime = 0.5f;
    [SerializeField] private float _BulletsSpeed;


    [Header("Sound")] 
    [SerializeField] private AudioClip ShootingAudioClip;
    [SerializeField] private AudioClip RealoadAudioClip;
    [SerializeField] private AudioClip EmptyGunAudioclip;
    [SerializeField] private AudioClip EmptyMagazinAudioclip;

    private bool canShoot = true;
    private bool canReload = true;

    private Timer _ReloadTimer;
    private Timer _FireRateTimer;

    public void Shoot(Vector3 position, Ray direction)
    {
        canReload = true;
        if (CanShoot())
        {
            DisableCanShoot();
            if (!IsShootingTimerAvailable())
            {
                _FireRateTimer = new Timer(_FireRate, SetCanShoot);
            }
            
            _FireRateTimer.ResetPlay();

            _CurrentAmountOfBulletsInMagazin--;

            SelectShooting(position, direction);
            
            if(_MuzzleFlashEffect != null)
                _MuzzleFlashEffect.Play();
            return;
        }

        if (CanReload())
        {
            Reload();
        }
        else
        {
            Debug.Log("Can't use");
        }
    }

    private void SelectShooting(Vector3 position, Ray ray)
    {
        switch (_ShootingStyle)
        {
            case ShootingStyle.NONE:
                break;
            case ShootingStyle.HITMARKER:
                ShootHitMarker(position, ray);
                break;
            case ShootingStyle.INSTANCE:
                ShootingBullet(position, ray.direction);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ShootingBullet(Vector3 position, Vector3 direction)
    {
        BulletBehaviours bullet = BulletPoolerManagement.Instance.GetBullet(position,_BulletsDamage);
        Rigidbody RB_Bullets = bullet.gameObject.GetComponent<Rigidbody>();
        
        RB_Bullets.AddForce(direction * _BulletsSpeed,ForceMode.Impulse);
    }
    
    private void ShootHitMarker(Vector3 position, Ray direction)
    {
        RaycastHit hitData;
        
        if (Physics.Raycast(direction, out hitData, Mathf.Infinity))
        {
            Debug.Log("You hit something");
            if (hitData.collider.gameObject.TryGetComponent<TokenBehaviours>(out TokenBehaviours shootingObject))
            {
                shootingObject.GetDamage(_BulletsDamage);
            }
        }

        Debug.DrawRay(direction.origin,direction.direction * 1000,
            hitData.collider == null ? Color.green : Color.red,10f);
    }

    public void Reload()
    {
        DisableCanReload();
        DisableCanShoot();

        if (!IsReloadTimerAvailable())
        {
            _ReloadTimer = new Timer(_ReloadTime, SetCanReload);
        }
        _ReloadTimer.ResetPlay();    
        
        int amountOfBulletToReaload = _MaxAmountOfBulletsPerMagazin - _CurrentAmountOfBulletsInMagazin;

        int amountOfBullet = _CurrentAmountOfBulletsReserve - amountOfBulletToReaload;

        if (amountOfBullet < 0)
        {
            amountOfBulletToReaload = _CurrentAmountOfBulletsReserve;
        }
        
        _CurrentAmountOfBulletsReserve -= amountOfBulletToReaload;

        _CurrentAmountOfBulletsInMagazin += amountOfBulletToReaload;
    }

    public bool CanShoot()
    {
        return _CurrentAmountOfBulletsInMagazin > 0 && canShoot;
    }
    
    public bool CanReload()
    {
        return _CurrentAmountOfBulletsReserve > 0 && canReload;
    }

    public void SetCanShoot()
    {
        canShoot = true;
    }
    
    public void DisableCanShoot()
    {
        canShoot = false;
    }
    
    public void SetCanReload()
    {
        canReload = true;
        canShoot = true;
    }
    
    public void DisableCanReload()
    {
        canReload = false;
        canShoot = false;

    }
    
    private bool IsShootingTimerAvailable()
    {
        return _FireRateTimer != null;
    }
    
    private bool IsReloadTimerAvailable()
    {
        return _ReloadTimer != null;
    }
    
}
