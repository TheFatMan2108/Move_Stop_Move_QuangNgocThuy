using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance { get; private set; }
    [SerializeField] GameObject joysitckController;
    [SerializeField] UI_InGame_Controller uiInGame;
    [SerializeField] UI_MainMenu_Controller ui_MainMenu_Controller;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    private void Reset()
    {
        uiInGame = GetComponentInChildren<UI_InGame_Controller>();
        ui_MainMenu_Controller = GetComponentInChildren<UI_MainMenu_Controller>();
        joysitckController = transform.GetChild(0).gameObject;
    }
    public void UpdateAlivePeopel(float amount) => uiInGame.UpdateAlivePeopel(amount);
    public void UpdateInfoEndGame(float top, string name, Color killedColor, float star, float coin) => uiInGame.UpdateInfoEndGame(top, name, killedColor, star, coin);
    public UI_InGame_Controller UIInGame { get { return uiInGame; } }
    public UI_MainMenu_Controller UIMainMenu { get { return ui_MainMenu_Controller; } }
}
