using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    [SerializeField]
    private AudioSource audio;
    [SerializeField]
    private ParticleSystem particles;
    [SerializeField]
    private GameObject canvas;

    public int currentLevel;

    private void Awake()
    {
        instance = FindObjectOfType<LevelManager>();
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
            currentLevel = 0;
        }
    }    

    public void CompleteLevel()
    {
        audio.Play();
        canvas.SetActive(true);
        particles.Play();
    }

    public void LoadLevelSelection()
    {
        canvas.SetActive(false);
        particles.Stop();
        particles.Clear();
        audio.Stop();

        currentLevel += 1;
        SceneManager.LoadScene("Menu");
    }
}
