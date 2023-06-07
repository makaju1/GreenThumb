using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tipToShow;
    public float timeToWait = 0.5f;
    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(StarTimer());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        HoverTipManager.OnMouseLoseFocus();
    }
    private void ShowMessage()
    {
        HoverTipManager.OnMouseHover(tipToShow,Input.mousePosition);
    }

    private IEnumerator StarTimer()
    {
        yield return new WaitForSeconds(timeToWait);
        ShowMessage();
    }
    public void SetMessage(string message)
    {
        tipToShow = message;
    }
}
