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

    [FirestoreProperty]
    public int count { get; set; } // Tracks how many times the restaurant has been added

    public Restaurant() {
        image_url = "";
        name = "";
        phone = "";
        rating = 0.0;
        review_count = 0;
        yelp_url = "";
        price = "";
        full_address = "";
        count = 0; // Initialize count to 0
    }

    public Restaurant(string url, string name, string phone, double rating, int review_count, string yelp_url, string price, string fullAddress) {
        image_url = url;
        this.name = name;
        this.phone = phone;
        this.rating = rating;
        this.review_count = review_count;
        this.yelp_url = yelp_url;
        this.price = price;
        full_address = fullAddress;
        count = 1; // Initialize count to 1 when creating a new restaurant entry
    }
}
