#if CODE_DONTCOMPILE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class TestScript : ItemBase
{
    protected override void ActionEvent()
    {

    }

    protected override void DeadEvent()
    {

    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SceneManager.LoadScene("Running");
        }
    }

}

#endif