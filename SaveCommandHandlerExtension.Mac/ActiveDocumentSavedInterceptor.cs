using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Editor.Commanding.Commands;
using MonoDevelop.Ide.Gui;
using System;
using System.Collections.Generic;

namespace SaveCommandHandlerExtension.Mac
{
    public class ActiveDocumentSavedInterceptor : IDisposable
    {
        private readonly ActiveDocumentSavedDispatcher _dispatcher;
        private readonly SaveCommandHandler _handler;

        private readonly List<Document> _savingDocuments;

        private bool _isIntercepting;

        public ActiveDocumentSavedInterceptor(ActiveDocumentSavedDispatcher dispatcher, SaveCommandHandler handler)
        {
            _dispatcher = dispatcher;
            _handler = handler;

            _savingDocuments = new List<Document>();
        }

        public void StartIntercepting()
        {
            if (_isIntercepting)
            {
                return;
            }

            _isIntercepting = true;
            _dispatcher.ActiveDocumentSaved += OnActiveDocumentSaved;
        }

        public void StopIntercepting()
        {
            if (!_isIntercepting)
            {
                return;
            }

            _dispatcher.ActiveDocumentSaved -= OnActiveDocumentSaved;
            _isIntercepting = false;
        }

        private void OnActiveDocumentSaved(object sender, EventArgs e)
        {
            var document = sender as Document;
            if (document is null)
            {
                return;
            }

            if (_savingDocuments.Contains(document))
            {
                _savingDocuments.Remove(document);
                return;
            }
            
            _savingDocuments.Add(document);

            var textView = document.GetContent<ITextView>();
            var commandArgs = new SaveCommandArgs(textView, document.TextBuffer);
            var commandState = _handler.GetCommandState(commandArgs);
            if (commandState.IsAvailable && commandState.IsEnabled)
            {
                _handler.ExecuteCommand(commandArgs, null);
            }

            document.Save();
        }

        public void Dispose()
        {
            StopIntercepting();
        }
    }
}