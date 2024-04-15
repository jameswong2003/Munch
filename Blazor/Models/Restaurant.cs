using Google.Cloud.Firestore;

[FirestoreData]
public class Restaurant {
    
    [FirestoreProperty]
    public string Image_Url { get; set; }

    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty]
    public string Phone { get; set; }

    [FirestoreProperty]
    public double Rating { get; set; }

    [FirestoreProperty]
    public string FullAddress { get; set; }

    public Restaurant() {
        Image_Url = "";
        Name = "";
        Phone = "";
        Rating = 0;
        FullAddress = "";
    }

    public Restaurant(string url, string name, string phone, double rating, string fullAddress) {
        Image_Url = url;
        Name = name;
        Phone = phone;
        Rating = rating;
        FullAddress = fullAddress;
    }
}