using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootController : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private PlayerShip playerShip;

    
    private Weapon _currentWeapon;
    private Vector2 mousePosition;

    public event Action<int> OnWeaponSelected;

    private void Start()
    {
        if (playerShip.HasWeapon())
        {

            var firstWeapon = playerShip.GetFirst();
            ChooseWeapon(firstWeapon.weapon);
            OnWeaponSelected?.Invoke(firstWeapon.index);
        }
    }

    private void ChooseWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;

    }
    
    private bool isShooting = false;

    private void Update()
    {
        if (_currentWeapon != null)
        {
            
            mousePosition = Mouse.current.position.ReadValue();
            var worldMousePosition = camera.ScreenToWorldPoint(mousePosition);
            worldMousePosition.z = 0;
            _currentWeapon.Rotate(worldMousePosition);
            
            Debug.Log("Mouse pos" + mousePosition);

            if (Mouse.current.leftButton.isPressed)
            {
                _currentWeapon.Attack();
            }
            else
            {
                _currentWeapon.StopAttack();
            }

        }

        IsButtonPressed();
    }

    public void IsButtonPressed()
    {
        int index = -1;
        if (Keyboard.current.digit1Key.isPressed)
        {
            index = 0;
        }
        
        if (Keyboard.current.digit2Key.isPressed)
        {
            index = 1;
        }
        
        if (Keyboard.current.digit3Key.isPressed)
        {
            index = 2;
        }
        
        if (Keyboard.current.digit4Key.isPressed)
        {
            index = 3;
        }
        
        if (Keyboard.current.digit5Key.isPressed)
        {
            index = 4;
        }
        
        if (Keyboard.current.digit6Key.isPressed)
        {
            index = 5;
        }

        if (index > -1)
        {
            var weapon = playerShip.GetWeapon(index);
            if (weapon != null && weapon != _currentWeapon)
            {
                ChooseWeapon(weapon);
                OnWeaponSelected?.Invoke(index);
            }
            OnWeaponSelected?.Invoke(index);
        }
        

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
        Debug.Log("Mouse pos" + mousePosition);
    }

    public void OnNumbersPressed(InputAction.CallbackContext context)
    {
        var obj = context.ReadValueAsObject();
        Debug.LogWarning("Numbers Pressed" + obj);
    }
    
    public void FirstWeapon(InputAction.CallbackContext context)
    {
        var weapon = playerShip.GetWeapon(0);
        if (weapon != null && weapon != _currentWeapon)
        {
            ChooseWeapon(weapon);
        }
        Debug.Log("Mouse pos" + mousePosition);
    }
    
    public void SecondWeapon(InputAction.CallbackContext context)
    {
        var weapon = playerShip.GetWeapon(1);
        if (weapon != null&& weapon != _currentWeapon)
        {
            ChooseWeapon(weapon);
        }
        
        Debug.Log("Mouse pos" + mousePosition);
    }
}