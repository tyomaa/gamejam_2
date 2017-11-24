using UnityEngine;
using UnityEngine.SceneManagement;

public class DanceManager : MonoBehaviour
{
	void Start()
	{
	    LoadLevel(1);
	}

    private void LoadLevel(int level)
    {
        TextAsset levelAsset = Resources.Load<TextAsset>("Levels/" + level);
        var data = JsonUtility.FromJson<LevelData>(levelAsset.text);
        var go = new GameObject();
        go.AddComponent<BubbleGenerator>().Init(data.bubbles);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("dance");
    }
}
