using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    private string gameId = "4156865";
    private bool testMode = false;

    public Action onVideoEnd;


    void Start()
    {
        // Initialize the Ads service:
        Advertisement.Initialize(gameId, testMode);
        Advertisement.AddListener(this);
    }

    public void ShowInterstitialAd()
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video");
            // Replace mySurfacingId with the ID of the placements you wish to display as shown in your Unity Dashboard.
        }
        else
        {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }

    public void ShowRewardedAd()
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo");
            // Replace mySurfacingId with the ID of the placements you wish to display as shown in your Unity Dashboard.
        }
        else
        {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        //Debug.Log($"{placementId} ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log($"Error {message}");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log($"{placementId} started");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(placementId == "rewardedVideo" && showResult == ShowResult.Finished)
        {
            onVideoEnd?.Invoke();
        }
    }
}
