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
        timer += Time.deltaTime;
        if(timer > timeToBreak)
        {   
            foreach(var piece in breakablePieces)
            {   
                piece.SetActive(true);
                piece.transform.parent = null;
            }
            OnObjectBroke?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
