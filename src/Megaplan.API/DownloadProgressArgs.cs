namespace Megaplan.API
{
    public class DownloadProgressArgs
    {
        public DownloadProgressArgs(long position, long totalSize)
        {
            Position = position;
            TotalSize = totalSize;
        }

        public long Position { get; set; }

        public long TotalSize { get; set; }
    }
}