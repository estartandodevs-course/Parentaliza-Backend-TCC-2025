using System.Diagnostics;

namespace Orders.Api;

public static class DiagnosticsConfig
{
    public static readonly ActivitySource ActivitySource = new("Orders.Api");
}
