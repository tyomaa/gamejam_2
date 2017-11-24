using UnityEngine;

public class DanceManager : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	    LoadLevel(1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LoadLevel(int level)
    {
        TextAsset levelAsset = Resources.Load<TextAsset>("Levels/" + level);
        var data = JsonUtility.FromJson<LevelData>(levelAsset.text);
        var go = new GameObject();
        go.AddComponent<BubbleGenerator>().Init(data.bubbles);
    }
}
