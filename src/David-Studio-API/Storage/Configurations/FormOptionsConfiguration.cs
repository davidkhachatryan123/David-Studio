using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;

namespace Storage.Configurations
{
    public class FormOptionsConfiguration : IConfigureNamedOptions<FormOptions>
    {
        public void Configure(string? name, FormOptions options)
        {
            Configure(options);
        }

        public void Configure(FormOptions options)
        {
            options.ValueLengthLimit = int.MaxValue;
            options.MultipartBodyLengthLimit = int.MaxValue;
            options.MemoryBufferThreshold = int.MaxValue;
        }
    }
}