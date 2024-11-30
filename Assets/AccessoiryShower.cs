using UnityEngine;

public class AccessoiryShower : MonoBehaviour
{
    public enum WeaponType
    {
        Katana,
        Nothing,
        MobilePhone,
        Sniper,
        Vector,
        Laptop
    }

    [Header("Weapon Management")]
    public WeaponType activeWeapon = WeaponType.Katana;  // Default weapon
    public GameObject katana;
    public GameObject mobilePhone;
    public GameObject sniper;
    public GameObject vector;
    public GameObject laptop;

    private void Start()
    {
        // Ensure only the specified weapon is enabled
        SetActiveWeapon(activeWeapon);
    }

    public void SetActiveWeapon(WeaponType weapon)
    {
        // Disable all weapons
        katana.SetActive(false);
        mobilePhone.SetActive(false);
        sniper.SetActive(false);
        vector.SetActive(false);
        laptop.SetActive(false);

        // Enable the specified weapon
        switch (weapon)
        {
            case WeaponType.Katana:
                katana.SetActive(true);
                break;
            case WeaponType.MobilePhone:
                mobilePhone.SetActive(true);
                break;
            case WeaponType.Sniper:
                sniper.SetActive(true);
                break;
            case WeaponType.Vector:
                vector.SetActive(true);
                break;
            case WeaponType.Laptop:
                laptop.SetActive(true);
                break;
            case WeaponType.Nothing:
                // Ensure no weapon is enabled
                break;
        }
    }
}
