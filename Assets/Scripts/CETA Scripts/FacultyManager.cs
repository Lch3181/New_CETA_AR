using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class FacultyManager : MonoBehaviour
{
    //public GameObject Faculty;
    //public GameObject scrollContent;
    private FirebaseDatabase database;

    /// Start is called before the first frame update
    void Start()
    {
        DBinit();
    }

    /// <summary>
    /// initial Database connection
    /// </summary>
    void DBinit()
    {
        //set connection
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://root-wharf-237820.firebaseio.com/");
        database = FirebaseDatabase.DefaultInstance;
        //check if data changes
        FirebaseDatabase.DefaultInstance.GetReference("Faculty").ValueChanged += HandleValueChanged;
    }

    /// <summary>
    /// Get data from database and display on debug log for now until atucal use
    /// </summary>
    async void GetData()
    {
        //remove all old events
        //foreach (Transform child in scrollContent.transform)
        //{
        //    Destroy(child.gameObject);
        //}
        //getdata once
        var snapshot = await database.GetReference("Faculty").OrderByChild("Office Number").GetValueAsync();

        //set each event
        foreach (DataSnapshot child in snapshot.Children)
        {
            //GameObject newFaculty = Instantiate(Faculty) as GameObject;
            //EventScript eventDetails = newEvent.GetComponent<EventScript>();

            Debug.Log(
            "/Office Number: " +
            child.Child("Office Number").Value.ToString() +
            " /Job Type: " +
            child.Child("Job Type").Value.ToString() +
            " /Name: " +
            child.Child("First").Value.ToString() +
            "  " +
            child.Child("Last").Value.ToString()
            );
            //newEvent.GetComponent<Button>().onClick.AddListener(() => eventDetails.openLink());

            //newEvent.transform.SetParent(scrollContent.transform, false);
        }
    }

    /// <summary>
    /// Check if new update on Faculty database, update panel if any
    /// </summary>
    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        // Do something with the data in args.Snapshot
        else
        {
            //refresh data
            GetData();
        }
    }

}
