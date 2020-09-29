using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public List<Button> buttons = new List<Button>();
    [SerializeField] private Sprite card_back;
    public Sprite[] all_images;
    public List<Sprite> used_images = new List<Sprite>();


    private bool guess1 = false ,guess2 = false;
    private string g1_id,g2_id;

    private int g1_index,g2_index;
    private bool cooldown = false;
    private int totalguess,countguess;
    void Start()
    {
        Store();
        AddClick();
        SelectImage();
        shuffle(used_images);
        totalguess = used_images.Count/2;
    } 
    void   Store()       //function to store all the buttons in list buttons
    {
        
        GameObject[] btn = GameObject.FindGameObjectsWithTag("PBTN");
        for(int j=0;j<btn.Length;j+=1)
        {
            buttons.Add(btn[j].GetComponent<Button>());
            buttons[j].image.sprite = card_back;
            buttons[j].GetComponent<Image>().type = Image.Type.Simple;
        }
    }
    void AddClick()     //function to add listeners to the buttons
    {
        foreach(Button i in buttons)
        {
            i.onClick.AddListener(()=> ClickAButton());
        }
    }
    void SelectImage()
    {
        int index =0;
        for(int i=0;i<buttons.Count;i+=1)
        {
            if(index == buttons.Count/2)
            {
                index = 0;
            }
            used_images.Add(all_images[index]);
            index+=1;
        }
    }
    void resetCooldown()        //cooldown to avoid click spam
    {
        cooldown = false;
    }
    public void ClickAButton()
    {
        
        if(cooldown == false)
        {
        if(guess1 == false)
        {
            g1_index = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            guess1 = true;
            buttons[g1_index].image.sprite = used_images[g1_index];
            g1_id = used_images[g1_index].name;
            cooldown = true;
            Invoke("resetCooldown",0.5f);
        }
        else if (guess2 == false)
        {
            g2_index = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            if(g2_index != g1_index)
            {
            guess2 = true;
            buttons[g2_index].image.sprite = used_images[g2_index];
            g2_id = used_images[g2_index].name;
            StartCoroutine(Check());
            }
            
        }
        }
        else
        {
            resetCooldown();
        }

    }

    void shuffle(List<Sprite> s)    //shuffle the cards randomly
    {
        for(int i=0;i<s.Count;i+=1)
        {
            Sprite temp = s[i];
            int r = Random.Range(i,s.Count);
            s[i] = s[r];
            s[r] = temp;
        }
    }
    IEnumerator Check()         //check if both the cards match or not
    {
            yield return new WaitForSeconds (0.5f);

            if(g1_id == g2_id)
            {
                yield return new WaitForSeconds(0.5f);
                buttons[g1_index].interactable = false;
                buttons[g2_index].interactable = false;

                buttons[g1_index].image.color = new Color(0,0,0,0);
                buttons[g2_index].image.color = new Color(0,0,0,0);
                Finished();
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
                buttons[g1_index].image.sprite = card_back;
                buttons[g2_index].image.sprite = card_back;
            }

            yield return new WaitForSeconds(0.5f);
            guess1 = false;
            guess2 = false;
        

    } 

    void Finished()         // Finish the game
    {
        countguess +=1;
        if(countguess == totalguess)
        {
            Debug.Log("FINISHED");
        }
    }

        
    
}
