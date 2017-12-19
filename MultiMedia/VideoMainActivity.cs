using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;

namespace MultiMedia
{
    [Activity(Label = "VideoMainActivity")]
    public class VideoMainActivity : Activity
    {
        private VideoView videoVw;
        private MediaRecorder recorder;
        private Button playBtn;
        private Button stopBtn;
        private Button recordBtn;
        private Button fileBtn;
        private TextView infoTvw;
        private Android.Net.Uri videoPath;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.VideoMain);

            // get video player reference
            this.videoVw = this.videoVw = this.FindViewById<VideoView>(Resource.Id.playerVw);

            // get reference to the info textview
            this.infoTvw = this.FindViewById<TextView>(Resource.Id.video_infoTvw);

            // get file btn reference
            this.fileBtn = this.FindViewById<Button>(Resource.Id.fileBtn);
            this.fileBtn.Click += (sender, args) =>
            {

                var intent = new Intent(); // create new intent
                intent.SetType("video/*"); // set intent type
                intent.SetAction(Intent.ActionGetContent); // set intent action
                this.StartActivityForResult(Intent.CreateChooser(intent, "select a file to play"), 0); // emit event when file is selected

            };

            // enable stop button and handle click event
            this.stopBtn = this.FindViewById<Button>(Resource.Id.stopBtn);
            this.stopBtn.Enabled = false;
            this.stopBtn.Click += (obj, ars) =>
            {

                // check if the video is playing and pause
                if (this.videoVw != null && this.videoVw.IsPlaying)
                {
                    this.videoVw.Pause();
                }

                // check if the recorder is recording and stop
                if (this.recorder != null)
                {
                    this.recorder.Stop();
                    this.clearRecorder();
                    this.videoVw.SetVideoURI(this.videoPath);
                }

                this.stopBtn.Enabled = false;
                this.playBtn.Enabled = true;
                this.recordBtn.Enabled = true;
                this.fileBtn.Enabled = true;
            };

            // get record btn reference and handle click event
            this.recordBtn = this.FindViewById<Button>(Resource.Id.recordBtn);
            recordBtn.Click += (sender, args) =>
            {
                // enable stop btn
                this.stopBtn.Enabled = true;
                this.playBtn.Enabled = false;
                this.fileBtn.Enabled = false;
                this.recordBtn.Enabled = false;

                // set video path
                string path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/Movies/mov_" +
                                DateTime.Now.ToString().Replace('/', '_').Replace(':', '_').Replace(' ', '_');
                this.videoVw.SetVideoURI(this.videoPath);

                // find info text view
                this.infoTvw.Text = path;

                // prepare recorder and record
                this.recorder = new MediaRecorder();
                this.recorder.SetVideoSource(VideoSource.Camera);
                this.recorder.SetAudioSource(AudioSource.Mic);
                this.recorder.SetOutputFormat(OutputFormat.Default);
                this.recorder.SetVideoEncoder(VideoEncoder.Default);
                this.recorder.SetAudioEncoder(AudioEncoder.Default);
                this.recorder.SetOutputFile(path);
                this.recorder.SetPreviewDisplay(videoVw.Holder.Surface);
                this.recorder.SetOrientationHint(90);
                this.recorder.Prepare();
                this.recorder.Start();
            };

            // find play btn and handle click event
            this.playBtn = this.FindViewById<Button>(Resource.Id.playBtn);
            this.playBtn.Enabled = false;
            playBtn.Click += (sender, args) =>
            {
                // check if video isn't playing and start it
                if (!videoVw.IsPlaying)
                {
                    videoVw.Start();
                    this.stopBtn.Enabled = true;
                    this.playBtn.Enabled = false;
                    this.recordBtn.Enabled = false;
                    this.fileBtn.Enabled = false;
                }
            };
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {
                // enable play button
                this.playBtn.Enabled = true;
                this.stopBtn.Enabled = false;


                // find info text view
                this.infoTvw.Text = data.DataString;

                // set path of the video
                this.videoPath = data.Data;
                this.videoVw.SetVideoURI(this.videoPath);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            this.clearRecorder();

            if (this.videoVw != null)
            {
                this.videoVw.Dispose();
                this.videoVw = null;
            }
        }

        private void clearRecorder()
        {
            if (this.recorder != null)
            {
                this.recorder.Release();
                this.recorder.Dispose();
                this.recorder = null;
            }
        }
    }
}

