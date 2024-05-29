namespace PupsCore.Components;

public interface IPupsTryResult
{
  protected bool Success { get; set; }
  protected PupsException Exception { get; set; }
}
