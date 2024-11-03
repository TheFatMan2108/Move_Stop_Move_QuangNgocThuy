
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UICharacter : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI nameCharacter,levelCharacter;

    public void SetName(string name)=>nameCharacter.text = name;
    public void SetLevel(string level)=>levelCharacter.text = level;
    void Update()
    {
        transform.LookAt(transform.position+Camera.main.transform.rotation*Vector3.forward,Camera.main.transform.rotation*Vector3.up);
    }
}
