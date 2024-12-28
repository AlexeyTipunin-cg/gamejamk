using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputController : MonoBehaviour
{
    private Vector2 stickPosition;
    [SerializeField] private Rigidbody2D rb;
    [field: SerializeField] public PlayerConfig playerConfig { get; set; }
    [SerializeField] private Camera playerCamera;
    [SerializeField] private PlayerHealthComponent playerHealthComponent;

    private bool isDead;

    private Vector3 upBorder;
    [SerializeField] private float weightMod;
    private Vector2 movementMod;


    private void Awake()
    {
        upBorder = playerCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        playerHealthComponent.OnDeath += () => { isDead = true; };
        weightMod = (SceneConnection.engineConfig.enginePower - SceneConnection.engineConfig.weight - SceneConnection.bodyConfig.weight) / SceneConnection.engineConfig.enginePower + 0.05f;
        movementMod = new Vector2(1, playerConfig.verticalSpeed * weightMod);
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }

        var yComponent = Vector2.up * stickPosition.y;

        if (yComponent.y > 0)
        {
            rb.MoveRotation(Quaternion.Euler(0, 0, playerConfig.noseRotationAngle.x * stickPosition.y));
            // rb.rotation = Quaternion.RotateTowards(rb.rotation, Quaternion.Euler(0,0, playerConfig.noseRotationAngle.x), )
        }

        if (yComponent.y < 0)
        {
            rb.MoveRotation(Quaternion.Euler(0, 0, playerConfig.noseRotationAngle.y * -stickPosition.y));
            // rb.rotation = Quaternion.RotateTowards(rb.rotation, Quaternion.Euler(0,0, playerConfig.noseRotationAngle.x), )
        }

        if (yComponent.y == 0)
        {
            rb.MoveRotation(Quaternion.Euler(0, 0, 0));
        }

        var yPos = rb.position + yComponent;
        if (yPos.y > upBorder.y)
        {
            yPos.y = upBorder.y;
        }

        rb.MovePosition(yPos + Vector2.right * playerConfig.speed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {

        stickPosition = context.ReadValue<Vector2>() * movementMod;
        Debug.Log(stickPosition);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isDead = true;
        playerHealthComponent.ReduceHealth(10000000000);
    }
}