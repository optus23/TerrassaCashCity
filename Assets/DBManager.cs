using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DBManager : MonoBehaviour
{
    DatabaseReference reference;

    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://terrassacashcity.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        
        InitDB();
    }

    void InitDB()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;                    
                UnityEngine.Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
        });
    }

    public void ButtonTest()
    {
        writeNewUser("Marc");
    }

    private void writeNewUser(string name)
    {
        User user = new User(name);
        string json = JsonUtility.ToJson(user);

        reference.SetRawJsonValueAsync(json);
    }
}

public class User
{
    public string mellamo;

    public User(string username)
    {
        this.mellamo = username;
    }
}
