using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator _enemyAnimation;
    private int _charactersCount;

    private void Start()
    {
        Finish.Instance.OnFinish += OnFinish;

    }

    private void OnDisable()
    {
        Finish.Instance.OnFinish -= OnFinish;
    }


    private void OnFinish()
    {
        _enemyAnimation.SetBool("isEnemyFight ", true);

        if (Finish.Instance.playerBrain.stackedPlayers.Count < 10)
        {
            _enemyAnimation.SetBool("isEnemyWin", true);
        }
        else
        {
            _enemyAnimation.SetBool("isEnemyDeath", true);
        }
    }
}
