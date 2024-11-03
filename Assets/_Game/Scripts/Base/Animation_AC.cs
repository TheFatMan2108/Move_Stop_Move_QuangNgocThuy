
using UnityEngine;

public class Animation_AC : MonoBehaviour
{
   CharacterBase character;

    private void Awake()
    {
        character = GetComponentInParent<CharacterBase>();

    }

    public void Attack() =>character.Attack();
    public void TriggerCalled()=>character.TriggerCalled();
    public void HideCharacter()=>character.HideCharacter();
    public void DeadTrigger()=>character.DeadTrigger();
}
