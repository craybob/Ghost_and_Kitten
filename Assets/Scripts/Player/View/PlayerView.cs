using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public Animator Anim;
    public CharacterController Controller;

    [SerializeField] private Transform holdPoint;
    private Rigidbody _held;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
        Controller = GetComponent<CharacterController>();
    }

    public void SetPresenter(PlayerPresenter presenter) { /* по желанию */ }

    public void Move(Vector3 delta) => Controller.Move(delta);
    public bool IsGrounded() => Controller.isGrounded;
    public void SetBlend(float v, float smooth) => Anim.SetFloat("Blend", v, smooth, Time.deltaTime);
    public Transform GetTransform() => transform;
    public Camera GetCamera() => Camera.main;

    public bool IsHolding => _held != null;
    public float GetHeldWeight() => _held ? _held.mass : 0f;

    public void TryGrab(Rigidbody target)
    {
        if (_held != null || target == null) return;
        _held = target;
        _held.useGravity = false;
        _held.interpolation = RigidbodyInterpolation.Interpolate;
        _held.transform.SetParent(holdPoint, worldPositionStays: false);
        _held.transform.localPosition = Vector3.zero;
        _held.transform.localRotation = Quaternion.identity;
        _held.GetComponent<BoxCollider>().enabled = false;
    }

    public void Drop()
    {
        if (_held == null) return;
        _held.useGravity = true;
        _held.GetComponent<BoxCollider>().enabled = true;
        _held.transform.SetParent(null);
        _held = null;
        
    }
}
