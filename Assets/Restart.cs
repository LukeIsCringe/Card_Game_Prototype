using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("Main_Scene");
    }
}
