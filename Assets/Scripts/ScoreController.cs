using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private float hitCheck;
    private float timer;
    public bool GatheringScore;

    private void Start()
    {
        GameManager.Instance.OnStartGame += StartGame;
    }

    private void StartGame()
    {
        timer = hitCheck;
        GatheringScore = true;
        StartCoroutine(Cooldown());
    }

    private void OnCollisionEnter(Collision collision)
    {
        timer = hitCheck;
        GatheringScore = true;
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1f);
        timer--;
        if (timer <= 0) GatheringScore = false;
        StartCoroutine(Cooldown());
    }
}
