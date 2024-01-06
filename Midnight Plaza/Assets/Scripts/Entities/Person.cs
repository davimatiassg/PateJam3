using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Person", menuName = "Entities/new Person", order = 0)]
public class Person : ScriptableObject, IValuable, IGrabable, IDestructable {
    public Sprite sprite;
    public float speed;
    public Sprite worriedSprite;
    
    [SerializeField] private int scoreValue;
    public int ScoreValue { get {return this.scoreValue;}  set{this.scoreValue = value;} }

    public void getGrabed(){return;}
    public void getDestroyed(){return;}

    #if UNITY_EDITOR

    [CustomEditor(typeof(Person))]
    public class CardEditor : Editor
    {       
        public override Texture2D RenderStaticPreview(string assetPath, UnityEngine.Object[] subAssets, int width, int height)
        {
            Person person = target as Person;
            Texture2D newIcon = new Texture2D(width, height);
            if (person.sprite != null) {
                EditorUtility.CopySerialized(person.sprite.texture, newIcon);
                return newIcon;
            }
            return base.RenderStaticPreview(assetPath, subAssets, width, height);
        }
    }
    #endif
}
