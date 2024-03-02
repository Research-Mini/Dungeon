using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class GenerateNavMesh : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshSurface[] surfaces = null;
    void Start()
    {
        surfaces = gameObject.GetComponentsInChildren<NavMeshSurface>();

        foreach (var s in surfaces)
        {
            s.RemoveData();
            s.BuildNavMesh();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
