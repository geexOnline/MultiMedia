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
    [Activity(Label = "AudioRecorder")]
    public class AudioRecorder : Activity
    {
        MediaRecorder recorder;
        MediaPlayer player;
        Button start;
        Button stop;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AudioRecorder);
            start = FindViewById<Button>(Resource.Id.start);
            stop = FindViewById<Button>(Resource.Id.stop);

            string path = "/sdcard/test.3gpp";

            start.Click += delegate {
                stop.Enabled = !stop.Enabled;
                start.Enabled = !start.Enabled;

                recorder.SetAudioSource(AudioSource.Mic);
                recorder.SetOutputFormat(OutputFormat.ThreeGpp);
                recorder.SetAudioEncoder(AudioEncoder.AmrNb);
                recorder.SetOutputFile(path);
                recorder.Prepare();
                recorder.Start();
            };

            stop.Click += delegate {
                stop.Enabled = !stop.Enabled;

                recorder.Stop();
                recorder.Reset();

                player.SetDataSource(path);
                player.Prepare();
                player.Start();
            };



        // Create your application here
    }
        protected override void OnResume()
        {
            base.OnResume();

            recorder = new MediaRecorder();
            player = new MediaPlayer();

            player.Completion += (sender, e) => {
                player.Reset();
                start.Enabled = !start.Enabled;
            };

        }
        protected override void OnPause()
        {
            base.OnPause();

            player.Release();
            recorder.Release();
            player.Dispose();
            recorder.Dispose();
            player = null;
            recorder = null;
        }
    }
}