using UnityEngine;
using System.Collections;

public class OnLose : MonoBehaviour {


    public void QuitGame()
    {
        Application.Quit();
    }

    public void TryAgain()
    {
        Application.LoadLevel(0);
    }
}
