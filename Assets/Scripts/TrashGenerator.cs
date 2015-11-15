using UnityEngine;
using System.Collections;

public class TrashGenerator : MonoBehaviour {

    public double delay = .5;
    private double remainingTime;

    public Vector2 origin;
    public Vector2 dimensions;

    public float spawnHeight;

    Object[] prefabs;

    public int totalTrash = 0;
    public int collectedTrash = 0;

    //The amount of trash needed to reach peak dirtiness.
    public int maxTrashToDirtiness = 50;

    //This proportion indicates how dirty the ocean is.
    public float trashProportion = 0;

    // Use this for initialization
    void Start()
    {
        string folderPath = Application.dataPath + "/TrashPrefabs";

        prefabs = Resources.LoadAll("TrashPrefabs");

        //Generate 5 pieces of trash to start.
        for (int i = 0; i < 5; i++)
        {
            generateGarbage();
        }
    }
	
	// Update is called once per frame
	void Update () {
        remainingTime -= Time.deltaTime;
        //If the timer expired, then create a new object.
        if (remainingTime < 0)
        {
            remainingTime = delay;
            generateGarbage();
        }
	}

    void generateGarbage()
    {
        totalTrash++;
        trashProportion = totalTrash / maxTrashToDirtiness;

        //Location the object will spawn at.
        Vector3 spawnPosition = new Vector3(Random.Range(origin.x, dimensions.x), spawnHeight, Random.Range(origin.y, dimensions.y));
        Debug.Log(spawnPosition);    


        //Create the actual object.
        GameObject newObject = (GameObject) Object.Instantiate(prefabs[Random.Range(0, prefabs.Length)], spawnPosition, new Quaternion());

        //If it doesn't have the trashscript, add it so that it collides with player.
        if (!newObject.GetComponent<TrashScript>())
        {
            newObject.AddComponent<TrashScript>();
        }
    }

    public void trashDestroyed()
    {
        totalTrash--;
        trashProportion = totalTrash / maxTrashToDirtiness;
        //Change color.
    }
}
