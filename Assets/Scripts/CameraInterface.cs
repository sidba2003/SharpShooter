using UnityEngine;
using Cinemachine;

public class CameraInterface : MonoBehaviour
{
    CinemachineVirtualCamera camera;
    public static CameraInterface instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ChangeFOV(float amount)
    {
        camera.m_Lens.FieldOfView += amount;
    }

}
