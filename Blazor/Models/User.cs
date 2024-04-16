using client2.Pages;
using Google.Cloud.Firestore;

[FirestoreData]
public class User: IFirestoreEntity {

    [FirestoreProperty]
    public string Id { get; set; }

    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty]
    public string Email { get; set; }
    
    [FirestoreProperty]
    public string UserName { get; set; }

    [FirestoreProperty]
    public List<Restaurant> LikedRestaurants {get; set;}

    public User() {
        Id = "";
        Name = "";
        Email = "";
        UserName = "";
        LikedRestaurants = new List<Restaurant>{};
    }

    public User(string id, string name, string email, string userName) {
        Id = id;
        Name = name;
        Email = email;
        UserName = userName;
        LikedRestaurants = new List<Restaurant>();
    }

    public void AddRestaurant(Restaurant res) {
        LikedRestaurants.Add(res);
    } 
}