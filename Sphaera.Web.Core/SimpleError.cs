namespace Sphaera.Web.Core
{
    public struct SimpleError
    {
        public int StatusCode;
        public string RequestUrl;
        public string ControllerName;
        public string ActionName;
        public string Message;
        public string Exception;
        public string Trace;
    }
}