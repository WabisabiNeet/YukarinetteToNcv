using Plugin;
using YukarinetteToNcv;
using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Threading.Tasks;
using System.Runtime.Remoting;

namespace YukarinetteReceiverPlugin
{
    public class YukarinetteReceiverPlugin : IPlugin
    {
        public IPluginHost Host
        {
            get;
            set;
        }

        public bool IsAutoRun => true;

        public string Description => "ゆかりねっとから音声認識結果を受信するプラグイン";

        public string Version => "1.0.0";

        public string Name => "YukarinetteReceiver";

        IpcServerChannel servChannel = null;
        public void AutoRun()
        {
            servChannel = new IpcServerChannel("yukarinettetoncv");
            try
            {
                ChannelServices.RegisterChannel(servChannel, true);
            }
            catch (RemotingException rex)
            {
            }
            catch (Exception ex)
            {
                throw;
            }
            msg = new RecognitionResultMessage();
            msg.OnTrance += new RecognitionResultMessage.CallEventHandler(onTrance);
            RemotingServices.Marshal(msg, "RecognitionResult", typeof(RecognitionResultMessage));
        }

        public void Run()
        {
            //Host.BroadcastConnected += Host_BroadcastConnected;
            //Host.BroadcastDisConnected += Host_BroadcastDisConnected;
        }

        RecognitionResultMessage msg = null;
        private void Host_BroadcastConnected(object sender, EventArgs e)
        {
        }

        private void Host_BroadcastDisConnected(object sender, EventArgs e)
        {
        }

        private void onTrance(RecognitionResultMessage.RecognitionResultMessageEventArg e)
        {
            Task.Delay(4000).Wait();
            if (Host.IsConnected)
            {
                Host.SendOwnerComment(e.Text);
            }
        }
    }
}
