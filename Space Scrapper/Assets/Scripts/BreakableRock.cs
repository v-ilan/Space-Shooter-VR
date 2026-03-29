using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakableRock : MonoBehaviour, IBreakable
{
    public UnityEvent OnObjectBroke;
    [SerializeField] private List<GameObject> breakablePieces;
    private float timeToBreak = 1.5f;
    private float timer = 0;

    private bool isBroken = false;

    // Start is called before the first frame update
    private void Start()
    {
        foreach(var piece in breakablePieces)
        {
            piece.SetActive(false);
        }
    }

    public void Break()
    {
        if(!isBroken)
        {
            timer += Time.deltaTime;
            if(timer > timeToBreak)
            {   Debug.Log("Break time: " + Time.time);
                isBroken = true;
                foreach(var piece in breakablePieces)
                {   
                    piece.SetActive(true);
                    piece.transform.parent = null;
                }
                OnObjectBroke?.Invoke();
                gameObject.SetActive(false);
                //Destroy(gameObject);
                Debug.Log("False time: " + Time.time);
            }
        }
    }

    // If the Timeline tries to force the cube back to life after I've broken...
    private void OnEnable()
    {
        gameObject.SetActive(!isBroken);
    }
}
