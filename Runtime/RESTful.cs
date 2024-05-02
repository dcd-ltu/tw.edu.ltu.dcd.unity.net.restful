using System;
using System.IO;
using System.Net;
using UnityEngine;

public static class RESTful
{
    private static string _token;
    public static string Token => _token;

    public static void SetupToken(string token) //token設定
    {
        _token = token;
    }

    public static T Get<T>(string url) where T : new() //撈取資料庫
    {
        T result;
        try
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            if (!string.IsNullOrEmpty(_token))
            {
                httpWebRequest.SetJwtAuthorization(_token);
            }

            HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string json = reader.ReadToEnd();
                //json 反序列化
                result = JsonUtility.FromJson<T>(json);
            }
        }
        catch (Exception exp)
        {
            Console.WriteLine(exp.ToString());
            throw;
        }

        return result;
    }

    public static RESPONSE Post<REQUEST, RESPONSE>(string url, REQUEST model)
        where REQUEST : new() where RESPONSE : new() //上傳資料 帳密登入成功時回傳Jwt
    {
        RESPONSE result = default;
        try
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            if (!string.IsNullOrEmpty(_token))
            {
                httpWebRequest.SetJwtAuthorization(_token);
            }

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                // json 序列化
                string json = JsonUtility.ToJson(model);
                streamWriter.Write(json);
            }

            HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var json = streamReader.ReadToEnd();
                //json 反序列化
                result = JsonUtility.FromJson<RESPONSE>(json);
            }
        }
        catch (WebException exp)
        {
            HttpWebResponse httpResponse = (HttpWebResponse)exp.Response;

            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.OK: //200 表示成功登陸
                    break;
                case HttpStatusCode.Unauthorized: //401, 帳號或密碼錯誤
                    result = new RESPONSE();
                    //result.ErrorMsg = "帳號或密碼錯誤";
                    break;
                default:
                    break;
            }

            Debug.Log(exp.ToString());
        }
        catch (Exception exp)
        {
            Console.WriteLine(exp);
            throw;
        }

        return result;
    }
}