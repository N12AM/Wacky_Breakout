using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// A container for the configuration data
public class ConfigurationData
{

    const string ConfigurationDataFileName = "ConfigurationData.csv";

    // configuration data
    private static float _paddleMoveUnitsPerSecond = 15;
    private static float _ballImpulseForce = 2f;
    private static float _ballLifetimeSeconds = 45f;
    private static int _ballSpawnMinDelay = 20;
    private static int _ballSpawnMaxDelay = 25;
    private static int _totalNumberOfBalls = 5;
    private static int _pointsPerStandardBlock = 1;
    private static int _pointsPerBonusBlock = 2;
    private static int _pointsPerFreezerBlock = 5;
    private static int _pointsPerSpeederBlock = 5;



    /// Gets the paddle move units per second
    public float PaddleMoveUnitsPerSecond
    {
        get { return _paddleMoveUnitsPerSecond; }
    }

    /// Gets the impulse force to apply to move the ball
    public float BallImpulseForce
    {
        get { return _ballImpulseForce; }
    }
    
    //gets the ball lifetime in seconds, after the time expires the ball will be destroyed
    public float BallLifetimeSeconds => _ballLifetimeSeconds;

    //minimum delay for a new ball to spawn into the game Screen
    public int BallSpawnMinDelay => _ballSpawnMinDelay;

    //maximum delay for a new ball to spawn into the game Screen
    public int BallSpawnMaxDelay => _ballSpawnMaxDelay;
    
    //returns the total number of balls in the current game scene
    public int TotalNumberOfBalls => _totalNumberOfBalls;
    
    //gets the appropriate points for each types of blocks
    public int PointsPerStandardBlock => _pointsPerStandardBlock;
    public int PointsPerBonusBlock => _pointsPerBonusBlock;
    public int PointsPerFreezerBlock => _pointsPerFreezerBlock;
    public int PointsPerSpeederBlock => _pointsPerSpeederBlock;
 

    /// Constructor
    /// Reads configuration data from a file. If the file
    /// read fails, the object contains default values for
    /// the configuration data
    public ConfigurationData()
    {

        StreamReader input = null;

        try
        {
            input = File.OpenText(Path.Combine(Application.streamingAssetsPath, ConfigurationDataFileName));
            var names = input.ReadLine();
            var values = input.ReadLine();
            SetValuesTo(values);
        }
        catch (Exception e)
        {
            // ignored
            Debug.Log("FAILED"+e);
        }
        finally
        {
            input?.Close();
        }
    }

    void SetValuesTo(string inputValues)
    {
        string[] value = inputValues.Split(',');

        _ballImpulseForce = float.Parse(value[0]);
        _paddleMoveUnitsPerSecond = float.Parse(value[1]);
        _ballLifetimeSeconds = float.Parse(value[2]);
        _ballSpawnMinDelay = int.Parse(value[3]);
        
        //adding  1 to _ballSpawnMaxDelay because we will be using the Random class
        //and Random. Range excludes the max value
        _ballSpawnMaxDelay = int.Parse(value[4]) + 1;

        _totalNumberOfBalls = int.Parse(value[5]);
        
        //points per types of blocks
        _pointsPerStandardBlock = int.Parse(value[6]);
        _pointsPerBonusBlock = int.Parse(value[7]);
        _pointsPerFreezerBlock = int.Parse((value[8]));
        _pointsPerSpeederBlock = int.Parse(value[9]);




    }
    

}
