using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class TouchManager : MonoBehaviour
{
    public static TouchManager instance;
    public PlayerInput playerInput { get; private set; }

    private void Awake()
    {
        instance = this;
        playerInput = GetComponent<PlayerInput>();
    }
}
