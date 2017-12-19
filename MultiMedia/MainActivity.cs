using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace MultiMedia
{
    [Activity(Label = "MultiMedia", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button musicPlayer = (Button)FindViewById<Button>(Resource.Id.btnMusicPlayer);
            Button videoPlayer = (Button)FindViewById<Button>(Resource.Id.btnVideoPlayer);
            Button audioRecorder = (Button)FindViewById<Button>(Resource.Id.btnSoundRecorder);
            //Button videoRecorder = (Button)FindViewById<Button>(Resource.Id.btnVideoRecorder);

            musicPlayer.Click += delegate
            {
                var goMusicPlayer = new Intent(this, typeof(MusicMain));
                StartActivity(goMusicPlayer);
            };
            videoPlayer.Click += delegate
            {
                var goVideoPlayer = new Intent(this, typeof(VideoMainActivity));
                StartActivity(goVideoPlayer);
            };

            audioRecorder.Click += delegate
            {
                var goAudioRecorder = new Intent(this, typeof(AudioRecorder));
                StartActivity(goAudioRecorder);
            };





        }
    }
}

