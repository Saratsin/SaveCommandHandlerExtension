using Mono.Addins;
using Mono.Addins.Description;

[assembly: Addin(
    "SaveCommandHandlerExtension",
    Namespace = nameof(SaveCommandHandlerExtension),
    Version = "1.0"
)]

[assembly: AddinName("SaveCommandHandlerExtension")]
[assembly: AddinCategory("IDE extensions")]
[assembly: AddinDescription("SaveCommandHandlerExtension")]
[assembly: AddinAuthor("Taras Shevchuk")]