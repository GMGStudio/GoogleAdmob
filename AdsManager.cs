using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public static AdsManager instance;

    private AdMobManager adMobManager;
    private string bannersShownKey = "bannersShown";
    private readonly int bannersShowForInterstitial = 5;

    private void Awake()
    {
        SingletonPattern();
        adMobManager = gameObject.AddComponent<AdMobManager>();
        adMobManager.Initialize();
    }

    public void HideBanners()
    {
        adMobManager.HideAllBanners();
    }

    public void ShowInterstitialPerhabs()
    {
        if (ShouldShowInterstitial())
        {
            adMobManager.HideAllBanners();
            adMobManager.ShowInterstitialAd();
            PlayerPrefs.SetInt(bannersShownKey, 0);
        }
        else
        {
            adMobManager.ShowBanner();
        }
    }

    public void ShowRewardedAd()
    {
        adMobManager.HideAllBanners();
        adMobManager.ShowRewardedAd();
    }

    public void RewardedAdFinished()
    {
        /* Call you Database and UI Manager here. 
         * 
        CoinManager.instance.AddCoins(200);
        MenuUIManager.instance.RewardedAdFinished();
        */

    }


    private bool ShouldShowInterstitial()
    {
        int bannersShown = PlayerPrefs.GetInt(bannersShownKey, 0);
        bannersShown += 1;
        PlayerPrefs.SetInt(bannersShownKey, bannersShown);
        return bannersShown >= bannersShowForInterstitial;
    }

    private void SingletonPattern()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
