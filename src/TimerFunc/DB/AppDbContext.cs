

public class AppDbContext
{
    public AppDbContext()
    {
        // Initialize the database connection
    }

    public void SaveData()
    {
        // Save data to the database
        Thread.Sleep(1000);
        Console.WriteLine("Data saved successfully");
    }
}