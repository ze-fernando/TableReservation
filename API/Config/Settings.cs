using DotNetEnv;

namespace API.Config;

public static class Settings
{
    public static string SenderMail => Env.GetString("EMAIL");
    public static string SenderPass => Env.GetString("PASS");

    public static string Host => Env.GetString("HOST");
    public static int Port => Env.GetInt("PORT");
}
