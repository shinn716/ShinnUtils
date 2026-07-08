using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance = null;
    public PlayerInput PlayerInput { get; private set; } = null;
    public Orbit orbitControls = null;
    public Player firstPersonControls = null;

    private void Awake()
    {
        Instance = this;
        PlayerInput = GetComponent<PlayerInput>();
    }

    public void SetInput(bool enabled)
    {
        orbitControls.Enable = enabled;
        firstPersonControls.Enable = enabled;
    }
}
