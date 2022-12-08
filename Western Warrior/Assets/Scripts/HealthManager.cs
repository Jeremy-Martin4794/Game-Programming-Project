using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public const int maxHealth = 4;
    public int health = maxHealth;
    [SerializeField] Image[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Image heart in hearts)
        {
            heart.sprite = emptyHeart;
        }
        for(int i = 0; i < health; ++i)
        {
            hearts[i].sprite = fullHeart;
        }
    }
}
