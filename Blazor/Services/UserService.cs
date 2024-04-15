public class UserService
{
    public bool IsLoggedIn { get; private set; }
    public string UserName { get; private set; }
    public string Id { get; private set; }

    public event Action OnChange;

    public void Login(string username, string id)
    {
        Id = id;
        UserName = username;
        IsLoggedIn = true;
        NotifyStateChanged();
    }

    public void Logout()
    {
        UserName = string.Empty;
        Id = string.Empty;
        IsLoggedIn = false;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
