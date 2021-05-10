using Microsoft.AspNetCore.Routing;
using System;
using System.Text.RegularExpressions;

namespace Useful.Transformers
{
    /// <summary>
    /// Classe para padronizar as rotas dos serviços
    /// </summary>
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value) =>
            value == null ? string.Empty :
            Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2", RegexOptions.CultureInvariant, TimeSpan.FromMilliseconds(100)).ToLowerInvariant();
    }
}
