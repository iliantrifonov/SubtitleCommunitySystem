namespace SubtitleCommunitySystem.Web
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    public class EngineConfig
    {
        public static void RegisterEngines(ViewEngineCollection viewEngineCollection)
        {
            viewEngineCollection.Clear();
            viewEngineCollection.Add(new RazorViewEngine());
        }
    }
}