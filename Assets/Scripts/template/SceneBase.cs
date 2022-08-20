using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SceneBase : MonoBehaviour
{
    public abstract void OnUnLoad();


    public abstract void OnLoad();


    public abstract void OnUpdate();
}
