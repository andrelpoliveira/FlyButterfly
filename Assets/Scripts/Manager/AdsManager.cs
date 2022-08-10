using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private string play_store_id = "4867371";
    private string app_store_id = "4867370";
    private string system;

    public bool is_test_ads;

    private void Start()
    {
        if (!Advertisement.isInitialized)
        {
            Initialized();
        }

        GameController.instance.ads = this;
    }

    public void Initialized()
    {
        system = (Application.platform == RuntimePlatform.IPhonePlayer) ? app_store_id : play_store_id;
        Advertisement.Initialize(system, is_test_ads, this);
    }

    // advertisement inicializado com sucesso
    public void OnInitializationComplete()
    {
        print("esta certo");
    }

    // falha ao inicializar advertisement
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {

    }

    // ads carregado
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Advertisement.Show(placementId, this);
    }

    // falha ao carregar
    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        print($"falha ao carregar {error}");
    }

    // falha ao exibir
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        if (placementId.Equals("Rewarded_Android"))
        {
            PlayInterstital();
        }
        else
        {
            PlayReward();
        }
    }

    // Começou a exibir
    public void OnUnityAdsShowStart(string placementId)
    {
        Time.timeScale = 0;
    }

    // clicou quando estava exibindo
    public void OnUnityAdsShowClick(string placementId)
    {
        throw new System.NotImplementedException();
    }

    // terminou de exibir
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        GameController.instance.RestartGame();
    }

    // meus metodos
    public void PlayReward()
    {
        Advertisement.Load("Rewarded_Android", this);
    }

    public void PlayInterstital()
    {
        Advertisement.Load("Interstitial_Android", this);
    }
}
