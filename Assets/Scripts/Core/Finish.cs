using System;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public static Finish Instance;
    private bool isFinish;
    public Action OnFinish;
    public PlayerBrain playerBrain;

    private void Awake()
    {
        Instance = this;
    }


    public void FinishTrigger()
    {
        if (isFinish)
        {
            return;
        }
        isFinish = true;
        OnFinish?.Invoke();

        if(playerBrain.stackedPlayers.Count > 10)
        {
            Debug.Log("Win");
        }
        else
        {
            Debug.Log("Fail");
        }
    }
}
