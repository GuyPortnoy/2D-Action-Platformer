using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SpriteRenderer ChPtRenderer;

    public Sprite cpOn, cpOff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //on entering the trigger, if it was a player, turn off all checkpoints, activate the current one
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointController.instance.DeactivateCheckpoints();
            ChPtRenderer.sprite = cpOn;
            CheckpointController.instance.SetSpawnPoint(transform.position);
        }
    }

    public void ResetCheckpoint()
    {
        ChPtRenderer.sprite = cpOff;
    }
}
