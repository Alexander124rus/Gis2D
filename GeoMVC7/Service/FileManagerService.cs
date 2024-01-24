using System;

namespace GeoMVC7.Service
{
    public class FileManagerService
    {
        public Object FileManagerGet(HttpRequest request)
        {
            var hostUrl = new UriBuilder(request.Scheme, request.Host.Host, request.Host.Port ?? -1);

            var _ajaxSettings = new
            {
                url = hostUrl + "api/FileManager/FileOperations",
                getImageUrl = hostUrl + "api/FileManager/GetImage",
                uploadUrl = hostUrl + "api/FileManager/Upload",
                downloadUrl = hostUrl + "api/FileManager/Download"
            };
            return _ajaxSettings;
        }
    }
}
