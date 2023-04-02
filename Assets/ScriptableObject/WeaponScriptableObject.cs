using UnityEngine;

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
    
    
    [Header("Visual")]
    [SerializeField] private GameObject _GunVisual;
    [SerializeField] private ParticleSystem _MuzzleFlashEffect;

    [Header("Bullets")]
    [SerializeField] private int _CurrentAmountOfBulletsInMagazin;
    [SerializeField] private int _CurrentAmountOfBulletsReserve;
    [SerializeField] private int _MaxAmountOfBulletsPerMagazin;
    [SerializeField] private int _MaxAmountOfBullets;
    [SerializeField] private GameObject _BulletsPrefabs;
    [SerializeField] private float _ReloadDuration;

    
    [Header("Parameter")]
    [SerializeField] private ShootingParam _ShootingParameter;
    [SerializeField] private float _BulletsDistance;
    [SerializeField] private float _FireRate;
    [SerializeField] private float _BulletsSpeed;


    [Header("Sound")] 
    [SerializeField] private AudioClip ShootingAudioClip;
    [SerializeField] private AudioClip RealoadAudioClip;
    [SerializeField] private AudioClip EmptyGunAudioclip;
    [SerializeField] private AudioClip EmptyMagazinAudioclip;

    private bool canShoot;

    public void Shooting()
    {
        if(_CurrentAmountOfBulletsInMagazin <= 0)
            if (_CurrentAmountOfBulletsReserve > 0)
                Reload();
            else
            {
                Debug.Log("No Bullets");
                return;
            }
            
        _CurrentAmountOfBulletsInMagazin--;
        /*GameObject bullet = Instantiate(_BulletsPrefabs);
        Rigidbody RB_Bullets = _BulletsPrefabs.GetComponent<Rigidbody>();
        
        RB_Bullets.AddForce(Vector3.forward * _BulletsSpeed,ForceMode.Impulse);
        
        if(_MuzzleFlashEffect != null)
            _MuzzleFlashEffect.Play();*/

    }

    public void Reload()
    {
        int amountOfBulletToReaload = _MaxAmountOfBulletsPerMagazin - _CurrentAmountOfBulletsInMagazin;

        int amountOfBullet = _CurrentAmountOfBulletsReserve - amountOfBulletToReaload;

        if (amountOfBullet < 0)
        {
            amountOfBulletToReaload = _CurrentAmountOfBulletsReserve;
        }


        _CurrentAmountOfBulletsReserve -= amountOfBulletToReaload;

        _CurrentAmountOfBulletsInMagazin += amountOfBulletToReaload;
    }

}
