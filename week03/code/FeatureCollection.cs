public class FeatureCollection
{
    // Represents the entire earthquake JSON response.
    // The JSON contains an array called "features".
    public Feature[] Features { get; set; } = [];
}

// Represents one earthquake from the "features" array.
public class Feature
{
    // Contains the earthquake information such as magnitude and location.
    public Properties Properties { get; set; } = new();
}

// Contains the earthquake's magnitude and location.
public class Properties
{
    // Earthquake magnitude.
    public double Mag { get; set; }

    // Description of where the earthquake occurred.
    public string Place { get; set; } = "";
}