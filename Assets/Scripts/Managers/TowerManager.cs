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

    private List<GameObject> currentTowers;
    public bool CanPlaceTowers;

    public UnityEvent<int> m_OnTowerBuilt;
    public UnityEvent<int> m_OnMaxTowersChanged;

    public void AddTower(GameObject tower)
    {
        currentTowers.Add(tower);
        CheckTowerCount();
        m_OnTowerBuilt?.Invoke(currentTowers.Count);
    }

    public void CheckTowerCount()
    {
        if (currentTowers.Count == _maxTowers)
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
        currentTowers = new List<GameObject>();
        Debug.Log("Tower Count: " + currentTowers.Count.ToString());
        Debug.Log("Max Tower: " + _maxTowers.ToString());

        CheckTowerCount();
    }
}
