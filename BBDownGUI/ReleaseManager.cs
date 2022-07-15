using RestSharp;
using System.Runtime.InteropServices;

namespace BBDownGUI
{
    abstract class ReleaseManager
    {
        private static Release _release = null;
        public Release Release
        {
            get
            {
                if (_release == null)
                {
                    var client = new RestClient("https://api.github.com")
                        .AddDefaultHeader(KnownHeaders.Accept, "application/vnd.github.v3+json");
                    var request = new RestRequest(API_URL);
                    _release = client.GetAsync<Release>(request).GetAwaiter().GetResult();
                };
                _release.assets = _release.assets.FindAll((x) =>
                {
                    return x.name.Contains(NameOf(RuntimeInformation.Platform)) &&
                           x.name.Contains(NameOf(RuntimeInformation.Architecture));
                });
                return _release;
            }
        }
        protected abstract string Developer { get; }
        protected abstract string ProjectName { get; }
        protected abstract string NameRegex { get; }
        protected string API_URL => $"repos/{Developer}/{ProjectName}/releases/latest";
        public List<AssetsItem> AssetsItems { get => Release.assets; }
        protected abstract Action<string, PlatformID> ActionAfterDownload { get; }
        protected abstract string NameOf(Architecture architecture);
        protected abstract string NameOf(PlatformID platformID);
    }
    class BBDownReleaseManager : ReleaseManager
    {
        protected override string Developer => "nilaoda";
        protected override string ProjectName => "BBDown";
        protected override string NameRegex => "BBDown_{version}_{date}_{os}-{architecture}.[zip]";
        protected override Action<string, PlatformID> ActionAfterDownload => throw new NotImplementedException();

        protected override string NameOf(Architecture architecture)
        {
            return architecture switch
            {
                Architecture.Arm64 => "arm64",
                Architecture.X64 => "x64",
                _ => throw new UnsupportedArchitectureException()
            };
        }

        protected override string NameOf(PlatformID platformID)
        {
            return platformID switch
            {
                PlatformID.Win32NT => "win",
                PlatformID.Unix => "linux",
                PlatformID.MacOSX => "osx",
                _ => throw new UnsupportedOSException()
            };
        }
    }

    class FFmpegReleaseManager : ReleaseManager
    {
        protected override string Developer => "BtbN";
        protected override string ProjectName => "FFmpeg-Builds";
        protected override string NameRegex => "ffmpeg-{version}-latest-{os}-{licence}.[zip|tar.xz]";
        protected override Action<string, PlatformID> ActionAfterDownload => throw new NotImplementedException();

        protected override string NameOf(Architecture architecture)
        {
            return architecture switch
            {
                Architecture.Arm64 => "arm64",
                Architecture.X64 => "64",
                _ => throw new UnsupportedArchitectureException()
            };
        }

        protected override string NameOf(PlatformID platformID)
        {
            return platformID switch
            {
                PlatformID.Win32NT => "win",
                PlatformID.Unix => "linux",
                _ => throw new UnsupportedOSException()
            };
        }
    }
}
