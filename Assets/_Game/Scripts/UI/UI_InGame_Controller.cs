using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_InGame_Controller : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] GameObject aliveUI, gameOverUI;
    [SerializeField] TextMeshProUGUI txtAlive,txtTop,txtName,txtStar,txtCoin;
    private void Reset()
    {
        aliveUI = transform.GetChild(0).gameObject;
        gameOverUI = transform.GetChild(1).gameObject;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        gameOverUI.SetActive(false);
        GameManager.Instance.BackToMain();
    }

    public void UpdateAlivePeopel(float amount)
    {
        txtAlive.text = string.Format("Alive: {0}", amount);
    }
    public void UpdateInfoEndGame(float top,string name,Color killedColor,float star,float coin)
    {
        txtName.text = name;
        txtName.color = killedColor;
        txtStar.text = star.ToString();
        txtCoin.text = coin.ToString();
        txtTop.text = string.Format("#{0}", top);  
        gameOverUI.SetActive(true);
        aliveUI.SetActive(false);
    }
    public void UpdateAliveUI(bool isActive)=>aliveUI.SetActive(isActive);
    public void UpdateGameOverUI(bool isActive) =>gameObject.SetActive(isActive);
}
