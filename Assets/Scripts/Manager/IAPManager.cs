using UnityEngine;

public class IAPManager : MonoBehaviour
{

    public void Package500()
    {
        GameController.instance.nectar_max += 500;
        GameController.instance.data.SaveCoin(GameController.instance.nectar_max);
        GameController.instance.UpdateHUD();
    }
    
    public void Package800()
    {
        GameController.instance.nectar_max += 800;
        GameController.instance.data.SaveCoin(GameController.instance.nectar_max);
        GameController.instance.UpdateHUD();
    }
    
    public void Package1200()
    {
        GameController.instance.nectar_max += 1200;
        GameController.instance.data.SaveCoin(GameController.instance.nectar_max);
        GameController.instance.UpdateHUD();
    }
    
    public void Package2000()
    {
        GameController.instance.nectar_max += 2000;
        GameController.instance.data.SaveCoin(GameController.instance.nectar_max);
        GameController.instance.UpdateHUD();
    }
}
