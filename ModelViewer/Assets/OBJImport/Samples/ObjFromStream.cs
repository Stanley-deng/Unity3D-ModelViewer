using Dummiesman;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ObjFromStream : MonoBehaviour
{
    IEnumerator Start()
    {
        string url = "https://people.sc.fsu.edu/~jburkardt/data/obj/lamp.obj";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error downloading OBJ file: " + webRequest.error);
                yield break;
            }

            // Create stream and load
            var textStream = new MemoryStream(Encoding.UTF8.GetBytes(webRequest.downloadHandler.text));
            var loadedObj = new OBJLoader().Load(textStream);
        }
    }
}
