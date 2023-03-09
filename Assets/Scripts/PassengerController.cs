using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerController : MonoBehaviour
{
    [SerializeField] private ScoreController scoreController;

    [SerializeField] private Hat hat;
    [SerializeField] private Top top;
    [SerializeField] private Pants pants;

    private Animator animator;
    [SerializeField] private string dieAnim;
    [SerializeField] private string idleAnim;

    private bool dead = false;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animator.Play(idleAnim);

        scoreController =  GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreController>();

        /*
        hat = (Hat)Random.Range(0, (int)Hat.hatCount);
        top = (Top)Random.Range(0, (int)Top.topCount);
        pants = (Pants)Random.Range(0, (int)Pants.pantsCount);
        */

        Debug.Log($"{hat}, {top}, {pants}, {dieAnim}");

        SetPosition();
    }

    public void Shot()
    {
        animator.Play(dieAnim);

        scoreController.GetKillScore(hat, top, pants);

        dead = true;
    }

    public void Remove()
    {
        if(dead == false)
            scoreController.GetPassScore(hat, top, pants);

        Destroy(gameObject);
    }

    // sprite마다 다른 이미지규격으로 인해 pivot설정 불가능. 태그(sprite수정자)에 따라 오브젝트 position값 변경으로 대체
    private void SetPosition()
    {
        Vector3 p = transform.position;

        if (gameObject.CompareTag("UO"))
        {
            p.y += 0.295f;

            transform.position = p;
        }
        else if(gameObject.CompareTag("UY"))
        {
            p.y += 0.39f;

            transform.position = p;
        }
    }
}
