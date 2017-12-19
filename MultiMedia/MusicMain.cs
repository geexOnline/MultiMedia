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
    [Activity(Label = "MusicMain")]
    public class MusicMain : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MusicMain);
            Songs songs = new Songs();
            var listView = FindViewById<ListView>(Resource.Id.songsList);
            var adapter = new ArrayAdapter(this, Resource.Layout.CustomListView, Android.Resource.Id.Text1, songs.GetSongs());

            listView.Adapter = adapter;

            listView.ItemClick += (sender, e) =>
            {
                var songActivity = new Intent(this, typeof(SongActivity));
                songActivity.PutExtra("songIndex", e.Position);
                StartActivity(songActivity);
            };

            // Create your application here
        }
    }
}