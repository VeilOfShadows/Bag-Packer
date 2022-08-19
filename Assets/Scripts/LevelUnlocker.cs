using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelUnlocker : MonoBehaviour
{
    #region Variables
    //public List<Transform> progressBars = new List<Transform>();
    [SerializeField]
    private List<Transform> levels = new List<Transform>();
    [SerializeField]
    private Image progressBar;

    [SerializeField]
    private LevelManager levelManager;
    #endregion

    #region Unity Methods
    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        if(levelManager.currentLevel > 0)
        {
            UnlockLevel(levelManager.currentLevel);
        }
    }
    #endregion

    #region Level Management
    private void LoadLevel(GameObject button)
    {
        SceneManager.LoadScene("Level " + button.GetComponent<TextMeshProUGUI>().text);
    }

    public void UnlockLevel(int level)
    {
        progressBar.fillAmount = 0.33f * level;
        for(int i = 0; i < level; i++)
        {
            //progressBars[i].Find("Bar").gameObject.SetActive(true);
            levels[i].Find("Lock").gameObject.SetActive(false);
        }
    }
    #endregion
}
