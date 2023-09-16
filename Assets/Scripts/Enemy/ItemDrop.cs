using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class ItemDrop : MonoBehaviour
    {

        [SerializeField] private GameObject[] itemList;
        [SerializeField] private float spawnChance = 0.5f;
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        /*private void OnDestroy()
        {
            if (itemList.Length > 0 && spawnChance > Random.value)
            {
                int randomIndex = Random.Range(0, itemList.Length);
                Instantiate(itemList[randomIndex], gameObject.transform.position, Quaternion.identity);
            }
        }*/

        public void DropItem()
        {
            if (itemList.Length > 0 && spawnChance > Random.value)
            {
                int randomIndex = Random.Range(0, itemList.Length);
                Instantiate(itemList[randomIndex], gameObject.transform.position, Quaternion.identity);
            }
        }
    }
}
