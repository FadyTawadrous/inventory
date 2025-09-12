public class UsersStateService
{
    public List<User> Users { get; private set; } = new List<User>();

    public event Action? OnChange;

    public void UpdateUsers(List<User> newUsers)
    {
        Users = newUsers;
        NotifyStateChanged();
        Console.WriteLine("UsersStateService Notfication sent.");
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}