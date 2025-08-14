using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using System.Text;
using System;

public class APIClient : MonoBehaviour
{
    public static APIClient instance { get; private set; }
    private const string BASE_URL = "http://localhost:3000/api";

    private void Awake()
    {
        instance = this;
    }

    public async UniTask<bool> Register(string userId, string password)
    {
        string url = $"{BASE_URL}/register";
        string json = JsonUtility.ToJson(new User { userId = userId, password = password });
        
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            try 
            {
                await request.SendWebRequest();
                
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"회원가입 오류: {request.error}");
                    return false;
                }
                
                Debug.Log("회원가입 성공");
                return true;
            }
            catch (UnityWebRequestException ex)
            {
                Debug.LogError($"회원가입 요청 실패: {ex.Message}");
                return false;
            }
        }
    }

    public async UniTask<bool> Login(string userId, string password)
    {
        string url = $"{BASE_URL}/login";
        string json = JsonUtility.ToJson(new User { userId = userId, password = password });
        
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            try 
            {
                await request.SendWebRequest();
                
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"로그인 오류: {request.error}");
                    return false;
                }
                
                Debug.Log("로그인 성공");
                return true;
            }
            catch (UnityWebRequestException ex)
            {
                Debug.LogError($"로그인 요청 실패: {ex.Message}");
                return false;
            }
        }
    }

    public async UniTask<UserInfo> GetUserInfo(string userId)
    {
        try
        {
            string url = $"{BASE_URL}/userInfo?userId={userId}";

            var request = new UnityWebRequest(url, "GET");
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            var operation = await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var response = JsonUtility.FromJson<ApiResponse>(request.downloadHandler.text);
                if (response.success)
                {
                    UserInfo userInfo = response.userInfo;
                    Debug.Log($"UserInfo: {userInfo.userId}, {userInfo.maxHp}, {userInfo.attackDamage}, {userInfo.bossGrade}");
                    return userInfo;
                }
                else
                {
                    Debug.LogError($"Failed to get user info: {request.error}");
                    return null;
                }
            }
            else
            {
                Debug.LogError($"Error: {request.error}");
                Debug.LogError($"Response: {request.downloadHandler.text}");
                return null;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Exception in GetUserInfo: {ex.Message}");
            return null;
        }
    }

    public async UniTask<bool> SaveInfo(string userId, float maxHp, float attackDamage, int bossGrade)
    {
        string url = $"{BASE_URL}/saveInfo";
        string json = JsonUtility.ToJson(new UserInfo { userId = userId, maxHp = maxHp, attackDamage = attackDamage, bossGrade = bossGrade });
        
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            try 
            {
                await request.SendWebRequest();
                
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"저장 오류: {request.error}");
                    return false;
                }
                
                Debug.Log("저장 성공");
                return true;
            }
            catch (UnityWebRequestException ex)
            {
                Debug.LogError($"저장 요청 실패: {ex.Message}");
                return false;
            }
        }
    }

    [System.Serializable]
    public class User
    {
        public string userId;
        public string password;
    }

    [System.Serializable]
    public class ApiResponse
    {
        public bool success;
        public string message;
        public UserInfo userInfo;
    }

    [System.Serializable]
    public class UserInfo
    {
        public string userId;
        public float maxHp;
        public float attackDamage;
        public int bossGrade;
    }
}
