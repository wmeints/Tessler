using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfoSupport.Tessler.Core
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class LeavePageDialogAttribute : Attribute
    {
        internal DialogResultAction ResultAction { get; private set; }
        internal string ExpectedMessage { get; set; }

        public LeavePageDialogAttribute(DialogResultAction resultAction = DialogResultAction.DoNothing)
        {
            ResultAction = resultAction;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class AlertDialogAttribute : LeavePageDialogAttribute
    {
        public AlertDialogAttribute(DialogResultAction resultAction = DialogResultAction.DoNothing, string expectedMessage = null)
            : base(resultAction)
        {
            ExpectedMessage = expectedMessage;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ConfirmDialogAttribute : AlertDialogAttribute
    {
        internal bool Confirmation { get; private set; }

        public ConfirmDialogAttribute(DialogResultAction resultAction = DialogResultAction.DoNothing, string expectedMessage = null, bool confirmation = true)
            : base(resultAction, expectedMessage)
        {
            Confirmation = confirmation;
        }
    }

    public enum DialogResultAction
    {
        DoNothing,
        Assert,
        Verify
    }
}
