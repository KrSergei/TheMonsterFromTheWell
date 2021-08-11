using UnityEngine;

public class SideWallCreator : MonoBehaviour
{
    public GameObject prefabWall;
    public float offsetBeetwenElementsWall = 8.66f;
    public float offsetBeetwenCentersAndWall = 20.5f;
    public int countElementsInWall = 10;

    private void Start()
    {
        float posYForSpawnElement = transform.position.y;

        for (int i = 0; i < countElementsInWall; i++)
        {
            //Создание стены слева
            Vector3 leftPosition = new Vector3()
            {
                x = transform.position.x - offsetBeetwenCentersAndWall,
                y = transform.position.y + posYForSpawnElement,
                z = transform.position.z
            };
            Instantiate(prefabWall.transform, leftPosition, Quaternion.identity).transform.parent = transform;

            //Создание стены справа
            Vector3 rightPosition = new Vector3()
            {
                x = transform.position.x + offsetBeetwenCentersAndWall,
                y = transform.position.y + posYForSpawnElement,
                z = transform.position.z
            };
            Instantiate(prefabWall, rightPosition, Quaternion.identity).transform.parent = transform;

            posYForSpawnElement -= offsetBeetwenElementsWall;
        }
    }
}
