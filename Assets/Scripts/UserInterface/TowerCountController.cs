using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerCountController : MonoBehaviour
{
    private TowerManager _towerManager;
    private TextMeshPro _textMesh;

    private int _towerCount;
    private int _maxTowers;

    // Start is called before the first frame update
    void Start()
    {
        _textMesh = GetComponentInChildren<TextMeshPro>();

        //subscribe to events
        _towerManager = TowerManager.GetInstance();
        _towerManager.m_OnTowerBuilt.AddListener(OnTowerBuilt);
        _towerManager.m_OnMaxTowersChanged.AddListener(OnMaxTowersChanged);
    }

    void OnTowerBuilt(int numTowers)
    {
        _towerCount = numTowers;
        UpdateTextMesh();
    }

    void OnMaxTowersChanged(int maxTowers)
    {
        _maxTowers = maxTowers;
        UpdateTextMesh();
    }

    void UpdateTextMesh()
    {
        if (!_textMesh) return;
        _textMesh.text = _towerCount.ToString() + " / " + _maxTowers.ToString();
    }
}
