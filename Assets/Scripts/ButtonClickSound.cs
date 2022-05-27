using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClickSound : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        SoundHandler.Instance.PlayAudio(SoundEffect.Click);
    }
}
