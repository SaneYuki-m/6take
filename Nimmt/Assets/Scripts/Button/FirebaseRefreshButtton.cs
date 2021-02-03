using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class FirebaseRefreshButtton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void FirevaseRefreshButton(){
        FirebaseDatabase.DefaultInstance
        .GetReference("GameRoom1")
        .GetValueAsync().ContinueWith(task => {
        if (task.IsFaulted) {
          // Handle the error...
          Debug.Log("fault");
        }
        else if (task.IsCompleted) {
          DataSnapshot snapshot = task.Result;
          // Do something with snapshot...
          Debug.Log(snapshot);

        }
      });
    }
}
