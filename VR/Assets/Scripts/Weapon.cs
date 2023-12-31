using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _magazineReloadTrigger;
    [SerializeField] private Transform _magazineSpot;

    [SerializeField] private GameObject _magazinePrefab;

    [Space]
    [Header("Audio clips")]
    [SerializeField] private AudioClip _weaponShotSound;
    [SerializeField] private AudioClip _weaponReloadSound;
    [SerializeField] private AudioClip _emptyWeaponSound;
    [SerializeField] private AudioClip _weaponEjectedMagazineSound;

    [SerializeField] private bool _hasMagazineOnStart;

    private AudioSource _audioSource;

    private GameObject _loadedMagazine;

    private bool _isMagazineLoaded;

    public bool IsMagazineLoaded => _isMagazineLoaded;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(Shoot);

        if(_hasMagazineOnStart)
        {
            LoadMagazine();
        }
    }

    private void LoadMagazine()
    {
        if (_magazineSpot != null)
        { 
            GameObject magazine = Instantiate(_magazinePrefab, _magazineSpot.transform);

            _loadedMagazine = magazine;

            Destroy(magazine.GetComponent<XRGrabInteractable>());
            Destroy(magazine.GetComponent<Rigidbody>());
            Destroy(magazine.GetComponent<Magazine>());
        
            _hasMagazineOnStart = magazine;
        }

        _isMagazineLoaded = true;

        SetAudioClipAndPlay(_emptyWeaponSound);
    }

    private void Update()
    {
        InputData.RightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool isPressed);
        if (isPressed)
        {
            Eject();
        }
    }

    private void Shoot(ActivateEventArgs arg0)
    {
        if (!_isMagazineLoaded)
            return;

        Physics.Raycast(transform.position, transform.forward, out RaycastHit hit);

        if(hit.collider != null) 
        { 
            if(hit.collider.TryGetComponent(out HitReaction hitable))
            {
                hitable.HitReaction();
            }
        }

        SetAudioClipAndPlay(_weaponShotSound);
    }

    private void Eject() 
    {
        if (!_isMagazineLoaded)
            return;

        if(_loadedMagazine != null) 
        { 
            _loadedMagazine.AddComponent<Rigidbody>();
            _loadedMagazine.transform.SetParent(null);

            _loadedMagazine = null;        
        }
        else
        {
            GameObject magazine = Instantiate(_magazinePrefab, transform.position, transform.rotation);

            //Destroy(magazine.GetComponent<Magazine>());
        }

        _isMagazineLoaded = false;

        SetAudioClipAndPlay(_emptyWeaponSound);
    }

    public void Reload()
    {
        if(!_isMagazineLoaded) 
            LoadMagazine();
    }

    private void SetAudioClipAndPlay(AudioClip clip)
    {
        _audioSource.clip = clip; 
        _audioSource.Play();
    }
}