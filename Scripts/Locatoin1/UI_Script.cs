using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Script : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject GrandButton;
    public GameObject _PauseBut;
    public GameObject FinalExitBut;

    public bool YesPause = false;

    public KeyCode Escape;

    public Script1 Sc1;
  
    void Start()
    {
        
    }

    public void Conitune()
    {
        GrandButton.SetActive (true);
        PausePanel.SetActive (false);

        if (Sc1.YesFinal == true)
        {
            FinalExitBut.SetActive(true);
        }
    }


    public void BackToMainMenu ()
    {
        SceneManager.LoadScene (0);
    }

    public void PauseBut()
    {
        if (YesPause == false)
        {
            _PauseBut.SetActive (false);
            GrandButton.SetActive (false);
            PausePanel.SetActive (true);
        }
    }

    
    void Update()
    {
        if (YesPause == false && Input.GetKey (Escape))
        {
            GrandButton.SetActive (false);
            PausePanel.SetActive (true);
        }

        
    }
}
