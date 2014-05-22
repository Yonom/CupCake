using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CupCake.Core.Services;
using Nito;

namespace CupCake.Upload.Services
{
    public class UploadService : CupCakeService
    {
        private Thread _uploadthread;
        private Deque<UploadRequest> _requests = new Deque<UploadRequest>();
        private Queue<UploadRequest> _checkQueue = new Queue<UploadRequest>();
        private bool[,,] _uploaded;
        private int _version;
        private int _liveVersion;

        protected override void Enable()
        {
            this._uploadthread = new Thread(this.RunUploadThread);
        }

        private void RunUploadThread()
        {
            while (true)
            {
                this.SendNext();
                Thread.Sleep(6);
            }
        }

        private void SendNext()
        {
            while (true)
            {
                if (_requests.Count > 0)
                {
                    UploadRequest block = _requests.RemoveFromFront();

                    if (block.SendMessage(myClient))
                    {
                        if (!myUploadedArray(block.Layer, block.X, block.Y))
                        {
                            myUploadedArray(block.Layer, block.X, block.Y) = true;

                            lock (myLagCheckQueue)
                            {
                                myLagCheckQueue.Enqueue(block);
                            }
                        }
                    }
                    else
                    {
                        goto retry;
                    }
                }
                else
                {
                    if (myLagCheckQueue.Count > 0)
                    {
                        LastLagCheck();
                    }
                    if (!(myVersion == myFinishedUploadVersion))
                    {
                        myFinishedUploadVersion = myVersion;
                        if (FinishedUpload != null)
                        {
                            FinishedUpload(this, EventArgs.Empty);
                        }
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _uploadthread.Abort();
            }

            base.Dispose(disposing);
        }
    }
}
