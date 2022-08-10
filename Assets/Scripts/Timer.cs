using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timeLevelTxt;
    public static bool stopTime;

    private bool is_finish;
    private float timer;
    private float delay;

    // Start is called before the first frame update
    void Start()
    {
        stopTime = false;
        delay = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Score();
    }
    public void Score()
    {
        if (stopTime == false)
        {
            timer += Time.deltaTime;

            if (timer > delay)
            {
                timer = 0;
                GameController.instance.score_current++;
                timeLevelTxt.text = GameController.instance.score_current.ToString("F0");
            }
        }
        else if (is_finish == false)
        {
            is_finish = true;
            for (int i = 0; i < GameController.instance.missions.Length; i++)
            {
                if (GameController.instance.missions[i].missionType == MissionType.TotalMeters)
                {
                    print($"total meters id {GameController.instance.id_mission[i]}");
                    GameController.instance.data.SaveProgress(GameController.instance.id_mission[i], GameController.instance.score_current);
                    GameController.instance.missions[i].currentProgress += GameController.instance.score_current;
                }
                else if (GameController.instance.missions[i].missionType == MissionType.NectarSingleRun)
                {
                    print($"nectar id {GameController.instance.id_mission[i]}");
                    GameController.instance.missions[i].currentProgress += GameController.instance.nectar_current;
                    GameController.instance.data.SaveProgress(GameController.instance.id_mission[i], GameController.instance.nectar_current);
                }
                else if (GameController.instance.missions[i].missionType == MissionType.SingleRun)
                {
                    print("single run");
                    GameController.instance.missions[i].currentProgress = GameController.instance.score_current;
                }
            }
        }
    }
}
