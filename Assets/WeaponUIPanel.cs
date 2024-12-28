using System.Linq;
using UnityEngine;

public class WeaponUIPanel : MonoBehaviour
{
    private WeaponSelectionIcon[] icons;

    private WeaponSelectionIcon _currentSelected;
    private void Awake()
    {

        var controller = FindFirstObjectByType<ShootController>();
        controller.OnWeaponSelected += Select;
        var weapons = SceneConnection.weapons;
        
        icons = GetComponentsInChildren<WeaponSelectionIcon>();
        for (int i = 0; i < icons.Length; i++)
        {
            
            var result = weapons.FirstOrDefault(s =>
            {
                if (s == null)
                {
                    return false;
                }
                
                return s.slotIndex == i;
            });
            if (result == null)
            {
                icons[i].icon.gameObject.SetActive(false);
            }
            icons[i].slotNumber.text = (i + 1).ToString();
            icons[i].selectionIcon.gameObject.SetActive(false);
        }
    }

    private void Select(int index)
    {
        _currentSelected?.selectionIcon.gameObject.SetActive(false);
        _currentSelected = icons[index];
        _currentSelected?.selectionIcon.gameObject.SetActive(true);
    }
}
