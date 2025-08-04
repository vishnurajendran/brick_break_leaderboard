namespace BrickBreakerLeaderboard;

public static class InstanceRegistry
{
    private static Dictionary<Type, object> _instanceDict = new Dictionary<Type, object>();

    public static T Get<T>() where T : class
    {
        var type = typeof(T);
        if (_instanceDict.ContainsKey(type))
        {
            return (T)_instanceDict[type];
        }
        
        return null;
    }

    public static void Register<T>(T instance)
    {
        if(instance == null)
            return;
        
        var type = typeof(T);
        _instanceDict[type] = instance;
    }
}