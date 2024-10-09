using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using System;
using UnityEngine.InputSystem;
using Cysharp.Threading.Tasks;
using Delta.Input;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera uiCam;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private SpriteRenderer footprint;
    [SerializeField] private float minDistance = 40;
    [SerializeField] private float maxDistance = 80;
    [SerializeField] Vector3 offset = Vector3.zero;
    [Header("Rotation"), SerializeField] private float rotCameraSpeed = .1f;
    [Header("Scroll"), SerializeField] private float zoomDampening = .5f;

    public bool Enable { get; set; } = true;
    public Camera GetCamera { get { return mainCam; } }

    private float distance = 60;
    private bool onMoving = false;
    private InputAction dClickAction = null;
    private InputAction dragAction = null;
    private InputAction scrollAction = null;
    private InputAction moveAction = null;


    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    async void OnEnable()
    {
        await UniTask.WaitUntil(() => InputManager.Instance.PlayerInput != null);
        dClickAction = InputManager.Instance.PlayerInput.actions["DClick"];
        dragAction = InputManager.Instance.PlayerInput.actions["Drag"];
        scrollAction = InputManager.Instance.PlayerInput.actions["Scroll"];
        moveAction = InputManager.Instance.PlayerInput.actions["Move"];

        dragAction.performed += Drag;
        scrollAction.performed += Scroll;
        dClickAction.performed += Click;
        moveAction.performed += Move;
        onMoving = false;
    }

    private void OnDisable()
    {
        scrollAction.performed -= Scroll;
        dragAction.performed -= Drag;
        dClickAction.performed -= Click;
        moveAction.performed -= Move;
    }

    private void Click(InputAction.CallbackContext ctx)
    {
        if (!Enable)
            return;

        if (onMoving)
            return;

        Vector3 mousePosition = Mouse.current.position.ReadValue();
        if (Physics.Raycast(mainCam.ScreenPointToRay(mousePosition), out RaycastHit hit))
        {
            if (hit.collider.CompareTag("floor"))
                MoveToLocation(hit.point);
        }
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        if (!Enable)
            return;

        if (onMoving)
            return;

        var move = ctx.ReadValue<Vector2>();
        if (Physics.Raycast(mainCam.ScreenPointToRay(move), out RaycastHit hit))
        {
            if (hit.point.y > (transform.position.y / 4.5f))
                return;

            if (hit.collider.CompareTag("floor"))
            {
                footprint.enabled = true;
                var rot = Quaternion.LookRotation(Vector3.up * 90, footprint.transform.localPosition - transform.localPosition);
                footprint.transform.SetPositionAndRotation(hit.point + offset, rot);
            }
            else
            {
                footprint.enabled = false;
            }
        }
    }

    private void Drag(InputAction.CallbackContext ctx)
    {
        if (!Enable)
            return;

        var drag = ctx.ReadValue<Vector2>();
        Rotation(drag.x * rotCameraSpeed, drag.y * rotCameraSpeed);
    }

    private void Scroll(InputAction.CallbackContext ctx)
    {
        if (!Enable)
            return;

        var scroll = ctx.ReadValue<Vector2>();
        distance -= scroll.y * zoomDampening;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
        mainCam.fieldOfView = distance;
        if (uiCam != null)
            uiCam.fieldOfView = distance;
    }


    private void Rotation(float posX, float posY)
    {
        var tarObj = mainCam.transform;
        tarObj.Rotate(new Vector3(-posY, posX, 0));
        tarObj.rotation = Quaternion.Euler(Vector3.Scale(tarObj.rotation.eulerAngles, new Vector3(1, 1, 0)));
    }

    private void Move(Vector3 hitPoint, Action action)
    {
        onMoving = true;

        var result = transform.rotation * mainCam.transform.localRotation;
        var euler = result.eulerAngles;
        euler.x = 0;
        euler.z = 0;

        mainCam.transform.DOLocalRotate(Vector3.zero, 1).SetEase(Ease.OutSine);
        transform.DORotate(euler, 1).SetEase(Ease.OutSine);
        StartCoroutine(SetDestination(hitPoint, action));
    }

    private IEnumerator SetDestination(Vector3 loc, Action action = null)
    {
        agent.SetDestination(loc);
        if (!agent.isOnNavMesh)
            yield break;

        yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance < 0.1f);
        onMoving = false;
        action?.Invoke();
    }


    public void SetLocation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
        mainCam.transform.localPosition = Vector3.zero;
        mainCam.transform.localRotation = Quaternion.identity;
    }

    public void MoveToLocation(Vector3 hitPoint, Action action = null)
    {
        Move(hitPoint, action);
    }

    public void SetDefault()
    {
        mainCam.fieldOfView = 60;
        if (uiCam != null)
            uiCam.fieldOfView = 60;
    }
}
