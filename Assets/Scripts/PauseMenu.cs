using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject OptionsMenu, QualityMenu;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
            StartCoroutine(Waiting());
        }
    }


    void Pause()
    {
        Time.timeScale = 0;
        OptionsMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        OptionsMenu.SetActive(false);

        if (QualityMenu.activeSelf)
        {
            QualityMenu.SetActive(false);
        }
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(1f); // Pause for 1 second
        while(true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Resume();
                
                break;
            }
            yield return new WaitForSeconds(1f); // Pause for 1 second
        }
    }
}
