using System;

namespace YukarinetteToNcv
{
    public class RecognitionResultMessage : MarshalByRefObject
    {
        public class RecognitionResultMessageEventArg : EventArgs
        {
            public string Text { get; set; }

            public RecognitionResultMessageEventArg(string text)
            {
                Text = text;
            }
        }
 
        public delegate void CallEventHandler(RecognitionResultMessageEventArg e);
        public event CallEventHandler OnTrance;
        public void DataTrance(string text)
        {
            if (OnTrance != null)
            {
                OnTrance(new RecognitionResultMessageEventArg(text));
            }
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}
