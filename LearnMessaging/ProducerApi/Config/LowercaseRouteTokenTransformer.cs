namespace ProducerApi.Config
{
    public class LowercaseRouteTokenTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            var strValue = value as string;
            return strValue?.ToLowerInvariant();
        }
    }
}
