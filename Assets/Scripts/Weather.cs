using System;

[Flags]
public enum Weather
{
    Cloudy = 0,
    WindE  = 1,
    WindW  = 1 << 1,
    WindS  = 1 << 2,
    WindN  = 1 << 3,
    Sunny  = 1 << 4,
    Rain   = 1 << 5,
    Windy = WindE | WindW | WindS | WindN
}