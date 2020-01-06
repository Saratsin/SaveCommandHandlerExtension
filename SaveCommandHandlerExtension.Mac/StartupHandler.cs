using MonoDevelop.Components.Commands;

namespace SaveCommandHandlerExtension.Mac
{
    public class StartupHandler : CommandHandler
    {
        protected override void Run()
        {
            var activeDocumentSavedDispatcher = ActiveDocumentSavedDispatcher.Instance;
            var activeDocumentSavedHandler = new SaveCommandHandler();
            var activeDocumentSavedInterceptor = new ActiveDocumentSavedInterceptor(activeDocumentSavedDispatcher, activeDocumentSavedHandler);

            activeDocumentSavedDispatcher.Initialize();
            activeDocumentSavedInterceptor.StartIntercepting();
        }
    }
}
