using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CheckBoxHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Sprite regularSprite;
    [SerializeField]
    private Sprite highlightSprite;
    [SerializeField]
    private Image imageComponent;

    [SerializeField]
    private UnityEvent<bool> onClickEvent;

    private bool IsActive = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        IsActive = !IsActive;

        imageComponent.sprite = IsActive ? highlightSprite : regularSprite;
        onClickEvent.Invoke(IsActive);
    }
}
