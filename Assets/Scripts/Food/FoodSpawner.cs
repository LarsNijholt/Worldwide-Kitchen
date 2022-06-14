using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Food
{
    public class FoodSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _asiaFoodList = new List<GameObject>();
        [SerializeField] private List<Transform> _asiaSpawnPoints = new List<Transform>();

        [SerializeField] private List<GameObject> _europeFoodList = new List<GameObject>();
        [SerializeField] private List<Transform> _europeSpawnPoints = new List<Transform>();

        [SerializeField] private List<GameObject> _oceaniaFoodList = new List<GameObject>();
        [SerializeField] private List<Transform> _oceaniaSpawnPoints = new List<Transform>();

        [SerializeField] private List<GameObject> _africaFoodList = new List<GameObject>();
        [SerializeField] private List<Transform> _africaSpawnPoints = new List<Transform>();
        private void Awake()
        {
            SpawnFood(_asiaFoodList, _asiaSpawnPoints);
            SpawnFood(_europeFoodList, _europeSpawnPoints);
            SpawnFood(_africaFoodList, _africaSpawnPoints);
            SpawnFood(_oceaniaFoodList, _oceaniaSpawnPoints);
        }

        /// <summary>
        /// Spawns the food gameobjects and takes a random index.
        /// </summary>
        private void SpawnFood(List<GameObject> AreaFoodList, List<Transform> AreaSpawnList)
        {
            List<int> RandomList = new List<int>();
            for (int i = 0; i < AreaFoodList.Count; i++)
            {
                GameObject FoodItem = Instantiate<GameObject>(AreaFoodList[i].gameObject);
            }
            for (int itr = 0; itr < AreaSpawnList.Count; itr++)
            {
                print(itr);
                if (itr == AreaFoodList.Count)
                {
                    return;
                }
                int random = Random.Range(0, AreaSpawnList.Count);
                if (!RandomList.Contains(random))
                {
                    RandomList.Add(random);
                    Transform spawnlocation = AreaSpawnList[random];
                    AreaFoodList[itr].GetComponent<Transform>().position = spawnlocation.position;
                }
                else
                {
                    itr--;
                }
            }
        }
    } 
}
