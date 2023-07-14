using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AdReward))]
public class MobAdsRewarded : MonoBehaviour
{
    [SerializeField] private Button _Ad_Button;
    [SerializeField] private Button _Gems_Button;
    [SerializeField] private GameObject _Reward_Panel;
    private Action _Rew;
    private AdReward _Reward;
    private RewardedAd _Rewarded_Ad;

    private const string _RewarderAdID = "ca-app-pub-3940256099942544/5224354917";

    private void Awake()
    {
        MobileAds.Initialize(initStatus => {});
        _Reward = GetComponent<AdReward>();
    }

    private void OnEnable()
    {
        NewRewardedAd();

        _Ad_Button.onClick.AddListener(ShowRewardedAd);
        _Gems_Button.onClick.AddListener(SpendGems);
        
        GameEvents._Monster_Death += ShowButton;
    }

    private void Start()
    {
        ShowButton();
    }

    public void ShowRewardedAd()
    {
        if (_Rewarded_Ad.CanShowAd())
        {
            _Rewarded_Ad.Show();
        }
    }

    public void SpendGems()
    {
        if (PlayerData.SpendGems(5))
        {
            _Reward.RemoveObstacle();
            _Reward.CurrentGems();
            HideButton();
        }
            
    }

    private void Reward()
    {
        _Reward.AdGems();
        _Reward.RemoveObstacle();
    }

    private void NewRewardedAd()
    {
        if (_Rewarded_Ad != null)
        {
            _Rewarded_Ad.Destroy();
            _Rewarded_Ad = null;
        }

        _Rewarded_Ad = new RewardedAd(_RewarderAdID);
        AdRequest _request = new AdRequest.Builder().Build();
        _Rewarded_Ad.LoadAd(_request);
        RegisterEventHandlers(_Rewarded_Ad);
    }

    

    private void ShowButton()
    {
        if (_Reward.LevelGrid && _Reward.LevelGrid.LockedTiles.Count > 0)
        {
            int _index = Random.Range(0, _Reward.LevelGrid.LockedTiles.Count);
            float _offset = 1;
            _Reward.LockedTileIndex = _index;
            _Reward_Panel.SetActive(true);
            _Reward_Panel.transform.position = new Vector2(_Reward.LevelGrid.LockedTiles[_index].position.x,
                _Reward.LevelGrid.LockedTiles[_index].position.y + _offset);
        }
    }
    
    private void HideButton()
    {
        _Reward_Panel.SetActive(false);
    }

    private void OnDisable()
    {
        GameEvents._Monster_Death -= ShowButton;
    }
    
    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Reward();
            NewRewardedAd();
            HideButton();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            NewRewardedAd();
            HideButton();
        };
        ad.OnAdPaid += (AdValue adValue) =>
        {
        };
    }
}
