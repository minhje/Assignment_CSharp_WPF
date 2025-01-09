namespace Business.Helpers;

public static class IdGenerator
{
    public static string GenerateUniqueId() => Guid.NewGuid().ToString();
}
