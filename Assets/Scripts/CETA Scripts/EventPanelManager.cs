using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class EventPanelManager : MonoBehaviour
{
    public GameObject eventEntry;
    public GameObject scrollContent;
    private FirebaseDatabase database;

    /// Start is called before the first frame update
    void Start()
    {
        DBinit();
    }

    /// <summary>
    /// initial Database
    /// 
    /// Set Connections, and check data update
    /// </summary>
    void DBinit()
    {
        //set connection
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://root-wharf-237820.firebaseio.com/");
        database = FirebaseDatabase.DefaultInstance;
        //check if data changes
        FirebaseDatabase.DefaultInstance.GetReference("Events").ValueChanged += HandleValueChanged;
    }

    /// <summary>
    /// Get all Event data from Database and Set into EventScript.cs 
    /// </summary>
    async void GetData()
    {
        //remove all old events
        foreach (Transform child in scrollContent.transform)
        {
            Destroy(child.gameObject);
        }
        //getdata once
        var snapshot = await database.GetReference("Events").OrderByChild("ID").GetValueAsync();

        //set each event
        foreach (DataSnapshot child in snapshot.Children)
        {
            GameObject newEvent = Instantiate(eventEntry) as GameObject;
            EventScript eventDetails = newEvent.GetComponent<EventScript>();

            eventDetails.setDate(child.Child("Date").Value.ToString());
            eventDetails.setStart(child.Child("DateTime").Value.ToString());
            eventDetails.setEnd(child.Child("EndDateTime").Value.ToString());
            eventDetails.setTitle(child.Child("Title").Value.ToString());
            eventDetails.setLoc(child.Child("Location").Value.ToString());
            eventDetails.setLink(child.Child("Website").Value.ToString());
            newEvent.GetComponent<Button>().onClick.AddListener(() => eventDetails.openLink());

            newEvent.transform.SetParent(scrollContent.transform, false);
        }
    }

    /// <summary>
    /// If and new update on event database, update event panel
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
