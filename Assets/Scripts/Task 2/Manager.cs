using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public GameObject circlePrefab;
    public GameObject linePrefab;
    public Transform background;

    GameObject currentLine;
    void Start()
    {
        SpawnCircles(Random.Range(5, 11));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            currentLine = SpawnLine(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetMouseButtonUp(0))
            DestroyAll();
    }

    public void SpawnCircles(int random)
    {
        for(int i = 0; i < random; i++)
        {
            //Spawn position within the background area
            Vector2 position = new Vector2(Random.Range(-background.localScale.x/2, background.localScale.x/2), Random.Range(-background.localScale.y/2, background.localScale.y/2));
            Instantiate(circlePrefab, position, Quaternion.identity);
        }
    }

    public GameObject SpawnLine(Vector2 position)
    {
        return Instantiate(linePrefab, position, Quaternion.identity);
    }

    public void DestroyAll()
    {
        foreach (GameObject circle in currentLine.GetComponent<LineDrawer>().circles)
            Destroy(circle);
        Destroy(currentLine);
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
