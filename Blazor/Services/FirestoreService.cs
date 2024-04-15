using Google.Cloud.Firestore;

public class FirestoreService {
    private readonly FirestoreDb _db = null!;

    public FirestoreService(FirestoreDb db) {
        _db = db;
    }

    public async Task AddOrUpdate<T>(T entity) where T: IFirestoreEntity {
        var doc = _db.Collection(typeof(T).Name).Document(entity.Id);
        await doc.SetAsync(entity, cancellationToken: new CancellationToken());
    }

    public async Task<T> Get<T>(string id) where T: IFirestoreEntity {
        var doc = _db.Collection(typeof(T).Name).Document(id);
        var snap = await doc.GetSnapshotAsync(new CancellationToken());
        return snap.ConvertTo<T>();
    }
}