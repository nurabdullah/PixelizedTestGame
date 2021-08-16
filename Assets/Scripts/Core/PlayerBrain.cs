using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBrain : MonoBehaviour
{
    public List<Player> stackedPlayers;
    [SerializeField] private List<Transform> usableSpawnPoints;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private int countOnCircle;
    [SerializeField] private float space;
    public Action OnFinish;
    private Transform centerSpawnpoint;


    private void Awake()
    {
        SetSpawnPoints();
        SpawnPlayer();
    }

    private void Start()
    {
        Finish.Instance.playerBrain = this;
    }

    public void AddPlayer(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnPlayer();
        }
    }


    public void SubstractPlayer(int count)
    {
        for (int i = 0; i < count; i++)
        {
            DeletePlayer();
        }
    }

    public void MultiplyPlayer(int count)
    {
        AddPlayer(stackedPlayers.Count * (count - 1));
    }

    public void DividePlayer(int count)
    {
        //Debug.Log($"DividePlayer : {count}");
        SubstractPlayer(Mathf.FloorToInt(stackedPlayers.Count / count));
    }

    private void SetSpawnPoints()
    {
        SpawnPoint(transform.position);
        for (int i = 0; i < countOnCircle; i++) //8
        {
            SpawnPoint(CalculatePlayerPos(transform.position, space, i, countOnCircle)); //(center, radius, index, coundOnCircle)
        }

        for (int i = 0; i < countOnCircle * 2; i++) //16
        {
            SpawnPoint(CalculatePlayerPos(transform.position, space * 2, i, countOnCircle * 2));
        }

        for (int i = 0; i < countOnCircle * 3; i++)
        {
            SpawnPoint(CalculatePlayerPos(transform.position, space * 3, i, countOnCircle * 3));
        }

        for (int i = 0; i < countOnCircle * 4; i++)
        {
            SpawnPoint(CalculatePlayerPos(transform.position, space * 4, i, countOnCircle * 4));
        }
        for (int i = 0; i < countOnCircle * 5; i++)
        {
            SpawnPoint(CalculatePlayerPos(transform.position, space * 5, i, countOnCircle * 6));
        }
        for (int i = 0; i < countOnCircle * 6; i++)
        {
            SpawnPoint(CalculatePlayerPos(transform.position, space * 6, i, countOnCircle * 7));
        }
        for (int i = 0; i < countOnCircle * 7; i++)
        {
            SpawnPoint(CalculatePlayerPos(transform.position, space * 7, i, countOnCircle * 8));
        }
        for (int i = 0; i < countOnCircle * 8; i++)
        {
            SpawnPoint(CalculatePlayerPos(transform.position, space * 8, i, countOnCircle * 9));
        }
    }

    private void SpawnPoint(Vector3 point) {
        GameObject spawnPoint = new GameObject("SpawnPoint");
        spawnPoint.transform.parent = transform;
        spawnPoint.transform.position = point;
        usableSpawnPoints.Add(spawnPoint.transform);
    }

    private void SpawnPlayer()
    {
        GameObject spawnedPlayer = Instantiate(playerPrefab, usableSpawnPoints[0]); //(prefab , parent) // (prefab, pos, rot)
        usableSpawnPoints.Remove(usableSpawnPoints[0]);
        Player player = spawnedPlayer.GetComponent<Player>();
        stackedPlayers.Add(player);
    }

    private void DeletePlayer()
    {
        Player selectedPlayer = stackedPlayers[stackedPlayers.Count - 1];
        usableSpawnPoints.Add(selectedPlayer.transform.parent);
        stackedPlayers.Remove(selectedPlayer);
        Destroy(selectedPlayer.gameObject);
        if (stackedPlayers.Count <= 0)
        {
            Time.timeScale = 0;           
            Debug.Log("Fail");
        }
    }

    public void DeletePlayer(Player player)
    {
        usableSpawnPoints.Add(player.transform.parent);
        stackedPlayers.Remove(player);
        Destroy(player.gameObject);
        if(stackedPlayers.Count <= 0)
        {
            Time.timeScale = 0;
            Debug.Log("Fail");
        }
    }

    Vector3 CalculatePlayerPos(Vector3 center, float radius, int index, int _countOnCircle)
    {
        float ang = (360 / _countOnCircle) * index;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        return pos;
    }

   
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Booster"))
        {
            Booster booster = other.gameObject.GetComponent<Booster>();
            if (booster)
            {
                switch (booster.boostType)
                {
                    case BoostType.Add:
                        AddPlayer(booster.count);
                        break;
                    case BoostType.Substract:
                        SubstractPlayer(booster.count);
                        break;
                    case BoostType.Multiply:
                        MultiplyPlayer(booster.count);
                        break;
                    case BoostType.Divide:
                        DividePlayer(booster.count);
                        break;
                }
            }
            Destroy(booster);
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            OnFinish?.Invoke();
            Finish.Instance.FinishTrigger();
        }
    }
}
