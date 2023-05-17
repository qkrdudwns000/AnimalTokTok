using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi.Events;


public class GPGSBinder
{
    static GPGSBinder inst = new GPGSBinder();
    public static GPGSBinder Inst => inst;



    ISavedGameClient SavedGame =>
        PlayGamesPlatform.Instance.SavedGame;

    IEventsClient Events =>
        PlayGamesPlatform.Instance.Events;



    void Init()
    {
        var config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        //EnableSavedGames() = 클라우드 저장 기능
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }


    public void Login(Action<bool, UnityEngine.SocialPlatforms.ILocalUser> onLoginSuccess = null)
    {
        Debug.Log("GPGS의 로그인 함수가 호출 되었음");
        Init();
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (success) =>
        {
            onLoginSuccess?.Invoke(success == SignInStatus.Success, Social.localUser);
        });
    }

    public void Logout()
    {
        PlayGamesPlatform.Instance.SignOut();
    }

    //SavedGame.OpenWithAutomaticConflictResolution(): 
    //          열다 함께  자동    갈등     해결
    public void SearchCloud(string fileName, Action istrue = null, Action isfalse = null)
    {
        SavedGame.OpenWithAutomaticConflictResolution(fileName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, (status, game) =>
        {
            if (status == SavedGameRequestStatus.Success)
            {
                #region TEST
                // 파일이 이미 있음
                Debug.Log($"{fileName} 이라는 클라우드가 있음");
                // 있다면 파일안에 데이터 불러오기
                LoadCloud(fileName, (success, data) => Debug.Log($"닉네임 파일 저장된 문자는 {data} 입니다."));
                #endregion
                istrue?.Invoke();
            }
            else
            {
                // 파일이 없음
                Debug.Log($"{fileName} 이라는 클라우드가 없음");
                isfalse?.Invoke();
            }
        });
    }

    public void SaveCloud(string fileName, string saveData, Action<bool> onCloudSaved = null)
    {
        SavedGame.OpenWithAutomaticConflictResolution(fileName, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLastKnownGood, (status, game) =>
            {
                if (status == SavedGameRequestStatus.Success)
                {
                    var update = new SavedGameMetadataUpdate.Builder().Build();
                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(saveData);
                    SavedGame.CommitUpdate(game, update, bytes, (status2, game2) =>
                    {
                        onCloudSaved?.Invoke(status2 == SavedGameRequestStatus.Success);
                    });
                }
            });
    }

    public void LoadCloud(string fileName, Action<bool, string> onCloudLoaded = null)
    {
        SavedGame.OpenWithAutomaticConflictResolution(fileName, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLastKnownGood, (status, game) =>
            {
                if (status == SavedGameRequestStatus.Success)
                {
                    SavedGame.ReadBinaryData(game, (status2, loadedData) =>
                    {
                        if (status2 == SavedGameRequestStatus.Success)
                        {
                            string data = System.Text.Encoding.UTF8.GetString(loadedData);
                            onCloudLoaded?.Invoke(true, data);
                        }
                        else
                            onCloudLoaded?.Invoke(false, null);
                    });
                }
                else
                {
                    Debug.Log("클라우드 로드 실패");
                }
            });
    }

    public void DeleteCloud(string fileName, Action<bool> onCloudDeleted = null)
    {
        SavedGame.OpenWithAutomaticConflictResolution(fileName,
            DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, (status, game) =>
            {
                if (status == SavedGameRequestStatus.Success)
                {
                    SavedGame.Delete(game);
                    onCloudDeleted?.Invoke(true);
                }
                else
                    onCloudDeleted?.Invoke(false);
            });
    }

    public void ShowAchievementUI() =>
        Social.ShowAchievementsUI();

    public void UnlockAchievement(string gpgsId, Action<bool> onUnlocked = null) =>
        Social.ReportProgress(gpgsId, 100, success => onUnlocked?.Invoke(success));

    public void IncrementAchievement(string gpgsId, int steps, Action<bool> onUnlocked = null) =>
        PlayGamesPlatform.Instance.IncrementAchievement(gpgsId, steps, success => onUnlocked?.Invoke(success));


    public void ShowAllLeaderboardUI() =>
        Social.ShowLeaderboardUI();

    public void ShowTargetLeaderboardUI(string gpgsId) =>
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(gpgsId);

    public void ReportLeaderboard(string gpgsId, long score, Action<bool> onReported = null) =>
        Social.ReportScore(score, gpgsId, success => onReported?.Invoke(success));

    public void LoadAllLeaderboardArray(string gpgsId, Action<UnityEngine.SocialPlatforms.IScore[]> onloaded = null) =>
        Social.LoadScores(gpgsId, onloaded);

    public void LoadCustomLeaderboardArray(string gpgsId, int rowCount, LeaderboardStart leaderboardStart,
        LeaderboardTimeSpan leaderboardTimeSpan, Action<bool, LeaderboardScoreData> onloaded = null)
    {
        PlayGamesPlatform.Instance.LoadScores(gpgsId, leaderboardStart, rowCount, LeaderboardCollection.Public, leaderboardTimeSpan, data =>
        {
            onloaded?.Invoke(data.Status == ResponseStatus.Success, data);
        });
    }


    public void IncrementEvent(string gpgsId, uint steps)
    {
        Events.IncrementEvent(gpgsId, steps);
    }

    public void LoadEvent(string gpgsId, Action<bool, IEvent> onEventLoaded = null)
    {
        Events.FetchEvent(DataSource.ReadCacheOrNetwork, gpgsId, (status, iEvent) =>
        {
            onEventLoaded?.Invoke(status == ResponseStatus.Success, iEvent);
        });
    }

    public void LoadAllEvent(Action<bool, List<IEvent>> onEventsLoaded = null)
    {
        Events.FetchAllEvents(DataSource.ReadCacheOrNetwork, (status, events) =>
        {
            onEventsLoaded?.Invoke(status == ResponseStatus.Success, events);
        });
    }

}