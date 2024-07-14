﻿namespace TinyUrl.UrlShortBL
{
    public class ShortUrlCheckSum : IShortUrl
    {
        private readonly IChecksum _checksum;
        public ShortUrlCheckSum(IChecksum checksum)
        {
            _checksum = checksum;
        }

        public string CreateShortUrl(string originalUrl)
        {
            return _checksum.Run(originalUrl);
        }
    }
}
