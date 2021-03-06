﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

/// <summary>
/// GetSet all the rooms information and rooms number from database
/// </summary>
public class RoomManager : MonoBehaviour
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
        FirebaseDatabase.DefaultInstance.GetReference("Rooms").ValueChanged += HandleValueChanged;
    }

    /// <summary>
    /// Get data from database and show to debug log until actual use
    /// </summary>
    async void GetData()
    {
        //remove all old events
        //foreach (Transform child in scrollContent.transform)
        //{
        //    Destroy(child.gameObject);
        //}
        //getdata once
        var snapshot = await database.GetReference("Rooms").OrderByChild("Floor").GetValueAsync();

        //set each event
        foreach (DataSnapshot child in snapshot.Children)
        {
            //GameObject newFaculty = Instantiate(Faculty) as GameObject;
            //EventScript eventDetails = newEvent.GetComponent<EventScript>();

            Debug.Log(
            "/Floor: " +
            child.Child("Floor").Value.ToString() +
            " /Room Number: " +
            child.Child("Room Number").Value.ToString() +
            " /Room Type: " +
            child.Child("Room Type").Value.ToString()
            );
            //newEvent.GetComponent<Button>().onClick.AddListener(() => eventDetails.openLink());

            //newEvent.transform.SetParent(scrollContent.transform, false);
        }
    }

    /// <summary>
    /// Check if any new updates, get data again if any new update
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
