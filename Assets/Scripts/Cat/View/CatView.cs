using UnityEngine;

public class CatView : MonoBehaviour
{
    private CatPresenter _presenter;
    private Rigidbody _rb;

    private void Awake() => _rb = GetComponent<Rigidbody>();
    public void SetPresenter(CatPresenter presenter) => _presenter = presenter;

    private void OnCollisionEnter(Collision collision)
    {
        if (_presenter == null) return;
        var contact = collision.contacts[0];
        _presenter.OnHit(collision.gameObject.tag, contact.normal);
    }

    public void Bounce(Vector3 dir, float force)
    {
        _rb.AddForce(dir * force, ForceMode.Impulse);
    }
}
