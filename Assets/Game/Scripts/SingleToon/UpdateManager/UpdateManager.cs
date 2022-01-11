using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpdateManager : SingleToon<UpdateManager>
{
    private Dictionary<string,IUpdate> m_updateProcessesDictionAry = new Dictionary<string, IUpdate>();


    private void Awake()
    {
        init();
    }


    private void Update()
    {
        foreach(IUpdate update in m_updateProcessesDictionAry.Values)
        {
            
            update.updateProcesses();
        }
    }

    public void addProcesses(IUpdate processes)
    {
        Debug.Log("Update Processes " + processes.id);
        m_updateProcessesDictionAry.Add(processes.id, processes);
    }

    public void removeProcesses(IUpdate processes)
    {
        Debug.Log("Remove Processes " + processes.id);
        m_updateProcessesDictionAry.Remove(processes.id);
    }


}
