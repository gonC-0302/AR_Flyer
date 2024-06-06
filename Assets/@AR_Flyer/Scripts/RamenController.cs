using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class RamenController : MonoBehaviour
{
    [SerializeField]
    private GameObject _ramenMesh,_ramenFull,_ramenEmpty;
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField]
    private GameObject _word;

    IEnumerator Start()
    {
        SetUpEventTrigger();
        _ramenMesh.layer = LayerMask.NameToLayer("TransparentFX");
        yield return new WaitForSeconds(4f);
        _ramenMesh.layer = LayerMask.NameToLayer("Default");
        PlayHintWord();
    }

    private void SetUpEventTrigger()
    {
        EventTrigger trigger = gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventDate) => { OnTapRamen(); });
        trigger.triggers.Add(entry);
    }

    private void OnTapRamen()
    {
        _smoke.Play();
        _word.gameObject.SetActive(false);
        _ramenFull.SetActive(false);
        _ramenEmpty.SetActive(true);
    }

    private void PlayHintWord()
    {
        _word.transform.localScale = Vector3.zero;
        _word.SetActive(true);
        _word.transform.DOScale(1, 1.5f).SetEase(Ease.OutCubic);
    }
}
