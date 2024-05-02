using System.Net;

public static class HttpWebRequestExtensions
{
    /// <summary>
    /// 針對Header追加JWT Token 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    public static void SetJwtAuthorization(this HttpWebRequest request, string token)
    {
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Set("Authorization", $"Bearer {token}");
        }
    }
}