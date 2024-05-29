using System;

namespace PupsCore.Statics;

public class PupsCoreStatic
{
  private readonly static Random random = new();
  public static int RandomInt(int min, int max) => random.Next(min, max + 1);
}