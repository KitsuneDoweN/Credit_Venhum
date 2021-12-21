using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpdate 
{
    public string id { get; }
    public void updateProcesses();
}
