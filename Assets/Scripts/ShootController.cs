using UnityEngine;
using UnityEngine.InputSystem;

public class ShootController : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private PlayerShip playerShip;

    
    private Weapon _currentWeapon;
    private Vector2 mousePosition;

    private void Start()
    {
        if (playerShip.HasWeapon())
        {
            ChooseWeapon(playerShip.GetWeapon(0));
        }
    }

    private void ChooseWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;

    }

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

        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
        Debug.Log("Mouse pos" + mousePosition);
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