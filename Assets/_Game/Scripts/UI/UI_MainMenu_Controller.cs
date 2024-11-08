using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu_Controller : MonoBehaviour,IStateGame
{
    [SerializeField] Slider exp;
    [SerializeField] TextMeshProUGUI txtCoin,txtHightScore;
    [SerializeField] Button btnMute,btnChoiceSkinLeft,btnChoiceSkinRight, btnPlay;
    [SerializeField] TMP_InputField edtName;
    [SerializeField] Image muteImgCurent;
    [SerializeField] Sprite muteImg,notMuteImg;
    bool isMute = true;
    private void Awake()
    {
        btnMute.onClick.AddListener(UpdateMute);
        btnChoiceSkinLeft.onClick.AddListener(UpdateChoiceSkinLeft);
        btnChoiceSkinRight.onClick.AddListener(UpdateChoiceSkinRight);
        btnPlay.onClick.AddListener(StartGameButon);
        edtName.onEndEdit.AddListener(UpdateNamePlayer);
    }
    private void Start()
    {
        GameManager.Instance.AddStateGame(this);
    }
    public void UpdateEXP(float expPoint)
    {
        exp.value = expPoint;
        // tinh gioi han abc
    }

    public void UpdateCoin(float coin)=>txtCoin.text = coin.ToString("n0");
    public void UpdateHighScore(float hightScore) => txtHightScore.text = string.Format("Survival - Best: #{0}",hightScore);
    public void UpdateMute()
    {
        // logic mute
        isMute =!isMute;
        if(isMute)muteImgCurent.sprite = muteImg;
        else muteImgCurent.sprite = notMuteImg;
    }
    public void UpdateChoiceSkinLeft()
    {
        // logic skin left
    }
    public void UpdateChoiceSkinRight()
    {
        // logic skin right
    }
    public void StartGameButon()
    {
        // logic start game
        GameManager.Instance.OnStartGame();
        transform.GetChild(0).gameObject.SetActive(false);
        UI_Manager.instance.UIInGame.UpdateAliveUI(true);

    }
    public void UpdateNamePlayer(string name)
    {
        // logic update
        GameManager.Instance.OnChangeName(name);
    }

    public void StopGame()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    private void OnDestroy()
    {
        GameManager.Instance.RemoveStateGame(this);
    }

    public void StartGame()
    {
        
    }
}
