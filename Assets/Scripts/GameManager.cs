using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    [SerializeField] private Selectable firstitemtoselect;
    RigidBody rb;

    private void Awake()
    {
    }

    private void Start()
    {
        if (eventSystem == null) 
            return;
        
        eventSystem.firstSelectedGameObject = firstitemtoselect.gameObject;
    }

    private void Update()
    { 
        
    }

    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    
}
