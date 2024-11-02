using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindState : IState<Bot>
{
    
    public void OnEnter(Bot t)
    {
        
        
    }

    public void OnExecute(Bot t)
    {
        t.ChangeAnim(Constants.RUN_ANIM_NAME);
       
    }

    public void OnExit(Bot t)
    {
       
    }
}
