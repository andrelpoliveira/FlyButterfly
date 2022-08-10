using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    //[HideInInspector]
    public MissionBase[] missions;
    [HideInInspector]
    public int[] id_mission;
    [HideInInspector]
    public int id_current;

    public TMP_Text nectarText;

    [HideInInspector]
    public int nectar_max;
    //[HideInInspector]
    public int nectar_current;
    [HideInInspector]
    public int score_max;
    //[HideInInspector]
    public int score_current;
    [HideInInspector]
    public SaveController data;
    [HideInInspector]
    public AdsManager ads;
    [HideInInspector]
    public bool is_mission;


    [Header("---Controle de skin---")]
    public Animator anim_Current;
    public Animator butterfly;
    public bool[] isBuying;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }

        data = GetComponent<SaveController>();
    }

    private void Start()
    {
        // s� entra caso � tenha miss�o
        if (is_mission == false)
        {
            missions = new MissionBase[2];
            id_mission = new int[2];

            for (int i = 0; i < missions.Length; i++)
            {
                GameObject newMission = new GameObject("Mission" + i);
                newMission.transform.SetParent(transform);
                MissionType[] missionType = { MissionType.SingleRun, MissionType.NectarSingleRun, MissionType.TotalMeters };
                int randomType = Random.Range(0, missionType.Length);
                if (randomType == (int)MissionType.SingleRun)
                {
                    missions[i] = newMission.AddComponent<SingleRun>();
                }
                else if (randomType == (int)MissionType.TotalMeters)
                {
                    missions[i] = newMission.AddComponent<TotalMeters>();
                }
                else if (randomType == (int)MissionType.NectarSingleRun)
                {
                    missions[i] = newMission.AddComponent<NectarSingleRun>();
                }
                missions[i].Created();
                id_mission[i] = id_current;
                data.SaveMission(id_current, missions[i].max, missions[i].progress, missions[i].reward, missions[i].missionType, missions[i].GetMissionComplete());
                id_current++;
            }
        }

        data.LoadCoin();
        UpdateHUD();

    }

    public void UpdateHUD()
    {
        nectarText.text = nectar_max.ToString("N0");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1;
    }



    public void LoadScenes(string cena)
    {
        SceneManager.LoadScene(cena);
        Time.timeScale = 1;
    }

    public MissionBase GetMission(int index)
    {
        return missions[index];
    }

    public void StartMission()
    {
        for (int i = 0; i < 2; i++)
        {
            missions[i].RunStart();
        }
    }

    public void GenerateMission(int index)
    {
        Destroy(missions[index].gameObject);
        GameObject newMission = new GameObject("Mission" + index);
        newMission.transform.SetParent(transform);
        MissionType[] missionType = { MissionType.SingleRun, MissionType.NectarSingleRun, MissionType.TotalMeters };
        int randomType = Random.Range(0, missionType.Length);
        if (randomType == (int)MissionType.SingleRun)
        {
            missions[index] = newMission.AddComponent<SingleRun>();
        }
        else if (randomType == (int)MissionType.TotalMeters)
        {
            missions[index] = newMission.AddComponent<TotalMeters>();
        }
        else if (randomType == (int)MissionType.NectarSingleRun)
        {
            missions[index] = newMission.AddComponent<NectarSingleRun>();
        }
        missions[index].Created();
        id_current++;
        data.SaveMission(id_current, missions[index].max, missions[index].progress, missions[index].reward, missions[index].missionType, missions[index].GetMissionComplete());

        FindObjectOfType<MissionController>().SetMission();
    }

    public void SetChild()
    {
        for (int i = 0; i < missions.Length; i++)
        {
            missions[i] = transform.GetChild(i).GetComponentInChildren<MissionBase>();
        }
    }
}
