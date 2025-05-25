using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    [SerializeField] public ParticleSystem HitVFX;


    public int Damage = 1;
    public float FireRate = 0.5f;
    public bool IsAutomatic = false;
}
