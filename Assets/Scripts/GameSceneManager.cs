using SFB;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour {
    public Component map;
    public Component actor;
    public Button button;
    
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start() {
      
        if (map != null)
            spriteRenderer = map.GetComponent<SpriteRenderer>(); 
        if (button != null)
            button.onClick.AddListener(OnClick); 
    }

    private void OnClick() {
         StandaloneFileBrowser.OpenFilePanelAsync("ѡ��ͼƬ", "", "png", false, (paths) => {
             // string path = EditorUtility.OpenFilePanel("ѡ��ͼƬ", "", "png,jpg");
             if (paths!=null&&paths.Length==1) {
                 SetImage(paths[0]);
                 actor.transform.position = Vector3.zero;
             }
             else {
                 Debug.Log("No file selected");
             }

         });
     
    }
    

    // Update is called once per frame
    
    private void SetImage(string path) {
        // ���ļ�·������Texture2D
        Texture2D texture = LoadTexture2D(path);

        // ʹ��Texture2D����һ��Sprite
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        // ����SpriteRenderer��Sprite����
        spriteRenderer.sprite = sprite;
    }
    private Texture2D LoadTexture2D(string path) {
        Texture2D texture = null;

        if (!string.IsNullOrEmpty(path)) {
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);
        }

        return texture;
    }
}
