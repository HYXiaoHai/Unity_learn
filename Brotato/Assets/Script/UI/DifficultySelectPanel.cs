using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelectPanel : MonoBehaviour
{
    public static DifficultySelectPanel Instance;

    public Transform _difficultyContent;
    public CanvasGroup _canvasGroup;

    private void Awake()
    {
        Instance = this;
        _difficultyContent = GameObject.Find("DifficultyContent").transform;
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
