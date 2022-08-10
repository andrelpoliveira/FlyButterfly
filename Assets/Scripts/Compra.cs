using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Compra : MonoBehaviour
{
    public List<GameObject> valoresBorboletas = new List<GameObject>();
    public Animator[] anim;
    public Animator butterfly;
    public ButtonManager btnFecharLoja;
    // Start is called before the first frame update
    void Start()
    {
        CheckIsBuying();
        GameController.instance.butterfly = butterfly;
        GameController.instance.butterfly.runtimeAnimatorController = GameController.instance.anim_Current.runtimeAnimatorController;
        btnFecharLoja = FindObjectOfType(typeof(ButtonManager)) as ButtonManager;
    }
        

    public void EquiparBorboleta(int id)
    {

       GameController.instance.anim_Current = anim[id];
       GameController.instance.butterfly.runtimeAnimatorController = GameController.instance.anim_Current.runtimeAnimatorController;
       btnFecharLoja.btnCloseStore();
    }

    public void Comprar(int id)
    {
        TMP_Text temp = valoresBorboletas[id].GetComponentInChildren<TMP_Text>();
        if (temp.text.Equals("equipar") || temp.text.Equals("free"))
        {
            EquiparBorboleta(id);
            return;
        }
        if (GameController.instance.nectar_max >= int.Parse(temp.text))
        {
            GameController.instance.nectar_max -= int.Parse(temp.text);
            GameController.instance.UpdateHUD();
            temp.text = "equipar";
            GameController.instance.isBuying[id] = true;
            GameController.instance.data.SaveCoin(GameController.instance.nectar_max);
            GameController.instance.data.SavePurchase();
        }
    }
    public void CheckIsBuying()
    {
        for (int i = 0; i < GameController.instance.isBuying.Length; i++)
        {
            if (GameController.instance.isBuying[i] == true)
            {
                valoresBorboletas[i].GetComponentInChildren<TMP_Text>().text = "equipar";
            }
        }
    }
}
