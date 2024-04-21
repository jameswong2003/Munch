using Google.Cloud.Firestore;

[FirestoreData]
public class Restaurant {
    
    [FirestoreProperty]
    public string image_url { get; set; }

    [FirestoreProperty]
    public string name { get; set; }

    [FirestoreProperty]
    public string phone { get; set; }

    [FirestoreProperty]
    public double rating { get; set; }
    
    [FirestoreProperty]
    public int review_count { get; set; }

    [FirestoreProperty]
    public string yelp_url { get; set; }

    [FirestoreProperty]
    public string price { get; set; }

    [FirestoreProperty]
    public string full_address { get; set; }

    public Restaurant() {
        image_url = "";
        name = "";
        phone = "";
        rating = 0;
        review_count = 0;
        yelp_url = "";
        price = "";
        full_address = "";
    }

    public Restaurant(string url, string name, string phone, double rating, int review_count, string yelp_url, string price, string fullAddress) {
        image_url = url;
        name = name;
        phone = phone;
        rating = rating;
        review_count = review_count;
        yelp_url = yelp_url;
        price = price;
        full_address = fullAddress;
    }
}