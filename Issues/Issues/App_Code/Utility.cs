namespace Issues
{
  public static class Utility
  {
    /// <summary>
    /// Force the browser to remove the cash file after update by generate
    /// new url end with build number
    /// </summary>
    /// <param name="url">Url Address</param>
    /// <returns>Url with build number</returns>
    public static string GetBuildUrl(string url) => $"{url}?v={AppSetting.Build}";
  }
}