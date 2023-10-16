using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TowerManager : Singleton<TowerManager>
{
    private int _maxTowers = 10;
    public int MaxTowers
    {
        get { return _maxTowers; }
        set 
        {
            _maxTowers = value;
            CheckTowerCount();
            m_OnMaxTowersChanged?.Invoke(_maxTowers);
        }
    }

    private GameObject[] currentTowers;
    public bool CanPlaceTowers;

    public UnityEvent<int> m_OnTowerBuilt;
    public UnityEvent<int> m_OnMaxTowersChanged;

    public void AddTower(GameObject tower)
    {
        currentTowers.Append(tower);
        CheckTowerCount();
        m_OnTowerBuilt?.Invoke(currentTowers.Length);
    }

    public void CheckTowerCount()
    {
        if (currentTowers.Length == _maxTowers)
        {
            CanPlaceTowers = false;
        }
        else
        {
            CanPlaceTowers = true;
        }
    }

    private void Start()
    {
        m_OnTowerBuilt?.Invoke(currentTowers.Length);
        m_OnMaxTowersChanged?.Invoke(_maxTowers);
    }
}
