using Google.Cloud.Firestore;
using System.Collections.Generic;

[FirestoreData]
public class User : IFirestoreEntity {
    [FirestoreProperty]
    public string Id { get; set; }

    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty]
    public string Email { get; set; }
    
    [FirestoreProperty]
    public string UserName { get; set; }

    [FirestoreProperty]
    public List<Restaurant> LikedRestaurants { get; set; }

    [FirestoreProperty]
    public Dictionary<string, int> CategoryCounts { get; set; } // Tracks category interactions

    public User() {
        Id = "";
        Name = "";
        Email = "";
        UserName = "";
        LikedRestaurants = new List<Restaurant>();
        CategoryCounts = new Dictionary<string, int>();
    }

    // Adding this constructor to match the signup parameters
    public User(string id, string name, string email, string userName) {
        Id = id;
        Name = name;
        Email = email;
        UserName = userName;
        LikedRestaurants = new List<Restaurant>();
        CategoryCounts = new Dictionary<string, int>();
    }

    public void AddRestaurant(Restaurant res) {
        LikedRestaurants.Add(res);
    }
}
