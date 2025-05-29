using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using StarterAssets;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] String enemyTag;
    [SerializeField] Animator animator;
    [SerializeField] GameObject ZoomEffectImage;
    [SerializeField] float CameraFOVZoomIncrement;
    [SerializeField] float ZoomedInSensitivity;

    const String SHOOT_STRING = "Shoot";
    public float FireAvailableTime = 0f;
    Weapon currentWeapon;
    public static float FireTimeIncrementor;
    public static bool IsAutomatic;
    public static ActiveWeapon instance;
    static float newAutomaticFireTime;
    public static bool CanZoom;
    CameraInterface cameraInterface;
    FirstPersonController firstPersonController;

    private void Awake()
    {
        instance = this;

        // Setting this value in the Start() method
        // this is because sometimes this variable will have value > 9.0 even when game has just been started
        newAutomaticFireTime = 0f;
    }

    private void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();

        cameraInterface = CameraInterface.instance;
        firstPersonController = FirstPersonController.instance;
    }

    void OnShoot()
    {
        if (IsAutomatic)
        {
            if (Time.time < newAutomaticFireTime) return;

            newAutomaticFireTime = Time.time + FireTimeIncrementor;
            StartCoroutine(FireMachineGun());
        }
        else
        {
            if (Time.time < FireAvailableTime) return;

            animator.Play(SHOOT_STRING, 0, 0f);
            currentWeapon.Shoot(enemyTag);

            FireAvailableTime = Time.time + FireTimeIncrementor;
        }
    }

    void OnZoom()
    {
        ZoomHandler();
    }

    void ZoomHandler()
    {
        if (!CanZoom) return;

        GameObject SniperRifleReference = GetActiveWeaponDirectory().transform.GetChild(0).gameObject;
        
        if (ZoomEffectImage.activeInHierarchy)
        {
            SniperRifleReference.SetActive(true);
            cameraInterface.ChangeFOV(CameraFOVZoomIncrement);
            ZoomEffectImage.SetActive(false);
            firstPersonController.RotationSpeed = 1f;
        }
        else
        {
            ZoomEffectImage.SetActive(true);
            cameraInterface.ChangeFOV(-CameraFOVZoomIncrement);
            SniperRifleReference.SetActive(false);
            firstPersonController.RotationSpeed = ZoomedInSensitivity;
        }
    }

    public GameObject GetActiveWeaponDirectory()
    {
        // Gets the Camera Root
        GameObject cameraRoot = GetComponent<Transform>().GetChild(0).gameObject;

        // Returns the Active Weapon Directory Game Object
        return cameraRoot.GetComponent<Transform>().GetChild(0).gameObject;
    }

    public void SetNewWeaponReference(Weapon newWeaponReference)
    {
        currentWeapon = newWeaponReference;
        StopAllCoroutines();
    }

    private IEnumerator FireMachineGun()
    {
        while (Input.GetKey(KeyCode.Mouse0))
        {
            animator.Play(SHOOT_STRING, 0, 0f);
            currentWeapon.Shoot(enemyTag);

            yield return new WaitForSeconds(FireTimeIncrementor);
        }

        yield return null;
    }
}
