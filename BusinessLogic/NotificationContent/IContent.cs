using ModelLibrary;
using System.Collections.Generic;

namespace BusinessLogic.NotificationContent
{
    public interface IContent
    {
        List<PushContent> Execute();
    }
}