using UnityEngine;

public class Door : MonoBehaviour
{
    public string sceneToLoad;
    public int doorNumber;
    public int doorTarget;
    public Transform spawnLocation;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            //argghhh berantakan anjir gara gara persistence data
            PlayerController playerController = other.GetComponent<PlayerController>();
            Health playerHealth = other.GetComponent<Health>();
            DataManager.instance.SaveHealth(playerHealth.GetCurrentHealth());
            DataManager.instance.SaveSilk(playerController.GetCurrentSilk());

            ChangeSceneManager.instance.SetDoorTarget(doorTarget);
            ChangeSceneManager.instance.ChangeScene(sceneToLoad);
        }
    }

    public Vector2 GetSpawnLocation()
    {
        return spawnLocation.position;
    }
}
