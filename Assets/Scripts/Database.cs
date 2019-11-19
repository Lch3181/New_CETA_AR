using UnityEngine;
using Firebase.Storage;

public class Database : MonoBehaviour
{
    FirebaseStorage storage;
    StorageReference storage_ref;
    StorageReference images_ref;
    StorageReference videos_ref;

    // Start is called before the first frame update
    void Start()
    {
        storage = FirebaseStorage.DefaultInstance;
        storage_ref = storage.GetReferenceFromUrl("gs://ceta-ar-unity");
        images_ref = storage_ref.Child("Images");
        videos_ref = storage_ref.Child("Videos");
    }

}
