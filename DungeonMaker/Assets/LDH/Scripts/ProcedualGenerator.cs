using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
namespace LDH
{
    public class ProcedualGenerator : MonoBehaviour
    {
        public GameObject[] generators;
        public GameObject gen;
        public int[] x;
        public int[] y;
        public int count;
        // Start is called before the first frame update
        void Start()
        {
            generators = new GameObject[count];
            for (int i = 0; i < count; i++)
            {
                generators[i] = Instantiate(gen);
                //generators[i].GetComponent<DungeonGenerator>().startX = (x[i] - 1) * 15;
                //generators[i].GetComponent<DungeonGenerator>().startY = (1 - y[i]) * 15;
                generators[i].GetComponent<DungeonGenerator>().enabled = true;
            }

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}