using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Poolable : MonoBehaviour
{
    public string id;
    protected bool loaded = false;
    protected Texture2D texture;
    public Poolable Load(string id)
    {
        name = id;
        this.id = id;

        string poolName = string.Format("{0}_Pool", this.GetType().Name);
        var pool = GameObject.Find(poolName);
        pool = pool ? pool : new GameObject(poolName);

        var alreadyMade = pool.transform.Find(id);
        if (alreadyMade)
        {
            Destroy(gameObject);
            return alreadyMade.GetComponent<Poolable>();
        }
        else
        {
            gameObject.transform.parent = pool.transform;
        }
      
        return this;
    }

    public IEnumerator GetTexture(string url, Image i)
    {

        if (!loaded && !string.IsNullOrEmpty(url))
        {
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
                    texture = DownloadHandlerTexture.GetContent(webRequest);
                    loaded = true;
                    yield return new WaitForEndOfFrame();
                }

            }
        }
        if (loaded)
        {
            i.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
            i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        }
        else
        {
            i.sprite = null;
            i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        }
        yield return 0;
    }
}
