using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDistribution : MonoBehaviour
{
    [SerializeField]private Transform puzzlefield;
    

    [SerializeField]private GameObject b;
    private void Awake() {
        
        for(int i=0;i<8;i+=1)
        {
            GameObject btn = Instantiate(b);
            btn.name = ""+i;
            btn.transform.SetParent(puzzlefield,false);
    
        }
    }
}
