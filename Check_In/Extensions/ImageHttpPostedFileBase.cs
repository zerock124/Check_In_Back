using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Drawing;

namespace Check_In.Extensions
{
    /// <summary>
    /// 繼承 HttpPostedFileBase
    /// </summary>
    public class ImageHttpPostedFileBase : HttpPostedFileBase
    {
        Stream stream;
        string contentType;
        string fileName;
        public int ImageQuality;
        public Size ImageThumbnailSize;
        public ImageResizeMode ImageResizeMode;

        public ImageHttpPostedFileBase(HttpPostedFileBase file, Size imageThumbnailSize, int ImageQuality, ImageResizeMode imageResizeMode)
            : this(file.InputStream, file.ContentType, file.FileName, imageThumbnailSize, ImageQuality, imageResizeMode)
        {}


        public ImageHttpPostedFileBase(Stream stream, string contentType, string fileName, Size imageThumbnailSize, int ImageQuality, ImageResizeMode imageResizeMode)
        {
            this.stream = stream;
            this.contentType = contentType;
            this.fileName = fileName;
            this.ImageQuality = ImageQuality;
            this.ImageThumbnailSize = imageThumbnailSize;
            this.ImageResizeMode = imageResizeMode;
        }

        public override void SaveAs(string filename)
        {
            var image = Image.FromStream(stream);
            using (var thumbnailImage = image.Resize(ImageThumbnailSize.Width, ImageThumbnailSize.Height, ImageResizeMode, Color.White))
                thumbnailImage.SaveAuto(filename, ImageQuality);
        }
    }
}