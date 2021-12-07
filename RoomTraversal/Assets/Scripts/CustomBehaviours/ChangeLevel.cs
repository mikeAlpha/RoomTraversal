using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class ChangeLevel : ActionTask
{
    public BBParameter<string> Level = "LevelName";
    public BBParameter<GameObject> Target;

    protected override void OnExecute()
    {
        Debug.Log(Target.value.GetComponentInParent<Grid>().name);
        Target.value.GetComponentInParent<Grid>().ChangeLevel(Level.value);
        EndAction(true);
    }
}
