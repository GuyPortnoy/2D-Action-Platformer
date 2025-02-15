using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public static CheckpointController instance;
    [SerializeField] private Checkpoint[] checkpoints;
    [SerializeField] private Vector3 spawnPoint;
    //assign singleton
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    //on start, finds all checkpoint objects in the level and puts them into an array, sets player spawnpoint
    void Start()
    {
        checkpoints = FindObjectsOfType<Checkpoint>();

        spawnPoint = PlayerController.instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //turns off all checkpoints in the level
    public void DeactivateCheckpoints ()
    {
        for (int i =0; i<checkpoints.Length;i++)
        {
            checkpoints[i].ResetCheckpoint();
        }
    }
    //func gets coordinates and sets them as the new player spawn point
    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }
    public Vector3 getSpawnPoint()
    {
        return spawnPoint;
    }
}
