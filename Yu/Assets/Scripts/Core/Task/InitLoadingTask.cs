using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitLoadingTask : Task
{
    private bool m_isLoadingFinish = false;

    public InitLoadingTask()
    {
        //TO DO
        //Load something by startup task
        //when all done, send EGameEvents.StartupInitFinished
        ListenForEvent(EGameEvents.StartupInitFinished, OnInitFinished);
    }

    public override void Pause()
    {
    }

    public override void Resume()
    {
    }

    public override void Show(bool show)
    {
       
    }

    public override void Update(float deltaTime)
    {
        if (m_isLoadingFinish == false)
        {
            Services.Get<EventManager>().SendEvent((int)EGameEvents.StartupInitFinished);
            m_isLoadingFinish = true;
        }
    }

    private EventResult OnInitFinished(object eventData)
    {
        EventResult eventresult = new EventResult(false);
        return eventresult;
    }

    private void SetSay()
    {

    }
}
