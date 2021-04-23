using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager
{
    private StoryController storyController;

    public StoryManager(string referencePath)
    {
        storyController = Services.Get<ResourcesManager>().Spawn(referencePath).GetComponent<StoryController>();
    }

    public void ChangeStoryController(string referencePath)
    {
        if(storyController != null)
        {
            storyController.DestroySelf();
        }
        storyController = Services.Get<ResourcesManager>().Spawn(referencePath).GetComponent<StoryController>();
    }

    public void ClueProcess(object eventData)
    {
        storyController.ClueProcess(eventData);
    }

    public void NPCProcess(object eventData)
    {
        storyController.NPCProcess(eventData);
    }

    public void DoorProcess(object eventData)
    {
        storyController.DoorProcess(eventData);
    }

    public void StateProcess(object eventData)
    {
        storyController.StateProcess(eventData);
    }

    public void OpenUIProcess(object eventData)
    {
        storyController.OpenUIProcess(eventData);
    }

    public void ActionProcess(object eventData)
    {
        storyController.ActionProcess(eventData);
    }

    public void SetFlowVarString(string name, string value)
    {
        storyController.SetFlowVarString(name, value);
    }

    public void SetFlowVarBool(string name, bool value)
    {
        storyController.SetFlowVarBool(name, value);
    }

    public void SetFlowVarFloat(string name, float value)
    {
        storyController.SetFlowVarFloat(name, value);
    }

    public void Reset()
    {
        storyController.Reset();
    }
}
