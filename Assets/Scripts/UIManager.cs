using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{
    public static UIManager Singleton;
    [SerializeField] private int bpm;
    private float _bps;
    private float _btime; // ms
    private float _timer = 0f;
    private TMP_Text _scoreText;
    private TMP_Text _comboText;
    private int _score = 0;
    private int _combo = 0;
    private ClickerControl[] _clickers;

    private void Awake()
    {
        Singleton = this;
        _bps = bpm / 60f;
        _btime = 1000 / _bps;
        _scoreText = GameObject.FindWithTag("score").GetComponent<TMP_Text>();
        _comboText = GameObject.FindWithTag("combo").GetComponent<TMP_Text>();
        var clickerParent = GameObject.FindWithTag("clickerParent").GetComponent<Transform>();
        var clickerCount = clickerParent.childCount;
        _clickers = new ClickerControl[clickerCount];
        for (var i = 0; i < clickerCount; i++)
        {
            _clickers[i] = clickerParent.GetChild(i).GetComponent<ClickerControl>();
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime * 1000;
        if (_timer >= _btime)
        {
            GenerateNote();
            _timer = 0;
        }
    }

    private void GenerateNote()
    {
        var index = Random.Range(0, _clickers.Length);
        if (!_clickers[index].IsTiming)
        {
            _clickers[index].IsTiming = true;
            print("clickers activated");
        }
        
    }
    public void UpdateScore(string condition)
    {
        if (condition == "perfect")
        {
            _score += 10;
        }

        _scoreText.text = _score.ToString();
        if (condition == "good")
        {
            
        }
    }

    public void UpdateCombo(string condition)
    {
        if (condition == "miss")
        {
            _combo = 0;
        }

        if (condition == "pass")
        {
            _combo += 1;
        }
        _comboText.text = _combo.ToString();
    }
    
    
}
