using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LevelSelect()
    {
        SceneManager.LoadScene(1);
    }
    public void OptionsMenu()
    {
        SceneManager.LoadScene(2);
    }
}
    