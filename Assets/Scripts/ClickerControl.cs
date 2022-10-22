using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClickerControl : MonoBehaviour
{
    private Image _sr;
    private float _timer = 0;
    private float _noteWindowLength = 2000f; // ms
    private bool _isTiming = false;
    private readonly float _perfectHitArea = 0.1f, _goodHitArea = 0.4f;
    private float _perfectLowerBound, _perfectUpperBound;
    private float _goodLowerBound, _goodUpperBound;

    // Start is called before the first frame update
    void Start()
    {
        _sr = GetComponent<Image>();
        _perfectLowerBound = _noteWindowLength * (0.5f - _perfectHitArea / 2);
        _perfectUpperBound = _noteWindowLength * (0.5f + _perfectHitArea / 2);
        _perfectLowerBound = _noteWindowLength * (0.5f - _goodHitArea / 2);
        _perfectUpperBound = _noteWindowLength * (0.5f + _goodHitArea / 2);
    }

    // Update is called once per frame
    void Update()
    {

        if (_isTiming)
        {
            _timer += Time.deltaTime * 1000;
            if (_timer >= _noteWindowLength)
            {
                _timer = 0;
                _isTiming = false;
                UIManager.Singleton.UpdateCombo("miss");
            }

            float t = Mathf.InverseLerp(0, _noteWindowLength, _timer);
            _sr.color = Color.Lerp(Color.white, Color.red, t);
        }
        else
        {
            _sr.color = Color.white;
        }
    }

    private bool WithinRange(float f, string condition)
    {
        if (condition == "perfect")
        {
            if (f <= _perfectUpperBound && f >= _perfectLowerBound)
            {
                return true;
            }

            return false;
        }

        if (condition == "good")
        {
            if (f <= _goodUpperBound && f >= _goodLowerBound)
            {
                return true;
            }
            return false;
        }
        return false;
    }
    
    public void Verdict()
    {
        if (_isTiming)
        {
            if (WithinRange(_timer, "perfect"))
            {
                UIManager.Singleton.UpdateScore("perfect");
            }

            if (WithinRange(_timer, "good"))
            {
                UIManager.Singleton.UpdateScore("good");
            }

            _isTiming = false;
            UIManager.Singleton.UpdateCombo("pass");
        }
    }

    public bool IsTiming
    {
        get => _isTiming;
        set => _isTiming = value;
    }
}
