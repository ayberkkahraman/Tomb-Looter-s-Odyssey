using System;
using System.Collections;
using System.Collections.Generic;
using Project._Scripts.Runtime.Library.Controller;
using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Project._Scripts.Runtime.Managers.ManagerClasses
{
  [DefaultExecutionOrder(580)]
  public class DialogueManager : MonoBehaviour
  {
    public UIManager UIManager { get; set; }
    public DialogueData DialogueData { get; set; }
    public bool IsDialogueActive;

    [Range(0f, .5f)]public float DialogueTypeDuration = .025f;
    [HideInInspector] public Queue<string> Sentences;
    public delegate void OnDialogueStart(DialogueData dialogueData);
    public OnDialogueStart OnDialogueStartHandler;
    public delegate void OnDialogueEnd(DialogueData dialogueData);
    public OnDialogueEnd OnDialogueEndHandler;

    public DialogueData.Dialogue CurrentDialogue{ get; set; }
    public DialogueData CurrentDialogueData{ get; set; }
    
    [Serializable]
    public struct Dialogue
    {
      public string Name;
      public DialogueData DialogueData;
      public UnityEvent OnDialogueStartEvent;
      public UnityEvent OnDialogueEndEvent;
    }

    public List<Dialogue> Dialogues;
    public static DialogueData GetDialogueData(string name) => Instance.Dialogues.Find(x => x.Name == name).DialogueData;

    public static DialogueManager Instance;

    private void Awake()
    {
      Instance = this;
      Sentences = new Queue<string>();
    }
    private void Start()
    {
      UIManager = ManagerContainer.Instance.GetInstance<UIManager>();

      CurrentDialogueData = GetDialogueData("Fall");
      CurrentDialogue = GetDialogueData("Fall").Dialogues[0];
      
      UpdateDialogueData(CurrentDialogueData);
    }

    private void OnEnable()
    {
      OnDialogueEndHandler += EndDialogue;
      OnDialogueStartHandler += StartDialogue;
    }

    private void OnDisable()
    {
      OnDialogueEndHandler -= EndDialogue;
      OnDialogueStartHandler -= StartDialogue;
    }

    public void Update()
    {
      if (InputController.Interact().HasInputTriggered() && !IsDialogueActive) StartDialogue(CurrentDialogueData);
      
      if (!IsDialogueActive) return;

      if (!InputController.Jump().HasInputTriggered()) return;

      if(Sentences.Count == 0 && CurrentDialogueData.DialogueOptionData != null) return;
      
      DisplayNextSentence();
    }

    public void StartSpesificDialogue(DialogueData dialogueData)
    {
      OnDialogueStartHandler(dialogueData);
    }
    
    public void StartSpesificDialogueAfterSecond(DialogueData dialogueData)
    {
      ManagerContainer.Instance.GetInstance<BehaviourManager>().RunAfterSeconds(1f, () =>
      {
        OnDialogueStartHandler(dialogueData);
      });
    }
    

    public void StartDialogue(DialogueData dialogueData)
    {
      CurrentDialogueData = dialogueData;

      UIManager.UpdateDialogueNameText(CurrentDialogue.CharacterData.Name);
      UIManager.UpdateDialogueImage(CurrentDialogue.CharacterData.Image);
      
      UpdateDialogueData(dialogueData);

      UIManager.DialogueBox.SetActive(true);

      IsDialogueActive = true;

      DisplayNextSentence();
      Dialogues.Find(x => x.DialogueData == CurrentDialogueData).OnDialogueStartEvent?.Invoke();
    }
    
    public void UpdateDialogueData(DialogueData dialogueData)
    {
      DialogueData = dialogueData;
      Sentences = new Queue<string>();
      DialogueData.Dialogues.ForEach(x => Sentences.Enqueue(x.Sentence));
    }

    public void EndDialogue(DialogueData dialogueData)
    {
      IsDialogueActive = false;
      UIManager.DialogueText.text = "";
      UIManager.DialogueBox.SetActive(false);
      Dialogues.Find(x => x.DialogueData == dialogueData).OnDialogueEndEvent?.Invoke();
    }
    
    public void DisplayNextSentence()
    {
      if(Sentences.Count == 0 && CurrentDialogueData.DialogueOptionData == null)
      {
        OnDialogueEndHandler(DialogueData);
        return;
      }
      
      string sentence = Sentences.Dequeue();
      CurrentDialogue = DialogueData.Dialogues.Find(x => x.Sentence == sentence);
      
      UIManager.UpdateDialogueNameText(CurrentDialogue.CharacterData.Name);
      UIManager.UpdateDialogueImage(CurrentDialogue.CharacterData.Image);

      StopAllCoroutines();
      StartCoroutine(TypeSentence(sentence));
    }

    public void InitializeOptions()
    {
      UIManager.SetDialogueOptions(CurrentDialogueData.DialogueOptionData.DialogueOptions.Length);
    }

    public IEnumerator TypeSentence(string sentence)
    {
      var text = "";
      foreach(char letter in sentence)
      {
        text += letter;
        yield return new WaitForSeconds(DialogueTypeDuration);
        // if(i%3 == 0)ManagerContainer.Instance.GetInstance<AudioManager>().PlayAudio("Type");
        UIManager.UpdateDialogueText(text);
      }

      if (Sentences.Count != 0)
        yield break;
      
      if (CurrentDialogueData.DialogueOptionData != null)
      {
        InitializeOptions();
      }
    }
  }
}
