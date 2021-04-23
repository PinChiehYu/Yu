using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfoController : MonoBehaviour
{
    public Animator itemAnimator;
    protected GameEventListener m_eventListener;
    // Start is called before the first frame update
    void Start()
    {
        m_eventListener = new GameEventListener();
        m_eventListener.ListenForEvent(EGameEvents.ShowMapItemUI, OnShowMapItemUI);
    }

    private EventResult OnShowMapItemUI(object eventData)
    {
        EventResult eventResult = new EventResult(false);
        itemAnimator.SetBool("Got", true);
        StartCoroutine(resume());
        return eventResult;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator resume()
    {
        yield return new WaitForSeconds(2);
        itemAnimator.SetBool("FadeOut", true);
    }
}
