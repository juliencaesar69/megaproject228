using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public Saves _Saves;

    public GameObject Warning_Panel;
    public GameObject ErrorPanel;

    public void New_Start()
    {
        if (_Saves._SavePoz == 0)
        {
            SceneManager.LoadScene (1);
        }
        else if (_Saves._SavePoz != 0)
        {
            Warning_Panel.SetActive (true);
        }
    }

    public void ResetStart()
    {
        _Saves.ResetSaves();
        SceneManager.LoadScene (1);
    }

    public void LoadSave()
    {
        if(_Saves._SavePoz != 0)
        {
            SceneManager.LoadScene (1);
        }
        else if(_Saves._SavePoz == 0)
        {
            ErrorPanel.SetActive (true);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
