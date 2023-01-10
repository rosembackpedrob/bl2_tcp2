using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] private int selectedWeapon = 0;
    public bool weapon0On = false;
    public bool weapon1On = false;


    void Start()
    {
        SelectWeapon();   
    }

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;
        
        if(Input.GetMouseButtonDown(2))
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }

        if(previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        
        int i = 0;
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(false);
            if(i == selectedWeapon)
            {
                if(weapon0On)
                    weapon.gameObject.SetActive(true);
                if(weapon1On)
                    weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
