using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void NewGameClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OptionsClicked()
    {

    }

    public void QuitClicked()
    {
        Application.Quit();
    }
}
