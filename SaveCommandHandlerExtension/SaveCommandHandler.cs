using Microsoft.VisualStudio.Commanding;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
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

        [ImportingConstructor]
        public SaveCommandHandler()
        {
        }

        public bool ExecuteCommand(SaveCommandArgs args, CommandExecutionContext executionContext)
        {
            try
            {
                var textBuffer = args.SubjectBuffer;
                var currentSnapshot = textBuffer.CurrentSnapshot;
                var rawText = currentSnapshot.GetText();
                var newText = rawText + "\n// I am executing something on save";
                var replaceSpan = new Span(0, rawText.Length);
                textBuffer.Replace(replaceSpan, newText);
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