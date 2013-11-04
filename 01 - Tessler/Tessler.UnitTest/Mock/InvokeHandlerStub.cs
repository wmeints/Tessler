using Microsoft.Practices.Unity.InterceptionExtension;
using Moq;

namespace InfoSupport.Tessler.UnitTest.Mock
{
    public class InvokeHandlerStub
    {
        public InvokeHandlerStub()
        {
            InvokeHandlerMock = new Mock<IInvokeHandler>();

            GetNextHandlerDelegate = new GetNextHandlerDelegate(() =>
            {
                return new InvokeHandlerDelegate((input, getNext) => InvokeHandlerMock.Object.InvokeHandler(input, getNext));
            });
        }

        public GetNextHandlerDelegate GetNextHandlerDelegate { get; private set; }

        public Mock<IInvokeHandler> InvokeHandlerMock { get; set; }
    }

    public interface IInvokeHandler
    {
        IMethodReturn InvokeHandler(IMethodInvocation input, GetNextHandlerDelegate getNext);
    }
}
