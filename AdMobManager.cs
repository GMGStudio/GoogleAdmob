using GoogleMobileAds.Api;
using System;
using UnityEngine;

class AdMobManager : MonoBehaviour
{
    // Replace these IDs with your IDs vom https://admob.google.com/home/ 
    // These are just Test IDs
#if UNITY_IOS
    private string BannerID = "	ca-app-pub-3940256099942544/2934735716";
    private string InterstitialAdID = "ca-app-pub-3940256099942544/4411468910";
    private string RewardedAdID = "ca-app-pub-3940256099942544/1712485313";
#elif UNITY_ANDROID
    private string BannerID = "ca-app-pub-3940256099942544/6300978111";
    private string InterstitialAdID = "ca-app-pub-3940256099942544/1033173712";
    private string RewardedAdID = "ca-app-pub-3940256099942544/5224354917";
#endif


    private BannerView banner;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;

    public void Initialize()
    {
        MobileAds.Initialize(initStatus => { });
        CreateBanner();
        CreateInterstitial();
        CreateRewarded();
        RequestBanner();
        RequestInterstitialAd();
    }


    private void CreateBanner()
    {
        banner = new BannerView(BannerID, AdSize.Banner, AdPosition.Bottom);
        banner.OnAdFailedToLoad += HandleOnAdFailedToLoad;
    }

    private void CreateInterstitial()
    {
        interstitialAd = new InterstitialAd(InterstitialAdID);
        interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        interstitialAd.OnAdClosed += HandleOnAdClosed;
    }

    private void CreateRewarded()
    {
        rewardedAd = new RewardedAd(RewardedAdID);
        rewardedAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
    }



    private void RequestBanner()
    {
        AdRequest request = new AdRequest.Builder().Build();
        banner.LoadAd(request);
    }

    private void RequestInterstitialAd()
    {
        AdRequest request = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(request);
    }

    private void RequestRewardedAd()
    {
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
    }



    public void ShowBanner()
    {
        banner?.Show();
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }
    }

    public void ShowRewardedAd()
    {
        RequestRewardedAd();
    }

    public void HideAllBanners()
    {
        if (banner == null) { return; }
        banner.Hide();
        RequestBanner();
    }

    private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.LogError("Something went wron, " + sender + " couldn't load ");
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        rewardedAd.Show();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        AdsManager.instance.RewardedAdFinished();
    }

    private void HandleOnAdClosed(object sender, EventArgs args)
    {
        RequestInterstitialAd();
        ShowBanner();
    }

}