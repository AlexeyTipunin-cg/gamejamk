using UnityEngine;
using UnityEngine.InputSystem;

public class ShootController : MonoBehaviour
{

    [SerializeField] private Weapon[] weapon;
    [SerializeField] private Camera camera;

    
    private Weapon _currentWeapon;
    private Vector2 mousePosition;


    private void Awake()
    {
        ChooseWeapon(weapon[0]);
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
            // Rotate(worldMousePosition);

            // var angle = Vector2.Angle(worldMousePosition - _currentWeapon.transform.position, Vector2.left);
            // .Rotate(angle);
            
            Debug.Log("Mouse pos" + mousePosition);

            if (Mouse.current.leftButton.isPressed)
            {
                _currentWeapon.Attack();
            }

        }
    }

    private void Rotate(Vector3 mousePosition)
    {
        Vector3 diff = mousePosition - _currentWeapon.transform.position;
        float f = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        _currentWeapon.transform.rotation = Quaternion.Euler(0, 0, f);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
        Debug.Log("Mouse pos" + mousePosition);
        // if (_currentWeapon != null)
        // {
        //    var worldMousePosition = camera.ScreenToWorldPoint(mousePosition);
        //    worldMousePosition.z = 0;
        //
        //    var angle = Vector2.Angle(worldMousePosition - _currentWeapon.transform.position, _currentWeapon.transform.forward);
        //    _currentWeapon.Rotate(angle);
        // }
    }
}