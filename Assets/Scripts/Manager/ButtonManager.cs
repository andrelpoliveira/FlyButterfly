using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject missions;
    public GameObject store;
    public GameObject fecharMission;
    public GameObject fecharStore;

    public void openMissions()
    {
        missions.SetActive(true);
    }

    public void openStore()
    {
        store.SetActive(true);
    }

    public void LoadScenes(string cena)
    {
        SceneManager.LoadScene(cena);
    }

    public void btnCloseMission()
    {
        fecharMission.SetActive(true);
        missions.SetActive(false);        
    }
    public void btnCloseStore()
    {
        fecharStore.SetActive(true);        
        store.SetActive(false);
    }
}
