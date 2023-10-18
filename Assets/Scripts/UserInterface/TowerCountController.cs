using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerCountController : MonoBehaviour
{
    private TowerManager _towerManager;
    private TextMeshProUGUI _textMesh;

    private int _towerCount;
    private int _maxTowers;

    // Start is called before the first frame update
    void Start()
    {
        //store reference to tower manager
        _towerManager = TowerManager.GetInstance();
        //set initial text
        _textMesh = GetComponentInChildren<TextMeshProUGUI>();
        _towerCount = 0;
        _maxTowers = _towerManager.MaxTowers;
        UpdateTextMesh();


        //subscribe to events

        _towerManager.m_OnTowerBuilt.AddListener(OnTowerBuilt);
        _towerManager.m_OnMaxTowersChanged.AddListener(OnMaxTowersChanged);
    }

    public void OnTowerBuilt(int numTowers)
    {
        Debug.Log("ui updated!");
        _towerCount = numTowers;
        UpdateTextMesh();
    }

    public void OnMaxTowersChanged(int maxTowers)
    {
        Debug.Log("ui updated!");
        _maxTowers = maxTowers;
        UpdateTextMesh();
    }

    public void UpdateTextMesh()
    {
        if (!_textMesh) return;
        _textMesh.SetText("{0} / {1}", _towerCount, _maxTowers);
    }
}
