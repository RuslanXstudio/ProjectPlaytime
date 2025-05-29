using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    public string CurrentScene;
    public List<GameObject> deathmessages;
    public GameObject GetUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        SceneManager.LoadScene(CurrentScene);
    }

    public void sendDeathMessage()
    {
        // Check if the list is not empty
        if (deathmessages.Count > 0)
        {
            // Choose a random index within the range of the list
            int randomIndex = Random.Range(0, deathmessages.Count);

            // Set the randomly chosen object to active
            deathmessages[randomIndex].SetActive(true);
        }
        else
        {
            Debug.LogWarning("The list of objects to activate is empty!");
        }
    }

    public void ClearDeathMessages()
    {
        foreach (GameObject obj in deathmessages)
        {
            obj.SetActive(false);
        }
        GetUp.SetActive(true);

    }
}
