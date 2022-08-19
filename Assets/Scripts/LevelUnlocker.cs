using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelUnlocker : MonoBehaviour
{
    #region Variables
    public List<Transform> progressBars = new List<Transform>();
    public List<Transform> levels = new List<Transform>();

    public LevelManager levelManager;
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
    public void LoadLevel(GameObject button)
    {
        SceneManager.LoadScene("Level " + button.GetComponent<TextMeshProUGUI>().text);
    }

    public void UnlockLevel(int level)
    {
        for(int i = 0; i < level; i++)
        {
            progressBars[i].Find("Bar").gameObject.SetActive(true);
            levels[i].Find("Lock").gameObject.SetActive(false);
        }
    }
    #endregion
}
