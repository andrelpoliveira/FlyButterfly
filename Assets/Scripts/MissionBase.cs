using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public enum MissionType
{
    SingleRun,TotalMeters, NectarSingleRun
}
public abstract class MissionBase : MonoBehaviour
{
    public int max; //quanto falta para completar a missão
    public int progress;
    public int reward;
    public butterfly butterfly;
    public int currentProgress;
    public MissionType missionType;

    public abstract void Created();
    public abstract string GetMissionDescription();
    public abstract void RunStart();
    public abstract void Update();

    public bool GetMissionComplete()
    {
        if((progress + currentProgress) >= max)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class SingleRun : MissionBase
{
    public override void Created()
    {
        missionType = MissionType.SingleRun;
        int[] maxValues = {100,200,200,400,500};
        int randomMaxValue = Random.Range(0, maxValues.Length);
        int[] rewards = { 50, 75, 100, 125, 150 };
        reward = rewards[randomMaxValue];
        max = maxValues[randomMaxValue];
        progress = 0;
    }

    public override string GetMissionDescription()
    {
        return $"Voe {max} M em uma fase";
    }

    public override void RunStart()
    {
        progress = 0;
        butterfly = FindObjectOfType<butterfly>();
    }

    public override void Update()
    {
        //if (butterfly == null) return;
        //progress = GameController.instance.score_current;
    }
}

public class TotalMeters : MissionBase
{
    public override void Created()
    {
        missionType = MissionType.TotalMeters;
        int[] maxValues = { 750, 1000, 1250, 1500, 1750 };
        int randomMaxValue = Random.Range(0, maxValues.Length);
        int[] rewards = { 300, 400, 500, 600, 700 };
        reward = rewards[randomMaxValue];
        max = maxValues[randomMaxValue];
        progress = 0;
    }

    public override string GetMissionDescription()
    {
        return $"Voe {max} M no total";
    }

    public override void RunStart()
    {
        progress += currentProgress;
        butterfly = FindObjectOfType<butterfly>();
    }

    public override void Update()
    {
        //if (butterfly == null) return;
        //currentProgress = GameController.instance.score_current;
    }
}

public class NectarSingleRun : MissionBase
{
    public override void Created()
    {
        missionType = MissionType.NectarSingleRun;
        int[] maxValues = { 50, 75, 100, 150, 200};
        int randomMaxValue = Random.Range(0, maxValues.Length);
        int[] rewards = { 100, 150, 200, 250, 300 };
        reward = rewards[randomMaxValue];
        max = maxValues[randomMaxValue];
        progress = 0;
    }

    public override string GetMissionDescription()
    {
        return $"colete o total de {max} nectares ";
    }

    public override void RunStart()
    {
        progress = 0;
        butterfly = FindObjectOfType<butterfly>();
    }

    public override void Update()
    {
        //if (butterfly == null) return;
        //progress = GameController.instance.nectar_current;
    }
}

