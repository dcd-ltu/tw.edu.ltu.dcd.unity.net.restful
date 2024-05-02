using UnityEngine;

public class RESTfulDemo : MonoBehaviour
{
    void Start()
    {
        
        string url = "http://zip5.5432.tw/zip5json.py?adrs=台北市重慶南路一段122號";
        ZipCode6 zipCode6 = RESTful.Get<ZipCode6>(url);
        Debug.Log($"name==>{zipCode6.zipcode6}");
        //see also: https://zip5.5432.tw/zip5api.html
    }

    void Update()
    {
        
    }
}
