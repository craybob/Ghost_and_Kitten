using System;
using UnityEngine;

public class CatPresenter
{
    private readonly CatView _view;
    private readonly CatModel _model;
    private bool _upsetRaised;

    public event Action<float> OnHealthChanged;
    public event Action OnCatUpset;

    public CatPresenter(CatView view, CatModel model)
    {
        _view = view;
        _model = model;
    }

    public float Mood => _model.Mood; // для GameManager

    public void OnUpdate() { /* пока нечего делать по кадрам */ }

    public void OnHit(string tag, Vector3 contactNormal)
    {
        if (tag != "Danger") return;

        _model.Mood = Mathf.Clamp(_model.Mood - 5f, 0f, 100f);
        OnHealthChanged?.Invoke(_model.Mood);

        // рикошет
        _view.Bounce(new Vector3(0,1f ,-1f ), _model.KnockbackForce);

        if (_model.Mood <= 0f && !_upsetRaised)
        {
            _model.IsUpset = true;
            _upsetRaised = true;
            OnCatUpset?.Invoke(); // поражение по событию
        }
    }
}
