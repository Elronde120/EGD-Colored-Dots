using System.IO;

public static class JSONFileIO 
{
    public static void SaveToFile(string path, string JSONString)
    {
        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteAsync(JSONString);
        writer.Close();
    }
    
    public static string ReadFile(string path)
    {
        StreamReader reader = new StreamReader(path);
        string returnValue = reader.ReadToEnd();
        reader.Close();
        return returnValue;
    }
}
