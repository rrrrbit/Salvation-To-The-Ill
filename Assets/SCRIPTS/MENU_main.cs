using UnityEngine;
using UnityEngine.SceneManagement;

public class MENU_main : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressPlayBtn()
    {
        Play();
    }

    void Play()
    {
        SceneManager.LoadScene(1);
    }
}
