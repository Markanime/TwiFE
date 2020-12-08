using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Channel : Poolable
{
    public IEnumerator GetTexture(Image i)
    {
        //string url = string.Format("https://www.twitch.tv/{0}", id);
        string url = string.Format("https://m.twitch.tv/{0}/profile", name);
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            webRequest.certificateHandler = new AcceptAllCertificates();
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                ///requestErrorOccurred = true;
            }
            else
            {
                string urlImage = FindImageOnHTML(webRequest.downloadHandler.text);
                yield return GetTexture(urlImage, i);
            }

            yield return 0;
        }
    }

    public string FindImageOnHTML(string html)
    {
        foreach (string text in html.Split('"'))
            if (text.Contains("jtv_user_pictures") && text.Contains("profile_image"))
                return text;
        return string.Empty;
    }
}
