using UnityEngine;

public class showInfrastructure : MonoBehaviour
{
	public GameObject hospital;
    public GameObject busStop;
    public GameObject train;
    public GameObject nonScholaric;
    public GameObject basketball;
    public GameObject playground;
    public GameObject higherEducation;
    public GameObject lowerEducation;
    public GameObject skateTrack;
    public GameObject volleyball;
    public Marker marker;

    [SerializeField]
	public DataLoader dataLoader;
    // Start is called before the first frame update
    void Start()
    {
        // Maybe not needed?!
        marker = new Marker();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleInformation(string type)
    {
        
        GameObject parent = GameObject.Find(type);
        if (parent)
        {
            foreach (Transform child in parent.transform)
                child.gameObject.SetActive(!child.gameObject.activeSelf);
        } else
        {
            marker.CreateMarker(type, dataLoader);
        }
    }

    
}