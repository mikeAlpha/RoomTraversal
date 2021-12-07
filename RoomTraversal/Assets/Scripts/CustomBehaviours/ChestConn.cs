using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using System;

public class ChestConn : ActionTask
{

    public BBParameter<GameObject> Target;

    protected override void OnExecute()
    {
        string msgInfo = (string)Target.value.GetComponent<Blackboard>().GetVariable("Date").value +"T"+ Target.value.GetComponent<Blackboard>().GetVariable("Time").value + "Z";
        WebClient.instance.Send(msgInfo);
        if (!String.IsNullOrEmpty(WebClient.instance.ReceivedInfo))
        {
            Debug.Log("Chest Not empty");
            Target.value.GetComponent<Blackboard>().SetVariableValue("Info", (string)WebClient.instance.ReceivedInfo);
        }
        EndAction(true);
    }

    protected override void OnUpdate()
    {
        //if (!String.IsNullOrEmpty(WebClient.instance.ReceivedInfo))
        //{
        Debug.Log("Chest Not empty");
        Target.value.GetComponent<Blackboard>().SetVariableValue("Info", (string)WebClient.instance.ReceivedInfo);
        //}
    }
}
