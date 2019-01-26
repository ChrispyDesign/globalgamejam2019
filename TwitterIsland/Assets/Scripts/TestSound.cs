using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{

    public void Test1()
    {
        SoundManager.instance.Play("Action_Hunt");
    }

    public void Test2()
    {
        SoundManager.instance.Play("Action_Build_Base");
    }

    public void Test3()
    {
        SoundManager.instance.Play("Action_Build_Church");
    }

    public void Test4()
    {
        SoundManager.instance.Play("Action_Harvest");
    }
}
