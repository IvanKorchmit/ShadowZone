using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransitionEffect : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public static TransitionEffect Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartFading() => animator.SetTrigger("FadeStart");
}
 