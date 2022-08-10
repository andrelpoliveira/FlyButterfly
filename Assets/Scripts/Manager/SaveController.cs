using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SaveController : MonoBehaviour
{
    private SaveData data;
    private string path;

    private void Awake()
    {
        data = new SaveData();
        path = Application.persistentDataPath + "/data.json";
        data.mission_id = new List<int>();
        data.mission_max_value = new List<int>();
        data.reward = new List<int>();
        data.missionType = new List<MissionType>();
        data.mission_current_progress = new List<int>();
        data.is_complete = new List<bool>();

        if (File.Exists(path))
        {
            LoadPurchase();
            LoadMission();
        }
    }

    // carrega as skins do player
    private void LoadPurchase()
    {
        string json = File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(json, data);

        for (int i = 0; i < data.is_purchase.Count; i++)
        {
            GameController.instance.isBuying[i] = data.is_purchase[i];
        }
    }

    // salva as skins do player
    public void SavePurchase()
    {
        data.is_purchase = new List<bool>();
        for (int i = 0; i < GameController.instance.isBuying.Length; i++)
        {
            data.is_purchase.Add(GameController.instance.isBuying[i]);
        }

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    // salva o coin do player
    public void SaveCoin(int favo)
    {
        data.nectar = favo;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    // carrega o dinheiro do player
    public void LoadCoin()
    {
        string json = File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(json, data);
        GameController.instance.nectar_max = data.nectar;
    }

    // valores para salvar id, valor, recompensa, tipo
    public void SaveMission(int id, int value, int progress, int reward, MissionType type, bool is_complete)
    {
        data.mission_id.Add(id);
        data.mission_max_value.Add(value);
        data.reward.Add(reward);
        data.missionType.Add(type);
        data.mission_current_progress.Add(progress);
        data.is_complete.Add(is_complete);

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    // valores para carregar das missões
    public void LoadMission()
    {
        string json = File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(json, data);

        GameController.instance.id_current = data.mission_id[data.mission_id.Count - 1];
        GameController.instance.missions = new MissionBase[2];
        GameController.instance.id_mission = new int[2];
        int temp = 0;

        for (int i = 0; i < data.mission_id.Count; i++)
        {
            if (data.is_complete[i] == false)
            {
                print($"id da mission {data.mission_id[i]}");
                GameController.instance.is_mission = true;
                GameController.instance.id_mission[temp] = data.mission_id[i];
                temp++;
            }
        }

        for (int i = 0; i < GameController.instance.missions.Length; i++)
        {
            GameObject newMission = new GameObject("Mission" + i);
            newMission.transform.SetParent(transform);
            

            if (data.missionType[GameController.instance.id_mission[i]] == MissionType.SingleRun)
            {
                GameController.instance.missions[i] = newMission.AddComponent<SingleRun>();
            }
            else if (data.missionType[GameController.instance.id_mission[i]] == MissionType.TotalMeters)
            {
                GameController.instance.missions[i] = newMission.AddComponent<TotalMeters>();
            }
            else if (data.missionType[GameController.instance.id_mission[i]] == MissionType.NectarSingleRun)
            {
                GameController.instance.missions[i] = newMission.AddComponent<NectarSingleRun>();
            }
            print($"valor de i {i} type {data.missionType[GameController.instance.id_mission[i]]}");
            GameController.instance.missions[i].max = data.mission_max_value[GameController.instance.id_mission[i]];
            GameController.instance.missions[i].reward = data.reward[GameController.instance.id_mission[i]];
            GameController.instance.missions[i].currentProgress = data.mission_current_progress[GameController.instance.id_mission[i]];
            GameController.instance.missions[i].missionType = data.missionType[GameController.instance.id_mission[i]];
        }
    }

    // sobre escre se a missão terminou
    public void SaveMissionComplete(int id, bool is_complete)
    {
        data.is_complete[id] = is_complete;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    // sobre escre se o progresso da missão 
    public void SaveProgress(int id, int progress)
    {
        data.mission_current_progress[id] += progress;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }
}

public class SaveData
{
    public int nectar;                         // valor total  
    public List<int> mission_max_value;         // valor maximo da missão
    public List<int> mission_id;                // valor id da missão
    public List<int> mission_current_progress; // salva valor acumulado da missão das missões que acumula
    public List<int> reward;                  // valor de recompensas para o player
    public List<MissionType> missionType;    // tipo de missão
    public List<bool> is_complete;           // verifica se a missão esta completa
    public List<bool> is_purchase;          // verifica se a skin foi comprada
}