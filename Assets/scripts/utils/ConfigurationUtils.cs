using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Provides access to configuration data
public static class ConfigurationUtils
{
    private static ConfigurationData _configurationData;
    
    /// Gets the paddle move units per second
    /// <value>paddle move units per second</value>
    public static float PaddleMoveUnitsPerSecond => _configurationData.PaddleMoveUnitsPerSecond;
    public static float BallImpulseForce => _configurationData.BallImpulseForce;
    
    //ball lifetime duration
    public static float BallLifetimeSeconds => _configurationData.BallLifetimeSeconds;

    //the min delay for a new ball to be spawned into the Game Screen
    public static int BallSpawnMinDelay => _configurationData.BallSpawnMinDelay;
    
    //the MAX delay for a new ball to be spawned into the Game Screen;
    public static int BallSpawnMaxDelay => _configurationData.BallSpawnMaxDelay;
    
    //returns the total number of the balls for the current game scene
    public static int TotalNumberOfBalls => _configurationData.TotalNumberOfBalls;
    
    //gets the points for each types of blocks
    public static int PointsPerStandardBlock => _configurationData.PointsPerStandardBlock;
    public static int PointsPerBonusBlock => _configurationData.PointsPerBonusBlock;
    public static int PointsPerFreezerBlock => _configurationData.PointsPerFreezerBlock;
    public static int PointsPerSpeederBlock => _configurationData.PointsPerSpeederBlock;

    
    
    /// Initializes the configuration utils
    public static void Initialize()
    {
        _configurationData = new ConfigurationData();
    }
}
