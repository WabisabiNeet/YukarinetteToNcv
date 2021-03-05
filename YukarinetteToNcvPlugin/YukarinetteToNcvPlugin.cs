using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using Yukarinette;

namespace YukarinetteToNcv
{
    public class YukarinetteToNcvPlugin : IYukarinetteInterface
    {
        public override string Name => "ゆかりねっとToNCVプラグイン";

        private RecognitionResultMessage msg = null;

        public YukarinetteToNcvPlugin() : base()
        {
        }

        public override void Loaded()
        {
            YukarinetteConsoleMessage.Instance.WriteMessage("[YukarinetteToNcvPlugin] SpeechRecognitionStart.");

            // IPC Channel を作成
            IpcClientChannel clientChannel = new IpcClientChannel();
            // リモートオブジェクトを登録
            ChannelServices.RegisterChannel(clientChannel, true);
            // オブジェクトを作成
            msg = (RecognitionResultMessage)Activator.GetObject(typeof(RecognitionResultMessage), "ipc://yukarinettetoncv/RecognitionResult");

            base.SpeechRecognitionStart();
        }

        public override void Speech(string text)
        {
            try
            {
                msg?.DataTrance(text);
            }
            catch (Exception ex)
            {
                YukarinetteConsoleMessage.Instance.WriteMessage($"[YukarinetteToNcvPlugin] AfterSpeech exception occurred. [{ex}]");
            }
        }
    }
}
