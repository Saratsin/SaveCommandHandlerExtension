using Microsoft.VisualStudio.Commanding;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Editor.Commanding;
using Microsoft.VisualStudio.Text.Editor.Commanding.Commands;
using Microsoft.VisualStudio.Utilities;
using System;
using System.ComponentModel.Composition;
using System.Diagnostics;

namespace SaveCommandHandlerExtension
{
    [Export(typeof(ICommandHandler))]
    [Name(nameof(SaveCommandHandler))]
    [ContentType(StandardContentTypeNames.Code)]
    [TextViewRole(PredefinedTextViewRoles.PrimaryDocument)]
    public class SaveCommandHandler : ICommandHandler<SaveCommandArgs>
    {
        public string DisplayName => nameof(SaveCommandHandler);

        private readonly IEditorCommandHandlerServiceFactory _editorCommandHandlerServiceFactory;

        [ImportingConstructor]
        public SaveCommandHandler(IEditorCommandHandlerServiceFactory editorCommandHandlerServiceFactory)
        {
            _editorCommandHandlerServiceFactory = editorCommandHandlerServiceFactory;
        }

        public bool ExecuteCommand(SaveCommandArgs args, CommandExecutionContext executionContext)
        {
            try
            {
                var service = _editorCommandHandlerServiceFactory.GetService(args.TextView);
                Debug.WriteLine($"I am executing something on save with {service.GetType()}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return true;
        }

        public CommandState GetCommandState(SaveCommandArgs args)
        {
            return CommandState.Available;
        }

    }
}