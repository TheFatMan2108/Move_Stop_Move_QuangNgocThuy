
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SlotItem : MonoBehaviour
{
    [SerializeField] protected Image icon;
    [SerializeField] protected TextMeshProUGUI txtNameItem, txtPrice;
    [SerializeField] protected Button btnBuy;
    public int status;//0 = chưa mua, 1 = đã mua, 2 = đang sử dụng
    protected int price;
    protected int index;
    protected Action<int,int> updateStatus;
    public void UpdateStatus(int status)
    {
        btnBuy.onClick.RemoveAllListeners();
        switch (status)
        {
            case 0:
                btnBuy.GetComponent<Image>().color = Color.white;
                btnBuy.onClick.AddListener(Buy);
                break;
            case 1:
                btnBuy.GetComponent<Image>().color = Color.cyan;
                btnBuy.onClick.AddListener(Equitment);
                txtPrice.text = "Equitment";
                break;
            case 2:
                btnBuy.GetComponent<Image>().color = Color.gray;
                txtPrice.text = "Equipping";
                break;
            default:
                btnBuy.GetComponent<Image>().color = Color.white;
                btnBuy.onClick.AddListener(Buy);
                break;
        }
    }

    public virtual void Buy()
    {
        if (price <= GameManager.Instance.coin)
        {
            GameManager.Instance.UpdateCoin(-price);
            Equitment();
        }
        else
        {
            Debug.Log("Ban ngheo qua ban oi");
        }
    }
    public virtual void Equitment()
    {

    }
}
