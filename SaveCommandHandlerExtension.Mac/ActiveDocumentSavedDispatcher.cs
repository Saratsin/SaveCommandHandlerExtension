using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;
using System;

namespace SaveCommandHandlerExtension.Mac
{
    public class ActiveDocumentSavedDispatcher : IDisposable
    {
        private bool _isInitialized;
        private Document _currentActiveDocument;

        private ActiveDocumentSavedDispatcher()
        {
        }

        public static ActiveDocumentSavedDispatcher Instance { get; } = new ActiveDocumentSavedDispatcher();

        public void Initialize()
        {
            if (_isInitialized)
            {
                throw new InvalidOperationException("Dispatcher is already initialized");
            }

            _isInitialized = true;
            IdeApp.Workbench.ActiveDocumentChanged += OnActiveDocumentChanged;
            _currentActiveDocument = IdeApp.Workbench.ActiveDocument;
            SubscribeDocumentSaved();
        }

        public event EventHandler ActiveDocumentSaved;

        private void OnActiveDocumentChanged(object sender, DocumentEventArgs e)
        {
            UnsubscribeDocumentSaved();

            _currentActiveDocument = e.Document;

            SubscribeDocumentSaved();
        }

        private void SubscribeDocumentSaved()
        {
            var currentActiveDocument = _currentActiveDocument;
            if (currentActiveDocument is null)
            {
                return;
            }

            currentActiveDocument.Saved += OnCurrentActiveDocumentSaved;
        }

        private void UnsubscribeDocumentSaved()
        {
            var currentActiveDocument = _currentActiveDocument;
            if (currentActiveDocument is null)
            {
                return;
            }

            currentActiveDocument.Saved -= OnCurrentActiveDocumentSaved;
        }

        private void OnCurrentActiveDocumentSaved(object sender, EventArgs e)
        {
            ActiveDocumentSaved?.Invoke(sender, e);
        }

        public void Dispose()
        {
            IdeApp.Workbench.ActiveDocumentChanged -= OnActiveDocumentChanged;
            UnsubscribeDocumentSaved();
            _currentActiveDocument = null;
        }
    }
}
