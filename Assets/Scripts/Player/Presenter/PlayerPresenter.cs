using UnityEngine;

public class PlayerPresenter
{
    private readonly PlayerView _view;
    private readonly PlayerModel _model;

    public PlayerPresenter(PlayerView view, PlayerModel model)
    {
        _view = view;
        _model = model;
    }

    public void OnUpdate()
    {
        ApplyGravity();
        HandleInput();
        HandleGrab();
    }

    private void ApplyGravity()
    {
        _model.IsGrounded = _view.IsGrounded();
        _model.VerticalVelocity = _model.IsGrounded ? 0f : _model.VerticalVelocity - 1f;
        _view.Move(new Vector3(0f, _model.VerticalVelocity * 0.2f * Time.deltaTime, 0f));
    }

    private void HandleInput()
    {
        _model.InputX = Input.GetAxis("Horizontal");
        _model.InputZ = Input.GetAxis("Vertical");
        _model.Speed = new Vector2(_model.InputX, _model.InputZ).sqrMagnitude;

        if (_model.Speed > _model.AllowPlayerRotation)
        {
            _view.SetBlend(_model.Speed, _model.StartAnimTime);
            MoveAndRotate();
        }
        else
        {
            _view.SetBlend(_model.Speed, _model.StopAnimTime);
        }
    }

    private void MoveAndRotate()
    {
        var cam = _view.GetCamera();
        Vector3 f = cam.transform.forward; f.y = 0f; f.Normalize();
        Vector3 r = cam.transform.right; r.y = 0f; r.Normalize();

        _model.DesiredMoveDirection = f * _model.InputZ + r * _model.InputX;

        if (!_model.BlockRotationPlayer)
        {
            var t = _view.GetTransform();
            var targetRot = Quaternion.LookRotation(_model.DesiredMoveDirection);
            t.rotation = Quaternion.Slerp(t.rotation, targetRot, _model.DesiredRotationSpeed);

            // замедление от веса
            float w = _view.GetHeldWeight();
            float speedMod = 1f / (1f + w * 0.1f);
            _view.Move(_model.DesiredMoveDirection * Time.deltaTime * _model.Velocity * speedMod);
        }
    }

    private void HandleGrab()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_view.IsHolding) { _view.Drop(); }
            else
            {
                // простой поиск Ч ближайший Rigidbody с тегом Grabbable
                var colls = Physics.OverlapSphere(_view.GetTransform().position, 2f);
                Rigidbody best = null; float bestDist = float.MaxValue;

                foreach (var c in colls)
                {
                    if (!c.attachedRigidbody || !c.CompareTag("Grabbable")) continue;
                    float d = Vector3.SqrMagnitude(c.transform.position - _view.GetTransform().position);
                    if (d < bestDist) { bestDist = d; best = c.attachedRigidbody; }
                }
                _view.TryGrab(best);
                
            }
        }
    }
}
