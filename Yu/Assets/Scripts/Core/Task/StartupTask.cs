using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskState
{
    NotStarted,
    Running,
    Completed,
    Failed,
    Finished
}

public abstract class StartupTask
{
    public TaskState State { get { return m_state; } }

    protected TaskState state { get { return m_state; } }
    private TaskState m_state = TaskState.NotStarted;

    private StartupTask[] m_dependancies = null;



    protected abstract void OnStart();
    protected abstract void OnReset();
    protected abstract bool OnUpdate();
    protected abstract void OnDestroy();

#if DEBUG_UI
		private float m_startTime = 0;
#endif


    public StartupTask(StartupTask[] dependancies)
    {
        m_dependancies = dependancies;
    }

    public StartupTask(StartupTask dependancy)
    {
        if (dependancy != null)
        {
            m_dependancies = new StartupTask[1];
            m_dependancies[0] = dependancy;
        }
    }


    public bool Update()
    {
        switch (m_state)
        {
            case TaskState.NotStarted:
                if (AreDependsDone())
                {
                    m_state = TaskState.Running;
#if DEBUG_UI
						m_startTime = UnityEngine.Time.time;
#endif
                    OnStart();
                }
                return false;
            case TaskState.Failed:
                return false;
            case TaskState.Running:
                if (OnUpdate())
                {
                    Destroy();
                    return true;
                }
                break;
            case TaskState.Completed:
                Destroy();
                return true;
            case TaskState.Finished:
                return true;
        }

        return false;
    }


    public bool AreDependsDone()
    {
        if (m_dependancies == null)
        {
            return true;
        }

        StartupTask task;
        for (int i = 0; i < m_dependancies.Length; i++)
        {
            task = m_dependancies[i];

            if (task != null &&
                task.State != TaskState.Finished)
            {
                return false;
            }
        }

        return true;
    }

    public void Reset()
    {
        m_state = TaskState.NotStarted;
        OnReset();
    }


    public void Destroy()
    {
        OnDestroy();
        m_state = TaskState.Finished;
    }


    protected void ChangeState(TaskState newState)
    {
        if (m_state == newState)
        {
            return;
        }

        m_state = newState;

#if DEBUG_UI
			if(newState == TaskState.Completed)
			{
				float secsPassed = UnityEngine.Time.time - m_startTime;

				RiDebug.LogWarningFormat("Loading: {0} took {1} seconds", this.ToString(), secsPassed);
			}
#endif
    }
}